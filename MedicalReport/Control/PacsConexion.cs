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
		
		// hacer un "request" al pacs segun lo necesario"
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
		//enviar archivo al pacs
		public void EnviarArchivo()
		{

		}
		public static DataTable ToDataTable<T>(IList<T> data)
		{
			PropertyDescriptorCollection props =
			TypeDescriptor.GetProperties(typeof(T));
			DataTable table = new DataTable();
			for (int i = 0; i < props.Count; i++)
			{
				PropertyDescriptor prop = props[i];
				table.Columns.Add(prop.Name, prop.PropertyType);
			}
			object[] values = new object[props.Count];
			foreach (T item in data)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = props[i].GetValue(item);
				}
				table.Rows.Add(values);
			}
			return table;
		}
	}
}
