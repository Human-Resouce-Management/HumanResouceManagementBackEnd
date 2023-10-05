using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace baicolinh2
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-OUN7I3O;Initial Catalog=QLNV28_09;Integrated Security=True");
            string sql = "select * from PhongBan";
            SqlDataAdapter da = new SqlDataAdapter(sql,conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string sql2 = "select * from NhanVien";
            SqlDataAdapter da2 = new SqlDataAdapter(sql2 ,conn);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode nodecha = new TreeNode();
                nodecha.Tag = dt.Rows[i][0].ToString();
                nodecha.Text = dt.Rows[i][1].ToString();
                treeView1.Nodes.Add(nodecha);
                for(int j = 0;j< dt2.Rows.Count; j++)
                {
                    if (dt.Rows[i]["MaPhong"].ToString() == dt2.Rows[j]["MaPhong"].ToString())
                    {
                        TreeNode nodecon = new TreeNode();
                        nodecon.Tag = dt2.Rows[i][0].ToString();
                        nodecon.Text = dt2.Rows[i][1].ToString();
                       nodecha.Nodes.Add(nodecon);
                    }
                }
            }


        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treeView1.SelectedNode.Parent == null)
            {
                string mapb = treeView1.SelectedNode.Tag.ToString();
              
                string sql = "select *from NhanVien where MaPhong = '" + mapb + "' ";
                SqlDataAdapter da = new SqlDataAdapter( sql,conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt2 = new DataTable();
                string manv = treeView1.SelectedNode.Tag.ToString();
                string sql2 = "select *from NhanVien where MaNV = '" + manv + "' ";
                SqlDataAdapter da2 = new SqlDataAdapter( sql2,conn);
                da2.Fill(dt2 );
                textBox1.Text = dt2.Rows[0][0].ToString();
                textBox2.Text = dt2.Rows[0][1].ToString();
                dateTimePicker1.Text = dt2.Rows[0][2].ToString();
                //if (dt2.Rows[0][3].ToString() == "True")
                //{
                //    radioButton1.Checked = true; radioButton2.Checked = false;
                //}
                //else
                //{
                //    radioButton2.Checked = true; radioButton1.Checked = false; 
                //}
                textBox3.Text = dt2.Rows[0][3].ToString();
                comboBox1.Text = dt2.Rows[0][4].ToString();
            }
        }
    }
}
