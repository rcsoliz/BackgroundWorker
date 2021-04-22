using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTwo
{
    class Program
    {
        static BackgroundWorker bg = new BackgroundWorker();

        static void Main(string[] args)
        {
            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.DoWork += new DoWorkEventHandler(bg_DoWork2);
            bg.DoWork += new DoWorkEventHandler(bg_DoWork3);

            bg.DoWork += new DoWorkEventHandler(bg_DoWork4);
            bg.DoWork += new DoWorkEventHandler(bg_DoWork5);

            bg.ProgressChanged += new ProgressChangedEventHandler(bg_ProgressChanged);
       //     bg.ProgressChanged += new ProgressChangedEventHandler(bg_ProgressChanged2);

            bg.WorkerReportsProgress = true;
            bg.RunWorkerAsync();

            Console.ReadKey();
        }

        static void bg_DoWork(object sender, EventArgs e)
        {
            // Console.WriteLine("--------------------- one bg_DoWork -------------------------");
            for (int bob = 0; bob< 5; bob++)
            {
                bg.ReportProgress(bob / 2);
            }
           // Console.WriteLine("--------------------- end bg_DoWork -------------------------");
        }

        static void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage);
        }

        static void bg_DoWork2(object sender, EventArgs e)
        {
            for (int bob1 = 110; bob1 < 115; bob1++)
            {
                bg.ReportProgress(bob1 + 10);
            }
        }

        static void bg_DoWork3(object sender, EventArgs e)
        {
            for (int bob1 = 50; bob1 < 55; bob1++)
            {
                bg.ReportProgress(bob1 - 2);
            }
        }

        static void bg_DoWork4(object sender, EventArgs e)
        {
            for (int bob1 = 200; bob1 < 205; bob1++)
            {
                bg.ReportProgress(bob1 +5);
            }
        }

        static void bg_DoWork5(object sender, EventArgs e)
        {
            for (int bob5 = 300; bob5 < 305; bob5++)
            {
                bg.ReportProgress(bob5);
            }
        }

        static void bg_ProgressChanged2(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Valor bob1: " + e.ProgressPercentage);
        }


    }
}
