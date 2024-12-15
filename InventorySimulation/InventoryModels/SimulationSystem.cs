using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            DemandDistribution = new List<Distribution>();
            LeadDaysDistribution = new List<Distribution>();
            SimulationTable = new List<SimulationCase>();
            PerformanceMeasures = new PerformanceMeasures();
        }

        ///////////// INPUTS /////////////

        public int OrderUpTo { get; set; }
        public int ReviewPeriod { get; set; }
        public int NumberOfDays { get; set; }
        public int StartInventoryQuantity { get; set; }
        public int StartLeadDays { get; set; }
        public int StartOrderQuantity { get; set; }
        public List<Distribution> DemandDistribution { get; set; }
        public List<Distribution> LeadDaysDistribution { get; set; }

        public Random random = new Random();

        ///////////// OUTPUTS /////////////

        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }

        ///////////// METHODS /////////////

        public int getRandomDemand(int random_value)
        {
            foreach (var distribution in DemandDistribution)
            {
                //Console.WriteLine(random_value);
                //Console.WriteLine($"{distribution.Value} {distribution.MinRange} {distribution.MaxRange}");
                if (distribution.MinRange <= random_value && distribution.MaxRange >= random_value)
                    return distribution.Value;

            }
            return 0;
        }
        public int getRandomLeadDays(int random_value)
        {
            //Console.WriteLine(random_value);
            foreach (var lead in LeadDaysDistribution)
            {
                //Console.WriteLine($"{lead.Value} {lead.MinRange} {lead.MaxRange}");
            }
            foreach (var distribution in LeadDaysDistribution)
                if (distribution.MinRange <= random_value && distribution.MaxRange >= random_value)
                    return distribution.Value;
            return 0;
        }

        public void getSimulationTable()
        {
            SimulationCase simulationCase = new SimulationCase(this, StartInventoryQuantity, StartLeadDays - 1, StartOrderQuantity);
            SimulationTable.Add(simulationCase);
            PerformanceMeasures.ShortageQuantityAverage += SimulationTable.Last().ShortageQuantity;
            PerformanceMeasures.EndingInventoryAverage += SimulationTable.Last().EndingInventory;
            for (int i = 1; i < NumberOfDays; i++)
            {
                SimulationTable.Add(new SimulationCase(this, SimulationTable.Last()));
                PerformanceMeasures.ShortageQuantityAverage += SimulationTable.Last().ShortageQuantity;
                PerformanceMeasures.EndingInventoryAverage += SimulationTable.Last().EndingInventory;
            }
            PerformanceMeasures.ShortageQuantityAverage /= NumberOfDays;
            PerformanceMeasures.EndingInventoryAverage /= NumberOfDays;
        }
    }
}
