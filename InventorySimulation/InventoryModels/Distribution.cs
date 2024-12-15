using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class Distribution
    {
        public Distribution()
        {

        }
        public Distribution(int Value, decimal Probability, decimal CummProbability, int MinRange, int MaxRange)
        {
            this.Value = Value;
            this.Probability = Probability;
            this.CummProbability = CummProbability;
            this.MinRange = MinRange;
            this.MaxRange = MaxRange;
        }
        public int Value { get; set; }
        public decimal Probability { get; set; }
        public decimal CummProbability { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
    }
}
