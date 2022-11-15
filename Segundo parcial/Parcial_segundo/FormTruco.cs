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
    public partial class FormTruco : Form
    {
        private bool respuesta;
        public FormTruco(int tipoTruco)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.respuesta = false;
            switch(tipoTruco)
            {
                case 0:
                    this.label1.Text = $"Un jugador ha cantado truco";
                    this.button2.Text = "Quiero re truco";
                    break;
                case 1:
                    this.label1.Text = $"Un jugador ha cantado re truco";
                    this.button2.Text = "Quiero vale 4";
                    break;
                case 2:
                    this.label1.Text = $"Un jugador ha cantado quiero vale 4";
                    this.button2.Visible = false;
                    break;
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.OK;
            this.respuesta = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.Yes;
            this.respuesta = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.Cancel;
            this.respuesta = true;
        }

        private void FormTruco_FormClosing(object sender, FormClosingEventArgs e)
        {  
            if(this.respuesta == false)
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
