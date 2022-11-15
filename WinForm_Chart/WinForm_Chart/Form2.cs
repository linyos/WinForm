using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using WinForm_Chart.Model;
using WinForm_Chart.service;


namespace WinForm_Chart
{
    public partial class Form2 : Form
    {
        public DataTable dt;

        public Form2()
        {
            InitializeComponent();
            dt = dt ?? BuildDataTable();
            dataGridView1.DataSource = dt;
        }
        
        private DataTable BuildDataTable()
        {
            using (DataTable table = new DataTable())
            {
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("Firstname", typeof(string));
                table.Columns.Add("Lastname", typeof(string));
                table.Columns.Add("City", typeof(string));
                return table;
            }
        }


       
        private void DBBind()
        {
            //List<Data> source = new List<Data>();
            string path = @"C:\SEN\Coding\C#\WinForm\WinForm_Chart\WinForm_Chart\test.json";
            //using (StreamReader reader = new StreamReader(path))
            //{
            //    string jsonText = reader.ReadToEnd();
            //    source = System.Text.Json.JsonSerializer.Deserialize<List<Data>>(jsonText);
            //}

           
            var source =Method.ReadJson(path);


            foreach (var item in source)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = item.Id;
                dr["Firstname"] = item.Firstname;
                dr["Lastname"] = item.Lastname;
                dr["City"] = item.City;
                dt.Rows.Add(dr);
            }
        
        
        }


        private void Form2_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBBind();
        }
    }
}
