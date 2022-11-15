using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Jugador
    {
        private List<Carta> cartas;
        private int envido;
        private int puntos;
       
        public Jugador()
        {
            this.cartas = new List<Carta>();
            this.puntos = 0;
        }

        public List<Carta> Cartas { get => cartas; set => cartas = value; }
        public int Envido { get => envido; set => envido = value; }
        public int Puntos { get => puntos; set => puntos = value; }     
    }
}
