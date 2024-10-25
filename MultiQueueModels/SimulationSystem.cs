using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MultiQueueModels.HelperClass;
namespace MultiQueueModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            this.Servers = new List<Server>();
            this.InterarrivalDistribution = new List<TimeDistribution>();
            this.PerformanceMeasures = new PerformanceMeasures();
            this.SimulationTable = new List<SimulationCase>();
            this.NumberOfCustomers = 100;
        }

        ///////////// INPUTS ///////////// 
        public int NumberOfServers { get; set; }
        public int StoppingNumber { get; set; }
        public List<Server> Servers { get; set; }
        public List<TimeDistribution> InterarrivalDistribution { get; set; }
        public Enums.StoppingCriteria StoppingCriteria { get; set; }
        public Enums.SelectionMethod SelectionMethod { get; set; }
        public int NumberOfCustomers { get; set; }
        public int EndTimeSimulation { get; set; }

        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }
        
        ///////////// Simulate ///////////// 
        public void Simulate()
        {

            #region initialization for Idle Server Manager
            IdleServers idleServers = new IdleServers(SelectionMethod);
            foreach (Server server in Servers)
                idleServers.registerServer(server);
            SortedDictionary<int, List<Server>> sortedFinished = new SortedDictionary<int, List<Server>>();
            #endregion
            #region initialization for customer 
            Random random = new Random();
            SimulationCase customer = new SimulationCase();
            #endregion
            #region initialization needed calculations for PerformanceMeasures
            int waitedTime = 0, numberOfWaitedCustomer = 0;
            int FirstCustomerIndexInQ = 0, maxQLen = 0;
            #endregion
            #region serve Customer 
            Action<int> serveCustomer = (int i) => {
                customer.RandomService = random.Next(1, NumberOfCustomers);
                AssignCustomerToServer(ref customer, ref sortedFinished, ref idleServers);
                EndTimeSimulation = Math.Max(EndTimeSimulation, customer.EndTime);
                if (customer.TimeInQueue != 0)
                {
                    waitedTime += customer.TimeInQueue;
                    numberOfWaitedCustomer += 1;
                    // we can do binary search but not importart as it's small factor!
                    while (SimulationTable[FirstCustomerIndexInQ].StartTime <= customer.ArrivalTime)
                        FirstCustomerIndexInQ++;
                    maxQLen =  Math.Max(maxQLen,i - FirstCustomerIndexInQ + 1);
                }
                else
                    FirstCustomerIndexInQ = i + 1;
                SimulationTable.Add(customer);
            };
            #endregion
            #region special case customer 1
            // receive Customer 1 & serve
            customer.CustomerNumber = 1;
            serveCustomer(1);
            #endregion
            #region recieve customer
            int clockTimeOfArrival = 0;
            Action<int> recieveCustomer = (int i) => {
                customer = new SimulationCase();
                customer.CustomerNumber = i;
                customer.RandomInterArrival = random.Next(1, NumberOfCustomers);
                customer.InterArrival = HelperClass.getTime(InterarrivalDistribution, customer.RandomInterArrival);
                clockTimeOfArrival += customer.InterArrival;
                customer.ArrivalTime = clockTimeOfArrival;
            };
            #endregion
            #region Simulate with appropriate stopping criteria
            switch (StoppingCriteria)
                {
                    case Enums.StoppingCriteria.NumberOfCustomers:
                        for (int i = 2; i <= StoppingNumber; i++)
                        {
                            recieveCustomer(i);
                            serveCustomer(i);
                        }
                        break;
                    default:
                        for (int i = 2; i <= NumberOfCustomers; i++)
                        {
                            recieveCustomer(i);
                            if (clockTimeOfArrival > StoppingNumber)
                                break;
                            serveCustomer(i);
                        }
                        break;
                }

            #endregion
            // simulation has been finished!
            #region calculate all PerformanceMeasures
            PerformanceMeasures.MaxQueueLength = maxQLen;
            PerformanceMeasures.AverageWaitingTime = ((decimal) waitedTime)/ SimulationTable.Count;
            PerformanceMeasures.WaitingProbability = numberOfWaitedCustomer / SimulationTable.Count;

            foreach (Server server in Servers) {
                server.AverageServiceTime = ((decimal)server.TotalWorkingTime) / server.TotalNumberOfCustomers;
                server.Utilization = ((decimal)server.TotalWorkingTime) / EndTimeSimulation;
                server.IdleProbability = 1 - server.Utilization;
            }
            #endregion
        }
    }
}
