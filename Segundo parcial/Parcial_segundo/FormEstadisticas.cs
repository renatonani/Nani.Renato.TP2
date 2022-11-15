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
    public partial class FormEstadisticas : Form
    {
        private Usuario usuario;
        private List<Usuario> listaUsuarios;
        public FormEstadisticas(Usuario usuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.usuario = usuario;
        }

        private void FormEstadisticas_Load(object sender, EventArgs e)
        {
            AccesoDatosUsuario datos = new AccesoDatosUsuario();
            this.listaUsuarios = datos.ObtenerListaDato();
            DataTable tabla = new DataTable();
            this.labelEstadisticas.Text = $"Estadísticas de jugadores al: {DateTime.Now:d}";
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Partidas jugadas");
            tabla.Columns.Add("Partidas ganadas");
            tabla.Columns.Add("Partidas perdidas");            

            foreach (Usuario usuario in listaUsuarios)
            {
                DataRow lineaAux = tabla.NewRow();
                lineaAux[0] = usuario.User;
                lineaAux[1] = usuario.PartidasJugadas;
                lineaAux[2] = usuario.PartidasGanadas;
                lineaAux[3] = usuario.PartidasPerdidas;                
                tabla.Rows.Add(lineaAux);
            }
            this.dataGridView1.DataSource = tabla;
            Usuario user = this.CalcularJugadorMasGanadas();
            this.labelGanador.Text = $"{user.User} con {user.PartidasGanadas} partidas";
        }

        private Usuario CalcularJugadorMasGanadas()
        {
            int i = 0;
            Usuario userAux = new Usuario();
            foreach (Usuario usuario in this.listaUsuarios)
            {                
                if(i == 0 || usuario.PartidasGanadas > userAux.PartidasGanadas)
                {
                    userAux = usuario;
                    i++;                    
                }
            }
            return userAux;
        }       
    }    
}
