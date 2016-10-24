using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace GregoryETPStatisticsParser
{
    public partial class ServiceGregory : ServiceBase
    {
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };

        System.Timers.Timer MainActionTimer;
        DateTime ScheduleTimeToStart = DateTime.Now.AddSeconds(10);
        const int mainIntervalInSeconds = 30 ;
        const string pathLogFile = @"ParserLog.txt";

        public ServiceGregory()
        {
            InitializeComponent();
            MainActionTimer = new System.Timers.Timer();          
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            File.AppendAllText(pathLogFile, "Service started " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "\n");

            // For first time, set amount of seconds between current time and schedule time
            MainActionTimer.Enabled = true;

            double nextRunInterval = ScheduleTimeToStart.Subtract(DateTime.Now).TotalSeconds;

            //if scheduled time in future
            if (nextRunInterval > 0)
            {
                MainActionTimer.Interval = nextRunInterval * 1000;
            }
            else
            {
                // if scheduled time is in past, then start immidiatley 
                MainActionTimer.Interval = 1;
            }
            
            MainActionTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            File.AppendAllText(pathLogFile, "Service timer action started " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "\n");

            // 1. Process Schedule Task
            // ----------------------------------
            // Add code to Process your task here
            // ----------------------------------


            // 2. If tick for the first time, reset next run to Const Interval
            if (MainActionTimer.Interval != mainIntervalInSeconds * 1000)
            {
                MainActionTimer.Interval = mainIntervalInSeconds * 1000;
            }
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);


        protected override void OnStop()
        {
            // Update the service state to Stop
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            File.AppendAllText(pathLogFile, "Service stopped " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "\n");
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
