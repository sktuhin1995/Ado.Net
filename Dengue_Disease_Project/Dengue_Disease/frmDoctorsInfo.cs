using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dengue_Disease
{
    public partial class frmDoctorsInfo : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=TUHIN\SQLEXPRESS;Initial Catalog=Dengue_Disease_DB;Integrated Security=True;");

        public frmDoctorsInfo()
        {
            InitializeComponent();
        }

        private void frmDoctorsInfo_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select * From Doctor_Appoinment_Days", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            cmbAppoinmentDay.DataSource = ds.Tables[0];
            cmbAppoinmentDay.DisplayMember = "AppoinmentDay";
            cmbAppoinmentDay.ValueMember = "AppoinmentDayId";
            con.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtPicture.Text = openFileDialog1.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(txtPicture.Text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Insert Into Doctors_Info(DoctorId, DoctorName, Age, DateOfBirth,DoctorContact, DoctorEmail, Picture, DoctorAppoinmentDayId)\r\nValues(@i, @n, @a, @d, @c, @m, @p, @di)";
            cmd.Parameters.AddWithValue("@i", txtDoctorID.Text);
            cmd.Parameters.AddWithValue("@n", txtDoctorName.Text);
            cmd.Parameters.AddWithValue("@a", txtAge.Text);
            cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@c", txtDoctorContact.Text);
            cmd.Parameters.AddWithValue("@m", txtDoctorEmail.Text);
            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value = ms.ToArray() });
            cmd.Parameters.AddWithValue("@di", cmbAppoinmentDay.SelectedValue);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Inserted Successfully!!";
            con.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtDoctorID.Clear();
            txtDoctorName.Clear();
            txtAge.Clear();
            dateTimePicker1.Text = "";
            txtDoctorContact.Clear();
            txtDoctorEmail.Clear();
            txtPicture.Clear();
            cmbAppoinmentDay.SelectedIndex = -1;
        }
    }
}
