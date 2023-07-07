using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DOAN_QLKhachSan
{
    public partial class DangNhap : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-29HM56H\SQLEXPRESS02;Initial Catalog=QUANLY_KHACHSACN_LAN1;Integrated Security=True");
        public static string UserName = "";
        public DangNhap()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.CommandText = "DangNhap";
            //    cmd.Parameters.AddWithValue("@UserName", txt_mnv.Text);
            //    cmd.Parameters.AddWithValue("@Password", txt_mk.Text);
            //    cmd.Connection = conn;
            //    UserName = txt_mnv.Text;
            //    object kq = cmd.ExecuteScalar();
            //    int code = Convert.ToInt32(kq);
            //    if (code == 1)
            //    {
            //        MessageBox.Show("Chào mừng Quản lý " + UserName + " đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FormQuanLy f = new FormQuanLy();
            f.Show();
            //    }
            //    else if (code == 2)
            //    {
            //        MessageBox.Show("Chào mừng Nhân viên " + UserName + " đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //FormNhanVien t = new FormNhanVien();
            //t.Show();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Nhập sai mã nhân viên hoặc mật khẩu !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txt_mnv.Text = "";
            //        txt_mk.Text = "";
            //        txt_mnv.Focus();
            //    }
            //    conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_show.Checked)
            {
                txt_mk.PasswordChar = (char)0;
            }
            else
            {
                txt_mk.PasswordChar = '*';
            }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            txt_mk.PasswordChar = '*';
        }

    }
}
