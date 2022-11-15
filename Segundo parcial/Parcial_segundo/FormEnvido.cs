using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial_segundo
{
    public partial class FormEnvido : Form
    {
        private bool respuesta;
        public FormEnvido(int tipoEnvido)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.respuesta = false;
            if(tipoEnvido == 1)
            {
                this.buttonFaltaEnvido.Visible = false;
            }
        }

        private void buttonQuiero_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.respuesta = true;
        }

        private void buttonFaltaEnvido_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.respuesta = true;
        }

        private void buttonNoQuiero_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.respuesta = true;
        }

        private void FormEnvido_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.respuesta == false)
            {
                if (MessageBox.Show($"Si no selecciona una opción se asumirá que no quiere.\n¿Está seguro de que desea continuar?", "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.respuesta = true;
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
