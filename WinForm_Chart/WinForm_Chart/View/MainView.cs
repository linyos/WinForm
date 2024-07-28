using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm_Chart.Controller;

namespace WinForm_Chart.View
{
    public partial class MainView : Form , IMainView
    {

        private readonly ISQLController _controller = null;
        
        public MainView(ISQLController controller)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));

            InitializeComponent();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
       
            NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
            Logger.Info("MainView_Load");

            if (_controller.ConnectSQL(out MySqlConnection connection))
            {
                labelX1.Text = "連結成功";
            }
           



        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
          
            if (_controller.ConnectSQL(out MySqlConnection connection))
            {
                _controller.CreateDataBases(connection, textBoxX1.Text);
            }

           

        }
    }
}
