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
using System.Data.Common;

namespace MultiQueueSimulation
{

    public partial class Form1 : Form
    {
        // Create an instance of SimulationSystem
        SimulationSystem simulationSystem;
        String inputFileName;
        private void PreprocessingVisualization()
        {
            // Graph
            graph.ChartAreas[0].AxisX.Minimum = 0;
            graph.ChartAreas[0].AxisY.Maximum = 1;
            graph.ChartAreas[0].AxisY.Interval = 1;
            graph.ChartAreas[0].AxisX.Title = "Time";
            graph.ChartAreas[0].AxisY.Title = "B(t)";
            graph.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10);
            graph.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12);
            graph.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12);
            graph.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            graph.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            graph.ChartAreas[0].AxisY.MajorGrid.Enabled = false;



        }
        public Form1()
        {
            InitializeComponent();
            PreprocessingVisualization();
            dgvInterarrival.Hide();
            serverBox.Hide();
            runButton.Hide();
            outputGrid.Hide(); 
            graph.Hide();
            graphLabel.Hide();
            graphBox.Hide();
            serverPerformance.Hide();
            simulationPerformance.Hide();
            panel1.AutoScroll = true;
            panel1.Hide();
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

                inputFileName = System.IO.Path.GetFileName(filePath);
                // read the file
                string[] lines = System.IO.File.ReadAllLines(filePath);
                // loop through the file content
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    ParseInputLine(lines, ref i, line);
                }
                // Set radio buttons based on the StoppingCriteria value
                if (simulationSystem.StoppingCriteria == Enums.StoppingCriteria.NumberOfCustomers)
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
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
                if(startRange<= endRange)
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
            dgview.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgview.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgview.RowHeadersVisible = false;
            foreach (DataGridViewColumn column in dgview.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
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
            panel1.Show();
            dgvInterarrival.Show();
            runButton.Show();
            List<String> inputCols = new List<String> { "Time" , "Probability", "Cumulative Probability", "Range" };
            initializeGridView(ref dgvInterarrival, ref inputCols);
            // Display the number of servers in textBox1 and make it read-only
            textBox1.Text = simulationSystem.NumberOfServers.ToString();
            textBox1.ReadOnly = true;
            // Populate comboBox1 with selection methods and set the selected method
            comboBox1.Items.Clear();
            foreach (Enums.SelectionMethod method in Enum.GetValues(typeof(Enums.SelectionMethod)))
            {
                comboBox1.Items.Add(method);
            }
            comboBox1.SelectedItem = simulationSystem.SelectionMethod; // Set initial selected method
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; // Make it a dropdown list

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
            serverPerformance.Show();
            graphBox.Items.Clear();
            graphBox.DropDownStyle = ComboBoxStyle.DropDownList;
            graphBox.Font = new Font("Arial", 10, FontStyle.Regular);
            serverPerformance.DefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Regular);
            serverPerformance.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            serverPerformance.ReadOnly = true;

            foreach (Server s in simulationSystem.Servers)
            {
                graphBox.Items.Add(s.ID);
            }
            if (simulationSystem.Servers.Count > 0)
            {
                graphBox.SelectedIndex = 0;
                drawServerGraph(simulationSystem.Servers[0]);
                // output server performance
                drawServerPerformance(simulationSystem.Servers[0]);
            }

            // output simulation performance
        
            List<String> cols = new List<String>() { "Simulation Performace Measure", "Value" };
            initializeGridView(ref simulationPerformance, ref cols);
            simulationPerformance.Show();
            simulationPerformance.Rows.Add("Number of Customers", simulationSystem.NumberOfCustomers);
            simulationPerformance.Rows.Add("Number of Servers", simulationSystem.NumberOfServers);
            simulationPerformance.Rows.Add("Simulation End Time", simulationSystem.EndTimeSimulation);
            simulationPerformance.Rows.Add("Max Queue Length", simulationSystem.PerformanceMeasures.MaxQueueLength);
            simulationPerformance.Rows.Add("Average Waiting Time", simulationSystem.PerformanceMeasures.AverageWaitingTime.ToString("F4"));
            simulationPerformance.Rows.Add("Waiting Probability", simulationSystem.PerformanceMeasures.WaitingProbability.ToString("F4"));
            simulationPerformance.Rows.Add("Stopping Criteria", simulationSystem.StoppingCriteria.ToString());
            simulationPerformance.Rows.Add("Stopping Number", simulationSystem.StoppingNumber);
            simulationPerformance.Columns["Simulation Performace Measure"].SortMode = DataGridViewColumnSortMode.NotSortable;
            simulationPerformance.Columns["Value"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void drawServerPerformance(Server s)
        {
            // output performance
            List<String> cols = new List<String>() { "Server Performance Measure", "Value"};
            initializeGridView(ref serverPerformance, ref cols);
            serverPerformance.Rows.Add("Average Service Time", s.AverageServiceTime.ToString("F4"));
            serverPerformance.Rows.Add("Idle Probability", s.IdleProbability.ToString("F4"));
            serverPerformance.Rows.Add("Utilization", s.Utilization.ToString("F4"));
            serverPerformance.Rows.Add("Finish Time", s.FinishTime);
            serverPerformance.Rows.Add("Total Number Of Customers", s.TotalNumberOfCustomers);
            serverPerformance.Columns["Server Performance Measure"].SortMode = DataGridViewColumnSortMode.NotSortable;
            serverPerformance.Columns["Value"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void drawServerGraph(Server s)
        {
            graph.Legends.Clear();
            graph.Series.Clear();
            graph.ChartAreas[0].AxisX.CustomLabels.Clear();


            graph.ChartAreas[0].AxisX.Maximum = simulationSystem.EndTimeSimulation;
            graph.ChartAreas[0].AxisX.Interval = simulationSystem.EndTimeSimulation;

            Series series = new Series
            {
                ChartType = SeriesChartType.Area,
                Color = Color.Blue,
                BorderWidth = 2
            };

            // Add points to form a rectangle
            int l = 0, r = 0;
            foreach (KeyValuePair<int, int> range in s.WorkingRanges)
            {
                l = range.Key;
                r = range.Value;
                series.Points.AddXY(l, 0); // Top-left
                series.Points.AddXY(r, 0); // Top-right
                series.Points.AddXY(r, 1); // Bottom-right
                series.Points.AddXY(l, 1); // Bottom-left
                series.Points.AddXY(l, 0); // Close the rectangle
            }
            if (s.WorkingRanges.Count != 0)
            {
                l = s.WorkingRanges.First().Key;
                r = s.WorkingRanges.Last().Value;
                graph.Series.Add(series);
            }
            else
            {
                // Placeholder to render the axis
                Series placeholderSeries = new Series
                {
                    ChartType = SeriesChartType.Line,
                    Color = Color.Transparent
                };
                placeholderSeries.Points.AddXY(0, 0);
                placeholderSeries.Points.AddXY(graph.ChartAreas[0].AxisX.Maximum, 0);
                graph.Series.Add(placeholderSeries);
            }
        }
        private void runButton_Click(object sender, EventArgs e)
        {
            simulationSystem.Simulate();
            viewOutput();
            runButton.Hide();
            string result = TestingManager.Test(simulationSystem, inputFileName);
            MessageBox.Show(result);
        }
        private void graphBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = graphBox.SelectedIndex;
            if (idx == -1) return;
            drawServerGraph(simulationSystem.Servers[idx]);
            drawServerPerformance(simulationSystem.Servers[idx]);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                simulationSystem.StoppingCriteria = Enums.StoppingCriteria.NumberOfCustomers;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                simulationSystem.StoppingCriteria = Enums.StoppingCriteria.SimulationEndTime;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                simulationSystem.SelectionMethod = (Enums.SelectionMethod)comboBox1.SelectedItem;
            }
        }
    }
}
