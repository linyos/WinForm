using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Chart.Controller
{
    public interface ISQLController
    {
        /// <summary>
        /// 連接資料庫
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        bool ConnectSQL(out MySqlConnection connection);


        /// <summary>
        /// 新增資料庫
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        void CreateDataBases(MySqlConnection connection , string dbName);
    }
}
