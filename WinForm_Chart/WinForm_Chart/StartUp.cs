using Autofac;
using Autofac.Extras.NLog;
using Org.BouncyCastle.Asn1.X509;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinForm_Chart.Controller;
using WinFormsApp1.Server;

namespace WinForm_Chart
{
    public class StartUp
    {

        protected NLog.Logger _logger;
        public Autofac.IContainer Initalize()
        {
          
            var builder = new ContainerBuilder();

            ConfigSetup();
            builder.RegisterModule<NLogModule>();

            // Controller
            builder.RegisterType<SQLController>().As<ISQLController>();
            builder.RegisterType<MainController>().As<IMainController>();

            // Server
            builder.RegisterType<SQLServer>().As<ISQLServer>();
            

            var container = builder.Build();
            return container;
        }

        public  void ConfigSetup()
        {
            var config = new NLog.Config.LoggingConfiguration();
          
            string logFileName = string.Format(@"{0} {1}" , DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"), ".log");

            string currentDirectory = System.Environment.CurrentDirectory;
            var logDir = Path.Combine(currentDirectory, "LogFile");

            if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
            var logFilePath = Path.Combine(logDir, logFileName);

            var fileTarget = new NLog.Targets.FileTarget("f")
            {
                FileName = logFilePath,
                Layout = "${longdate} | ${level} | ${message} | ${exception}"
            };
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, fileTarget);




            var seqTarget = new NLog.Targets.Seq.SeqTarget()
            {
                ServerUrl = "http://localhost:5341", 
                ApiKey = "$Z2nHi5aziNULFd",
            };
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, seqTarget);
            NLog.LogManager.Configuration = config;
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }
    }
}
