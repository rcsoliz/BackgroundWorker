using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindApp
{
    public partial class Form1 : Form
    {
        public static List<int> data = new List<int>();         //our return data
        public static List<int> data_new = new List<int>();     //our return data
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int time = (int)e.Argument;
            List<int> temp = new List<int>();                   //our return data
            
            for (int i = 0; i <= 10; i++)
            {
                if (backgroundWorker1.CancellationPending)      //checks for cancel request
                {
                    e.Cancel = true;
                    break;                    
                }
                
                backgroundWorker1.ReportProgress(i * 10, i);    //reports ProgressPercentage AND Userstate
                Thread.Sleep(time);                             //used to simulate lengthy operations,
                
                temp.Add(i);                                    //in this case 10*300ms=3s(add using System.Threading)
            }
            
            e.Result = temp;//return temp
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) //it doesn't matter if the BG worker ends normally, or gets cancelled,
            {
                //both cases RunWorkerCompleted is invoked, so we need to check what has happened
                foreach (var item in data_new)
                {
                    textBox1.Text += item + "\r\n";
                }
                MessageBox.Show("You've cancelled the backgroundworker!.");
            }
  
            else
            {
                data.AddRange((List<int>)e.Result); //copies return value to public list we declared before 
                foreach (var item in data)
                {
                    textBox1.Text += item + "\r\n";
                }
                
                MessageBox.Show("Done");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            data.Clear();
            data_new.Clear();
            backgroundWorker1.RunWorkerAsync(300);              //300 gives a total of 3 seconds pause
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblProgress.Text = e.ProgressPercentage.ToString() + "%";
            
            data_new.Add((int)e.UserState); //casts the userstate into integer and adds it to a List
        }
    }
}
