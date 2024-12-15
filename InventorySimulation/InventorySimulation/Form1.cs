using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryModels;
using InventoryTesting;

namespace InventorySimulation
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
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Select a Text File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string filePath = openFileDialog.FileName;

                //simulationSystem.checkTestCase = System.IO.Path.GetFileName(filePath);
                // read the file
                string[] lines = System.IO.File.ReadAllLines(filePath);
                // loop through the file content
                string s = "";
                for (int i = 0; i < lines.Length; i++)
                {
                    s += lines[i] + "  ";
                }
                //testText.Text = s;

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    ParseInputLine(lines, ref i, line);
                }
                //simulationSystem.UnitProfit = simulationSystem.SellingPrice - simulationSystem.PurchasePrice;
                // Display data in the grid view
                //viewInput();
            }
        }

        private void ParseInputLine(string[] lines, ref int index, string line)
        {
            switch (line)
            {
                case "OrderUpTo":
                    simulationSystem.OrderUpTo = int.Parse(lines[++index].Trim());
                    break;

                case "ReviewPeriod":
                    simulationSystem.ReviewPeriod = int.Parse(lines[++index].Trim());
                    break;

                case "StartInventoryQuantity":
                    simulationSystem.StartInventoryQuantity = int.Parse(lines[++index].Trim());
                    break;

                case "StartLeadDays":
                    simulationSystem.StartLeadDays = int.Parse(lines[++index].Trim());
                    break;

                case "StartOrderQuantity":
                    simulationSystem.StartOrderQuantity = int.Parse(lines[++index].Trim());
                    break;

                case "NumberOfDays":
                    simulationSystem.NumberOfDays = int.Parse(lines[++index].Trim());
                    break;

                case "DemandDistribution":
                    List<Distribution> L = simulationSystem.DemandDistribution;
                    ParseDistribution(lines, ref index, ref L);
                    break;

                case "LeadDaysDistribution":
                    List<Distribution> LL = simulationSystem.LeadDaysDistribution;
                    ParseDistribution(lines, ref index, ref LL);
                    break;
            }
        }


        private void ParseDistribution(string[] lines, ref int index, ref List<Distribution> table)
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
                endRange = (int)(cumProb * 100);
                if (startRange <= endRange)
                    table.Add(new Distribution(value, probability, cumProb, startRange, endRange));
                startRange = endRange + 1;
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
            List<String> outputCols = new List<String> { "Day", "Cycle", "Day Within Cycle",
                "Beginning Env", "Random Digit For Demand", "Demand", "Ending Envintory", "Shortage Quantity",
                "Order Quantity", "Random Digit For Lead Time", "Lead Time", "Days Untill Order Arrives"};

            initializeGridView(ref outputGrid, ref outputCols);
            foreach (SimulationCase row in simulationSystem.SimulationTable)
            {
                outputGrid.Rows.Add(row.Day, row.Cycle, row.DayWithinCycle, row.BeginningInventory,
                    row.RandomDemand, row.Demand, row.EndingInventory, row.ShortageQuantity, row.OrderQuantity,
                    row.RandomLeadDays, row.LeadDays, row.ArrivingOrder.DaysUntilOrderArrives);
            }
            PerformanceMeasures p = simulationSystem.PerformanceMeasures;
            outputGrid.Rows.Add("", "", "",
                "", "", "", simulationSystem.PerformanceMeasures.EndingInventoryAverage, simulationSystem.PerformanceMeasures.ShortageQuantityAverage,
                "", "", "", "");

            unsoldNumberText.Visible = true;
            unsoldText.Visible = true;
            excessDeamndText.Visible = true;
            excessPaperNumber.Visible = true;

            //excessPaperNumber.Text = $"{simulationSystem.PerformanceMeasures.DaysWithMoreDemand}";
            //unsoldNumberText.Text = $"{simulationSystem.PerformanceMeasures.DaysWithUnsoldPapers}";

            setTestText();
        }

        private void setTestText()
        {
            testText.Visible = true;
            string result = TestingManager.Test(simulationSystem, Constants.FileNames.TestCase2);
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
