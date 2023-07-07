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

namespace DOAN_QLKhachSan
{
    public partial class KhachHang : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-29HM56H\SQLEXPRESS02;Initial Catalog=QUANLY_KHACHSACN_LAN1;Integrated Security=True");
        DataSet ds_QLKH = new DataSet();
        SqlDataAdapter da_KhachHang;
        public int flag = 0;

        public KhachHang()
        {
            InitializeComponent();
            string strSelect = "select * from KHACHHANG";
            da_KhachHang = new SqlDataAdapter(strSelect, conn);
        }
        
        public void load_database_gridview()
        {
            da_KhachHang.Fill(ds_QLKH, "KHACHHANG");
            DataGridView1.DataSource = ds_QLKH.Tables["KHACHHANG"];
        }
        public void load_cbo_gioitinh()
        {
           cbo_gioitinh.Items.Add("Nữ");
           cbo_gioitinh.Items.Add("Nam");
        }
        public void Databingdings(DataTable pDT)
        {
            txt_mk.DataBindings.Clear();
            txt_tenK.DataBindings.Clear();
            txt_sdt.DataBindings.Clear();
            txt_cccd.DataBindings.Clear();
            txt_diachi.DataBindings.Clear();
            cbo_gioitinh.DataBindings.Clear();

            txt_mk.DataBindings.Add("Text", pDT, "MA_KH");
            txt_tenK.DataBindings.Add("Text", pDT, "HOTEN_KH");
            txt_sdt.DataBindings.Add("Text", pDT, "SDT_KH");
            txt_cccd.DataBindings.Add("Text", pDT, "CCCD_KH");
            txt_diachi.DataBindings.Add("Text", pDT, "DIACHI_KH");
            cbo_gioitinh.DataBindings.Add("Text", pDT, "GIOITINH");
        }
        private void KhachHang_Load(object sender, EventArgs e)
        {
            load_database_gridview();
            load_cbo_gioitinh();
            DataGridView1.ReadOnly = true;
            DataGridView1.AllowUserToAddRows = false;
            foreach (Control item in DataGridView1.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() ==
                typeof(ComboBox) || item.GetType() == typeof(MaskedTextBox))

                    item.Enabled = false;

            }
            btn_xoa.Enabled = btn_luu.Enabled = false;
            Databingdings(ds_QLKH.Tables["KHACHHANG"]);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            btn_luu.Enabled = true;
            btn_them.Enabled = false;
            DataGridView1.ReadOnly = false;
            DataGridView1.AllowUserToAddRows = true;
            for (int i = 0; i < DataGridView1.Rows.Count - 1; i++)
            {
                DataGridView1.Rows[i].ReadOnly = true;
            }
            DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows.Count - 1;
            flag = 1;
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_KhachHang);
                da_KhachHang.Update(ds_QLKH, "KHACHHANG");
                Databingdings(ds_QLKH.Tables["KHACHHANG"]);
                MessageBox.Show("Thành công");
                btn_luu.Enabled = false;
                btn_them.Enabled = true;
                flag = 0;
            }
        }

        //private void btn_in_Click(object sender, EventArgs e)
        //{
        //    InKhachHang f = new InKhachHang();
        //    f.Show();
        //}
    }
}