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


namespace WinForm_Chart
{
    public partial class Form2 : Form
    {

        //private readonly string _path = $"C:\Users\user\Desktop\21G1606-AC5293_20220127_1321.json";

        public class Data
        {

            public string collapse_key { get; set; }

            public int time_to_live { get; set; }

            public bool delay_while_idle { get; set; }
            
            public data data1 { get; set; }

            List<int > registration_ids { get; set; }

        }
        public class data
        {
            public string score { get; set; }
            public DateTime time { get; set; }
        }
       



        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                //string jsonfromFile = "";
                //using (var reader = new StreamReader(_path))
                //{
                //    jsonfromFile = reader.ReadToEnd();
                //}


                string path = @"C:\Users\user\Desktop\test.json";

                string text = File.ReadAllText(path);
                var data = System.Text.Json.JsonSerializer.Deserialize<Data>(text);
                //richTextBox1.Text += data.collapse_key+ "\n";
                //richTextBox1.Text += data.time_to_live.ToString() + "\n";
                //richTextBox1.Text += data.delay_while_idle.ToString() +"\n";
                //richTextBox1.Text += data.data1[0].ToString(); ;


                //var data = System.Text.Json.JsonSerializer.Deserialize<Data>(text);
                ////richTextBox1.Text = jsonfromFile;
                //var data = JsonConvert.DeserializeObject<Data>(jsonfromFile);

                //richTextBox1.Text = data.operation_col.ToString();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
