using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueModels;
using MultiQueueTesting;
using System.Windows.Forms.DataVisualization.Charting;

namespace MultiQueueSimulation
{

    public partial class Form1 : Form
    {
        // Global variables
        private int numberOfCustomers = 100;

        // Create an instance of SimulationSystem
        SimulationSystem simulationSystem;
        public Form1()
        {
            InitializeComponent();
            dgvInterarrival.Hide();
            serverBox.Hide();
            runButton.Hide();
            outputGrid.Hide(); 
            graph.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // openFileDialog to browse for the input file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Select an Input File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // define new empty object
                simulationSystem = new SimulationSystem();
                // selected file path
                string filePath = openFileDialog.FileName;

                // read the file
                string[] lines = System.IO.File.ReadAllLines(filePath);

                // loop through the file content
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    ParseInputLine(lines, ref i, line);
                }
                // Display data in the grid view
                viewInput();
            }
        }

        private void ParseInputLine(string[] lines, ref int index, string line)
        {
            switch (line)
            {
                case "NumberOfServers":
                    simulationSystem.NumberOfServers = int.Parse(lines[++index].Trim());
                    break;

                case "StoppingNumber":
                    simulationSystem.StoppingNumber = int.Parse(lines[++index].Trim());
                    break;

                case "StoppingCriteria":
                    simulationSystem.StoppingCriteria = (Enums.StoppingCriteria)(int.Parse(lines[++index].Trim()));
                    break;

                case "SelectionMethod":
                    simulationSystem.SelectionMethod = (Enums.SelectionMethod)(int.Parse(lines[++index].Trim()));
                    break;
                case "InterarrivalDistribution":
                    List<TimeDistribution> L = simulationSystem.InterarrivalDistribution;
                    ParseDistribution(lines, ref index, ref L );
                    break;

                default:
                    if (line.StartsWith("ServiceDistribution_Server"))
                    {
                        int id = int.Parse(line.Substring(26));
                        Server newServer = new Server(id);
                        simulationSystem.Servers.Add(newServer);
                        ParseDistribution(lines, ref index, ref newServer.TimeDistribution);
                    }
                    break;
            }
        }
        private void ParseDistribution(string[] lines, ref int index, ref List<TimeDistribution> table)
        {

            // Read lines for the distribution until a new section
            decimal cumProb = 0;
            int startRange = 1, endRange;
            while (++index < lines.Length && lines[index].Contains(","))
            {
                string[] parts = lines[index].Split(',');
                int value = int.Parse(parts[0].Trim());
                decimal probability = decimal.Parse(parts[1].Trim());
                cumProb += probability;
                endRange = (int)(cumProb * numberOfCustomers);
                table.Add(new TimeDistribution(value, probability, cumProb, startRange, endRange));
                startRange = endRange + 1;
            }

            index--; // Step back to allow the main loop to process correctly
        }
        
        private void initializeGridView(ref DataGridView dgview, ref List<String> cols)
        {
            // Set header appearance (background color, font, etc.)
            dgview.EnableHeadersVisualStyles = false;
            dgview.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgview.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgview.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgview.ColumnHeadersHeight = 60; // Increase the height of the header

            // Optional: Set the row style to alternate colors
            dgview.RowsDefaultCellStyle.BackColor = Color.White;
            dgview.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Make the DataGridView read-only to prevent user input
            dgview.ReadOnly = true;
            // Configure DataGridView columns
            dgview.Columns.Clear();
            dgview.Rows.Clear();
            foreach(String col in cols)
            {
                dgview.Columns.Add(col, col);
            }
            dgview.DefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Regular);
            dgview.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
        }
        private void showInputGrid(int idx)
        {
            // Clear existing rows
            dgvInterarrival.Rows.Clear();
            List<TimeDistribution> lst;
            if (idx == 0)
            {
                dgvInterarrival.Columns[0].HeaderText = "Interarrival Time";
                lst = simulationSystem.InterarrivalDistribution;
            }
            else
            {
                dgvInterarrival.Columns[0].HeaderText = "Service Time";
                lst = simulationSystem.Servers[idx - 1].TimeDistribution;
            }
            // Load data into DataGridView from InterarrivalDistribution
            foreach (var dist in lst)
            {
                // Add a new row for each TimeDistribution object
                dgvInterarrival.Rows.Add(
                    dist.Time,
                    dist.Probability,
                    dist.CummProbability,
                    $"{dist.MinRange:D2} - {dist.MaxRange:D2}");
            }
            // Optionally set properties of the DataGridView (e.g., column autosizing)
            dgvInterarrival.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void viewInput()
        {
            dgvInterarrival.Show();
            runButton.Show();
            List<String> inputCols = new List<String> { "Time" , "Probability", "CumulativeProbability", "Range" };
            initializeGridView(ref dgvInterarrival, ref inputCols);
            // Fill comboBox
            serverBox.Items.Clear();
            serverBox.Show();
            serverBox.DropDownStyle = ComboBoxStyle.DropDownList;
            serverBox.Font = new Font("Arial", 10, FontStyle.Regular);
            serverBox.Items.Add("Inter arrival Distribution");
            serverBox.SelectedIndex = 0;
            for (int i = 0; i < simulationSystem.Servers.Count; i++)
            {
                serverBox.Items.Add("Server: " + simulationSystem.Servers[i].ID.ToString());
            }
            showInputGrid(0);
        }

        private void serverBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            showInputGrid(serverBox.SelectedIndex);
        }
        private void viewOutput()
        {
            outputGrid.Columns.Clear();
            outputGrid.Rows.Clear();
            outputGrid.Show();
            graph.Show();
            // view output table
            List<String> outputCols = new List<String> { "Customer Number", "Random Inter Arrival", "Inter Arrival Time",
                "Arrival Time", "Random Service", "Assigned Server ID", "Service Start", "Service Time", "Service End",
                "Time in Queue"};
            initializeGridView(ref outputGrid, ref outputCols);
            foreach (SimulationCase row in simulationSystem.SimulationTable)
            {
                outputGrid.Rows.Add(row.CustomerNumber, row.RandomInterArrival, row.InterArrival, row.ArrivalTime,
                    row.RandomService, row.AssignedServer.ID, row.StartTime, row.ServiceTime, row.EndTime, row.TimeInQueue);
            }
            dgvInterarrival.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//            drawGraph();
        }
        private void drawGraph(int idx = 0)
        {
            // not finished
            graph.Legends.Clear();

            Series series = new Series();
            series.ChartType = SeriesChartType.RangeColumn;
            series.Color = System.Drawing.Color.Blue;

            DataPoint rangePoint1 = new DataPoint();
            rangePoint1.SetValueXY(3, 5);  // X range [3, 5]
            rangePoint1.YValues = new double[] { 1 };  // Y value (height of the bar)
            series.Points.Add(rangePoint1);

            DataPoint rangePoint2 = new DataPoint();
            rangePoint2.SetValueXY(8, 10);  // X range [8, 10]
            rangePoint2.YValues = new double[] { 1 };  // Y value (height of the bar)
            series.Points.Add(rangePoint2);

            graph.ChartAreas[0].AxisX.Minimum = 0;
            graph.ChartAreas[0].AxisY.Maximum = 1;
            graph.ChartAreas[0].AxisY.Interval = 1;
            graph.ChartAreas[0].AxisX.Title = "Time";
            graph.ChartAreas[0].AxisY.Title = "B(t)";
            graph.Series.Add(series);

        }
        private void runButton_Click(object sender, EventArgs e)
        {
            viewOutput();            
        }

    }
}
