using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewspaperSellerModels.Enums;

namespace NewspaperSellerModels
{
    public class DayTypeDistribution
    {
        public DayTypeDistribution() { }
        public DayTypeDistribution(Enums.DayType daytype, decimal probability, decimal cummProbability, int minRange, int maxRange)
        {
            DayType = daytype;
            Probability = probability;
            CummProbability = cummProbability;
            MinRange = minRange;
            MaxRange = maxRange;
        }
        public Enums.DayType DayType { get; set; }
        public decimal Probability { get; set; }
        public decimal CummProbability { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
    }
}
