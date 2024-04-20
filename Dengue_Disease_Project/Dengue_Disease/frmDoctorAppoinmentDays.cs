using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Dengue_Disease
{
    public partial class frmDoctorAppoinmentDays : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=TUHIN\SQLEXPRESS;Initial Catalog=Dengue_Disease_DB;Integrated Security=True;");
        public frmDoctorAppoinmentDays()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert Into Doctor_Appoinment_Days Values('" + txtAppointmentDays.Text + "')", con);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Saved Successfully !";
            LoadGrid();
            txtAppointmentDays.Text = "";
            con.Close();
        }

        private void frmDoctorAppoinmentDays_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
         
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * From Doctor_Appoinment_Days", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
