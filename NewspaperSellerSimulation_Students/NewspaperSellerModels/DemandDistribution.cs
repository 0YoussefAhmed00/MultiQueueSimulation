using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class DemandDistribution
    {
        public DemandDistribution()
        {
            DayTypeDistributions = new List<DayTypeDistribution>();
        }
        public int Demand { get; set; }
        public List<DayTypeDistribution> DayTypeDistributions { get; set; }

        public static int goodStart = 1, goodEnd = 0;
        public static int fairStart = 1, fairEnd = 0;
        public static int poorStart = 1, poorEnd = 0;

        #region demand ranges
        public void setRanges()
        {

            DayTypeDistribution goodObj = DayTypeDistributions[0];
            DayTypeDistribution fairObj = DayTypeDistributions[1];
            DayTypeDistribution poorObj = DayTypeDistributions[2];

            goodEnd += (int)(goodObj.Probability * 100);
            fairEnd += (int)(fairObj.Probability * 100);
            poorEnd += (int)(poorObj.Probability * 100);

            goodObj.MinRange = goodStart;
            fairObj.MinRange = fairStart;
            poorObj.MinRange = poorStart;

            goodObj.MaxRange = goodEnd;
            fairObj.MaxRange = fairEnd;
            poorObj.MaxRange = poorEnd;


            goodStart = goodEnd + 1;
            fairStart = fairEnd + 1;
            poorStart = poorEnd + 1;
 
        }
        #endregion

    }
}

