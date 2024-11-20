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
        public Form1()
        {
            InitializeComponent();
            simulationSystem = new SimulationSystem();
            outputGrid.Visible = false;
            unsoldNumberText.Visible = false;
            unsoldText.Visible = false;
            excessDeamndText.Visible = false;
            excessPaperNumber.Visible = false;
            testText.Visible = false;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            simulationSystem.clearData();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Select a Text File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
             
                string filePath = openFileDialog.FileName;

                simulationSystem.checkTestCase = System.IO.Path.GetFileName(filePath);
                // read the file
                string[] lines = System.IO.File.ReadAllLines(filePath);
                // loop through the file content
                string s = "";
                for (int  i = 0;  i < lines.Length;  i++)
                {
                    s += lines[i] + "  ";
                }
                //testText.Text = s;

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
                    DemandDistribution.clearRanges();
                    List<DemandDistribution> distributions = new List<DemandDistribution>();
                    index++;
                    for (int i = index; i < lines.Length; i++)
                    {
                        string[] parts = lines[i].Split(',');
                        List<DayTypeDistribution> types = new List<DayTypeDistribution>();
                        DemandDistribution distribution = new DemandDistribution();
                        
                        distribution.Demand = int.Parse(parts[0]);
                        for (int j = 1; j < parts.Length ; j++) {
                            DayTypeDistribution type = new DayTypeDistribution();
                            type.DayType = (Enums.DayType)(j-1);
                            if (parts[j] != " 0.00")
                                type.Probability = decimal.Parse(parts[j]);
                            else
                                type.Probability = decimal.Parse("100.00"); 
                            types.Add(type); 
                        }
                        distribution.DayTypeDistributions = types;
                        distribution.setRanges();
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
            foreach (String col in cols)
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

        private void runButton_click(object sender, EventArgs e)
        {
            outputGrid.Visible = true;
            simulationSystem.getSimulationTable();
            outputGrid.Columns.Clear();
            outputGrid.Rows.Clear();
            //outputGrid.Show();
            List<String> outputCols = new List<String> { "Day", "Random News Paper Type", "News Paper Type",
                "Random Demand", "Demand", "Sales Profit", "Lost Profit", "Scrap Profit", "Daily Profit"};

            initializeGridView(ref outputGrid, ref outputCols);
            foreach (SimulationCase row in simulationSystem.SimulationTable)
            {
                outputGrid.Rows.Add(row.DayNo, row.RandomNewsDayType, row.NewsDayType, row.RandomDemand,
                    row.Demand, row.SalesProfit, row.LostProfit, row.ScrapProfit, row.DailyNetProfit);
            }
            PerformanceMeasures p = simulationSystem.PerformanceMeasures;
            outputGrid.Rows.Add("", "", "", "",
                 "", p.TotalSalesProfit, p.TotalLostProfit, p.TotalScrapProfit, p.TotalNetProfit);

            unsoldNumberText.Visible = true;
            unsoldText.Visible = true;
            excessDeamndText.Visible = true;
            excessPaperNumber.Visible = true;
        
            excessPaperNumber.Text = $"{simulationSystem.PerformanceMeasures.DaysWithMoreDemand}";
            unsoldNumberText.Text = $"{simulationSystem.PerformanceMeasures.DaysWithUnsoldPapers}";

            setTestText();
        }

        private void setTestText()
        {
            testText.Visible = true;
            string result = TestingManager.Test(simulationSystem, simulationSystem.checkTestCase);
            testText.Text = result;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

       
        
    }
}
