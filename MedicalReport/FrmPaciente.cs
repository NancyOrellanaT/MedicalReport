using MedicalReport.Control;
using MedicalReport.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace MedicalReport
{
    public partial class FrmPaciente : Form
    {/// <summary>
	/// variables globales para reportes 
	/// </summary>
		string[] nombreCompleto;
		DataGridViewSelectedCellCollection selectedCells;
		List<string> studios;
		List<Studios.StudioCabeza> reportes = new List<Studios.StudioCabeza>();

		public FrmPaciente(DataGridViewSelectedCellCollection selectedCells, List<string> studios)
		{
			nombreCompleto = selectedCells[1].Value.ToString().Split('^');
			this.selectedCells = selectedCells;
			this.studios = studios;



			//	nombreCompleto [] = selectedCells[0].
			//if(v)
			InitializeComponent();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private async void FrmPaciente_Load(object sender, EventArgs e)
		{
			if (nombreCompleto.Length >= 3)
			{
				txtNombres.Text = nombreCompleto[0];
				//txtApellidoMaterno.Text = nombreCompleto[1];
				//txtApellidoPaterno.Text = nombreCompleto[2];

			}
			else if (nombreCompleto.Length == 2)
			{
				txtNombres.Text = nombreCompleto[0];
				//txtApellidoMaterno.Text = nombreCompleto[1];
			}
			else
			{
				txtNombres.Text = nombreCompleto[0];

			}
			txtid.Text = selectedCells[0].Value.ToString();
			txtfecha.Text = selectedCells[2].Value.ToString();
			txtGenero.Text = selectedCells[3].Value.ToString();
			//obtiene los datos
			string valor = "";
			string dirStudio= "http://localhost:8042/studies/";
			for (int x=0;x<studios.Count;x++)
			{
				studios[x] = await PacsConexion.ObtenerDatos(dirStudio+studios[x]);
				
			}
			 reportes = new List<Studios.StudioCabeza>(PacienteControl.ConvertirEstudios(studios));
			try
				
			{
				comboBox1.Items.Clear();
				for (int x = 0; x < reportes.Count; x++)
				{
					comboBox1.Items.Add(reportes[x].MainDicomTags.StudyID + "-" + reportes[x].MainDicomTags.StudyDescription);
				}
			}
			catch(Exception)
			{
				MessageBox.Show("Ocurrio un error al cargar el paciente, intente otra vez");
			}
			//label7.Text = valor;
			

		}
		/// <summary>
		/// crea el pfg a partir de los datos 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPdf_Click(object sender, EventArgs e)
		{
			CrearPdf();
		}

		private void btnWord_Click(object sender, EventArgs e)
		{
			CrearPdf();
			CrearWord();
		}
		public void CrearPdf()
		{
			Document doc = new Document(PageSize.A4);
			try
			{
				//creo el dpf
				var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
				var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 17);

				var output = new FileStream(("reporte" + selectedCells[0].Value.ToString() + ".pdf"), FileMode.Create);
				var writer = PdfWriter.GetInstance(doc, output);


				doc.Open();
				/// IMAGEN A CAMBIAR SE PONE EN ...\medicalreport\medicalreport\bin\debug y es ahi donde se genera el archivo pdf
				string dir = Directory.GetCurrentDirectory() + "\\logo.png";

				var logo = iTextSharp.text.Image.GetInstance(dir);
				logo.SetAbsolutePosition(400, 700);
				logo.ScaleAbsoluteHeight(100);
				logo.ScaleAbsoluteWidth(100);
				doc.Add(logo);

				PdfPTable table1 = new PdfPTable(2);
				table1.DefaultCell.Border = 0;
				table1.WidthPercentage = 80;


				Paragraph t1 = new Paragraph("Datos de paciente", titleFont);
				t1.Alignment = (Element.ALIGN_CENTER);
				doc.Add(t1);
				doc.Add(new Paragraph("\n \n \n \n \n \n Reporte de Paciente", boldFont));
				doc.Add(new Paragraph("ID de paciente :  " + selectedCells[0].Value.ToString()));
				doc.Add(new Paragraph("Nombre de Paciente :  " + selectedCells[1].Value.ToString()));
				doc.Add(new Paragraph("Género :  " + selectedCells[2].Value.ToString()));
				doc.Add(new Paragraph("Fecha de nacimiento :  " + selectedCells[3].Value.ToString()));
				doc.Add(new Paragraph("\n\n"));

				//reporte 

				doc.Add(new Paragraph("Datos de Estudio :  ", boldFont));
				doc.Add(new Paragraph("Accesion Number :  " + reportes[comboBox1.SelectedIndex].MainDicomTags.AccesionNumber));
				doc.Add(new Paragraph("Fecha de estudio :  " + reportes[comboBox1.SelectedIndex].MainDicomTags.StudyDate));
				doc.Add(new Paragraph("Descripción :  " + reportes[comboBox1.SelectedIndex].MainDicomTags.StudyDescription));
				doc.Add(new Paragraph("Id de estudio :  " + reportes[comboBox1.SelectedIndex].MainDicomTags.StudyID));
				doc.Add(new Paragraph("InstanceUID de estudio :  " + reportes[comboBox1.SelectedIndex].MainDicomTags.StudyInstanceUID));
				doc.Add(new Paragraph("Hora de estudio :  " + reportes[comboBox1.SelectedIndex].MainDicomTags.StudyTime));

				doc.Close();
				MessageBox.Show("Reporte creado con éxito. Fecha: " + DateTime.Now);
			}
			catch (Exception)
			{
				MessageBox.Show("Seleccione estudio", "Importante");
				doc.Close();
			}
		}
		public void CrearWord()
		{
			string dir = Directory.GetCurrentDirectory() + "\\" + "reporte" + selectedCells[0].Value.ToString();

			string pdfFile = dir + ".pdf";
			string wordFile = dir + ".docx";

			// Convert PDF file to DOCX file 
			SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();

			f.OpenPdf(pdfFile);

			if (f.PageCount > 0)
			{
				// You may choose output format between Docx and Rtf. 
				f.WordOptions.Format = SautinSoft.PdfFocus.CWordOptions.eWordDocument.Docx;

				int result = f.ToWord(wordFile);

				// Show the resulting Word document. 
				if (result == 0)
				{
					System.Diagnostics.Process.Start(wordFile);
				}
			}
			MessageBox.Show("Reporte creado con éxito.");
		}
	}
}
