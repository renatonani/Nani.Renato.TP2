using System;

namespace Entidades
{
    public class Usuario
    {
        private string user;
        private string password;
        private int id;
        private int partidasJugadas;
        private int partidasGanadas;
        private int partidasPerdidas;
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public int Id { get => id; set => id = value; }
        public int PartidasJugadas { get => partidasJugadas; set => partidasJugadas = value; }
        public int PartidasGanadas { get => partidasGanadas; set => partidasGanadas = value; }
        public int PartidasPerdidas { get => partidasPerdidas; set => partidasPerdidas = value; }

        public static bool operator ==(Usuario usuario1, Usuario usuario2)
        {
            return usuario1.user == usuario2.user && usuario1.password == usuario2.password;            
        }

        public static bool operator !=(Usuario usuario1, Usuario usuario2)
        {
            return !(usuario1 == usuario2);
        }

        public override bool Equals(object obj)
        {
            bool validacion = false;

            if (obj is Usuario)
            {
                validacion = (this == ((Usuario)obj));
            }
            return validacion;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    
}
