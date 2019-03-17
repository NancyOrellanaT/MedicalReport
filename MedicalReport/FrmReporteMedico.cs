using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalReport
{
    public partial class FrmReporteMedico : Form
    {
        public FrmReporteMedico()
        {
            InitializeComponent();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.SelectedRows.Count == 1)
            {
                FrmPaciente frmPaciente = new FrmPaciente();
                frmPaciente.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar solamente un paciente.", "¡Error!");
            }
        }
    }
}
