using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class PerformanceMeasures
    {
        public decimal AverageWaitingTime { get; set; }
        public  int MaxQueueLength { get; set; }
        public static int leng { get; set; }
        public decimal WaitingProbability { get; set; }

       
        public PerformanceMeasures()
        {
          
        }
        public void SetAverageWaitingTime(decimal TotalTimeCWaited, int NumOfCustomer)
        {
            AverageWaitingTime = TotalTimeCWaited / NumOfCustomer;
            
        }
   
        public void SetWaitingProbability(decimal NumOfCustomerWhoWaited, int NumOfCustomer)
        {
            WaitingProbability = NumOfCustomerWhoWaited / NumOfCustomer;
            
        }

    }
}
