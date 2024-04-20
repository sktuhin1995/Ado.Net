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
    public partial class frmPatients : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=TUHIN\SQLEXPRESS;Initial Catalog=Dengue_Disease_DB;Integrated Security=True;");
        public frmPatients()
        {
            InitializeComponent();
        }

        private void frmPatients_Load(object sender, EventArgs e)
        {
            LoadCombo();
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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtPicture.Text = openFileDialog1.FileName;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(txtPicture.Text);
            MemoryStream ms = new MemoryStream();   
            img.Save(ms, ImageFormat.Bmp);

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Insert Into Patients_Info(PatientID, PatientName, Age, DateOfBirth,PatientContact, PatientMail, Picture, DengueTypeId)\r\nValues(@i, @n, @a, @d, @c, @m, @p, @di)";
            cmd.Parameters.AddWithValue("@i", txtPatientId.Text);
            cmd.Parameters.AddWithValue("@n", txtPatientName.Text);
            cmd.Parameters.AddWithValue("@a", txtAge.Text);
            cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@c", txtPatientContact.Text);
            cmd.Parameters.AddWithValue("@m", txtPatientMail.Text);
            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value = ms.ToArray() });
            cmd.Parameters.AddWithValue("@di", cmbDengueType.SelectedValue);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Inserted Successfully!!";
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
