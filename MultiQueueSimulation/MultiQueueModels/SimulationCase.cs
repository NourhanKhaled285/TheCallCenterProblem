using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class SimulationCase
    {
        public SimulationCase()
        {
            this.AssignedServer = new Server();
            
        }

        public int CustomerNumber { get; set; }
        public int RandomInterArrival { get; set; }
        public int InterArrival { get; set; }
        public int ArrivalTime { get; set; }
        public int RandomService { get; set; }
        public int ServiceTime { get; set; }
        public Server AssignedServer { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int waitingTime { get; set; }

        public int TimeInQueue { get; set; }
       // public SimulationSystem sm = new SimulationSystem();
        public void calculate_InterArrivale(List<TimeDistribution> TD,Random rd)
        { 
            RandomInterArrival = rd.Next(1, 100);
            for(int i = 0; i < TD.Count; i++)
            {
                if (RandomInterArrival <= TD[i].MaxRange && RandomInterArrival>=TD[i].MinRange)
                {
                    InterArrival = TD[i].Time;
                }
            }

        }
        public void calculate_ArrivalTime(int T_previous)
        {
            ArrivalTime = InterArrival + T_previous;
            for (int k = 0; k < SimulationSystem.queue.Count; k++)
            {
                if (ArrivalTime >= SimulationSystem.queue.ElementAt(k).StartTime)
                {
                    SimulationSystem.queue.Dequeue();
                    k=0;
                }
            }

        } 
        public void calculate_ServiceTime(Server s,Random rd)
        {
            this.AssignedServer = s;
            int serverid = AssignedServer.ID;
            this.RandomService = rd.Next(1, 100);
            for (int i = 0; i <AssignedServer.TimeDistribution.Count; i++)
            {
                if (this.RandomService <= AssignedServer.TimeDistribution[i].MaxRange && this.RandomService >= AssignedServer.TimeDistribution[i].MinRange)
                {
                    ServiceTime = AssignedServer.TimeDistribution[i].Time;
                    AssignedServer.TotalWorkingTime += ServiceTime;
                    break;
                }
            }
        }
     public void calculate_begin_end_service(int finishTime)
        {   
            if (finishTime <= ArrivalTime)
            {
               // Console.WriteLine("if");
                this.StartTime = ArrivalTime;
                this.TimeInQueue = 0;
            }
            else
            {
               
                    //Console.WriteLine("else");
                this.waitingTime =Math.Abs(finishTime - ArrivalTime);
                SimulationSystem.totalWaitingTime += waitingTime;
                SimulationSystem.NumOfCustomerWhoWaited++;
                this.StartTime = finishTime;
                this.TimeInQueue = this.waitingTime;   
            }
            this.EndTime = this.StartTime + this.ServiceTime;
            AssignedServer.FinishTime = this.EndTime;
        }

    }
   
}
