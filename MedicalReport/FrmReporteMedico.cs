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
using System.Globalization;

namespace MedicalReport
{
	
    public partial class FrmReporteMedico : Form
    {
		List<Paciente.PacienteCabeza> DatosPacientes = new List<Paciente.PacienteCabeza>();

		static private string valor = "";
		public FrmReporteMedico()
        {
            InitializeComponent();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {

			try
			{
				FrmPaciente frmPaciente = new FrmPaciente(dgvPacientes.SelectedCells, DatosPacientes[dgvPacientes.CurrentRow.Index].Studies);


				//FrmPaciente frmPaciente = new FrmPaciente(dgvPacientes.SelectedCells, DatosPacientes[dgvPacientes.CurrentRow.Index].Studies);

				frmPaciente.Show();


			}
			catch (Exception)
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
			 DatosPacientes=new List<Paciente.PacienteCabeza>(PacienteControl.ConvertirDatos(pacientes));

			//añado a la tabla
			dgvPacientes.Rows.Clear();
			foreach(var paciente in DatosPacientes)
			{
				int allstudies=0;
				for(int x=0;x<paciente.Studies.Count;x++)
				{
					allstudies += 1;
				}
				string fecha;
				try
				{
					 fecha = DateTime.ParseExact(paciente.MainDicomTags.PatientBirthDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
				}
				catch(Exception)
				{
					fecha = "";
				}


				this.dgvPacientes.Rows.Add(paciente.MainDicomTags.PatientID, paciente.MainDicomTags.PatientName,fecha ,paciente.MainDicomTags.PatientSex, allstudies, paciente.Type);
			}
			MessageBox.Show("Listado de pacientes completo");
		
		}
		

		private void dgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
