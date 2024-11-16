using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                    simulationSystem.PurchasePrice = int.Parse(lines[++index].Trim());
                    break;

                case "ScrapPrice":
                    simulationSystem.ScrapPrice = int.Parse(lines[++index].Trim());
                    break;
                case "SellingPrice":
                    simulationSystem.SellingPrice = int.Parse(lines[++index].Trim());
                    break;
                case "DayTypeDistributions":
                    List<DayTypeDistribution> L = simulationSystem.DayTypeDistributions;
                    ParseDayTypeDistribution(lines, ref index, ref L);
                    break;

                default:
                    if (line.StartsWith("ServiceDistribution_Server"))
                    {
                        int id = int.Parse(line.Substring(26));
                        //DemandDistribution newDistrib = new DemandDistribution(id);
                        //simulationSystem.DemandDistributions.Add(newDistrib);
                        //ParseDemandDistribution(lines, ref index, ref newServer.TimeDistribution);
                    }
                    break;
            }
        }
        private void ParseDayTypeDistribution(string[] lines, ref int index, ref List<DayTypeDistribution> list)
        {


            int startRange = 1, endRange;
            while (++index < lines.Length && lines[index].Contains(","))
            {
                string[] parts = lines[index].Split(',');
                decimal[] probablites = new decimal[4];
                decimal[] comulaitve = new decimal[4];
                comulaitve[0] = 0;
                for (int i = 1; i<4; i++)
                {
                    probablites[i-1] = decimal.Parse(parts[i-1].Trim());
                    comulaitve[i] += comulaitve[i - 1];
                }

                for (int i = 0; i < 3; i++) {
                    endRange = (int)(comulaitve[i + 1] * 100);
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
                    list.Add(new DayTypeDistribution(type, probablites[i], comulaitve[i+1], startRange, endRange));
                    startRange = endRange + 1;
                }

            }
            index--; // Step back to allow the main loop to process correctly
        }

        private void ParseDemandDistribution(string[] lines, ref int index, ref List<DayTypeDistribution> list)
        {


            int startRange = 1, endRange;
            while (++index < lines.Length && lines[index].Contains(","))
            {
                string[] parts = lines[index].Split(',');
                decimal[] probablites = new decimal[4];
                decimal[] comulaitve = new decimal[4];
                
                

            }
            index--; // Step back to allow the main loop to process correctly
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
    }
}
