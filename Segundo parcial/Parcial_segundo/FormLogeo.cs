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
    public partial class FormLogeo : Form
    {
        private AccesoDatosUsuario AccesoDatos;
        
        public FormLogeo()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormLogeo_Load(object sender, EventArgs e)
        {
            this.AccesoDatos = new AccesoDatosUsuario();
            this.AccesoDatos.ProbarConexion();           
        }

        private void buttonIniciarSesion_Click(object sender, EventArgs e)
        {            
            Usuario usuario = new Usuario();
            usuario.User = this.textBoxUsuario.Text;
            usuario.Password = this.textBoxContrasenia.Text;
            bool validacion = this.AccesoDatos.ExisteDato(usuario);
            
            if(!validacion)
            {
                MessageBox.Show("No existe ningún usuario con esos datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<Usuario> listaUsuarios = new List<Usuario>();
                listaUsuarios = this.AccesoDatos.ObtenerListaDato();
                foreach(Usuario usuarioAux in listaUsuarios)
                {
                    if(usuarioAux == usuario)
                    {
                        FormPrincipal form = new FormPrincipal(usuarioAux);
                        this.Visible = false;
                        if (form.ShowDialog() == DialogResult.Cancel)
                        {
                            Application.Exit();
                        }
                        break;
                    }
                }
                

                
                
            }
            
        }

        private void buttonRegistrarse_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.User = this.textBoxUsuario.Text;
            usuario.Password = this.textBoxContrasenia.Text;
            if (this.textBoxUsuario.Text != "" && this.textBoxContrasenia.Text != "")
            {
                if (this.textBoxUsuario.Text.Length < 50 && this.textBoxContrasenia.Text.Length < 50)
                {
                    bool validacion = this.AccesoDatos.UsuarioYaRegistrado(usuario);
                    if (!validacion)
                    {
                        MessageBox.Show("¡Se registró correctamente el usuario!");
                        this.AccesoDatos.AgregarDato(usuario);
                    }
                    else
                    {
                        MessageBox.Show("¡Ya existe un usuario con ese nombre!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("¡El nombre y/o la contraseña son demasiado largos!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("¡No pueden haber campos en blanco!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
