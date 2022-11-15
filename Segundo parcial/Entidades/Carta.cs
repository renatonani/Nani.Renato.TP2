using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entidades
{
    public class Carta
    {
        private int numero;
        private Palos palo;
        private int valor;
        private bool cartaTirada;
        
        public Carta(int numero, Palos palo, int valor)
        {
            this.numero = numero;
            this.palo = palo;
            this.valor = valor;
            this.cartaTirada = false;
        }

        public int Numero { get => numero; set => numero = value; }
        public Palos Palo { get => palo; set => palo = value; }
        public int Valor { get => valor; set => valor = value; }
        public bool CartaTirada { get => cartaTirada; set => cartaTirada = value; }
        /// <summary>
        /// Permite crear un archivo JSON con 40 cartas de Truco.
        /// </summary>
        public static void SerializarCartasTruco()
        {
            StreamWriter streamWriter = new StreamWriter("Cartas.json", true);
            string jsonString;
            Carta carta;
            int valor = 0;
            foreach (Palos palo in Enum.GetValues(typeof(Palos)))
            {                
                for (int i = 1; i < 13; i++)
                {
                    if (!(i == 8 || i == 9))
                    {   
                        
                        switch (i)
                        {
                            case 1:
                                if(palo == Palos.Espada)
                                {
                                    valor = 14;
                                }
                                else
                                {
                                    if(palo == Palos.Basto)
                                    {
                                        valor = 13;
                                    }
                                    else
                                    {
                                        valor = 8;
                                    }
                                }
                                break;
                            case 2:
                                valor = 9;
                                break;
                            case 3:
                                valor = 10;
                                break;
                            case 4:
                                valor = 1;
                                break;
                            case 5:
                                valor = 2;
                                break;
                            case 6:
                                valor = 3;
                                break;
                            case 7:
                                if (palo == Palos.Espada)
                                {
                                    valor = 12;
                                }
                                else
                                {
                                    if (palo == Palos.Oro)
                                    {
                                        valor = 11;
                                    }
                                    else
                                    {
                                        valor = 4;
                                    }
                                }
                                break;
                            case 10:
                                valor = 5;
                                break;
                            case 11:
                                valor = 6;
                                break;
                            case 12:
                                valor = 7;
                                break;

                        }
                        carta = new Carta(i, palo, valor);
                        jsonString = JsonSerializer.Serialize(carta);
                        streamWriter.WriteLine(jsonString);
                    }
                }
            }
            streamWriter.Close();
            streamWriter.Dispose();
        }
        /// <summary>
        /// Crea una lista con 40 cartas obtenidas a partir de un archivo JSON y la retorna.
        /// </summary>
        /// <returns></returns>
        public static List<Carta> ObtenerMazo()
        {
            List<Carta> mazo = new List<Carta>();
            Carta carta;
            StreamReader streamReader;
            string cartaString;
            try
            {
                streamReader = File.OpenText("Cartas.json");
                while ((cartaString = streamReader.ReadLine()) is not null)
                {
                    carta = JsonSerializer.Deserialize<Carta>(cartaString);
                    mazo.Add(carta);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }           

            return mazo;
        }
        /// <summary>
        /// Crea una lista de cartas donde se agregarán 3 cartas a partir del mazo recibido por parámetro.
        /// Al ser agregada una carta a la nueva lista, es eliminada del mazo original para evitar que la misma pueda repetirse.
        /// </summary>
        /// <param name="mazo"></param>
        /// <returns> Retorna una lista con 3 cartas que podrá ser asigana a un jugador </returns>
        public static List<Carta> Repartir(List<Carta> mazo)
        {
            Random n = new Random();           
            List<Carta> cartasJugador = new List<Carta>();

            for(int i = 0; i<3; i++)
            {
                int numero = n.Next(0, mazo.Count);
                cartasJugador.Add(mazo[numero]);
                mazo.RemoveAt(numero);
            }       
            
            return cartasJugador;
        }               

        public static bool operator == (Carta carta1, Carta carta2)
        {
            return (carta1.palo == carta2.palo);
        }
        public static bool operator !=(Carta carta1, Carta carta2)
        {
            return !(carta1 == carta2);
        }
        public override bool Equals(object obj)
        {
            bool validacion = false;

            if (obj is Carta)
            {
                validacion = (this == ((Carta)obj));
            }
            return validacion;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Calcula los posibles envidos envidos en base a las 3 cartas de un jugador y retorna el más alto posible.
        /// </summary>
        /// <param name="cartasJugador"></param>
        /// <returns></returns>
        public static int CalcularTantoJugador(List<Carta> cartasJugador)
        {
            int retorno = -1;
            int envido1 = -1;
            int envido2 = -1;
            int envido3 = -1;

            if(cartasJugador != null && cartasJugador.Count > 0)
            {
                envido1 = Carta.CalcularTanto(cartasJugador[0], cartasJugador[1]);
                envido2 = Carta.CalcularTanto(cartasJugador[0], cartasJugador[2]);
                envido3 = Carta.CalcularTanto(cartasJugador[1], cartasJugador[2]);

                if (envido1 > envido2 && envido1 > envido3)
                {
                    retorno = envido1;
                }
                else
                {
                    if (envido2 > envido3)
                    {
                        retorno = envido2;
                    }
                    else
                    {
                        retorno = envido3;
                    }
                }

            }

            return retorno;
        }
        /// <summary>
        /// Calcula el valor de envido que forman dos cartas puntuales.
        /// </summary>
        /// <param name="carta1"></param>
        /// <param name="carta2"></param>
        /// <returns></returns>
        private static int CalcularTanto(Carta carta1, Carta carta2)
        {

            int tanto = 0;
            if (carta1 == carta2)
            {
                tanto = 20;
                if (carta1.numero<10)
                {
                    tanto += carta1.numero;
                }

                if (carta2.numero < 10)
                {
                    tanto += carta2.numero;
                }
            }
            else
            {                
                if(carta1.numero < 10 && carta2.numero < 10)
                {
                    tanto += carta1.numero;
                }
                else
                {
                    if (carta1.numero > carta2.numero)
                    {
                        tanto += carta1.numero;
                    }
                    else
                    {
                        tanto += carta2.numero;
                    }
                }                           
                                    
            }
            return tanto;
        }

    }



}
