using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm_Chart.View;


namespace WinForm_Chart.Controller
{
    public class MainController : IMainController
    {

        // 頁面
        private MainView mainView = null;
        private readonly ISQLController _controller = null; 
        
        public MainController(ISQLController controller)
        {
            _controller = controller ??     throw new ArgumentNullException(nameof(controller)); 

        }


        public void DoWork()
        {
            using (mainView = new MainView(_controller))
            {

               
                Application.Run(mainView);
            }
        }
    }
}
