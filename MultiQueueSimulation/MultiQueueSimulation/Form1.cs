using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueModels;
using MultiQueueTesting;

namespace MultiQueueSimulation
{
    public partial class Form1 : Form
    {


        SimulationSystem sm = new SimulationSystem();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

             
               sm.set_time_distripution_table_servers();
               sm._SimulationCase();
               sm.set_performance();

              
               dataGridView1.AutoGenerateColumns = true;
            
              
            for (int i =0; i < sm.SimulationTable.Count; i++)
            {
                if (dataGridView1.ColumnCount == 0)
                {
                    dataGridView1.Columns.Add("Customer no.", "Customer no.");
                    dataGridView1.Columns.Add("Random Digits ", "Random Digits for Arrival");
                    dataGridView1.Columns.Add("Time between Arrival", "Time between Arrival");
                    dataGridView1.Columns.Add("clock Time Of Arrival", "clock Time Of Arrival");
                    dataGridView1.Columns.Add("Random Digits For Service ", "Random Digits For Service");
                    for (int j = 0; j < sm.Servers.Count; j++)
                    {
                        dataGridView1.Columns.Add("Time Service Begins "+(j+1).ToString(), "Time Service Begins" + (j + 1).ToString());
                        dataGridView1.Columns.Add("service time" + (j + 1).ToString(), " service time " + (j + 1).ToString());
                        dataGridView1.Columns.Add("Time Service Ends" + (j + 1).ToString(), "Time Service Ends" + (j + 1).ToString());
                    }
                    dataGridView1.Columns.Add("Time in Queue", "Time in Queue");
                }
               String[] row = new String[6+(3*sm.Servers.Count)];
                row[0]=(sm.SimulationTable[i].CustomerNumber.ToString());
                if (i == 0)
                {
                    row[1] = "_";
                    row[2] = "_";
                }
                else
                {
                    row[1] = (sm.SimulationTable[i].RandomInterArrival.ToString());
                    row[2] = (sm.SimulationTable[i].InterArrival.ToString());
                }
                row[3]=(sm.SimulationTable[i].ArrivalTime.ToString());
                row[4]=(sm.SimulationTable[i].RandomService.ToString());
                int var = 5;
                for (int j = 0; j < sm.Servers.Count; j++)
                {

                    if (sm.SimulationTable[i].AssignedServer.ID == j+1)
                    {
                        
                        row[var]=(sm.SimulationTable[i].StartTime.ToString());
                        row[var+1]=(sm.SimulationTable[i].ServiceTime.ToString());
                        row[var+2]=(sm.SimulationTable[i].EndTime.ToString());
                        var += 3;
                    }
                    else
                    {
                        row[var]=("");
                        row[var+1]=("");
                        row[var+2]=("");
                        var += 3;
                    }
                }
                row[row.Length-1] = sm.SimulationTable[i].TimeInQueue.ToString();
                dataGridView1.Rows.Add(row);
            }

            
           
        

            string testingResult = TestingManager.Test(sm, Constants.FileNames.TestCase2);
            MessageBox.Show(testingResult);

            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(sm);
            f2.Show(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = sm.PerformanceMeasures.AverageWaitingTime.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = sm.PerformanceMeasures.MaxQueueLength.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            textBox3.Text = sm.PerformanceMeasures.WaitingProbability.ToString();

        
        }
        
    }
}
