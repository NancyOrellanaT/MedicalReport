using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MedicalReport.Control
{
	class PacsConexion
	{
		//variables que contiene la respuesta de "Obtenerdatos"
		public static string  valor ="";

	
		/// <summary>
		/// 
		// hacer un "request" al pacs segun lo necesario"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public async static Task<string> ObtenerDatos(string url)
		{

			using (HttpClient cliente = new HttpClient())
			{
				using (HttpResponseMessage response = await cliente.GetAsync(url))
				{
					using (HttpContent content = response.Content)
					{

						string mycontent = await content.ReadAsStringAsync();

						return mycontent;
						


					}
				}
			}
		}
		
		
	}
}
