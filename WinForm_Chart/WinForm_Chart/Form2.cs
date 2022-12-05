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

     
        public DataTable dt;
        public DataTable dt1;

        public ParamData _ParamData; // 選到的資料
        private readonly string path = Directory.GetCurrentDirectory()+"\\test.json"; 


        public Form2()
        {
            InitializeComponent();
            //dt = dt ?? BuildDataTable();
            //dataGridView1.DataSource = dt;


            //dt1 = dt1 ?? BuildDB();
            //dataGridView2.DataSource = dt1;

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
                table.Columns.Add("Type", typeof(int)) ;
                return table;
            }
        }

        private DataTable BuildDB()
        {
            using (DataTable table = new DataTable())
            {
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Type", typeof(int));
                return table;
            }
        }

       
        private void DBBind()
        {
            var source = Method.ReadJsonType<Data>(path);

            var query = from c in source
                        where c != null
                        select new
                        {
                            ParamData = c.ParamData,
                            Id = c.Id
                        };
                        


            
            //foreach (var s in query)
            //{
            //    label1.Text = s.Id.ToString();
            //}
            
            //foreach (var s in source)
            //{
            //    if (s.ParamData.Name is null)
            //    {
            //        throw new ArgumentNullException(nameof(ParamData));
            //        //label1.Text = s.Id.ToString();
            //    }
            //}




            dt.Clear();
            foreach (var item in source)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = item.Id;
                dr["Firstname"] = item.Firstname;
                dr["Lastname"] = item.Lastname;
                dr["City"] = item.City;

                dr["Name"] = (from s in item.ParamData
                             select s.Name).First();

                dr["Type"] = (from s in item.ParamData
                              select s.Type).First();
             
                dt.Rows.Add(dr);
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DBBind();
            var source = Method.ReadJsonType<Data>(path);
            // 原始方法 : 使用dataRow 建立欄位
            //dt.Clear();
            //foreach (var item in source)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["ID"] = item.Id;
            //    dr["Firstname"] = item.Firstname;
            //    dr["Lastname"] = item.Lastname;
            //    dr["City"] = item.City;
            //    dr["Name"] = (from s in item.ParamData
            //                  select s.Name).First();
            //    dr["Type"] = (from s in item.ParamData
            //                  select s.Type).First();
            //    dt.Rows.Add(dr);
            //}


            // 新屬性
            var dataLists = new  List<DataList>();
            // 列舉所有資料屬性
            foreach (var item in source.ToList())
            {
                // 新屬性與所有屬性對接 (架構內容還是新屬性，但是所有資料屬性會輸入到指定屬性裡)
                var newData = new DataList()
                {
                    Name = (from s in item.ParamData
                            select s.Name).First(),
                    Type = (from s in item.ParamData
                            select s.Type).First()
                };

                //var newData = new DataList()
                //{
                //    tempData = item.ParamData
                //};

                // 再加入新創屬性列表中
                //dataLists.Add(newData);

                dataLists.Add(newData);
               
            }
            // 表格資料來源是新屬性
            //dataGridView1.DataSource = dataLists;
        
            dataGridView1.DataSource = source;
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
                //textBox1.Text = dataGridView1.CurrentRow.Cells["ID"].FormattedValue.ToString();
                //textBox2.Text = dataGridView1.CurrentRow.Cells["Firstname"].FormattedValue.ToString();
                //textBox3.Text = dataGridView1.CurrentRow.Cells["Lastname"].FormattedValue.ToString();
                //textBox4.Text = dataGridView1.CurrentRow.Cells["City"].FormattedValue.ToString();

                textBox5.Text = dataGridView1.CurrentRow.Cells["Name"].FormattedValue.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells["Type"].FormattedValue.ToString();



                label1.Text = dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString();

               

                
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
