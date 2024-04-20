using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dengue_Disease
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void entryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDenguesType fdt = new frmDenguesType();
            fdt.Show();
            fdt.MdiParent = this;   
        }

        private void entryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPatients fpa = new frmPatients();
            fpa.Show();
            fpa.MdiParent = this;
        }

        private void updateDeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPatientNameUpdateDelete fpa = new frmPatientNameUpdateDelete();
            fpa.Show();
            fpa.MdiParent = this;
        }

        private void entryToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmDoctorAppoinmentDays fda = new frmDoctorAppoinmentDays();
            fda.Show();
            fda.MdiParent = this;
        }

        private void entryToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmDoctorsInfo fdi = new frmDoctorsInfo();
            fdi.Show();
            fdi.MdiParent = this;
        }

        private void updateDeleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmDoctorUpdateDelete fdi = new frmDoctorUpdateDelete();
            fdi.Show();
            fdi.MdiParent = this;
        }

        private void doctorInformationReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoctorInformationReport fdir = new frmDoctorInformationReport();
            fdir.Show();
            fdir.MdiParent = this;
        }

        private void patientsInformationReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPatientInformationReport fpir = new frmPatientInformationReport();
            fpir.Show();
            fpir.MdiParent = this;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
