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
    public partial class ThanhToan : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-29HM56H\SQLEXPRESS02;Initial Catalog=QUANLY_KHACHSACN_LAN1;Integrated Security=True");
        DataSet ds_HD;
        SqlDataAdapter da_HD;
        public int flag = 0;
        public ThanhToan()
        {
            InitializeComponent();
        }
        public void load_database_gridview()
        {
            ds_HD = new DataSet();
            string strSelect = "select * from HOADON";
            da_HD = new SqlDataAdapter(strSelect, conn);
            da_HD.Fill(ds_HD, "HOADON");
            DataGridView1.DataSource = ds_HD.Tables["HOADON"];
        }
        public void Databingdings(DataTable pDT)
        {
            txt_mahd.DataBindings.Clear();
            txt_manv.DataBindings.Clear();
            txt_ngaytt.DataBindings.Clear();
            cbo_mapt.DataBindings.Clear();
            txt_songaythue.DataBindings.Clear();
            cbo_tinhtrang.DataBindings.Clear();
            cbo_sddv.DataBindings.Clear();
            txt_tongtien.DataBindings.Clear();

            txt_mahd.DataBindings.Add("Text", pDT, "MA_HD");
            txt_manv.DataBindings.Add("Text", pDT, "TENDANGNHAP");
            txt_ngaytt.DataBindings.Add("Text", pDT, "NGAYTHANHTOAN_HD");
            cbo_mapt.DataBindings.Add("Text", pDT, "MA_PTP");
            txt_songaythue.DataBindings.Add("Text", pDT, "SONGAYTHUE");
            cbo_tinhtrang.DataBindings.Add("Text", pDT, "TINHTRANG_HD");
            cbo_sddv.DataBindings.Add("Text", pDT, "MA_SD");
            txt_tongtien.DataBindings.Add("Text", pDT, "TONGTIEN_HD");
        }
        private void ThanhToan_Load(object sender, EventArgs e)
        {
            load_database_gridview();
            DataGridView1.ReadOnly = true;
            DataGridView1.AllowUserToAddRows = false;
            foreach (Control item in DataGridView1.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() ==
                typeof(ComboBox) || item.GetType() == typeof(MaskedTextBox))

                    item.Enabled = false;

            }
            btn_xoa.Enabled = btn_luu.Enabled = false;
            Databingdings(ds_HD.Tables["HOADON"]);
        }

        private void Thêm_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_HD);
                da_HD.Update(ds_HD, "HOADON");
                Databingdings(ds_HD.Tables["HOADON"]);
                MessageBox.Show("Thành công");
                btn_luu.Enabled = false;
                btn_them.Enabled = true;
                flag = 0;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
