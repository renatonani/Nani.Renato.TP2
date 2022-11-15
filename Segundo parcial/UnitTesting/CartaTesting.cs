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
    public class CartaTesting
    {
        [TestMethod]
        public void Test_Igualdad_Carta()
        {
            Carta carta1 = new Carta(1, Palos.Espada, 14);
            Carta carta2 = new Carta(2, Palos.Espada, 9);

            bool validacion = carta1 == carta2;

            Assert.IsTrue(validacion);           
        
        }

        [TestMethod]
        public void Test_Desigualdad_Carta()
        {
            Carta carta1 = new Carta(1, Palos.Espada, 14);
            Carta carta2 = new Carta(2, Palos.Basto, 9);

            bool validacion = carta1 != carta2;

            Assert.IsTrue(validacion);
        }

        [TestMethod]
        public void Test_ObtenerMazo_Carta()
        {
            List<Carta> mazo;
            mazo = Carta.ObtenerMazo();
            bool validacion;
            validacion = mazo.Count == 40;

            Assert.IsTrue(validacion);
        }

        [TestMethod]
        public void Test_Repartir_Carta()
        {
            List<Carta> mazo = new List<Carta>();
            List<Carta> cartasJugador1 = new List<Carta>();
            mazo = Carta.ObtenerMazo();
            bool validacion = false;

            cartasJugador1 = Carta.Repartir(mazo);
            if(cartasJugador1 != null)
            {
                validacion = true;
            }
            //VERIFICA SI SE LE REPARTIERON 3 CARTAS
            if(cartasJugador1.Count == 3)
            {
                validacion = true;
            }
            else
            {
                validacion = false;
            }

            //VERIFICA SI LAS CARTAS REPARTIDAS YA NO FORMAN PARTE DEL MAZO
            foreach(Carta carta in cartasJugador1)
            {
                foreach(Carta cartaAux in mazo)
                {
                    if(carta.Numero == cartaAux.Numero && carta.Palo == cartaAux.Palo)
                    {
                        validacion = false;
                    }
                }
            }

            Assert.IsTrue(validacion);
        }
        [TestMethod]
        public void Test_CalcularTantoJugador_Carta()
        {
            List<Carta> cartasJugador1 = null;
            bool validacion = false;
            int valorTanto;
            //cartasJugador1 es null
            valorTanto = Carta.CalcularTantoJugador(cartasJugador1);

            if (valorTanto == -1)
            {
                validacion = true;
            }
            cartasJugador1 = new List<Carta>();
            //cartasJugador1 no tiene cartas
            valorTanto = Carta.CalcularTantoJugador(cartasJugador1);
            if (valorTanto == -1)
            {
                validacion = true;
            }
            else
            {
                validacion = false;
            }

            Carta carta1 = new Carta(1, Palos.Espada, 14);
            Carta carta2 = new Carta(2, Palos.Basto, 9);
            Carta carta3 = new Carta(7, Palos.Espada, 9);
            cartasJugador1.Add(carta1);
            cartasJugador1.Add(carta2);
            cartasJugador1.Add(carta3);
            valorTanto = Carta.CalcularTantoJugador(cartasJugador1);
            if (valorTanto == 28)
            {
                validacion = true;
            }
            else
            {
                validacion = false;
            }

            Assert.IsTrue(validacion);
        }

        [TestMethod]
        public void Test_CalcularTanto_Carta()
        {            


        }
    }
}
