using System;
using System.Collections;
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

        // 對應資料來源 DataTable 的欄位索引
        private int IDColIndex = 0;
        private int ParentIDColIndex = 1;
        private int TextColIndex = 2;







    

        public Form1()
        {
            InitializeComponent();

            dt = dt ?? sampleData();

            dataGridView1.DataSource = dt;
        }


   
      
        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.CheckBoxes = true;
            treeView1.ShowLines = true;

            DataTable dt = GetTreeData();
            TreeBuild(dt);

            treeView1.ExpandAll();
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.AfterCheck += treeView1_AfterCheck;

            FocusOnRoot();

        }

        private void TreeBuild(DataTable dt)
        {
            TreeRootExist(dt);
            CreateRootNode(this.treeView1, dt);

        }
        private void FocusOnRoot()
        {
            // TreeView 建置完成後，Focus 要出現在 Root 上
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
        }



        private void TreeRootExist(DataTable dt)
        {
            EnumerableRowCollection<DataRow> result = dt.AsEnumerable()
                                                    .Where(r => r.Field<string>(this.ParentIDColIndex) == null);
            if (result.Any() == false)
                throw new Exception("沒有 Root 節點資料，無法建立 TreeView");

            if (result.Count() > 1)
                throw new Exception("Root 節點超過 1 個，無法建立 TreeView");
        }


        // 把點選 TreeNode 資訊顯示在 txtNodeInfo 裡
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            // 該階層索引值
            sb.AppendLine($"Index：{e.Node.Index}");
            // Name 必須在 TreeView 中是唯一的
            sb.AppendLine($"Name：{e.Node.Name}");
            // TreeNode 文字
            sb.AppendLine($"Text：{e.Node.Text}");
            // 備註說明，為 Object
            sb.AppendLine($"Tag：{e.Node.Tag}");
            // 父 TreeNode
            string Parent = e.Node.Parent == null ? "Root" : e.Node.Parent.Text;
            sb.AppendLine($"Parent：{Parent}");
            // 子節點數量
            sb.AppendLine($"Count：{e.Node.GetNodeCount(false)}");
            // 完整路徑
            sb.AppendLine($"FullPath：{e.Node.FullPath}");
            // 該 TreeNode Level 值
            sb.AppendLine($"FullPath：{e.Node.Level}");

            textBox1.Text = sb.ToString();
        }


        // 根據點選 TreeNode CheckBox 狀態，來變成子節點
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode treeNode in e.Node.Nodes)
            {
                treeNode.Checked = e.Node.Checked;
            }
        }

        // 建立Root
        private DataRow GetTreeRoot(DataTable dt)
        {
            return dt.AsEnumerable()
                .Where(r => r.Field<string>(this.ParentIDColIndex) == null)
                .First();
        }       

        private IEnumerable<DataRow> GetTreeNodes(DataTable dt, TreeNode Node)
        {
            return dt.AsEnumerable()
               .Where(r => r.Field<string>(this.ParentIDColIndex) == Node.Name)
               .OrderBy(r => r.Field<string>(this.IDColIndex));
        }

        private void CreateRootNode(TreeView tree, DataTable dt)
        {
            // 建立Root
            DataRow Root = GetTreeRoot(dt);
            TreeNode Node = new TreeNode();
            // 建立子點
            Node.Text = Root.Field<string>(this.TextColIndex);
            Node.Name = Root.Field<string>(this.IDColIndex);
            tree.Nodes.Add(Node);


            // 建立TreeNode
            CreateNode(tree, dt, Node);

        }

        // 建立TreeNode
        private void CreateNode(TreeView tree, DataTable dt, TreeNode node)
        {
            IEnumerable<DataRow> Rows = GetTreeNodes(dt, node);

            TreeNode NewNode;

            foreach (var item in Rows)
            {
                NewNode = new TreeNode();
                NewNode.Name = item.Field<string>(this.IDColIndex);
                NewNode.Text = item.Field<string>(this.TextColIndex);
                node.Nodes.Add(NewNode);

                // 做一個回歸，如果還有其他子節點
                CreateNode(tree, dt, NewNode);
            }
        }


        // 資料以DataTable 型式匯入
        private DataTable GetTreeData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DepID", typeof(string));
            dt.Columns.Add("ParentID", typeof(string));
            dt.Columns.Add("DepName", typeof(string));


            dt.Rows.Add("01", null, "公司");
            dt.Rows.Add("02", "01", "財務");
            dt.Rows.Add("03", "01", "行政");
            dt.Rows.Add("04", "03", "採購");
            dt.Rows.Add("05", "03", "人資");
            dt.Rows.Add("06", "03", "業務");
            dt.Rows.Add("07", "03", "技術服務");
            dt.Rows.Add("08", "01", "開發");
            dt.Rows.Add("09", "08", "企劃");
            dt.Rows.Add("10", "08", "品管");
            dt.Rows.Add("11", "01", "廠務");
            dt.Rows.Add("12", "11", "生產技術");
            dt.Rows.Add("13", "11", "製程保全");
            dt.Rows.Add("14", "11", "生產管理");
            dt.Rows.Add("15", "11", "廠務室 A");
            dt.Rows.Add("16", "15", "倉庫 A");
            dt.Rows.Add("17", "15", "板噴生產課");
            dt.Rows.Add("18", "17", "端板成型組");
            dt.Rows.Add("19", "17", "箱體塗裝組");
            dt.Rows.Add("20", "15", "熱交生產課");
            dt.Rows.Add("21", "20", "管件組");
            dt.Rows.Add("22", "20", "沖片組");
            dt.Rows.Add("23", "20", "回管組");
            dt.Rows.Add("24", "20", "氣焊組");
            dt.Rows.Add("25", "15", "裝配生產課");
            dt.Rows.Add("26", "25", "裝配一組");
            dt.Rows.Add("27", "25", "裝配二組");
            dt.Rows.Add("28", "11", "廠務室 B");
            dt.Rows.Add("29", "28", "倉庫 B");
            dt.Rows.Add("30", "28", "資材課");
            dt.Rows.Add("31", "28", "板金生產課");
            dt.Rows.Add("32", "31", "CNC 組");

            return dt;
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //System.Text.StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.AppendFormat("{0} = {1} ", "ColumnIndex ", e.ColumnIndex);
            //stringBuilder.AppendLine();
            //stringBuilder.AppendFormat("{0} = {1} ", "ColumnIndex ", e.ColumnIndex);
            //stringBuilder.AppendLine();
            //MessageBox.Show(stringBuilder.ToString());


            //string value = e.ColumnIndex.ToString();

            
            // 點兩下得到當下的值
            label1.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

       


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

            //box1.Text = dt.Rows[i]["Column1"].ToString();
            //label1.Text = dataGridView1.Rows[1].ToString();


            var a = sender as DataGridView;
            


        }



        private void cvDesBind()
        {
            DBData= new List<Data>();
            DBData.Add(new Data() {ID = 001 , Name="Allen"});
            

        }

        private void OnEditRow(object sender, DataGridViewCellEventArgs e)
        {
            var dvg = sender as DataGridView;
            //Get the current row's data, if any
            var row = dvg.Rows[e.RowIndex];

            var moive = row.DataBoundItem;
            label1.Text = moive.ToString();

        }

    }





    public class  Data
    {
        public int ID { get; set; }
        public string Name { get; set;}
    }
}
