using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class TimeDistribution
    {
        public TimeDistribution() { }
        public TimeDistribution(int time, decimal probability, decimal cummProbability, int minRange, int maxRange)
        {
            Time = time;
            Probability = probability;
            CummProbability = cummProbability;
            MinRange = minRange;
            MaxRange = maxRange;
        }

        public int Time { get; set; }
        public decimal Probability { get; set; }
        public decimal CummProbability { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }

    }
}
