using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1.Server
{
    public interface ISQLServer
    {

        /// <summary>
        /// 新增資料庫
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="newDatabaseName"></param>
        /// <returns></returns>
        bool CreateDataBase(MySqlConnection connection, string newDatabaseName);
    }
}
