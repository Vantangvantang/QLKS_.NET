using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_QLKhachSan
{
    public partial class FormNhanVien : Form
    {
        public FormNhanVien()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void openChild(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        private void btn_KhachHang_Click(object sender, EventArgs e)
        {
            openChild(new KhachHang());
        }

        private void btn_DatPhong_Click(object sender, EventArgs e)
        {
            openChild(new DatPhong());
        }

        private void btn_SDDV_Click(object sender, EventArgs e)
        {
            openChild(new SuDungDV());
        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            openChild(new ThanhToan());
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {

        }
    }
}
