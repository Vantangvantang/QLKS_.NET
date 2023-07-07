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
    public partial class DatPhong : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-29HM56H\SQLEXPRESS02;Initial Catalog=QUANLY_KHACHSACN_LAN1;Integrated Security=True");
        DataSet ds_DatPhong;
        SqlDataAdapter da_DatPhong;
        //DataSet ds_QLKH = new DataSet();
        //SqlDataAdapter da_KhachHang;
        DataSet ds_Phong;
        SqlDataAdapter da_Phong;
        public int flagK = 0;
        public int flag = 0;

        public DatPhong()
        {
            InitializeComponent();
            //string strSelect2 = "select * from KHACHHANG";
            //da_KhachHang = new SqlDataAdapter(strSelect2, conn);
        }
        public void load_database_gridview_Phong()
        {
            ds_Phong = new DataSet();
            string strSelect3 = "select * from PHONG where SOKHACHTOIDA >= '"+cbo_songuoi.Text+"'";
            da_Phong = new SqlDataAdapter(strSelect3, conn);
            da_Phong.Fill(ds_Phong, "PHONG");
            DataGridView1.DataSource = ds_Phong.Tables["PHONG"];
        }
        public void load_cbo_songuoi()
        {
            cbo_songuoi.Items.Add(1);
            cbo_songuoi.Items.Add(2);
            cbo_songuoi.Items.Add(3);
            cbo_songuoi.Items.Add(4);
            cbo_songuoi.Items.Add(5);
            cbo_songuoi.Items.Add(6);
        }
        public void load_cbo_tt()
        {
            cbo_tt.Items.Add("Đã vào ở");
            cbo_tt.Items.Add("Chưa vào ở");
        }
        private void DatPhong_Load(object sender, EventArgs e)
        {
            btn_themKH.Enabled = false;
            load_cbo_gioitinh();
            load_cbo_songuoi();
            load_database_gridview_DP();
            load_database_gridview_Phong();
            load_cbo_tt();
            load_cbo_phong();
            foreach (Control item in DataGridView2.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() ==
                typeof(ComboBox) || item.GetType() == typeof(MaskedTextBox))

                    item.Enabled = false;

            }
            DatabingdingsDatPhong(ds_DatPhong.Tables["PHIEUTHUEPHONG"]);
        }
        public void load_cbo_phong()
        {
            da_Phong.Fill(ds_Phong, "PHONG");
            cbo_maphong.DataSource = ds_Phong.Tables["PHONG"];
            cbo_maphong.DisplayMember = "MA_P";
            cbo_maphong.ValueMember = "MA_P";
        }
        /*-----------------------Dat phong-------------------*/
        public void load_database_gridview_DP()
        {
            ds_DatPhong = new DataSet();
            string strSelect = "select * from PHIEUTHUEPHONG";
            da_DatPhong = new SqlDataAdapter(strSelect, conn);
            da_DatPhong.Fill(ds_DatPhong, "PHIEUTHUEPHONG");
            DataGridView2.DataSource = ds_DatPhong.Tables["PHIEUTHUEPHONG"];
            DatabingdingsDatPhong(ds_DatPhong.Tables["PHIEUTHUEPHONG"]);
        }
        public void DatabingdingsDatPhong(DataTable pDT)
        {
            txt_maDP.DataBindings.Clear();
            txt_ngayden.DataBindings.Clear();
            cbo_maphong.DataBindings.Clear();
            cbo_songuoi.DataBindings.Clear();
            cbo_tt.DataBindings.Clear();
            txt_mk.DataBindings.Clear();

            txt_maDP.DataBindings.Add("Text", pDT, "MA_PTP");
            txt_ngayden.DataBindings.Add("Text", pDT, "NGAYDEN");
            cbo_maphong.DataBindings.Add("Text", pDT, "MA_P");
            cbo_songuoi.DataBindings.Add("Text", pDT, "SONGUOI");
            cbo_tt.DataBindings.Add("Text", pDT, "TINHTRANG_PTP");
            txt_mk.DataBindings.Add("Text", pDT, "MA_KH");
        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string insertString;
                insertString = "insert into PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN) values(N'" + cbo_maphong.SelectedValue.ToString() + "',N'" + txt_mk.Text + "',N'" + cbo_songuoi.Text + "',N'" + txt_ngayden.Text + "')";
                SqlCommand cmd = new SqlCommand(insertString, conn);
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                MessageBox.Show("Thêm đặt phòng thành công");
                load_database_gridview_DP();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thất bại");
                conn.Close();
            }
            flag = 0;
        }


        private static void loaddata()
        {
            throw new NotImplementedException();
        }
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string deleteString = "DELETE PHIEUTHUEPHONG WHERE Ma_PTP='" + txt_maDP.Text + "'";
                SqlCommand cmd = new SqlCommand(deleteString, conn);
                cmd.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                load_database_gridview_DP();
                MessageBox.Show("Thành Công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btn_sua_Click(object sender, EventArgs e)
        {
            //label11.Text = txt_maDP.Text;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string updateString = "UPDATE PHIEUTHUEPHONG SET MA_P=" + cbo_maphong.Text + "', MA_KH=" + txt_mk.Text + ", SONGUOI=" + cbo_songuoi.Text + ", TINHTRANG_PTP=N'" + cbo_tt.Text + "', NGAYDEN=N'" + txt_ngayden.Text + "' WHERE MA_PTP='" + txt_maDP.Text + "'";
                SqlDataAdapter da_DatPhong = new SqlDataAdapter(updateString, conn);
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_DatPhong);
                DataGridView2.DataSource = ds_DatPhong.Tables["PHIEUTHUEPHONG"];
                MessageBox.Show("Thành công");
                load_database_gridview_DP();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thất Bại");
            }
        }
        /*-----------------------Khach-------------------*/
        public void load_cbo_gioitinh()
        {
            cbo_gioitinh.Items.Add("Nữ");
            cbo_gioitinh.Items.Add("Nam");
        }
        //public void DatabingdingsKhach(DataTable pDT)
        //{
        //    txt_mk.DataBindings.Clear();
        //    txt_tenK.DataBindings.Clear();
        //    txt_sdt.DataBindings.Clear();
        //    txt_cccd.DataBindings.Clear();
        //    cbo_gioitinh.DataBindings.Clear();

        //    txt_mk.DataBindings.Add("Text", pDT, "MA_KH");
        //    txt_tenK.DataBindings.Add("Text", pDT, "HOTEN_KH");
        //    txt_sdt.DataBindings.Add("Text", pDT, "SDT_KH");
        //    txt_cccd.DataBindings.Add("Text", pDT, "CCCD_KH");
        //    cbo_gioitinh.DataBindings.Add("Text", pDT, "GIOITINH");
        //}

        private void btn_themKH_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //Xac dinh chuoi truy van
                string insertString;
                insertString = "insert into KHACHHANG(HOTEN_KH, CCCD_KH, SDT_KH, GIOITINH) values(N'" + txt_tenK.Text + "',N'" + txt_cccd.Text + "',N'" + txt_sdt.Text + "',N'" + cbo_gioitinh.Text + "')";

                //Khai bao commamnd moi
                SqlCommand cmd = new SqlCommand(insertString, conn);
                //Goi ExecuteNonQuery de gui command
                cmd.ExecuteNonQuery();
                //Kiem tra ket noi truoc khi dong
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                //Hien thi thong bao
                MessageBox.Show("Thêm khách hàng thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thất bại");
            }
        }

        public bool TimCCCD(string cccd)
        {
            conn.Open();
            string strSelect = "select * from KHACHHANG Where CCCD_KH = '" + cccd + "'";
            SqlCommand cmd = new SqlCommand(strSelect, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Close();
                conn.Close();
                return false;

            }
            else
            {
                rd.Close();
                conn.Close();
                return true;
               
            }
        }
        private void btn_timKH_Click(object sender, EventArgs e)
        {
            if (TimCCCD(txt_cccd.Text)==false)
            {
                MessageBox.Show("Đã có thông tin khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                xuatttkh();
            }
            else
            {
                MessageBox.Show("Chưa có thông tin khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_themKH.Enabled = true;
                txt_tenK.Enabled = true;
                txt_sdt.Enabled = true;
                txt_cccd.Enabled = true;
                cbo_gioitinh.Enabled = true;
            }
        }
        private void xuatttkh()
        {
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("select * from KHACHHANG where CCCD_KH = @CCCD", conn);
            cmd1.Parameters.AddWithValue("@CCCD", txt_cccd.Text.ToString());
            SqlDataReader rd1 = cmd1.ExecuteReader();
            while (rd1.Read())
            {
                txt_mk.Text = rd1.GetValue(0).ToString();
                txt_tenK.Text = rd1.GetValue(1).ToString();
                txt_sdt.Text = rd1.GetValue(4).ToString();
                cbo_gioitinh.Text = rd1.GetValue(5).ToString();
            }
            conn.Close();
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbo_songuoi_MouseClick(object sender, MouseEventArgs e)
        {
            load_database_gridview_Phong();
        }

        private void btn_cn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPD_TINHTRANG_PROC";
                cmd.Parameters.AddWithValue("@ma_p_dec", cbo_maphong.Text);
                load_database_gridview_Phong();
                cmd.Connection = conn;
                conn.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
