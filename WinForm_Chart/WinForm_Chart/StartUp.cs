using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForm_Chart.Controller;
using WinFormsApp1.Server;

namespace WinForm_Chart
{
    public class StartUp
    {
        public Autofac.IContainer Initalize()
        {
            var builder = new ContainerBuilder();


            // Controller
            builder.RegisterType<SQLController>().As<ISQLController>();
            builder.RegisterType<MainController>().As<IMainController>();

            // Server
            builder.RegisterType<SQLServer>().As<ISQLServer>();



            var container = builder.Build();

            return container;
        }
    }
}
