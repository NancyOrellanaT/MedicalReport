using MedicalReport.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI;
using System.Web.Script.Serialization;
using MedicalReport.Entity;
using Newtonsoft.Json;
using System.Reflection;

namespace MedicalReport
{
	
    public partial class FrmReporteMedico : Form
    {
		static private string valor = "";
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
		//lista  paciente
		

		//boton de listas
		private async void btnListar_ClickAsync(object sender, EventArgs e)
		{ 
			//obtiene los datos
			List<string> pacientes = new List<string>(await PacienteControl.ObtenerPacientes());			
			
			//guarda datos de cada paciente
			List<Paciente.PacienteCabeza> DatosPacientes=new List<Paciente.PacienteCabeza>(PacienteControl.ConvertirDatos(pacientes));

			//añado a la tabla
			dgvPacientes.Rows.Clear();
			foreach(var paciente in DatosPacientes)
			{
				string allstudies="";
				for(int x=0;x<paciente.Studies.Count;x++)
				{
					allstudies += paciente.Studies[x];
				}				
				
				this.dgvPacientes.Rows.Add(paciente.MainDicomTags.PatientID, paciente.MainDicomTags.PatientName,paciente.MainDicomTags.PatientBirthDate,paciente.MainDicomTags.PatientSex, allstudies, paciente.Type);
			}
		
		}
		

		private void dgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
