using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Chart
{
    public partial class Form1 : Form
    {
        public DataTable dt;
        public List<Data> DBData;

        public Form1()
        {
            InitializeComponent();

            dt = dt ?? sampleData();

            dataGridView1.DataSource = dt;
        }


   
      
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Text.StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0} = {1} ", "ColumnIndex ", e.ColumnIndex);
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("{0} = {1} ", "ColumnIndex ", e.ColumnIndex);
            stringBuilder.AppendLine();
            MessageBox.Show(stringBuilder.ToString());
        }

        private DataTable sampleData()
        {

            using (DataTable table = new DataTable())
            {
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("Name", typeof(string));


                return table;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {


            //DataRow dr = dt.NewRow();
            //dr["ID"] = 001;
            //dr["Name"] = "Allen";
            //dt.Rows.Add(dr);


            cvDesBind();


            DataRow dr = dt.NewRow();
            dr["ID"] = DBData[0].ID;
            dr["Name"] = DBData[0].Name;

            dt.Rows.Add(dr);

        }




        private void cvDesBind()
        {
            DBData= new List<Data>();
            DBData.Add(new Data() {ID = 001 , Name="Allen"});
            

        }
    }


    


    public class  Data
    {
        public int ID { get; set; }
        public string Name { get; set;}
    }
}
