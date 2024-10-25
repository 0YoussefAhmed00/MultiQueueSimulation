using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    internal class HelperClass
    {
        public static int getTime(List<TimeDistribution> timeDistributions, int randomNumber)
        {
            int l = 0, r = timeDistributions.Count - 1, md;
            while (l <= r)
            {
                md = (l + r) >> 1;
                if (timeDistributions[md].MinRange > randomNumber)
                    r = md - 1;
                else if (timeDistributions[md].MaxRange < randomNumber)
                    l = md + 1;
                else
                    return timeDistributions[md].Time;
            }
            return -1;
        }
        public class ServerPriorityComparer : IComparer<Server>
        {
            public int Compare(Server x, Server y)
            {
                // Compare based on ID for priority
                return x.ID.CompareTo(y.ID);
            }
        }
        public class ServerUtilizationComparer : IComparer<Server>
        {
            public int Compare(Server x, Server y)
            {
                // Compare based on Utilization for least utilized
                return x.TotalWorkingTime.CompareTo(y.TotalWorkingTime);
            }
        }
        public static void Swap<T>(ICollection<T> collection, int indexA, int indexB)
        {
            if (collection is List<T> list)
            {
                T temp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = temp;
            }
            else
            {
                throw new InvalidOperationException("Collection must implement List<T> for swapping.");
            }
        }

        public class IdleServers
        {
            private ICollection<Server> availableServers;

            public delegate void RegisterServerDelegate(Server s);
            public delegate Server SelectServerDelegate();

            public readonly RegisterServerDelegate registerServer;
            public readonly SelectServerDelegate selectServer;
            private Random random = new Random();

            public bool empty()
            {
                return availableServers.Count == 0;
            }

            public IdleServers(Enums.SelectionMethod selectionMethod)
            {
                switch (selectionMethod)
                {
                    case Enums.SelectionMethod.Random:
                        availableServers = new List<Server>();
                        registerServer = (Server s) =>
                        {
                            availableServers.Add(s);
                            HelperClass.Swap(availableServers, random.Next(0, availableServers.Count - 1), availableServers.Count - 1);
                        };
                        selectServer = () =>
                        {
                            HelperClass.Swap(availableServers, random.Next(0, availableServers.Count - 1), availableServers.Count - 1);
                            Server selected = availableServers.Last();
                            if (availableServers is List<Server> list)
                                list.RemoveAt(list.Count - 1);
                            return selected;
                        };
                        break;

                    default:
                        registerServer = (Server s) => availableServers.Add(s);
                        selectServer = () =>
                        {
                            Server selected = availableServers.First();
                            availableServers.Remove(selected);
                            return selected;
                        };
                        if (selectionMethod == Enums.SelectionMethod.HighestPriority)
                            availableServers = new SortedSet<Server>(new ServerPriorityComparer());
                        else
                            availableServers = new SortedSet<Server>(new ServerUtilizationComparer());
                        break;
                }
            }
        }

        public static void moveToIdle(ref SortedDictionary<int, List<Server>> sortedFinished, ref KeyValuePair<int, List<Server>> keyValue, ref IdleServers idleServers)
        {
            List<Server> serverList = keyValue.Value;
            while (serverList.Count > 0)
            {
                Server server = serverList.Last();
                serverList.RemoveAt(serverList.Count - 1);
                idleServers.registerServer(server);
            }
            sortedFinished.Remove(keyValue.Key);
        }
        public static void AssignCustomerToServer(ref SimulationCase customer, ref SortedDictionary<int, List<Server>> sortedFinished, ref IdleServers idleServers)
        {
            while (sortedFinished.Count > 0)
            {
                KeyValuePair<int, List<Server>> keyValue = sortedFinished.First();
                if (keyValue.Key > customer.ArrivalTime)
                {
                    if (idleServers.empty())
                        HelperClass.moveToIdle(ref sortedFinished, ref keyValue, ref idleServers);
                    break;
                }
                HelperClass.moveToIdle(ref sortedFinished, ref keyValue, ref idleServers);
            }
            Server server = idleServers.selectServer();
            customer.AssignedServer = server;
            customer.ServiceTime = HelperClass.getTime(server.TimeDistribution, customer.RandomService);
            customer.StartTime = Math.Max(customer.ArrivalTime, customer.AssignedServer.FinishTime);
            customer.TimeInQueue = customer.StartTime - customer.ArrivalTime;
            customer.EndTime = customer.StartTime + customer.ServiceTime;
            server.TotalWorkingTime += customer.ServiceTime;
            server.FinishTime = customer.EndTime;
            server.TotalNumberOfCustomers += 1;
            sortedFinished[server.FinishTime].Add(server);
        }
    }
}
