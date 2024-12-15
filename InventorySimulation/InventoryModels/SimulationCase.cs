using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class SimulationCase
    {
        public SimulationCase(SimulationSystem simulationSystem, int beginnin_Inventory, int start_lead_day, int start_order_quantity)
        {
            Day = 1;
            Cycle = 1;
            DayWithinCycle = 1;
            BeginningInventory = beginnin_Inventory;
            RandomDemand = simulationSystem.random.Next(1, 100);
            Demand = simulationSystem.getRandomDemand(RandomDemand);
            EndingInventory = Math.Max(BeginningInventory - Demand, 0);
            ShortageQuantity = Math.Max(Demand - BeginningInventory, 0);
            RandomLeadDays = 0;
            LeadDays = 0;
            ArrivingOrder = (start_order_quantity, start_lead_day);
        } public SimulationCase()
        {

        }

        public SimulationCase(SimulationSystem simulationSystem, SimulationCase old_simulation_case)
        {
            Day = old_simulation_case.Day + 1;
            Cycle = old_simulation_case.Cycle + (old_simulation_case.DayWithinCycle == simulationSystem.ReviewPeriod ? 1 : 0);
            DayWithinCycle = (old_simulation_case.DayWithinCycle % simulationSystem.ReviewPeriod) + 1;
            //Console.WriteLine(old_simulation_case.DayWithinCycle % simulationSystem.ReviewPeriod);
            //Console.WriteLine(DayWithinCycle);

            BeginningInventory = old_simulation_case.EndingInventory;
            RandomLeadDays = 0;
            LeadDays = 0;
            ArrivingOrder = (old_simulation_case.ArrivingOrder.OrderQuantity, Math.Max(0, old_simulation_case.ArrivingOrder.DaysUntilOrderArrives - 1));
            if (old_simulation_case.ArrivingOrder.DaysUntilOrderArrives == 0)
            {
                //Console.WriteLine(Day);
                //Console.WriteLine(old_simulation_case.ArrivingOrder.OrderQuantity);
                BeginningInventory += old_simulation_case.ArrivingOrder.OrderQuantity;
                ArrivingOrder = (0, 0);
            }
            // Demand
            RandomDemand = simulationSystem.random.Next(1, 100);
            Demand = simulationSystem.getRandomDemand(RandomDemand);
            EndingInventory = Math.Max(BeginningInventory - Demand - old_simulation_case.ShortageQuantity, 0);
            ShortageQuantity = Math.Max(old_simulation_case.ShortageQuantity + Demand - BeginningInventory, 0);
            if (DayWithinCycle == simulationSystem.ReviewPeriod)
            {
                RandomLeadDays = simulationSystem.random.Next(1, 100);
                LeadDays = simulationSystem.getRandomLeadDays(RandomLeadDays);
                OrderQuantity = simulationSystem.OrderUpTo - EndingInventory + ShortageQuantity;
                ArrivingOrder = (OrderQuantity, LeadDays);
            }
        }
        public int Day { get; set; }
        public int Cycle { get; set; }
        public int DayWithinCycle { get; set; }
        public int BeginningInventory { get; set; }
        public int RandomDemand { get; set; }
        public int Demand { get; set; }
        public int EndingInventory { get; set; }
        public int ShortageQuantity { get; set; }
        public int OrderQuantity { get; set; }
        public int RandomLeadDays { get; set; }
        public int LeadDays { get; set; }
        public (int OrderQuantity, int DaysUntilOrderArrives) ArrivingOrder { get; set; }
    }
}
