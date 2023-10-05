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

namespace bainhanvien
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=DESKTOP-OUN7I3O;Initial Catalog=QLNVPBNew;Integrated Security=True");
            con.Open();
            string sql = "select * from NhanVien ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            listView1.Items.Clear();
            string gt;
            //comboBox1.Items.Add("Nam");
            //comboBox1.Items.Add("Nu");
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][3].ToString() == "False")
                {
                    gt = "Nu";
                }
                else
                {
                    gt = "Nam";
                }
                listView1.Items.Add(dt.Rows[i][0].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i][1].ToString());      
                listView1.Items[i].SubItems.Add(DateTime.Parse(dt.Rows[i][2].ToString()).ToShortDateString());
                listView1.Items[i].SubItems.Add(gt);
                listView1.Items[i].SubItems.Add(dt.Rows[i][4].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i][5].ToString());
            }
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "MaPhong";
            comboBox1.ValueMember = "MaNV";
        }
        public void hienthi()
        {
            string sql = "select * from NhanVien ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            listView1.Items.Clear();
            string gt;
            //comboBox1.Items.Add("Nam");
            //comboBox1.Items.Add("Nu");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][3].ToString() == "False")
                {
                    gt = "Nu";
                }
                else
                {
                    gt = "Nam";
                }
                listView1.Items.Add(dt.Rows[i][0].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i][1].ToString());
                listView1.Items[i].SubItems.Add(DateTime.Parse(dt.Rows[i][2].ToString()).ToShortDateString());
                listView1.Items[i].SubItems.Add(gt);
                listView1.Items[i].SubItems.Add(dt.Rows[i][4].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i][5].ToString());
            }
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "MaPhong";
            comboBox1.ValueMember = "MaPhong";
        
    }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            int vt = listView1.SelectedIndices[0];
            textBox1.Text = listView1.Items[vt].SubItems[0].Text;
            textBox2.Text = listView1.Items[vt].SubItems[1].Text;
            textBox3.Text = listView1.Items[vt].SubItems[4].Text;
            dateTimePicker1.Text = listView1.Items[vt].SubItems[2].Text;
            comboBox1.Text = listView1.Items[vt].SubItems[5].Text;
            if(listView1.Items[vt].SubItems[3].Text == "Nam")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gt;
            if (radioButton2.Checked)
                gt = "1";
            else
                gt = "0";
            string sql = "INSERT INTO NhanVien(MaNV,HoTen, NgaySinh,GioiTinh, DiaChi,MaPhong)VALUES('" + textBox1.Text + "' , '" + textBox2.Text + "' , '" + string.Format("{0:MM/dd/yyyy}", dateTimePicker1.Value) + "' , '" + gt + "','"+textBox3.Text+"' , '" + comboBox1.Text + "')";
            try
            {
                
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("da ghi thanh cong");
                hienthi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string gt;
            if (radioButton2.Checked)
                gt = "1";
            else
                gt = "0";
            string sql = "update NhanVien set  HoTen = '" + textBox2.Text + "' , NgaySinh = '" + string.Format("{0:MM/dd/yyyy}", dateTimePicker1.Value) + "' , GioiTinh = '" + gt + "' ,DiaChi = '" + textBox3.Text + "',MaPhong = '" + comboBox1.Text + "' where MaNV = '"+textBox1.Text+"' ";
            try
            {

                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("oke");
                hienthi();
            }
            catch
            {
                MessageBox.Show("Sai Roi");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "Delete from NhanVien where MaNV = '" + textBox1.Text + "'";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("oke");
                hienthi();
            }
            catch
            {
                MessageBox.Show("Si roi ");
            }
        }
    }
}
