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
		/// <summary>
		/// obtiene todos los pacientes del Pacs
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// convierte de Json a objetos 
		/// </summary>
		/// <param name="pacientes"></param>
		/// <returns>lsita de objetos de paciente</returns>
		public static List<Paciente.PacienteCabeza> ConvertirDatos(List<string> pacientes)
		{
			List<Paciente.PacienteCabeza> DatosPacientes = new List<Paciente.PacienteCabeza>();
			for (int x = 0; x < pacientes.Count; x++)
			{
				DatosPacientes.Add(JsonConvert.DeserializeObject<Paciente.PacienteCabeza>(pacientes[x]));
			}

			return DatosPacientes;
		}
		/// <summary>
		/// convierte studios en objetos 
		/// </summary>
		/// <param name="studios"></param>
		/// <returns>lista de objetos </returns>
		public static List<Studios.StudioCabeza> ConvertirEstudios(List<string> studios)
		{
			List<Studios.StudioCabeza> datosStudios = new List<Studios.StudioCabeza>();
			for (int x = 0; x < studios.Count; x++)
			{
				datosStudios.Add(JsonConvert.DeserializeObject<Studios.StudioCabeza>(studios[x]));
			}

			return datosStudios;
		}

	}
}
