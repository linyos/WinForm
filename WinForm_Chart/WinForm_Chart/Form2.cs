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
using Newtonsoft.Json.Linq;
using System.Text.Json;
using WinForm_Chart.Model;
using WinForm_Chart.service;


namespace WinForm_Chart
{
    public partial class Form2 : Form
    {

        public class RetrieveMultipleResponse
        {
            public List<Attribute> Attributes { get; set; }
            public string Name { get; set; }
            public string Id { get; set; }
        }
        public class Value
        {
            [JsonProperty("Value")]
            public string value { get; set; }
            public List<string> Values { get; set; }
        }
        public class Attribute
        {
            public string Key { get; set; }
            public Value Value { get; set; }
        }



        public DataTable dt;

        private readonly string path = @"C:\SEN\Coding\C#\WinForm\WinForm_Chart\WinForm_Chart\test.json";
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
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Type", typeof(int));
                return table;
            }
        }


       
        private void DBBind()
        {
            var source = Method.ReadJsonType<Data>(path);
            dt.Clear();
            foreach (var item in source)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = item.Id;
                dr["Firstname"] = item.Firstname;
                dr["Lastname"] = item.Lastname;
                dr["City"] = item.City;
                dr["Name"] = item.ParamData.Name;
                dr["Type"] = item.ParamData.Type;
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //label1.Text = "Rows: " + e.RowIndex.ToString() + " Col: " + e.ColumnIndex.ToString();
            //label1.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
          //label1.Text=  dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["ID"].FormattedValue.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["Firstname"].FormattedValue.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["Lastname"].FormattedValue.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells["City"].FormattedValue.ToString();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // CurrentCell.ColumnIndex : 現在的column
            // CurrentCell.RowIndex: 現在的Row
            //label1.Text = "Rows: " + dataGridView1.CurrentCell.ColumnIndex.ToString() +
            //    " Cols: " + dataGridView1.CurrentCell.RowIndex.ToString();

            //int rows = dataGridView1.CurrentCell.ColumnIndex;
            //int cells = dataGridView1.CurrentCell.RowIndex;
            ////dataGridView1.Rows[cells].Cells[rows].Value = textBox1.Text;

            //dataGridView1.CurrentRow.Cells["ID"].Value = textBox1.Text;
            //dataGridView1.CurrentRow.Cells["Firstname"].Value = textBox2.Text;

            //ReviseJson(int.Parse(textBox1.Text));



            int idx = dataGridView1.CurrentCell.RowIndex;
            Method.ReviseJson( idx , int.Parse(textBox1.Text));
         
            
           
        }


        
    }
}
