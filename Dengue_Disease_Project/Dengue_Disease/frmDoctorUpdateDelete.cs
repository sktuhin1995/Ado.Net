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
    public partial class frmDoctorUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=TUHIN\SQLEXPRESS;Initial Catalog=Dengue_Disease_DB;Integrated Security=True;");

        public frmDoctorUpdateDelete()
        {
            InitializeComponent();
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
        private void frmDoctorUpdateDelete_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select DoctorId, DoctorName, Age, DateOfBirth, DoctorContact, DoctorEmail, Picture, DoctorAppoinmentDayId From Doctors_Info Where DoctorId = " + txtDoctorID.Text + "", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtDoctorName.Text = dt.Rows[0][1].ToString();
                txtAge.Text = dt.Rows[0][2].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][3].ToString());
                txtDoctorContact.Text = dt.Rows[0][4].ToString();
                txtDoctorEmail.Text = dt.Rows[0][5].ToString();
                MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][6]);
                Image img = Image.FromStream(ms);
                pictureBox1.Image = img;
                cmbAppoinmentDay.SelectedValue = dt.Rows[0][7].ToString();
            }
            else
            {
                lblMsg.ForeColor = Color.Teal;
                lblMsg.Text = "Data Not Found!!";
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtPicture.Text != "")
            {
                Image img = Image.FromFile(txtPicture.Text);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Update Doctors_Info Set DoctorName=@n,  Age=@a, DateOfBirth=@d, DoctorContact=@c, DoctorEmail=@m, Picture=@p, DoctorAppoinmentDayId=@di Where DoctorId = @i";
                cmd.Parameters.AddWithValue("@i", txtDoctorID.Text);
                cmd.Parameters.AddWithValue("@n", txtDoctorName.Text);
                cmd.Parameters.AddWithValue("@a", txtAge.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@c", txtDoctorContact.Text);
                cmd.Parameters.AddWithValue("@m", txtDoctorEmail.Text);
                cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value = ms.ToArray() });
                cmd.Parameters.AddWithValue("@di", cmbAppoinmentDay.SelectedValue);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Data Updated Successfully!!";
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Update Doctors_Info Set DoctorName=@n,  Age=@a, DateOfBirth=@d, DoctorContact=@c, DoctorEmail=@m, DoctorAppoinmentDayId=@di Where DoctorId = @i";
                cmd.Parameters.AddWithValue("@i", txtDoctorID.Text);
                cmd.Parameters.AddWithValue("@n", txtDoctorName.Text);
                cmd.Parameters.AddWithValue("@a", txtAge.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@c", txtDoctorContact.Text);
                cmd.Parameters.AddWithValue("@m", txtDoctorEmail.Text);
                cmd.Parameters.AddWithValue("@di", cmbAppoinmentDay.SelectedValue);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Data Updated Successfully!!";
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Doctors_Info WHERE DoctorId=@i ", con);
            cmd.Parameters.AddWithValue("@i", txtDoctorID.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Deleted successfully!!!";
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
