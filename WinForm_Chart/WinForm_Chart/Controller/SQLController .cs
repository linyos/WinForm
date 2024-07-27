using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WinFormsApp1.Server;

namespace WinForm_Chart.Controller
{
    public class SQLController : ISQLController
    {

        private readonly ISQLServer _sqlServer = null;  
        public SQLController(ISQLServer server)
        {
           _sqlServer = server ?? throw new ArgumentNullException(nameof(server));
        }


        /// <summary>
        /// 連接資料庫
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public bool ConnectSQL(out MySqlConnection connection)
        {
            bool status = false;
            connection = null;
            try
            {

                string connStr = "server=127.0.0.1;user=root;port=3306;password=123456;";
                MySqlConnection mysqlController = new MySqlConnection(connStr);
                mysqlController.Open();
                connection = mysqlController;
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
               
            }
            return status;
        }

        public void CreateDataBases(MySqlConnection connection , string dbName)
        {
            if (_sqlServer.CreateDataBase(connection, dbName))
            {
                MessageBox.Show($"新增資料庫 {dbName}");
            }
            else
                MessageBox.Show($"新增資料庫 {dbName} 失敗");

        }

    }
}
