using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WinForm_Chart.Controller;
using System.Reflection;

namespace WinForm_Chart
{
    static class Program
    {
        private static Autofac.IContainer container = null;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            try
            {
                var startup = new StartUp();
                container = startup.Initalize();
                using (var scope = container.BeginLifetimeScope())
                {
                    var control = scope.Resolve<IMainController>();
                    control.DoWork();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                container?.Dispose();
            }
           
        }
    }
}
