using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalReport.Entity
{
	class Paciente
	{
		// atributos para decserializar el archivos Json
		public class PacienteCabeza
		{			
			public string ID { get; set; }
			public string IsStable { get; set; }
			public string LastUpdate { get; set; }
			public MainDicomTags MainDicomTags { get; set; }
			public  List<string> Studies { get; set; }
			public string Type { get; set; }

		}
		public class Studies
		{
			public string Valor { get; set; }
		}
		
		public class MainDicomTags
		{
			public string PatientBirthDate { get; set; }
			public string PatientID { get; set; }
			public string PatientName { get; set; }
			public string PatientSex { get; set; }

		}

	}
}
