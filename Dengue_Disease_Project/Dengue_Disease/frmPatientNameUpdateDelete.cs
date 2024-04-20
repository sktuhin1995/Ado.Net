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
    public partial class frmPatientNameUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=TUHIN\SQLEXPRESS;Initial Catalog=Dengue_Disease_DB;Integrated Security=True;");
        public frmPatientNameUpdateDelete()
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
            SqlDataAdapter sda = new SqlDataAdapter("Select * From Type_Of_Dengue", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            cmbDengueType.DataSource = ds.Tables[0];
            cmbDengueType.DisplayMember = "DenguesType";
            cmbDengueType.ValueMember = "DengueId";
            con.Close();                 
        }
        private void frmPatientNameUpdateDelete_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select PatientID, PatientName, Age, DateOfBirth, PatientContact, PatientMail, Picture, DengueTypeId From Patients_Info Where PatientID = "+txtPatientId.Text+"", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0 )
            {
                txtPatientName.Text = dt.Rows[0][1].ToString();
                txtAge.Text = dt.Rows[0][2].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][3].ToString());
                txtPatientContact.Text = dt.Rows[0][4].ToString();
                txtPatientMail.Text = dt.Rows[0][5].ToString();
                MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][6]);
                Image img = Image.FromStream(ms);
                pictureBox1.Image = img;
                cmbDengueType.SelectedValue = dt.Rows[0][7].ToString();
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
            if(txtPicture.Text != "")
            {
                Image img = Image.FromFile(txtPicture.Text);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Update Patients_Info Set PatientName=@n,  Age=@a, DateOfBirth=@d, PatientContact=@c, PatientMail=@m, Picture=@p, DengueTypeId=@di Where PatientID = @i";
                cmd.Parameters.AddWithValue("@i", txtPatientId.Text);
                cmd.Parameters.AddWithValue("@n", txtPatientName.Text);
                cmd.Parameters.AddWithValue("@a", txtAge.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@c", txtPatientContact.Text);
                cmd.Parameters.AddWithValue("@m", txtPatientMail.Text);
                cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value = ms.ToArray() });
                cmd.Parameters.AddWithValue("@di", cmbDengueType.SelectedValue);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Data Updated Successfully!!";
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Update Patients_Info Set PatientName=@n,  Age=@a, DateOfBirth=@d, PatientContact=@c, PatientMail=@m, DengueTypeId=@di Where PatientID = @i";
                cmd.Parameters.AddWithValue("@i", txtPatientId.Text);
                cmd.Parameters.AddWithValue("@n", txtPatientName.Text);
                cmd.Parameters.AddWithValue("@a", txtAge.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@c", txtPatientContact.Text);
                cmd.Parameters.AddWithValue("@m", txtPatientMail.Text);
                cmd.Parameters.AddWithValue("@di", cmbDengueType.SelectedValue);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Data Updated Successfully!!";
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Patients_Info WHERE PatientID=@i ", con);
            cmd.Parameters.AddWithValue("@i", txtPatientId.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Deleted successfully!!!";
            con.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtPatientId.Clear();
            txtPatientName.Clear();
            txtAge.Clear();
            dateTimePicker1.Text = "";
            txtPatientContact.Clear();
            txtPatientMail.Clear();
            txtPicture.Clear();
            cmbDengueType.SelectedIndex = -1;
        }
    }
}
