using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueTesting;
using MultiQueueModels;

namespace MultiQueueSimulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SimulationSystem system = new SimulationSystem();
            Application.Run(new Form1());
/*            string result = TestingManager.Test(system, Constants.FileNames.TestCase1);
            MessageBox.Show(result);
*/        }
    }
}
