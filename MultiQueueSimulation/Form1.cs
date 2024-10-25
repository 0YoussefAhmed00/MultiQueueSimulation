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
        // Create an instance of SimulationSystem
        SimulationSystem simulationSystem;
        // Size bigWindow = new Size(1474, 864);
        public Form1()
        {
            InitializeComponent();
            dgvInterarrival.Hide();
            serverBox.Hide();
            runButton.Hide();
            outputGrid.Hide(); 
            graph.Hide();
            graphLabel.Hide();
            graphBox.Hide();
        }

        private void browseButton_Click(object sender, EventArgs e)
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
                endRange = (int)(cumProb * simulationSystem.NumberOfCustomers);
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
            List<String> inputCols = new List<String> { "Time" , "Probability", "Cumulative Probability", "Range" };
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
            // output table
            outputGrid.Columns.Clear();
            outputGrid.Rows.Clear();
            outputGrid.Show();
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

            // output graph
            graph.Show();
            graphBox.Show();
            graphLabel.Show();
            graphBox.Items.Clear();
            graphBox.DropDownStyle = ComboBoxStyle.DropDownList;
            graphBox.Font = new Font("Arial", 10, FontStyle.Regular);
            foreach (Server s in simulationSystem.Servers)
            {
                graphBox.Items.Add(s.ID);
            }
            if (simulationSystem.Servers.Count > 0)
            {
                graphBox.SelectedIndex = 0;
                drawServerGraph(simulationSystem.Servers[0]);
            }
        }

        private void drawGraph(List<double> l, List<double> r)
        {

            graph.Legends.Clear();
            graph.Series.Clear();
            Series series = new Series
            {
                ChartType = SeriesChartType.Area, Color = Color.Blue, BorderWidth = 2
            };
            // Add points to form a rectangle
            for (int i = 0; i < l.Count; i++)
            {
                series.Points.AddXY(l[i], 0); // Top-left
                series.Points.AddXY(r[i], 0); // Top-right
                series.Points.AddXY(r[i], 1); // Bottom-right
                series.Points.AddXY(l[i], 1); // Bottom-left
                series.Points.AddXY(l[i], 0); // Close the rectangle
            }
            graph.ChartAreas[0].AxisY.Maximum = 1;
            graph.ChartAreas[0].AxisX.Title = "Time";
            graph.ChartAreas[0].AxisY.Title = "B(t)";
            graph.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            graph.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            graph.Series.Add(series);
        }
        private void drawServerGraph(Server s)
        {
            List<double> l = new List<double>();
            List<double> r = new List<double>();
            foreach (SimulationCase row in simulationSystem.SimulationTable)
            {
                if (row.AssignedServer == s)
                {
                    l.Add(row.StartTime);
                    r.Add(row.EndTime);
                }
            }
            drawGraph(l, r);
        }
        private void runButton_Click(object sender, EventArgs e)
        {
            simulationSystem.Simulate();
            try
            {
                viewOutput();
            }
            catch
            {
                Console.WriteLine("Can't simulate the system.");
            }
        }
        private void graphBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = graphBox.SelectedIndex;
            if (idx == -1) return;
            drawServerGraph(simulationSystem.Servers[idx]);
        }
    }
}
