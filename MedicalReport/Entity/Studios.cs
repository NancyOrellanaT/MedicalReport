using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalReport.Entity
{
	class Studios
	{
		public class StudioCabeza
		{
			public string ID { get; set; }
			public MainDicomTags MainDicomTags { get; set; }
			public string Type { get; set; }


		}
		public class MainDicomTags
		{
			public string AccesionNumber { get; set; }
			public string StudyDate { get; set; }
			public string StudyDescription { get; set; }
			public string StudyID { get; set; }
			public string StudyInstanceUID { get; set; }
			public string StudyTime { get; set; }


		}
	}
}
