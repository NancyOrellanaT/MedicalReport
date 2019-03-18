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
		public async Task<List<string>> ListarPacientes()
		{
			var datos = await PacsConexion.ObtenerDatos("http://localhost:8042/patients/");
			List<string> pacientes=null;
			//separo los pacientes 			
			string[] lista_pacintes = datos.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			

			if (lista_pacintes.Length >= 3)
			{
				for (int x = 1; x < lista_pacintes.Length - 1; x++)
				{
					lista_pacintes[x] = lista_pacintes[x].Replace(',', ' ').Replace('"', ' ');
					string[] idPaciente = lista_pacintes[x].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					pacientes.Add( await PacsConexion.ObtenerDatos("http://localhost:8042/patients/" + idPaciente[0]));
				}
			}
			else
			{
				lista_pacintes[0] = lista_pacintes[0].Replace(',', ' ').Replace('"', ' ');
				string[] idPaciente = lista_pacintes[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				pacientes.Add( await PacsConexion.ObtenerDatos("http://localhost:8042/patients/" + idPaciente[1]));
			}
			return pacientes;
			
		}

		//boton de listas
		private async void btnListar_Click  (object sender, EventArgs e)
		{
			//guarda datos de cada paciente
			List<string> PacientesDatos = await ListarPacientes();
			List<Paciente.PacienteCabeza> DatosPacientes=null;
			for(int x=0;x<PacientesDatos.Count;x++)
			{
				DatosPacientes.Add(JsonConvert.DeserializeObject<Paciente.PacienteCabeza>(PacientesDatos[x]));
			}
		//	Paciente.PacienteCabeza o = JsonConvert.DeserializeObject<Paciente.PacienteCabeza>(valor1);
			//var model = JsonConvert.DeserializeObject<List<Paciente.PacienteCabeza>>(valor1);
			//DataTable tabla= PacsConexion.ToDataTable<Paciente>(model);
			/*DataTable dt = new DataTable("OutputData");

			DataRow dr = dt.NewRow();
			dt.Rows.Add(dr);

			o.GetType().GetProperties().ToList().ForEach(f =>
			{
				try
				{
					f.GetValue(o, null);
					dt.Columns.Add(f.Name, f.PropertyType);
					dt.Rows[0][f.Name] = f.GetValue(o, null);
				}
				catch { }
			});
			dgvPacientes.DataSource=(dt);*/
		}
		

		private void dgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
