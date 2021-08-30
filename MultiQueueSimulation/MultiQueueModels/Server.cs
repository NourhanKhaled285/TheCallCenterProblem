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
            this.ID = 0;
        }

        public int ID { get; set; }
        public decimal IdleProbability { get; set; }
        public decimal AverageServiceTime { get; set; } 
        public decimal Utilization { get; set; }
        public List<SimulationCase> customers=new List<SimulationCase>();
        public List<TimeDistribution> TimeDistribution;

        //optional if needed use them
        public int FinishTime { get; set; }
        public int TotalWorkingTime { get; set; }

        public void _IdleProbability(decimal totalIdelTime , int totalSysServiceTime)
        {
            IdleProbability = totalIdelTime / totalSysServiceTime;
        }
        public void _AverageServiceTime(int totalServiceTime,decimal numOfCustomer)
        {
            if (numOfCustomer == 0)
            {
                AverageServiceTime = 0;
            }
            else
            AverageServiceTime = totalServiceTime / numOfCustomer;
        }
        public void _Utilization(int totalServiceTime, decimal totalSysServiceTime)
        {
            Utilization = totalServiceTime / totalSysServiceTime;
        }
    }
}
