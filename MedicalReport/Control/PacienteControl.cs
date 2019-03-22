using MedicalReport.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalReport.Control
{
	class PacienteControl
	{
		//método que lista los pacientes
		public static async Task<List<string>> ObtenerPacientes()
		{
			var datos = await PacsConexion.ObtenerDatos("http://localhost:8042/patients/");
			List<string> pacientes = new List<string>();

			string valor = "";
			//separo los pacientes 			
			string[] lista_pacintes = datos.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

			//añado los valores a un objeto
			if (lista_pacintes.Length >= 3)
			{
				for (int x = 1; x < lista_pacintes.Length - 1; x++)
				{
					lista_pacintes[x] = lista_pacintes[x].Replace(',', ' ').Replace('"', ' ');
					string[] idPaciente = lista_pacintes[x].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					valor = (await PacsConexion.ObtenerDatos("http://localhost:8042/patients/" + idPaciente[0]));
					pacientes.Add(valor);
				}
			}
			else
			{
				lista_pacintes[0] = lista_pacintes[0].Replace(',', ' ').Replace('"', ' ');
				string[] idPaciente = lista_pacintes[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				valor = (await PacsConexion.ObtenerDatos("http://localhost:8042/patients/" + idPaciente[1]));
				pacientes.Add(valor);
			}
			
			return pacientes;
		}
		//convierte de JSON a objetos
		public static List<Paciente.PacienteCabeza> ConvertirDatos(List<string> pacientes)
		{
			List<Paciente.PacienteCabeza> DatosPacientes = new List<Paciente.PacienteCabeza>();
			for (int x = 0; x < pacientes.Count; x++)
			{
				DatosPacientes.Add(JsonConvert.DeserializeObject<Paciente.PacienteCabeza>(pacientes[x]));
			}
			
			return DatosPacientes;
		}

	}
}
