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
    public partial class QLDichVu : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-29HM56H\SQLEXPRESS02;Initial Catalog=QUANLY_KHACHSACN_LAN1;Integrated Security=True");
        DataSet ds_QLDV;
        SqlDataAdapter da_DichVu;
        public int flag = 0;
        public QLDichVu()
        {
            InitializeComponent();
        }
        public void load_database_gridview()
        {
            ds_QLDV = new DataSet();
            string strSelect = "select * from DICHVU";
            da_DichVu = new SqlDataAdapter(strSelect, conn);
            da_DichVu.Fill(ds_QLDV, "DICHVU");
            DataGridView1.DataSource = ds_QLDV.Tables["DICHVU"];
            Databingdings(ds_QLDV.Tables["DICHVU"]);
        }
        public void Databingdings(DataTable pDT)
        {
            txt_ma.DataBindings.Clear();
            txt_ten.DataBindings.Clear();
            txt_gia.DataBindings.Clear();

            txt_ma.DataBindings.Add("Text", pDT, "MA_DV");
            txt_ten.DataBindings.Add("Text", pDT, "TEN_DV");
            txt_gia.DataBindings.Add("Text", pDT, "GIA_DV");
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

        private void QLDichVu_Load(object sender, EventArgs e)
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
            btn_luu.Enabled = false;
            Databingdings(ds_QLDV.Tables["DICHVU"]);
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_DichVu);
                da_DichVu.Update(ds_QLDV, "DICHVU");
                Databingdings(ds_QLDV.Tables["DICHVU"]);
                MessageBox.Show("Thành công");
                load_database_gridview();
                btn_luu.Enabled = false;
                btn_them.Enabled = true;
                flag = 0;
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string deleteString = "DELETE DICHVU WHERE Ma_DV='" + txt_ma.Text + "'";
                SqlCommand cmd = new SqlCommand(deleteString, conn);
                cmd.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                load_database_gridview();
                MessageBox.Show("Thành Công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
