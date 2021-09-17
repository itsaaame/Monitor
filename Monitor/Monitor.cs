using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Monitor
{
    class Monitor
    {
        //waits for a set lifespan, periodically checking (frequency) and terminates (kills)
        //processes which live longer than lifespan

        string process_name;
        double lifespan, freq_check;
        DateTime reference_time;
        public Monitor(string name, double span, double freq)
        {
            //simple class constructor to set process name to monitor,
            //allowed lifespan of the process and frequency of checks 
            //whether the process is still running or not

            process_name = name;
            lifespan = span;
            freq_check = freq;
        }
        public void Watch()
        {
            //public method to start monitoring

            //timers and events are used for the purpose of the periodic checks

            var aTimer = new System.Timers.Timer(1000 * 60 * freq_check);

            aTimer.Elapsed += new ElapsedEventHandler(DoWork);
            aTimer.Enabled = true;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private void DoWork(object source, ElapsedEventArgs e)
        {
            GetReferenceTime();
            TerminateProcess();
        }

        private void GetReferenceTime()
        {
            //If there is no such processes with name process_name
            //then programm will keep monitoring untill such process (or multiple) occures
            //setting the reference time to earliest start time of the porcess with name process_name after

            Process[] to_terminate = Process.GetProcessesByName(process_name);
            if (to_terminate.Length == 0)
            {
                reference_time = DateTime.Now;
            }
            else
            {
                reference_time = to_terminate[0].StartTime;
                foreach (Process proc in to_terminate)
                {
                    if (proc.StartTime < reference_time)
                    {
                        reference_time = proc.StartTime;
                    }
                }
            }

        }

        private void TerminateProcess()
        {
            //Terminate (kill) process (or group that shares process_name)
            //if it lives longer than allowed lifespan

            if ((DateTime.Now - reference_time).TotalMinutes >= lifespan)
            {
                Process[] to_terminate = Process.GetProcessesByName(process_name);
                foreach (Process proc in to_terminate)
                {
                    proc.Kill(true);
                    proc.Dispose();
                }
            }
        }

    }
}
