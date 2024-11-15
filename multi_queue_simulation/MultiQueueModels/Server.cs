using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class Server
    {
        public Server()
        {
            this.TimeDistribution = new List<TimeDistribution>();
            this.WorkingRanges = new List<KeyValuePair<int, int>>();
        }
        public Server(int ID) : this()
        {
            this.ID = ID;
        }
        public int ID { get; set; }
        public decimal IdleProbability { get; set; }
        public decimal AverageServiceTime { get; set; } 
        public decimal Utilization { get; set; }

        public List<TimeDistribution> TimeDistribution;

        //optional if needed use them
        public int FinishTime { get; set; }
        public int TotalWorkingTime { get; set; }
        public int TotalNumberOfCustomers { get; set; }
        public List<KeyValuePair<int, int>> WorkingRanges { get; set; }

    }
}
