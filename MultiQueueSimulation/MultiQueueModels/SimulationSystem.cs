using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace MultiQueueModels
{
    public class SimulationSystem
    {
            ///////////// INPUTS ///////////// 
        public int NumberOfServers { get; set; }                                //done
        public int StoppingNumber { get; set; }                                 //done
        public List<Server> Servers { get; set; }                               //done
        public List<TimeDistribution> InterarrivalDistribution { get; set; }    //done
        public Enums.StoppingCriteria StoppingCriteria { get; set; }            //done
        public Enums.SelectionMethod SelectionMethod { get; set; }              //done

        // lists of reading from file
        public List<string> file { get; set; }                       // list of reading txt file
        public List<string> input_inform { get; set; }               // list of reading NumberOfServers,StoppingNumber,StoppingCriteria and SelectionMethod from the file
        public List<string> inter_arraival_table { get; set; }       // list of reading inter_arraival_table from the file
        public List<List<string>> servers_tables { get; set; }       // list of reading servers_tables from the file

        




        public SimulationSystem()
        {

            this.Servers = new List<Server>();
            this.InterarrivalDistribution = new List<TimeDistribution>();
            this.PerformanceMeasures = new PerformanceMeasures();
            this.SimulationTable = new List<SimulationCase>();

            this.file = new List<string>();
            this.input_inform = new List<string>();
            this.inter_arraival_table = new List<string>();
            this.servers_tables = new List<List<string>>();


            read_from_file();
            //set_time_distripution_table();
            set_time_distripution_table(InterarrivalDistribution, inter_arraival_table, inter_arraival_table.Count);
           

           

            
        }
        
        public void read_from_file()
        {

            FileStream fs = new FileStream(@"F:\lectcures and labs fourth year\1st term\Modeling and Simulation\assignments\task-1\Task 1\Template_Students\MultiQueueSimulation\MultiQueueSimulation\TestCases\TestCase2.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() != -1)
            {
                file.Add(sr.ReadLine());


            }
            sr.Close();
            fs.Close();
            for (int i = 1; i < 12; i = i + 3)
            {
                input_inform.Add(file[i]);

            }


            NumberOfServers = int.Parse(input_inform[0]);   //set NumberOfServers 

            StoppingNumber = int.Parse(input_inform[1]); //set StoppingNumber  from file

            int Stopping_Criteria = int.Parse(input_inform[2]); //set StoppingNumber  ''''

            int Selection_Method = int.Parse(input_inform[3]);  //''''''''''''''''''''''''

            if (Stopping_Criteria == 1)                        // set Stopping_Criteria 
            {

                StoppingCriteria = Enums.StoppingCriteria.NumberOfCustomers;
            }
            else

            {
                StoppingCriteria = Enums.StoppingCriteria.SimulationEndTime;

            }


            if (Selection_Method == 1)                         // set Selection_Method
            {
                SelectionMethod = Enums.SelectionMethod.HighestPriority;
            }
            else if (Selection_Method == 2)
            {
                SelectionMethod = Enums.SelectionMethod.Random;
            }
            else
            {
                SelectionMethod = Enums.SelectionMethod.LeastUtilization;
            }

            for (int i = 13; i < file.Count; i++)
            {

                if (file[i] == "")
                {
                    break;
                }
                inter_arraival_table.Add(file[i]);


            }





            int j = 13 + inter_arraival_table.Count + 2;

            for (int i = 0; i < NumberOfServers; i++)
            {

                servers_tables.Add(new List<string>());


                while (j < file.Count)
                {
                    if (file[j] == "")
                    {
                        break;
                    }
                    servers_tables[i].Add(file[j]);

                    j++;
                }

                j += 2;

            }
        }

     
      public void set_time_distripution_table(List<TimeDistribution> timedistripution, List<string> table, int table_size)
        {

            for (int i = 0; i < table.Count; i++)
            {
                timedistripution.Add(new TimeDistribution());

                string[] sperators = { ", " };
                int count1 = 2;
                string x1 = table[i];
                string[] x = x1.Split(sperators, count1, StringSplitOptions.RemoveEmptyEntries);

                timedistripution[i].Time = int.Parse(x[0]);
                timedistripution[i].Probability = decimal.Parse(x[1]);

            }
            calculate_CummProbability(timedistripution);
            calculate_range(timedistripution);
        }






      public void set_time_distripution_table_servers()
      {

          for (int j = 0; j < NumberOfServers; j++)
          {
              Servers.Add(new Server());
              Servers[j].ID = j + 1;

              set_time_distripution_table(Servers[j].TimeDistribution, servers_tables[j], servers_tables[j].Count);

          }
      }

        public void calculate_CummProbability(List<TimeDistribution> timedistripution)
        {


            for (int i = 0; i < timedistripution.Count; i++)
            {
                decimal sum = 0.0m;


                for (int j = 0; j <= i; j++)
                {
                    sum += timedistripution[j].Probability;
                }
                timedistripution[i].CummProbability = sum;
            }
        }



        public void calculate_range(List<TimeDistribution> timedistripution)
        {
            for (int i = 0; i < timedistripution.Count; i++)
            {
                if (timedistripution[i].Probability == 0)
                {
                    timedistripution[i].MinRange = 0;
                    int range = 0;
                    timedistripution[i].MaxRange = range;

                }
                else
                {
                    if (i == 0)
                    {
                        timedistripution[i].MinRange = 1;
                        Int16 range = Convert.ToInt16((timedistripution[i].CummProbability * 100));
                        timedistripution[i].MaxRange = range;

                    }
                    else
                    {
                        if (timedistripution[i - 1].Probability == 0)
                        {
                            timedistripution[i].MinRange = timedistripution[i - 2].MaxRange + 1;
                        }
                        else
                        {
                            timedistripution[i].MinRange = timedistripution[i - 1].MaxRange + 1;
                        }
                        Int16 range = Convert.ToInt16((timedistripution[i].CummProbability * 100));
                        timedistripution[i].MaxRange = range;
                    }
                }

            }
        }



        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }
        Random rd = new Random();
        Random rdServic = new Random();
        public int indexOfSimulationTable = 0;
        public static Queue<SimulationCase> queue = new Queue<SimulationCase>();
        public int totalSysServiceTime = 0;
        public static decimal totalWaitingTime;
        public int max = -9999;
        public static decimal NumOfCustomerWhoWaited=0;
        public void _SimulationCase()
        {
            int count=0;
            if (StoppingCriteria == Enums.StoppingCriteria.NumberOfCustomers)
                count = StoppingNumber;
            else                          // if Enums.StoppingCriteria.SimulationEndTime
            {
                count = InterarrivalDistribution.Count;
              

            }
            for (int i = 0; i <count ; i++)
            {
                SimulationCase sc = new SimulationCase();

                sc.CustomerNumber = i+1;
                
                sc.calculate_InterArrivale(InterarrivalDistribution,rd);

                if (i == 0)
                {
                    sc.ArrivalTime = 0;
                    sc.calculate_ServiceTime(Servers[0], rd);
                    sc.calculate_begin_end_service(0);
                }
                else
                {
                    //if (i == 1)                         
                    //    sc.calculate_ArrivalTime(0);
                    //else
                    sc.calculate_ArrivalTime(SimulationTable[i - 1].ArrivalTime);
                    //select server
                    if (Servers.Count == 1) 
                    {
                        sc.calculate_ServiceTime(Servers[0], rd);
                        sc.calculate_begin_end_service(SimulationTable[i-1].EndTime);
                    }
                    else
                    {
                        if (SelectionMethod == Enums.SelectionMethod.HighestPriority)
                        {
                            Server server = HighestPriorityMethod(i, sc.ArrivalTime);
                            sc.calculate_ServiceTime(server, rd);
                            if (indexOfSimulationTable == -1)
                            {
                                sc.calculate_begin_end_service(0); // server just start
                            }
                            else
                             sc.calculate_begin_end_service(server.FinishTime);
                        }
                        if (SelectionMethod == Enums.SelectionMethod.Random)
                        {
                            Server server = RandomMethod(i, sc.ArrivalTime);
                            sc.calculate_ServiceTime(server, rdServic);

                            sc.calculate_begin_end_service(SimulationTable[indexOfSimulationTable].EndTime);
                        }
                        if (SelectionMethod == Enums.SelectionMethod.LeastUtilization)
                        {
                            Server server = LeastUtilizationMethod(i, sc.ArrivalTime);
                            sc.calculate_ServiceTime(server, rdServic);
                            sc.calculate_begin_end_service(SimulationTable[indexOfSimulationTable].EndTime);
                        }
                    }
                }
                if (StoppingCriteria == Enums.StoppingCriteria.SimulationEndTime)
                {
                    if (sc.EndTime>= StoppingNumber)
                    {
                        break;
                    }
                }
                //whene  all servers are busy,serverId == 0 means put in queue;
                if (sc.AssignedServer.ID == 0|| sc.waitingTime>0 )
                {
                    queue.Enqueue(sc);
                }

                //calculate maxQueueLength
                max = queue.Count();
                if (max > PerformanceMeasures.leng)
                {
                    PerformanceMeasures.leng = max;
                }
                if(sc.EndTime>totalSysServiceTime)
                totalSysServiceTime =sc.EndTime;
                Servers[sc.AssignedServer.ID - 1].customers.Add(sc); //list of customers of server
                SimulationTable.Add(sc); 
            }

            int begin = 0, end = 0;
            decimal sum = 0;
            bool check = true;
            for(int i = 0; i < Servers.Count; i++)
            {
                for(int j = 0; j < SimulationTable.Count; j++)
                {
                    if (i+1 == SimulationTable[j].AssignedServer.ID)
                    {
                        if (check)
                        {
                            end = SimulationTable[j].EndTime;
                            check = false;
                        }
                        else
                        {
                            begin = SimulationTable[j].StartTime;
                            sum += begin - end;
                            end = SimulationTable[j].EndTime;
                        }
                    }
                }
                sum += totalSysServiceTime - end;
                Servers[i]._IdleProbability(sum,totalSysServiceTime);
                Servers[i]._AverageServiceTime(Servers[i].TotalWorkingTime,decimal.Parse (Servers[i].customers.Count.ToString()));
                Servers[i]._Utilization(Servers[i].TotalWorkingTime,totalSysServiceTime);
                begin = 0;end = 0; sum = 0;
            }
            //PerformanceMeasures.SetAverageWaitingTime(totalWaitingTime, StoppingNumber);
            //PerformanceMeasures.SetWaitingProbability(queue.Count,StoppingNumber);
        }

        public void set_performance()
        {
            PerformanceMeasures=new PerformanceMeasures();
            PerformanceMeasures.SetAverageWaitingTime(totalWaitingTime, SimulationTable.Count);
            PerformanceMeasures.SetWaitingProbability(NumOfCustomerWhoWaited, SimulationTable.Count);
            PerformanceMeasures.MaxQueueLength = PerformanceMeasures.leng;


        }
        public Server HighestPriorityMethod(int index, int ArrivalTime)
        {
            Server server = new Server();
            bool check= true;
            int k = 0;


                for (int j = 0; j < this.Servers.Count; j++)
                {
                   
                    check = false;
                    for ( k = SimulationTable.Count - 1; k >= 0; k--)
                    {
                        if (SimulationTable[k].AssignedServer.ID == j+1)
                        { 
                            check = true;
                            if (SimulationTable[k].EndTime <= ArrivalTime)
                            {
                                int serverid = this.Servers[j].ID;
                                return( this.Servers[j]);
                            }
                            break;
                        }  
                    }
                    if (check == false)
                    {
                      indexOfSimulationTable = -1;
                      return this.Servers[j];
                    }
                }
                int min = 9999;
                int idOfMiniFinishTime = 0;
                for (int i = 0; i < Servers.Count; i++)
                {
                  if (Servers[i].FinishTime < min)
                    {
                        min = Servers[i].FinishTime;
                        idOfMiniFinishTime = i;
                        indexOfSimulationTable = Servers[i].FinishTime;
                    }
                }
                min = 9999;
                return Servers[idOfMiniFinishTime]; 
            
            return server;
        }
        public Server RandomMethod(int index, int ArrivalTime)
        {
            Server server = new Server();
            bool check = true;
            
            if (index == 0)
            {
                server = this.Servers[0];
            }
            else 
            {
                for (int k = SimulationTable.Count - 1; k >= 0; k--)
                {
                    if (SimulationTable[k].EndTime <= ArrivalTime)
                    {
                        check = false;
                        indexOfSimulationTable = k;
                        server = SimulationTable[k].AssignedServer;
                        break;
                    }
                }
                if (check)
                {
                    int min = 9999;
                    int idOfMiniFinishTime = 0;
                    for (int i = 0; i < Servers.Count; i++)
                    {
                        if (Servers[i].FinishTime <= min)
                        {
                            min = Servers[i].FinishTime;
                            idOfMiniFinishTime = i;
                        }
                    }
                    min = 9999;
                    return Servers[idOfMiniFinishTime];
                }
            }
                return server;
        }
        public Server LeastUtilizationMethod(int index, int ArrivalTime)
        { 
            Server server = new Server();
            bool check = true;
            
            if (index == 0)
            {
                server = this.Servers[0];
            }
            else
            {
                List<Server> SortedList = Servers.OrderBy(o => o.TotalWorkingTime).ToList();
                for (int j = 0; j < SortedList.Count; j++)
                {
                    if (!check) //not false ==true ; 
                        return Servers[j - 1];
                    for (int k = SimulationTable.Count - 1; k >= 0; k--)
                    {
                        if (SimulationTable[k].AssignedServer.ID == j)
                        {
                            check = false;
                            if (SimulationTable[k].EndTime <= ArrivalTime)
                            {
                                indexOfSimulationTable = k;
                                return server = this.Servers[j];
                            }
                            break; 
                        }
                    }

                }
                if (check)
                {
                    int min = 9999;
                    int idOfMiniFinishTime = 0;
                    for (int i = 0; i < Servers.Count; i++)
                    {
                        if (Servers[i].FinishTime <= min)
                        {
                            min = Servers[i].FinishTime;
                            idOfMiniFinishTime = i;
                        }
                    }
                    min = 9999;
                    return Servers[idOfMiniFinishTime];
                }

            }
            return server;
        }
    }
  }

