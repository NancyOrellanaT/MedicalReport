using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalReport
{
    public partial class FrmPaciente : Form
    {
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

		}

	}
}
