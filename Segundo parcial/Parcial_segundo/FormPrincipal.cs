using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Parcial_segundo
{
    public partial class FormPrincipal : Form
    {
        private Usuario usuario;
        public FormPrincipal(Usuario usuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.usuario = usuario;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            this.labelUsuario.Text = $"¡Bienvenido {this.usuario.User}!";
        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            Task task = Task.Run(() => this.CrearSala());
        }

        private void buttonEstadisticas_Click(object sender, EventArgs e)
        {
            FormEstadisticas form = new FormEstadisticas(this.usuario);
            form.Show();
        }

        private void CrearSala()
        {
            SalaDeJuego sala = new SalaDeJuego(this.usuario);
            sala.ShowDialog();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show($"¿Está seguro de que desea salir de la aplicación?", "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
