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

namespace MultiQueueSimulation
{

    public partial class Form1 : Form
    {
        // Global variables
        private int numberOfCustomers = 100;

        // Create an instance of SimulationSystem
        SimulationSystem simulationSystem = new SimulationSystem();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // openFileDialog to browse for the input file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Select an Input File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
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
                PopulateDataGridView();
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
        private void PopulateDataGridView()
        {

            // Set header appearance (background color, font, etc.)
            dgvInterarrival.EnableHeadersVisualStyles = false;
            dgvInterarrival.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvInterarrival.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInterarrival.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvInterarrival.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInterarrival.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvInterarrival.ColumnHeadersHeight = 60; // Increase the height of the header

            // Optional: Set the row style to alternate colors
            dgvInterarrival.RowsDefaultCellStyle.BackColor = Color.White;
            dgvInterarrival.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Configure DataGridView columns
            dgvInterarrival.Columns.Clear();
            dgvInterarrival.Columns.Add("InterarrivalTime", "Interarrival Time");
            dgvInterarrival.Columns.Add("Probability", "Probability");
            dgvInterarrival.Columns.Add("CumulativeProbability", "Cumulative Probability");
            dgvInterarrival.Columns.Add("Range", "Range");

            // Clear existing rows
            dgvInterarrival.Rows.Clear();

            // Load data into DataGridView from InterarrivalDistribution
            foreach (var dist in simulationSystem.InterarrivalDistribution)
            {
                // Add a new row for each TimeDistribution object
                dgvInterarrival.Rows.Add(
                    dist.Time,
                    dist.Probability,
                    dist.CummProbability,
                    $"{dist.MinRange:D2}-{dist.MaxRange:D2}");
            }

            // Optionally set properties of the DataGridView (e.g., column autosizing)
            dgvInterarrival.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
