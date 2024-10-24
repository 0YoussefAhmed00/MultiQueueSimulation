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

              

                // Load inputs into the SimulationSystem
                simulationSystem.NumberOfServers = numberOfServers;
                simulationSystem.StoppingNumber = stoppingNumber;
                simulationSystem.StoppingCriteria = (Enums.StoppingCriteria)stoppingCriteria;
                simulationSystem.SelectionMethod = (Enums.SelectionMethod)selectionMethod;

                // Populate InterarrivalDistribution
                PopulateInterarrivalDistribution(simulationSystem);

                // Populate Servers and their Service Distributions
                PopulateServers(simulationSystem);

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
                    simulationSystem.StoppingCriteria = int.Parse(lines[++index].Trim());
                    break;

                case "SelectionMethod":
                    selectionMethod = int.Parse(lines[++index].Trim());
                    break;

                case "InterarrivalDistribution":
                    ParseDistribution(lines, ref index, ref timeDistributionsOfCustomers);
                    break;

                default:
                    if (line.StartsWith("ServiceDistribution_Server"))
                    {
                        int id = int.Parse(line.Substring(26));
                        Server newServer = new Server(id);
                        servers.Add(newServer);
                        ParseDistribution(lines, ref index, ref newServer.TimeDistribution);
                    }
                    else
                    {
                        throw new Exception("undefind line: " + line);
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
                endRange = (int)cumProb * numberOfCustomers;
                table.Add(new TimeDistribution(value, probability, cumProb, startRange, endRange));
                startRange = endRange + 1;
            }

            index--; // Step back to allow the main loop to process correctly
        }

        private void PopulateInterarrivalDistribution(SimulationSystem simulationSystem)
        {
            if (distributions.TryGetValue("InterarrivalDistribution", out var interarrival))
            {
                foreach (var (value, probability) in interarrival)
                {
                    simulationSystem.InterarrivalDistribution.Add(new TimeDistribution
                    {
                        Time = value,
                        Probability = (decimal)probability
                    });
                }
            }
        }

        private void PopulateServers(SimulationSystem simulationSystem)
        {
            for (int i = 1; i <= numberOfServers; i++)
            {
                string key = $"ServiceDistribution_Server{i}";
                if (distributions.TryGetValue(key, out var serviceDistribution))
                {
                    Server server = new Server
                    {
                        ID = i, // Set the server ID
                        TimeDistribution = new List<TimeDistribution>()
                    };

                    foreach (var (value, probability) in serviceDistribution)
                    {
                        server.TimeDistribution.Add(new TimeDistribution
                        {
                            Time = value,
                            Probability = (decimal)probability
                        });
                    }

                    simulationSystem.Servers.Add(server);
                }
            }
        }

        private void PopulateDataGridView()
        {
            // Clear previous data input
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Add columns
            dataGridView1.Columns.Add("Key", "Key");
            dataGridView1.Columns.Add("Value", "Value");
            dataGridView1.Columns.Add("Probability", "Probability");

            // Add rows
            dataGridView1.Rows.Add("NumberOfServers", numberOfServers, "");
            dataGridView1.Rows.Add("StoppingNumber", stoppingNumber, "");
            dataGridView1.Rows.Add("StoppingCriteria", stoppingCriteria, "");
            dataGridView1.Rows.Add("SelectionMethod", selectionMethod, "");

            // Add distribution data
            foreach (var distribution in distributions)
            {
                foreach (var (value, probability) in distribution.Value)
                {
                    dataGridView1.Rows.Add(distribution.Key, value, probability);
                }
            }
        }
    }
}
