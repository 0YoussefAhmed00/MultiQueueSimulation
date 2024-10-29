using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static void Swap<T>(List<T> list, int indexA, int indexB)
        {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }
        public interface IServerCollection
        {
            void AddServer(Server server);
            Server SelectServer();
            bool IsEmpty { get; }
            int Count { get; }
        }

        public class RandomCollection : IServerCollection
        {
            private List<Server> servers = new List<Server>();
            private readonly Random random = new Random();
            public void AddServer(Server server)
            {
                servers.Add(server);
                int idxLast = servers.Count - 1;
                HelperClass.Swap(servers,random.Next(0, idxLast), idxLast);
            }
            public Server SelectServer()
            {
                int idxLast = servers.Count - 1;
                Server server = servers[idxLast];
                servers.RemoveAt(idxLast);
                return server;
            }
            public bool IsEmpty => servers.Count == 0;
            public int Count => servers.Count;
        }

        public class PriorityCollection: IServerCollection
        {
            private SortedSet<Server> servers = new SortedSet<Server>(new ServerPriorityComparer());
            public void AddServer(Server server) => servers.Add(server);
            public Server SelectServer()
            {
                Server server = servers.First();
                servers.Remove(server);
                return server;
            }
            public bool IsEmpty => servers.Count == 0;
            public int Count => servers.Count;
        }
        public class UtillizationCollection: IServerCollection
        {
            private SortedDictionary<int, Queue<Server>> servers = new SortedDictionary<int, Queue<Server>>();
            private int count = 0;
            public void AddServer(Server server)
            {
                int priority = server.TotalWorkingTime;
                Queue<Server> serverList = servers.TryGetValue(priority, out var existingList) ? existingList : servers[priority] = new Queue<Server>();
                serverList.Enqueue(server);
                count++;
            }

            public Server SelectServer()
            {
                KeyValuePair<int, Queue<Server>> p = servers.First();
                int workingTime = p.Key;
                Queue<Server> q = p.Value;
                Server server = q.Dequeue();
                if (q.Count == 0)
                    servers.Remove(workingTime);
                count--;
                return server;
            }
            public bool IsEmpty => count==0;
            public int Count => count;
        }

        public static void moveToIdle(ref SortedDictionary<int, Queue<Server>> sortedFinished, ref KeyValuePair<int, Queue<Server>> keyValue, ref IServerCollection idleServers)
        {
            Queue<Server> serverQ = keyValue.Value;
            while (serverQ.Count > 0)
            {
                Server server = serverQ.Dequeue();
                idleServers.AddServer(server);
            }
            sortedFinished.Remove(keyValue.Key);
        }
        public static void AssignCustomerToServer(ref SimulationCase customer, ref SortedDictionary<int, Queue<Server>> sortedFinishedTime, ref IServerCollection idleServers)
        {
            while (sortedFinishedTime.Count > 0)
            {
                KeyValuePair<int, Queue<Server>> TimeServers = sortedFinishedTime.First();
                if (TimeServers.Key > customer.ArrivalTime)
                {
                    if (idleServers.IsEmpty)
                        HelperClass.moveToIdle(ref sortedFinishedTime, ref TimeServers, ref idleServers);
                    break;
                }
                HelperClass.moveToIdle(ref sortedFinishedTime, ref TimeServers, ref idleServers);
            }
            Server server = idleServers.SelectServer();
            customer.AssignedServer = server;
            customer.ServiceTime = HelperClass.getTime(server.TimeDistribution, customer.RandomService);
            customer.StartTime = Math.Max(customer.ArrivalTime, customer.AssignedServer.FinishTime);
            customer.TimeInQueue = customer.StartTime - customer.ArrivalTime;
            customer.EndTime = customer.StartTime + customer.ServiceTime;
            server.WorkingRanges.Add(new KeyValuePair<int, int>(customer.StartTime, customer.EndTime));
            server.TotalWorkingTime += customer.ServiceTime;
            server.FinishTime = customer.EndTime;
            server.TotalNumberOfCustomers += 1;
            if (!sortedFinishedTime.ContainsKey(server.FinishTime))
                sortedFinishedTime[server.FinishTime] = new Queue<Server>();  
            sortedFinishedTime[server.FinishTime].Enqueue(server);
        }
    }
}
