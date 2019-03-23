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
				txtApellidoMaterno.Text = nombreCompleto[1];
				txtApellidoPaterno.Text = nombreCompleto[2];

			}
			else if (nombreCompleto.Length == 2)
			{
				txtNombres.Text = nombreCompleto[0];
				txtApellidoMaterno.Text = nombreCompleto[1];
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
				valor += studios[x];
			}
			List<Studios.StudioCabeza> reportes = new List<Studios.StudioCabeza>(PacienteControl.ConvertirEstudios(studios));

			label7.Text = valor;
			

		}

		private void btnPdf_Click(object sender, EventArgs e)
		{
			//creo el dpf
			Document doc = new Document(PageSize.A4);
			var output = new FileStream(("MyFirstPDF.pdf"), FileMode.Create);
			var writer = PdfWriter.GetInstance(doc, output);


			doc.Open();
			string dir = Directory.GetCurrentDirectory() + "\\horda.jpg";

			var logo = iTextSharp.text.Image.GetInstance(dir);
			logo.SetAbsolutePosition(300, 600);
			logo.ScaleAbsoluteHeight(200);
			logo.ScaleAbsoluteWidth(200);
			doc.Add(logo);

			PdfPTable table1 = new PdfPTable(2);
			table1.DefaultCell.Border = 0;
			table1.WidthPercentage = 80;
			var docTitle = new Paragraph("UCSC Direct - Direct Payment Form");
			var titleFont = FontFactory.GetFont("Courier", 18, BaseColor.BLACK);

			PdfPCell cell11 = new PdfPCell();
			cell11.Colspan = 1;
			cell11.AddElement(new Paragraph("ABC Traders Receipt", titleFont));

			cell11.AddElement(new Paragraph("Thankyou for shoping at ABC traders,your order details are below", titleFont));


			cell11.VerticalAlignment = Element.ALIGN_LEFT;

			PdfPCell cell12 = new PdfPCell();


			cell12.VerticalAlignment = Element.ALIGN_CENTER;


			table1.AddCell(cell11);

			table1.AddCell(cell12);


			PdfPTable table2 = new PdfPTable(3);

			//One row added

			PdfPCell cell21 = new PdfPCell();

			cell21.AddElement(new Paragraph("Photo Type"));

			PdfPCell cell22 = new PdfPCell();

			cell22.AddElement(new Paragraph("No. of Copies"));

			PdfPCell cell23 = new PdfPCell();

			cell23.AddElement(new Paragraph("Amount"));


			table2.AddCell(cell21);

			table2.AddCell(cell22);

			table2.AddCell(cell23);


			//New Row Added

			PdfPCell cell31 = new PdfPCell();

			cell31.AddElement(new Paragraph("Safe"));

			cell31.FixedHeight = 300.0f;

			PdfPCell cell32 = new PdfPCell();

			cell32.AddElement(new Paragraph("2"));

			cell32.FixedHeight = 300.0f;

			PdfPCell cell33 = new PdfPCell();

			cell33.AddElement(new Paragraph("20.00 * " + "2" + " = " + (20 * Convert.ToInt32("2")) + ".00"));

			cell33.FixedHeight = 300.0f;



			table2.AddCell(cell31);

			table2.AddCell(cell32);

			table2.AddCell(cell33);


			PdfPCell cell2A = new PdfPCell(table2);

			cell2A.Colspan = 2;

			table1.AddCell(cell2A);

			PdfPCell cell41 = new PdfPCell();

			cell41.AddElement(new Paragraph("Name : " + "ABC"));

			cell41.AddElement(new Paragraph("Advance : " + "advance"));

			cell41.VerticalAlignment = Element.ALIGN_LEFT;

			PdfPCell cell42 = new PdfPCell();

			cell42.AddElement(new Paragraph("Customer ID : " + "011"));

			cell42.AddElement(new Paragraph("Balance : " + "3993"));

			cell42.VerticalAlignment = Element.ALIGN_RIGHT;


			table1.AddCell(cell41);

			table1.AddCell(cell42);


			doc.Add(table1);

			doc.Close();
			MessageBox.Show("Reporte creato onc éxito");
		}

		private void btnWord_Click(object sender, EventArgs e)
		{
			string dir = Directory.GetCurrentDirectory() + "\\MyFirstPDF";

			string pdfFile = dir+".pdf";
			string wordFile = dir+".docx";

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
			MessageBox.Show("Reporte creado con éxito");
		}
	}
}
