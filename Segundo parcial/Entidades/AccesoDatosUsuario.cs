using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Entidades
{
    public class AccesoDatosUsuario : IAccesoDatos<Usuario>
    {              
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;       

        public AccesoDatosUsuario()
        {            
            this.conexion = new SqlConnection(@"Server=.;Database=Aplicacion;Trusted_Connection=True;");
        }
        /// <summary>
        /// Comprueba que la conexión al servidor SQL haya sido exitosa.
        /// </summary>
        /// <returns> Retorna true si la conexion fue exitosa, false caso contrario </returns>
        public bool ProbarConexion()
        {
            bool retorno = true;

            try
            {                
                this.conexion.Open();
            }
            catch (Exception)
            {
                retorno = false;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }

            return retorno;
        }
        /// <summary>
        /// Devuelve una lista con todos los usuarios cargados en la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Usuario> ObtenerListaDato()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {                
                this.comando = new SqlCommand();

                this.comando.CommandType = CommandType.Text;
                this.comando.CommandText = "SELECT * FROM Usuarios";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                this.lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Usuario item = new Usuario();

                    item.Id = (int)lector[0];
                    item.User = (string)lector[1];
                    item.Password = (string)lector[2];
                    item.PartidasJugadas = (int)lector[3];
                    item.PartidasGanadas = (int)lector[4];
                    item.PartidasPerdidas = (int)lector[5];

                    lista.Add(item);
                }

                lector.Close();

            }
            catch (Exception)
            {
                
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }

            return lista;
        }

       /// <summary>
       /// Agrega un usuario a la basde datos.
       /// </summary>
       /// <param name="usuario"></param>
       /// <returns> Retorna true si el usuario fue agregado correctamente, false caso contrario </returns>
        public bool AgregarDato(Usuario usuario)
        {
            bool validacion = true;

            try
            {                
                string comando = "INSERT INTO Usuarios VALUES(";
                comando = comando + "'" + usuario.User + "', '" + usuario.Password + "',"+ 0 + "," +0 + "," + 0 +")";

                this.comando = new SqlCommand();

                this.comando.CommandType = CommandType.Text;
                this.comando.CommandText = comando;
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    validacion = false;
                }

            }
            catch (Exception)
            {
                validacion = false;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }

            return validacion;
        }
        /// <summary>
        /// Verifica si un usuario ya forma parte de la base de datos mediante la sobrecarga del == que compara por nombre de usuario y 
        /// contraseña
        /// </summary>
        /// <param name="usuarioAux"></param>
        /// <returns>Devuelve true si el usuario existe, false caso contrario</returns>
        public bool ExisteDato(Usuario usuarioAux)
        {           
            List<Usuario> listaUsuarios = this.ObtenerListaDato();
            bool validacion = false;
            foreach (Usuario item in listaUsuarios)
            {
                if (usuarioAux == item)
                {                    
                    validacion = true;
                    break; 
                }
            }
            return validacion;
        }
        /// <summary>
        /// Comprueba que el nombre de un usuario no se encuentre registrado en la base de datos. Esto para evitar que puedan haber usuarios
        /// con el mismo nombre y distintas contraseñas.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool UsuarioYaRegistrado(Usuario usuario)
        {            
            List<Usuario> listaUsuarios = this.ObtenerListaDato();
            bool validacion = false;
            foreach (Usuario item in listaUsuarios)
            {
                if (usuario.User == item.User)
                {
                    validacion = true;
                    break;
                }
            }
            return validacion;
        }
        /// <summary>
        /// Modifica los datos de un usuario en la base de datos que coincida con el nombre de usuario del usuario pasado por parametro.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns> Retorna true si los datos del usuario fueron modificados correctamente, false caso contrario </returns>
        public bool ModificarDato(Usuario usuario)
        {
            bool validacion = true;

            try
            {                
                this.comando = new SqlCommand();
                this.comando.Parameters.AddWithValue("@usuario", usuario.User);
                this.comando.Parameters.AddWithValue("@partidasJugadas", usuario.PartidasJugadas);
                this.comando.Parameters.AddWithValue("@partidasGanadas", usuario.PartidasGanadas);
                this.comando.Parameters.AddWithValue("@partidasPerdidas", usuario.PartidasPerdidas);

                string sql = "UPDATE Usuarios ";
                sql += "SET partidasJugadas = @partidasJugadas, partidasGanadas = @partidasGanadas, partidasPerdidas = @partidasPerdidas ";
                sql += "WHERE usuario = @usuario";

                this.comando.CommandType = CommandType.Text;
                this.comando.CommandText = sql;
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    validacion = false;
                }
            }
            catch(Exception)
            {
                validacion = false;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return validacion;
            
        }
    }
}
