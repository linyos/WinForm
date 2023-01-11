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
            var dataLists = new List<Data>();
            foreach (var item in source)
            {
                var dr = new Data()
                {
                    Id = item.Id,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    City = item.City,
                    ParamData = item.ParamData,
                };
                dataLists.Add(dr);
            
            }

            dataGridView1.DataSource = dataLists;
            // 新屬性
            //var dataLists = new  List<DataList>();
            //// 列舉所有資料屬性
            //foreach (var item in source.ToList())
            //{
            //    //// 新屬性與所有屬性對接 (架構內容還是新屬性，但是所有資料屬性會輸入到指定屬性裡)
            //    var newData = new DataList()
            //    {
            //        Name = (from s in item.ParamData
            //                select s.Name).First(),
            //        Type = (from s in item.ParamData
            //                select s.Type).First(),
            //        tempData = item.ParamData
            //    };
            //    ////再加入新創屬性列表中
            //    dataLists.Add(newData);
            //}
            // 表格資料來源是新屬性
            //dataGridView1.DataSource = dataLists;
            //dataGridView1.DataSource = source;
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

                ////textBox5.Text = dataGridView1.CurrentRow.Cells["Name"].FormattedValue.ToString();
                ////textBox6.Text = dataGridView1.CurrentRow.Cells["Type"].FormattedValue.ToString();



                //label1.Text = dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString();

               

                
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

        private void button3_Click(object sender, EventArgs e)
        {
            string json = File.ReadAllText(path);


            // Method 1 : Descialize
            // 反序列為物件
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            // 改變值
            jsonObj[0]["Bots"][0]["Password"] = "New0000";
            // 序列為物件
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            // 寫入檔案
            File.WriteAllText(path, output);

            string json1 = File.ReadAllText(path);
            // 新增物件
            //// 轉換成 JArray
            JArray jArray = JsonConvert.DeserializeObject<JArray>(json1);
            var itemToAdd = new JObject();
            // JObject 加欄位
            // 如果屬性是陣列 ， 要轉換
            var AStr = new[] { "A" };
            itemToAdd.Add("Admins", JArray.FromObject(AStr));
            //  JObject 加到 JArray
            jArray.Add(itemToAdd);
            var jsonToOut = JsonConvert.SerializeObject(jArray, Formatting.Indented);
            File.WriteAllText(path, jsonToOut);


            // 也是寫入的方法，但不適用
            //var rootObj = new RootObject();
            //JsonConvert.PopulateObject(json, rootObj);
            //rootObj.Bots[0].Password = "New Password";

            //using (StreamWriter file = File.CreateText(path))
            //{
            //    Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            //    jsonSerializer.Serialize(file, rootObj);
            //}


        }
        private void button4_Click(object sender, EventArgs e)
        {
        
           

            string json = File.ReadAllText(path);
            // 轉換成 JArray
            JArray jArray = JsonConvert.DeserializeObject<JArray>(json);

            //var array = JArray.Parse(json);
            // 新建 
            var itemToAdd = new JObject();
            // JObject 加欄位
            itemToAdd.Add("Name" ,"B");


            // 如果屬性是陣列 ， 要轉換
            var AStr = new[] { "A", "B", "C" };
            itemToAdd.Add("A",JArray.FromObject(AStr));

            //  JObject 加到 JArray
            jArray.Add(itemToAdd);
            
            var jsonToOut = JsonConvert.SerializeObject(jArray, Formatting.Indented);
            File.WriteAllText(path, jsonToOut);
        }

        public class Bot
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string DisplayName { get; set; }
            public string Backpack { get; set; }
            public string ChatResponse { get; set; }
            public string logFile { get; set; }
            public string BotControlClass { get; set; }
            public int MaximumTradeTime { get; set; }
            public int MaximumActionGap { get; set; }
            public string DisplayNamePrefix { get; set; }
            public int TradePollingInterval { get; set; }
            public string LogLevel { get; set; }
            public string AutoStart { get; set; }
        }



        public class RootObject
        {
            public List<string> Admins { get; set; }
            public string ApiKey { get; set; }
            public string mainLog { get; set; }
            public string UseSeparateProcesses { get; set; }
            public string AutoStartAllBots { get; set; }
            public List<Bot> Bots { get; set; }
        }




        public class  KbmObj
        {
            public string Name { get; set; }
            public List<string> A { get; set; } 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.ShowDialog();

            this.Close();
        }
    }
}
