using MultiQueueModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiQueueSimulation
{
    public partial class Form2 : Form
    {
        SimulationSystem sm=new SimulationSystem();
        int countServer = 0;
        public Form2(SimulationSystem sm)
        {
            this.sm = sm;
           
            InitializeComponent();
                 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.chart(countServer);
                
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text= sm.Servers[countServer].IdleProbability.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = sm.Servers[countServer].AverageServiceTime.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Text = sm.Servers[countServer].Utilization.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          countServer++;
          if (countServer < sm.Servers.Count) {
              textBox1.Text = "";
              textBox2.Text = "";
              textBox3.Text = "";
              this.chart(countServer);
            }
          else
            MessageBox.Show("No Servers More !");

    }
        public void chart(int countServer)
        {
            label4.Text = "server number " + (countServer + 1).ToString();
            int numOfcustomers = sm.Servers[countServer].customers.Count;
            int finishTimeOfSys = sm.SimulationTable[sm.SimulationTable.Count - 1].EndTime;
            for (int i = 0; i < numOfcustomers; i++)
            {
                decimal start = sm.SimulationTable[i].StartTime;
                decimal end = sm.SimulationTable[i].EndTime;
                if (i == 0 && start > 0)
                {
                    for (decimal k = 0; k < start; k++)
                        chart1.Series["busy"].Points.AddXY(k, 0);
                }
                else if (i == numOfcustomers - 1 && end < finishTimeOfSys)
                {
                    for (decimal k = end + 1; k <= finishTimeOfSys; k++)
                        chart1.Series["busy"].Points.AddXY(k, 0);
                }
                else
                {
                    for (decimal k = start; k <= end; k++)
                        chart1.Series["busy"].Points.AddXY(k, 1);
                }
            }


            /*         List<decimal> start = new List<decimal>();

            List<decimal> end = new List<decimal>();
            label4.Text = "server number " + (countServer + 1).ToString();
            int numOfcustomers = sm.Servers[countServer].customers.Count;
            decimal mins = 10 ^ 9;
            decimal maxs = 10 ^ -9;
             decimal mine = 10 ^ 9;
            decimal maxe = 10 ^ -9;
           
            int finishTimeOfSys = sm.SimulationTable[sm.SimulationTable.Count - 1].EndTime;
            for (int i = 0; i < numOfcustomers; i++)
            {
               mins= Math.Min(mins, sm.SimulationTable[i].StartTime);
               maxs= Math.Max(maxs, sm.SimulationTable[i].StartTime);

               mine = Math.Min(mine, sm.SimulationTable[i].EndTime);
               maxe = Math.Max(maxe, sm.SimulationTable[i].EndTime);
            }
             for (int i = 0; i < numOfcustomers; i++)
            {
                    start.Add((sm.SimulationTable[i].StartTime-mins)/(maxs-mins));
                    end.Add( (sm.SimulationTable[i].EndTime-mine)/(maxe-mine));
            }
             for (int i = 0; i < numOfcustomers; i++)
            {
           
                if (i == 0 && start[i] > 0)
                {
                    for (decimal k = 0; k < start[i]; k++)
                        chart1.Series["busy"].Points.AddXY(k, 0);
                }
                else if (i == numOfcustomers - 1 && end[i] < finishTimeOfSys)
                {
                    for (decimal k = end[i] + 1; k <= finishTimeOfSys; k++)
                        chart1.Series["busy"].Points.AddXY(k, 0);
                }
                 else
                {
                    for (decimal k = start[i]; k <= end[i]; k++)
                        chart1.Series["busy"].Points.AddXY(k, 1);
                }
            }



        }*/


        }
    }
}
