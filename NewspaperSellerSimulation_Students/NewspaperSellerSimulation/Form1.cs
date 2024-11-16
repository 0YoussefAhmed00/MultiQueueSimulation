using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerModels;
using NewspaperSellerTesting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace NewspaperSellerSimulation
{
    public partial class Form1 : Form
    {
        SimulationSystem simulationSystem;
        string filaPath;
        public Form1()
        {
            InitializeComponent();
            dataTable.Visible = false;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            simulationSystem = new SimulationSystem();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Select a Text File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
             
                string filePath = openFileDialog.FileName;

                filaPath = System.IO.Path.GetFileName(filePath);
                // read the file
                string[] lines = System.IO.File.ReadAllLines(filePath);
                // loop through the file content
                string s = "";
                for (int  i = 0;  i < lines.Length;  i++)
                {
                    s += lines[i] + "  ";
                }
                testText.Text = s;

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    ParseInputLine(lines, ref i, line);
                }
                simulationSystem.UnitProfit = simulationSystem.SellingPrice - simulationSystem.PurchasePrice;
                // Display data in the grid view
                //viewInput();
            }
        }

        private void ParseInputLine(string[] lines, ref int index, string line)
        {
            switch (line)
            {
                case "NumOfNewspapers":
                    simulationSystem.NumOfNewspapers = int.Parse(lines[++index].Trim());
                    break;

                case "NumOfRecords":
                    simulationSystem.NumOfRecords = int.Parse(lines[++index].Trim());
                    break;

                case "PurchasePrice":
                    simulationSystem.PurchasePrice = decimal.Parse(lines[++index].Trim());
                    break;

                case "ScrapPrice":
                    simulationSystem.ScrapPrice = decimal.Parse(lines[++index].Trim());
                    break;
                case "SellingPrice":
                    simulationSystem.SellingPrice = decimal.Parse(lines[++index].Trim());
                    break;
                case "DayTypeDistributions":
                    List<DayTypeDistribution> L = simulationSystem.DayTypeDistributions;
                    ParseDayTypeDistribution(lines, ref index, ref L);
                    break;

                case "DemandDistributions":
       
                    List<DemandDistribution> distributions = new List<DemandDistribution>();
                    index++;
                    for (int i = 0; i < lines.Length - index; i++)
                    {
                        string[] parts = lines[index].Split(',');
                        List<DayTypeDistribution> types = new List<DayTypeDistribution>();
                        DemandDistribution distribution = new DemandDistribution();
                        
                        distribution.Demand = int.Parse(parts[0]);
                        for (int j = 1; j < parts.Length ; j++) {
                            DayTypeDistribution type = new DayTypeDistribution();
                            type.DayType = (Enums.DayType)(j-1);
                            type.Probability = decimal.Parse(parts[j]);
                            types.Add(type); 
                        }
                        distribution.DayTypeDistributions = types;
                        distributions.Add(distribution);
                    }
                    simulationSystem.DemandDistributions = distributions;
                    
                    break;
            }
        }

        private List<int> ParseDayTypeDistribution(string[] lines, ref int index, ref List<DayTypeDistribution> list, int inc = 0)
        {
            List<int> demands = new List<int>(); 
            int startRange = 1, endRange;
            while (++(index) < lines.Length && lines[index].Contains(","))
            {
                string[] parts = lines[index].Split(',');
                decimal[] probabilities = new decimal[4];
                decimal[] cumulative = new decimal[4];
                cumulative[0] = 0;
                if (inc != 0) 
                    demands.Add(int.Parse(parts[0]));
                for (int i = 1; i<4; i++)
                {
                    probabilities[i-1] = decimal.Parse(parts[i-1 + inc].Trim());
                    cumulative[i] = cumulative[i-1] + probabilities[i-1];
                }

                for (int i = 0; i < 3; i++) {
                    endRange = (int)(cumulative[i + 1] * 100);
                    Enums.DayType type = Enums.DayType.Good;
                    switch (i)
                    {
                        case 1:
                            type = Enums.DayType.Fair;
                            break; 
                        case 2:
                            type = Enums.DayType.Poor;
                            break;
                        default:break;
                    }
                    list.Add(new DayTypeDistribution(type, probabilities[i], cumulative[i+1], startRange, endRange));
                    startRange = endRange + 1;
                }

            }
            index--;
            return demands;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void runButton_click(object sender, EventArgs e)
        {
            dataTable.Visible = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
