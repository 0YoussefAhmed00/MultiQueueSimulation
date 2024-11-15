using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            if (NumberOfCustomers == 0)
                return;
            #region initialization for Idle Server Manager
            IServerCollection IdleServers;
            switch (SelectionMethod)
            {
                case Enums.SelectionMethod.HighestPriority:
                    IdleServers = new PriorityCollection();
                    break;
                case Enums.SelectionMethod.LeastUtilization:
                    IdleServers = new UtillizationCollection();
                    break;
                default:
                    IdleServers = new RandomCollection();
                    break;
            }
            foreach (Server server in Servers)
                IdleServers.AddServer(server);
            SortedDictionary<int, Queue<Server>> sortedFinishedTime = new SortedDictionary<int, Queue<Server>>();
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
            Action<int> serveCustomer = (int i) =>
            {
                customer.RandomService = random.Next(1, NumberOfCustomers);
                AssignCustomerToServer(ref customer, ref sortedFinishedTime, ref IdleServers);
                SimulationTable.Add(customer);
                EndTimeSimulation = Math.Max(EndTimeSimulation, customer.EndTime);
                if (customer.TimeInQueue != 0)
                {
                    waitedTime += customer.TimeInQueue;
                    numberOfWaitedCustomer += 1;
                    // we can do binary search but not importart as it's small factor!
                    while (FirstCustomerIndexInQ < SimulationTable.Count && SimulationTable[FirstCustomerIndexInQ].StartTime <= customer.ArrivalTime)
                        FirstCustomerIndexInQ++;
                    maxQLen = Math.Max(maxQLen, i - FirstCustomerIndexInQ);
                }
                else
                    FirstCustomerIndexInQ = i;
            };
            #endregion
            #region recieve customer
            int clockTimeOfArrival = 0;
            Action<int> recieveCustomer = (int i) =>
            {
                customer = new SimulationCase();
                customer.CustomerNumber = i;
                customer.RandomInterArrival = random.Next(1, NumberOfCustomers);
                customer.InterArrival = HelperClass.getTime(InterarrivalDistribution, customer.RandomInterArrival);
                clockTimeOfArrival += customer.InterArrival;
                customer.ArrivalTime = clockTimeOfArrival;
            };
            #endregion
            #region special case customer 1
            Action<int> Customer1 = (int i) =>
            {
                // receive Customer 1 & serve
                customer.RandomInterArrival = 1;
                customer.CustomerNumber = 1;
                serveCustomer(1);
            };
            #endregion
            #region Simulate with appropriate stopping criteria
            switch (StoppingCriteria)
            {
                case Enums.StoppingCriteria.NumberOfCustomers:
                    int mx = Math.Min(StoppingNumber, NumberOfCustomers);
                    if (mx == 0) break;
                    Customer1(1);
                    for (int i = 2; i <= mx; i++)
                    {
                        recieveCustomer(i);
                        serveCustomer(i);
                    }
                    break;
                default:
                    Customer1(1);
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
            if (SimulationTable.Count == 0)
                return;
            // simulation has been finished!
            #region calculate all PerformanceMeasures
            PerformanceMeasures.MaxQueueLength = maxQLen;
            PerformanceMeasures.AverageWaitingTime = ((decimal)waitedTime) / SimulationTable.Count;
            PerformanceMeasures.WaitingProbability = ((decimal)numberOfWaitedCustomer) / SimulationTable.Count;
            foreach (Server server in Servers)
            {
                server.AverageServiceTime = ((decimal)server.TotalWorkingTime) / Math.Max(1, server.TotalNumberOfCustomers);
                server.Utilization = ((decimal)server.TotalWorkingTime) / EndTimeSimulation;
                server.IdleProbability = 1 - server.Utilization;
            }
            #endregion
        }
    }
}
