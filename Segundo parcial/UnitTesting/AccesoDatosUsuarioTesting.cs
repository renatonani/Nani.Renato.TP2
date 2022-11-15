using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;

namespace UnitTesting
{
    [TestClass]
    public class AccesoDatosUsuarioTesting
    {
        [TestMethod]
        public void Test_ProbarConexion_AccesoDatosUsuario()
        {
            AccesoDatosUsuario datos = new AccesoDatosUsuario();
            bool validacion = datos.ProbarConexion();
            Assert.IsTrue(validacion);
        }
        [TestMethod]
        public void Test_ObtenerListaDato_AccesoDatosUsuario()
        {
            AccesoDatosUsuario datos = new AccesoDatosUsuario();
            List<Usuario> lista = datos.ObtenerListaDato();
            bool validacion = false;
            if(lista != null)
            {
                validacion = true;
            }
            if(lista.Count > 0)
            {
                validacion = true;
            }
            else
            {
                validacion = false;
            }
            Assert.IsTrue(validacion);
        }
        //EL SIGUIENTE METODO SE ENCUENTRA COMENTADO PARA EVITAR LA
        //REPETICION DE CREACION DE USUARIOS EN LA BASE DE DATOS AL EJECUTAR LAS PRUEBAS UNITARIAS.
        /*[TestMethod]
        public void Test_AgregarDato_AccesoDatosUsuario()
        {            
            AccesoDatosUsuario datos = new AccesoDatosUsuario();
            Usuario usuario = new Usuario();
            bool validacion = false;
            usuario.User = "test";
            usuario.Password = "123";
            validacion = datos.AgregarDato(usuario);            

            Assert.IsTrue(validacion);
        }*/
        [TestMethod]
        public void Test_ExisteUsuario_AccesoDatosUsuario()
        {
            AccesoDatosUsuario datos = new AccesoDatosUsuario();
            Usuario usuario = new Usuario();
            usuario.User = "test";
            usuario.Password = "123";
            bool validacion = datos.ExisteDato(usuario);
            Assert.IsTrue(validacion);
        }
        [TestMethod]
        public void Test_UsuarioYaRegistrado_AccesoDatosUsuario()
        {
            AccesoDatosUsuario datos = new AccesoDatosUsuario();
            bool validacion = false;
            Usuario usuario = new Usuario();
            Usuario usuario2 = new Usuario();
            usuario.User = "test";
            usuario2.User = "test2";
            validacion = datos.UsuarioYaRegistrado(usuario);
            Assert.IsTrue(validacion);
            validacion = datos.UsuarioYaRegistrado(usuario2);
            Assert.IsFalse(validacion);
        }
        [TestMethod]
        public void Test_ModificarDato_AccesoDatosUsuario()
        {
            AccesoDatosUsuario datos = new AccesoDatosUsuario();
            bool validacion = false;
            Usuario usuario = new Usuario();
            usuario.User = "test";
            usuario.Password = "123";
            usuario.PartidasJugadas = 2;
            usuario.PartidasGanadas = 1;
            usuario.PartidasPerdidas = 1;
            validacion = datos.ModificarDato(usuario);
            Assert.IsTrue(validacion);
        }

    }
}
