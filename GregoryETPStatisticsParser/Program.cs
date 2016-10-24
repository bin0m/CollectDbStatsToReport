using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GregoryETPStatisticsParser
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main(string[] args)
        {
            // To run as Console app change project output type ot Console app, don't forget to change it back after debug/test
            if (Environment.UserInteractive)
            {
                // When run as Console app (just to debug/test Service)
                ServiceGregory service1 = new ServiceGregory();
                service1.TestStartupAndStop(args);
            }
            else
            {
                // Default Service Entry Point
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] {new ServiceGregory()};
                ServiceBase.Run(ServicesToRun);
            }
          
        }
    }
}
