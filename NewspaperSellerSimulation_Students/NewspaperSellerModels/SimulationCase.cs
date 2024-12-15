using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using static NewspaperSellerModels.PerformanceMeasures;

namespace NewspaperSellerModels
{
    public class SimulationCase
    {
        public int DayNo { get; set; }
        public int RandomNewsDayType { get; set; }
        public Enums.DayType NewsDayType { get; set; }
        public int RandomDemand { get; set; }
        public int Demand { get; set; }
        public decimal DailyCost { get; set; }
        public decimal SalesProfit { get; set; }
        public decimal LostProfit { get; set; }
        public decimal ScrapProfit { get; set; }
        public decimal DailyNetProfit { get; set; }

        public void SetSalesProfit(decimal sellingPrice, int newsPaperNo)
        {
            int tmp = newsPaperNo - Demand;
            if (tmp < 0)
                SalesProfit = newsPaperNo * sellingPrice;
            else
                SalesProfit = Demand * sellingPrice;


        }

        public void SetDailyCost(decimal cost, int newsPaperNo) { 
            DailyCost = (cost * newsPaperNo);
        }

        public void SetScrap_Lost_Profit(decimal scrapPrice, int newsPaperNo, decimal purchasePrice, decimal sellingPrice, ref int moreDemandDays,ref int unsoldPaperDays) {
            int tmp = newsPaperNo - Demand;
            
            if (tmp > 0){
                ScrapProfit = (tmp * scrapPrice);
                LostProfit = 0;
                unsoldPaperDays++;

            }
            else if (tmp < 0)
            {
                ScrapProfit = 0;
                LostProfit = (tmp * -1) * (sellingPrice - purchasePrice);
                moreDemandDays++;

            }
            else{
                ScrapProfit = 0; 
                LostProfit = 0;
            }
            
        }

        public void SetDailyNetProfit() {
            DailyNetProfit = SalesProfit - DailyCost - LostProfit + ScrapProfit;
        }
   
    }
}
