using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm_Chart.View;
using NLog;
using Serilog;


namespace WinForm_Chart.Controller
{
    public class MainController : IMainController
    {

        // 頁面
        private MainView mainView = null;
        private readonly ISQLController _controller = null;
        // Nlog
        private NLog.ILogger _logger = null; 
        public MainController(NLog.ILogger logger ,ISQLController controller)
        {
            _logger = logger; 
            _controller = controller ??     throw new ArgumentNullException(nameof(controller));
        }


        public void DoWork()
        {
            using (mainView = new MainView(_controller))
            {
                _logger.Info("開啟");




                Application.Run(mainView);
            }
        }
    }
}
