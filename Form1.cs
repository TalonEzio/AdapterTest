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
namespace AdapterTest
{
    public partial class Form1 : Form
    {
        public SqlConnection conn;

        public SqlDataAdapter da1, da2;
        public DataSet ds = new DataSet();
        public SqlCommandBuilder builder;
        DataTable tblHangHoa;
        public Form1()
        {
            InitializeComponent();

            conn = new SqlConnection("Data Source=talonezio\\sqlexpress;Initial Catalog=QuanLyHangHoa;Integrated Security=True");
            da2 = new SqlDataAdapter("Select * from HangHoa", conn);
            da1 = new SqlDataAdapter("Select * from NhaCungCap", conn);

            da1.Fill(ds, "tblNhaCungCap");
            da2.Fill(ds, "tblHangHoa");

            tblHangHoa = ds.Tables["tblHangHoa"];

            builder = new SqlCommandBuilder(da2);
        }
        List<NhaCungCap> DSNCC = new List<NhaCungCap>();
        List<NhaCungCap> DSNCCTimKiem = new List<NhaCungCap>();
        void TaiDuLieu()
        {
            da2.Update(ds.Tables["tblHangHoa"]);
            dgvDSHS.DataSource = ds.Tables["tblHangHoa"];
            if (dgvDSHS.Rows.Count > 0)
            {
                dgvDSHS.Rows[0].Selected = true;
                dgvDSHS.CurrentCell = dgvDSHS.Rows[0].Cells[0];
                dgvDSHS_CellClick(dgvDSHS, new DataGridViewCellEventArgs(0, 0));
            }
        }


        void TaiCombobox()
        {
            cmbNCC.Items.Clear();
            foreach (DataRow row in ds.Tables["tblNhaCungCap"].Rows)
            {
                DSNCC.Add(new NhaCungCap()
                {
                    MaNCC = row.ItemArray[0].ToString(),
                    TenNCC = row.ItemArray[1].ToString()
                });
            }
            DSNCCTimKiem = DSNCC.ToList();
            cmbNCC.DataSource = DSNCC;
            cmbLoc.DataSource = DSNCCTimKiem;
            cmbLoc.DisplayMember = cmbNCC.DisplayMember = "TenNCC";
            cmbLoc.ValueMember = cmbNCC.ValueMember = "MaNCC";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TaiDuLieu();
            TaiCombobox();
        }
        int index = -1;
        private void dgvDSHS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            index = e.RowIndex;
            DataGridViewRow row = dgvDSHS.Rows[index];
            txtMa.Text = row.Cells[0].Value.ToString();
            txtTen.Text = row.Cells[1].Value.ToString();
            numSoLuong.Value = (int)row.Cells[2].Value;
            txtDonGia.Text = row.Cells[3].Value.ToString();
            cmbNCC.SelectedValue = row.Cells[4].Value.ToString();
        }

        private void cmbNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNCC.SelectedIndex == -1) return;
        }


        bool KiemTraTonTai(string maHH)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from HangHoa where mahanghoa = @mhh", conn);
            cmd.Parameters.AddWithValue("mhh", maHH);
            int result = (int)cmd.ExecuteScalar();
            conn.Close();
            return result > 0;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraTonTai(txtMa.Text))
            {
                MessageBox.Show("Mã đã tồn tại");
                return;
            }
            DataRow row = tblHangHoa.NewRow();
            row[0] = txtMa.Text;
            row[1] = txtTen.Text;
            row[2] = numSoLuong.Value;
            row[3] = txtDonGia.Text;
            row[4] = cmbNCC.SelectedValue;

            tblHangHoa.Rows.Add(row);

            if (da2.Update(tblHangHoa) == 0)
            {
                MessageBox.Show("Thêm lỗi");
            }
            else
            {
                MessageBox.Show("Thêm thành công");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (index == -1)
            {
                MessageBox.Show("Hãy chọn dòng để sửa!");
                return;
            }
            if (!KiemTraTonTai(txtMa.Text))
            {
                MessageBox.Show("Mã chưa tồn tại");
                return;
            }

            DataRow row = tblHangHoa.Rows[index];

            row.BeginEdit();
            row[0] = txtMa.Text;
            row[1] = txtTen.Text;
            row[2] = numSoLuong.Value;
            row[3] = txtDonGia.Text;
            row[4] = cmbNCC.SelectedValue;
            row.EndEdit();

            if (da2.Update(tblHangHoa) == 0)
            {
                MessageBox.Show("Sửa bị lỗi");
            }
            else
            {
                MessageBox.Show("Sửa thành công");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (index == -1)
            {
                MessageBox.Show("Hãy chọn dòng để xóa!");
                return;
            }
            if (!KiemTraTonTai(txtMa.Text))
            {
                MessageBox.Show("Mã chưa tồn tại");
                return;
            }
            DataRow row = tblHangHoa.Rows[index];
            row.Delete();
            if (da2.Update(tblHangHoa) == 0)
            {
                MessageBox.Show("Xóa bị lỗi");
            }
            else
            {
                MessageBox.Show("Xóa thành công");
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from hanghoa where mancc = @mancc",conn);
            cmd.Parameters.AddWithValue("mancc", cmbLoc.SelectedValue);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dgvDSHS.DataSource = dt;
            conn.Close();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            TaiDuLieu();
        }
    }
}
