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
    public class UsuarioTesting
    {
        [TestMethod]
        public void Test_Igualdad_Usuario()
        {
            bool validacion = false;
            Usuario usuario1 = new Usuario();
            usuario1.User = "test";
            usuario1.Password = "123";

            Usuario usuario2 = new Usuario();
            usuario2.User = "test";
            usuario2.Password = "123";

            Usuario usuario3 = new Usuario();
            usuario3.User = "test2";
            usuario3.Password = "123";

            Usuario usuario4 = new Usuario();
            usuario4.User = "test";
            usuario4.Password = "1234";


            if (usuario1 == usuario2)
            {
                validacion = true;
            }
            if(usuario1 == usuario3)
            {
                validacion = false;
            }
            if (usuario2 == usuario3)
            {
                validacion = false;
            }
            if(usuario2==usuario4)
            {
                validacion = false;
            }            

            Assert.IsTrue(validacion);
        }
        public void Test_Desigualdad_Usuario()
        {
            bool validacion = true;
            Usuario usuario1 = new Usuario();
            usuario1.User = "test";
            usuario1.Password = "123";

            Usuario usuario2 = new Usuario();
            usuario2.User = "test";
            usuario2.Password = "123";

            if(usuario1 != usuario2)
            {
                validacion = false;
            }

            Assert.IsTrue(validacion);
        }
    }
}
