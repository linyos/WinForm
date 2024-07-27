using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WinFormsApp1.Server
{
    public class SQLServer : ISQLServer
    {


        /// <summary>
        /// 新增資料庫
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CreateDataBase(MySqlConnection connection ,  string newDatabaseName)
        {
            bool status = false;
            try
            {
                string createDatabaseQuery = $"CREATE DATABASE {newDatabaseName}";

                using (var cmd = new MySqlCommand(createDatabaseQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            
            return status;
        }
    }
}
