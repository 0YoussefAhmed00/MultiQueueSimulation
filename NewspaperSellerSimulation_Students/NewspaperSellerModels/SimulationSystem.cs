using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            DayTypeDistributions = new List<DayTypeDistribution>();
            DemandDistributions = new List<DemandDistribution>();
            SimulationTable = new List<SimulationCase>();
            PerformanceMeasures = new PerformanceMeasures();
        }

        ///////////// INPUTS /////////////
        public int NumOfNewspapers { get; set; }
        public int NumOfRecords { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ScrapPrice { get; set; }
        public decimal UnitProfit { get; set; }
        public List<DayTypeDistribution> DayTypeDistributions { get; set; }
        public List<DemandDistribution> DemandDistributions { get; set; }

        private int day = 1;

        Random random = new Random();

        public string checkTestCase;


        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }

        // -----
        #region calculating simulation cases
        private Enums.DayType getDayType(int type)
        {
            Enums.DayType t = Enums.DayType.Good;
            foreach (var d in DayTypeDistributions)
            {
                if(type <= d.MaxRange && type >= d.MinRange)
                {
                    t = d.DayType; 
                    break;
                }
            }
            return t;
        }

        private int getDemand(int n, Enums.DayType type)
        {
     
            for (int i = 0; i<DemandDistributions.Count; i++)
            {
                int min = DemandDistributions[i].DayTypeDistributions[(int)type].MinRange;
                int max = DemandDistributions[i].DayTypeDistributions[(int)type].MaxRange;
                if(n >= min && n <= max)
                    return DemandDistributions[i].Demand;
            }

            return -1;
        }


        private SimulationCase getSimulationCase()
        {
            int demand = random.Next(1, 101);
            int type = random.Next(1, 101);
            SimulationCase tmp = new SimulationCase();
            tmp.DayNo = day;
            tmp.RandomNewsDayType = type;
            tmp.NewsDayType = getDayType(type);
            tmp.RandomDemand = demand;
            int a = PerformanceMeasures.DaysWithMoreDemand;
            int b = PerformanceMeasures.DaysWithUnsoldPapers;
            tmp.Demand = getDemand(demand, tmp.NewsDayType);
            tmp.SetScrap_Lost_Profit(ScrapPrice, NumOfNewspapers, PurchasePrice, SellingPrice, ref a, ref b);
            PerformanceMeasures.DaysWithMoreDemand = a;
            PerformanceMeasures.DaysWithUnsoldPapers = b;
            tmp.SetSalesProfit(SellingPrice, NumOfNewspapers);
            tmp.SetDailyCost(PurchasePrice, NumOfNewspapers);
            tmp.SetDailyNetProfit();
            day++;
            return tmp;
        }

        public void getSimulationTable()
        {
            day = 1;

            for (int i = 0; i < NumOfRecords; i++)
            {
                SimulationCase simulationCase = getSimulationCase();
                SimulationTable.Add(simulationCase);
                PerformanceMeasures.TotalNetProfit += simulationCase.DailyNetProfit;
                PerformanceMeasures.TotalScrapProfit += simulationCase.ScrapProfit;
                PerformanceMeasures.TotalLostProfit += simulationCase.LostProfit;
                PerformanceMeasures.TotalSalesProfit += simulationCase.SalesProfit;
                PerformanceMeasures.TotalCost += simulationCase.DailyCost;
            }
        }
        #endregion
        public void clearData()
        {
            day = 1;
            DayTypeDistributions.Clear();
            SimulationTable.Clear();
            DemandDistributions.Clear();
            PerformanceMeasures.TotalNetProfit = 0;
            PerformanceMeasures.TotalScrapProfit = 0;
            PerformanceMeasures.TotalLostProfit = 0;
            PerformanceMeasures.TotalSalesProfit = 0;
            PerformanceMeasures.TotalCost = 0;
            PerformanceMeasures.DaysWithMoreDemand = 0;
            PerformanceMeasures.DaysWithUnsoldPapers = 0;   
        }
    }
}
