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
    public partial class QLNhanVien : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-29HM56H\SQLEXPRESS02;Initial Catalog=QUANLY_KHACHSACN_LAN1;Integrated Security=True");
        DataSet ds_QLNV;
        SqlDataAdapter da_NhanVien;
        public int flag = 0;
        public QLNhanVien()
        {
            InitializeComponent();
            
        }

        public void load_database_gridview()
        {
            ds_QLNV = new DataSet();
            string strSelect = "select * from NHANVIEN";
            da_NhanVien = new SqlDataAdapter(strSelect, conn);
            da_NhanVien.Fill(ds_QLNV, "NHANVIEN");
            DataGridView1.DataSource = ds_QLNV.Tables["NHANVIEN"];
        }
        public void Databingdings(DataTable pDT)
        {
            txt_ma.DataBindings.Clear();
            txt_ten.DataBindings.Clear();
            txt_sdt.DataBindings.Clear();
            txt_cccd.DataBindings.Clear();
            txt_diachi.DataBindings.Clear();
            txt_mk.DataBindings.Clear();
            txt_chucvu.DataBindings.Clear();

            txt_ma.DataBindings.Add("Text", pDT, "TENDANGNHAP");
            txt_ten.DataBindings.Add("Text", pDT, "HOTEN_NV");
            txt_sdt.DataBindings.Add("Text", pDT, "SDT_NV");
            txt_cccd.DataBindings.Add("Text", pDT, "CCCD_NV");
            txt_diachi.DataBindings.Add("Text", pDT, "DIACHI_NV");
            txt_mk.DataBindings.Add("Text", pDT, "MATKHAU");
            txt_chucvu.DataBindings.Add("Text", pDT, "CHUCVU");
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
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_NhanVien);
                da_NhanVien.Update(ds_QLNV, "NHANVIEN");
                Databingdings(ds_QLNV.Tables["NHANVIEN"]);
                MessageBox.Show("Thành công");
                btn_luu.Enabled = false;
                btn_them.Enabled = true;
                flag = 0;
                
                //load_database_gridview();
            }

        }

        private void QLNhanVien_Load(object sender, EventArgs e)
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
            Databingdings(ds_QLNV.Tables["NHANVIEN"]);
        }
    }
}
