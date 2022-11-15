using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Parcial_segundo
{
    public partial class SalaDeJuego : Form
    {
        private Jugador jugador1;
        private Jugador jugador2;
        private List<Carta> mazo;
        private Jugador jugadorTurno;
        private int manosGanadasJugador1;
        private int manosGanadasJugador2;
        private int puntosTruco;
        private bool trucoCantado;
        private bool envidoCantado;
        private bool faltaEnvido;
        private int puntosEnvido;
        private List<Carta> cartasTiradas;
        private int mano;
        private int ronda;
        private bool parda;
        private Usuario usuario;
        private AccesoDatosUsuario datos;
        private bool hayGanador;
        DateTime timer;



        public SalaDeJuego(Usuario usuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.usuario = usuario;
            this.datos = new AccesoDatosUsuario();
            this.jugador1 = new Jugador();
            this.jugador2 = new Jugador();
            this.cartasTiradas = new List<Carta>();          
        }

        private void SalaDeJuego_Load(object sender, EventArgs e)
        {
            timer = new DateTime();
            Task.Run(() => this.CambiarDuracion());
            this.puntosTruco = 0;
            this.puntosEnvido = 0;
            this.trucoCantado = false;
            this.envidoCantado = false;
            this.faltaEnvido = false;
            this.parda = false;
            this.mano = 1;
            this.ronda = 1;
            this.manosGanadasJugador1 = 0;
            this.manosGanadasJugador2 = 0;
            this.labelPuntosJug1.Text = this.jugador1.Puntos.ToString();
            this.labelPuntosJug2.Text = this.jugador2.Puntos.ToString();            
            this.hayGanador = false;
            this.mazo = Carta.ObtenerMazo();
            this.jugador1.Cartas = Carta.Repartir(this.mazo);
            this.jugador1.Envido = Carta.CalcularTantoJugador(this.jugador1.Cartas);
            this.jugador2.Cartas = Carta.Repartir(this.mazo);
            this.jugador2.Envido = Carta.CalcularTantoJugador(this.jugador2.Cartas);
            this.labelN4.Visible = false;
            this.labelN5.Visible = false;
            this.usuario.PartidasJugadas++;            

            this.CambiarTurno();
            this.labelRonda.Text = this.ronda.ToString();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.cartasTiradas.Add(this.jugadorTurno.Cartas[0]);
            this.TirarCarta(1);                       
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            this.cartasTiradas.Add(this.jugadorTurno.Cartas[1]);
            this.TirarCarta(2);                     
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            this.cartasTiradas.Add(this.jugadorTurno.Cartas[2]);
            this.TirarCarta(3);                       
        }        
        /// <summary>
        /// Recibe como parámetro cuál de las 3 cartas que se le muestran al jugador será la que va a tirar.
        /// En base al índice, acomoda la interfaz gráfica para mostrar la carta seleccionada en la mesa y dejar de mostrarla 
        /// entre las cartas del jugaor en la próxima mano.
        /// </summary>
        /// <param name="cartaSeleccionada"></param>
        private void TirarCarta(int cartaSeleccionada)
        {
            switch(cartaSeleccionada)
            {
                case 1:
                    this.labelN1.Visible = false;
                    this.pictureBox1.Visible = false;
                    if(jugadorTurno == jugador1)
                    {
                        this.labelN4.Visible = true;
                        this.pictureBox4.Visible = true;
                        this.labelN4.Text = this.labelN1.Text;
                        this.pictureBox4.Image = this.pictureBox1.Image;
                    }
                    else
                    {
                        this.labelN5.Visible = true;
                        this.pictureBox5.Visible = true;
                        this.labelN5.Text = this.labelN1.Text;
                        this.pictureBox5.Image = this.pictureBox1.Image;
                    }                    
                    this.jugadorTurno.Cartas[0].CartaTirada = true;
                    break;
                case 2:
                    this.labelN2.Visible = false;
                    this.pictureBox2.Visible = false;
                    if(jugadorTurno == jugador1)
                    {
                        this.labelN4.Visible = true;
                        this.pictureBox4.Visible = true;
                        this.labelN4.Text = this.labelN2.Text;
                        this.pictureBox4.Image = this.pictureBox2.Image;
                    }
                    else
                    {
                        this.labelN5.Visible = true;
                        this.pictureBox5.Visible = true;
                        this.labelN5.Text = this.labelN2.Text;
                        this.pictureBox5.Image = this.pictureBox2.Image;
                    }
                    
                    this.jugadorTurno.Cartas[1].CartaTirada = true;
                    break;
                case 3:
                    this.labelN3.Visible = false;
                    this.pictureBox3.Visible = false;
                    if(jugadorTurno == jugador1)
                    {
                        this.labelN4.Visible = true;
                        this.pictureBox4.Visible = true;
                        this.labelN4.Text = this.labelN3.Text;
                        this.pictureBox4.Image = this.pictureBox3.Image;
                    }
                    else
                    {
                        this.labelN5.Visible = true;
                        this.pictureBox5.Visible = true;
                        this.labelN5.Text = this.labelN3.Text;
                        this.pictureBox5.Image = this.pictureBox3.Image;
                    }                    
                    this.jugadorTurno.Cartas[2].CartaTirada = true;
                    break;                    
            }
            int ultimoJugadorQueTiro = 0;
            if (this.jugadorTurno == this.jugador1)
            {
                ultimoJugadorQueTiro = 1;
            }
            else
            {
                ultimoJugadorQueTiro = 2;
            }
            this.ActualizarManos(ultimoJugadorQueTiro);
        }
        /// <summary>
        /// Si ambos jugadores ya han tirado una carta, asigna el ganador de la mano. En caso de que se haya jugado un falta envido asigna al
        /// ganador de la partida.
        /// Si aún no se han tirado las dos cartas, cambia el turno al siguiente jugador.
        /// </summary>
        /// <param name="ultimoJugadorQueTiro"></param>
        private void ActualizarManos(int ultimoJugadorQueTiro)
        {
            if (this.cartasTiradas.Count == 2)
            {
                if(this.faltaEnvido == true)
                {
                    MessageBox.Show($"¡El jugador {ultimoJugadorQueTiro} ha ganado la partida con {this.jugadorTurno.Envido} de envido!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                if (ultimoJugadorQueTiro == 1)
                {
                    this.AsignarGanadorMano(1);
                }
                else
                {
                    this.AsignarGanadorMano(2);
                }               
                if (this.mano == 4)
                {
                    this.AsignarGanadorRonda();
                    this.ActualizarRondas();
                }
                else
                {
                    this.ActualizarRondas();
                    this.cartasTiradas.Clear();                    
                }                

            }
            else
            {
                this.CambiarTurno();
            }           
        }
        /// <summary>
        /// Si ya se han jugado 3 manos asigna el ganador de la ronda y reparte nuevamente las cartas para cada jugador.
        /// Si ya se han jugado 4 rondas asigna el ganador de la partida.  
        /// </summary>
        private void ActualizarRondas()
        {
            if (this.mano == 4)
            {
                if (this.ronda != 4)
                {
                    this.AsignarGanadorRonda();
                }
                else
                {
                    this.labelRonda.Text = "Partida finalizada";
                    this.AsignarGanadorPartida();
                }

                this.mano = 1;
                this.ronda++;
                this.labelRonda.Text = this.ronda.ToString();
                this.mazo = Carta.ObtenerMazo();
                this.jugador1.Cartas.Clear();
                this.jugador1.Cartas = Carta.Repartir(this.mazo);
                this.jugador1.Envido = Carta.CalcularTantoJugador(this.jugador1.Cartas);
                this.jugador2.Cartas.Clear();
                this.jugador2.Cartas = Carta.Repartir(this.mazo);
                this.jugador2.Envido = Carta.CalcularTantoJugador(this.jugador2.Cartas);                
                this.envidoCantado = false;
                this.trucoCantado = false;                

                if (this.ronda == 2 || this.ronda == 4)
                {
                    this.jugadorTurno = this.jugador2;
                    this.labelTurno.Text = "Jugador 2";
                }
                else
                {
                        this.jugadorTurno = this.jugador1;
                        this.labelTurno.Text = "Jugador 1";                   
                }
                                
                this.parda = false;
                this.manosGanadasJugador1 = 0;
                this.manosGanadasJugador2 = 0;
                this.MostrarCartasJugadores();
                this.labelN4.Visible = false;
                this.pictureBox4.Visible = false;
                this.labelN5.Visible = false;
                this.pictureBox5.Visible = false;            
                this.labelPuntosJug1.Text = this.jugador1.Puntos.ToString();
                this.labelPuntosJug2.Text = this.jugador2.Puntos.ToString();
            }            
        }
        /// <summary>
        /// En base a los puntos de cada jugador asigna el ganador de la partida. En caso de que en la última ronda ambos jugadores tengan la misma
        /// cantidad de puntos, la partida se considera un empate.
        /// </summary>
        private void AsignarGanadorPartida()
        {
            string tiempo = $"\nLa partida ha durado {this.labelDuracion.Text}";
            if(this.jugador1.Puntos > this.jugador2.Puntos)
            {
                MessageBox.Show($"¡El jugador 1 ha ganado la partida con {this.jugador1.Puntos} puntos!{tiempo}", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.usuario.PartidasGanadas++;
                this.datos.ModificarDato(this.usuario);
                this.hayGanador = true;
                this.Close();
            }
            else
            {
                if(this.jugador2.Puntos > this.jugador1.Puntos)
                {
                    MessageBox.Show($"¡El jugador 2 ha ganado la partida con {this.jugador2.Puntos} puntos{tiempo}!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.usuario.PartidasPerdidas++;
                    this.datos.ModificarDato(this.usuario);
                    this.hayGanador = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"¡Ambos jugadores empataron con {this.jugador1.Puntos} puntos!{tiempo}", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.datos.ModificarDato(this.usuario);
                    this.hayGanador = true;
                    this.Close();
                }
                   
                
            }
        }
        /// <summary>
        /// En base al índice que recibe por parámetro, muestra en un MessageBox quién es el ganador de la partida.
        /// </summary>
        /// <param name="ganador"></param>
        private void AsignarGanadorPartida(int ganador)
        {
            if(ganador == 1)
            {
                MessageBox.Show($"Ha ganado el jugador 1","Anuncio",MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.usuario.PartidasGanadas++;
                this.datos.ModificarDato(this.usuario);                
                this.hayGanador = true;
                this.Close();
            }
            else
            {
                MessageBox.Show($"Ha ganado el jugador 2", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.usuario.PartidasPerdidas++;
                this.datos.ModificarDato(this.usuario);                
                this.hayGanador = true;
                this.Close();
            }
            
        }
        /// <summary>
        /// Anuncia mediante un MessageBox quién fue el ganador de la ronda.
        /// </summary>
        private void AsignarGanadorRonda()
        {
            if(this.manosGanadasJugador1 == 2)
            {
                MessageBox.Show($"¡El jugador 1 ha ganado la ronda!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.richTextBox1.Text += $"\n¡El jugador 1 ha ganado la ronda {this.ronda}!";
            }
            else
            {
                if(this.manosGanadasJugador1 == 1 && this.parda == true)
                {
                    MessageBox.Show($"¡El jugador 1 ha ganado la ronda!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.richTextBox1.Text += $"\n¡El jugador 1 ha ganado la ronda {this.ronda}!";
                }
            }
            
            if(this.manosGanadasJugador2 == 2)
            {
                MessageBox.Show($"¡El jugador 2 ha ganado la ronda!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.richTextBox1.Text += $"\n¡El jugador 2 ha ganado la ronda {this.ronda}!";
            }
            else
            {
                if(this.manosGanadasJugador2 == 1 && this.parda == true)
                {
                    MessageBox.Show($"¡El jugador 2 ha ganado la ronda!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.richTextBox1.Text += $"\n¡El jugador 1 ha ganado la ronda {this.ronda}!";
                }
            }            
        }

        /// <summary>
        /// Dependiendo de quién haya sido el último jugador en tirar, compara las cartas tiradas por los jugadores
        /// y modifica el label con el ganador. En caso de que el jugador gane dos manos le asigna
        /// los puntos correspondientes y cambia la ronda.
        /// </summary>
        /// <param name="ultimoJugadorQueTiro"></param>
        private void AsignarGanadorMano(int ultimoJugadorQueTiro)
        {
            if(ultimoJugadorQueTiro == 1)
            {
                if (this.CompararCartasMano() == 1)
                {
                    this.manosGanadasJugador1++;
                    this.labelGanadorMano.Text = "Jugador 1";
                    this.mano++;
                    if (this.manosGanadasJugador1 ==2 || this.parda == true)
                    {                        
                        this.mano = 4;
                        this.jugador1.Puntos +=  1 + this.puntosTruco + this.puntosEnvido;                        
                        this.ActualizarRondas();
                    }               
                }
                else
                {
                    if (this.CompararCartasMano() == 2)
                    {
                        this.labelGanadorMano.Text = "Jugador 2";
                        this.manosGanadasJugador2++;
                        this.mano++;
                        this.CambiarTurno();
                        if (this.manosGanadasJugador2 == 2 || this.parda == true)
                        {                           
                            this.mano = 4;
                            this.jugador2.Puntos += 1 + this.puntosTruco + this.puntosEnvido;                            
                            this.ActualizarRondas();
                        }
                    }
                    else
                    {
                        this.labelGanadorMano.Text = "Parda";
                        this.mano++;
                        this.parda = true;
                        this.CambiarTurno();

                    }
                }
            }
            else
            {
                if (this.CompararCartasMano() == 1)
                {
                    this.labelGanadorMano.Text = "Jugador 2";
                    this.manosGanadasJugador2++;
                    this.mano++;
                    if (this.manosGanadasJugador2 == 2 || this.parda == true)
                    {                       
                        this.mano = 4;
                        this.jugador2.Puntos += 1 + this.puntosTruco + this.puntosEnvido;                        
                        this.ActualizarRondas();
                    }                   
                }
                else
                {
                    if (this.CompararCartasMano() == 2)
                    {
                        this.labelGanadorMano.Text = "Jugador 1";
                        this.manosGanadasJugador1++;
                        this.mano++;
                        this.CambiarTurno();
                        if (this.manosGanadasJugador1 == 2 || this.parda == true)
                        {
                            this.mano = 4;
                            this.jugador1.Puntos += 1 + this.puntosTruco + this.puntosEnvido;                              
                            this.ActualizarRondas();
                        }
                    }
                    else
                    {
                        this.labelGanadorMano.Text = "Parda";
                        this.mano++;
                        this.CambiarTurno();
                        this.parda = true;
                    }
                }
            }
            this.labelPuntosJug1.Text = this.jugador1.Puntos.ToString();
            this.labelPuntosJug2.Text = this.jugador2.Puntos.ToString();
        }
        /// <summary>
        /// Compara las cartas tiradas por los jugadores.
        /// </summary>
        /// <returns> Retorna 1 si la segunda carta tirada es la mayor, 0 si la primer carta tirada es la mayor o 2 si son iguales.</returns>
        private int CompararCartasMano()
        {
            int retorno = 2;           
            
            if (this.cartasTiradas[1].Valor > this.cartasTiradas[0].Valor)
            {
                retorno = 1;
            }
            else
            {
                if (this.cartasTiradas[1].Valor == this.cartasTiradas[0].Valor)
                {
                    retorno = 0;
                }
            }  
            
            return retorno;
        }
        /// <summary>
        /// Cambia el turno del jugador que le toca jugar y muestra las cartas correspondientes.
        /// </summary>
        private void CambiarTurno()
        {
            if (this.jugadorTurno == this.jugador1)
            {
                this.jugadorTurno = this.jugador2;
                this.labelTurno.Text = "Jugador 2";
            }
            else
            {
                this.jugadorTurno = this.jugador1;
                this.labelTurno.Text = "Jugador 1";
            }        

            this.MostrarCartasJugadores();
        }
        /// <summary>
        /// Muestra las cartas correspondientes al jugador que le toca jugar.
        /// </summary>
        private void MostrarCartasJugadores()
        {
            if (jugadorTurno.Cartas[0].CartaTirada == false)
            {
                this.labelN1.Visible = true;
                this.pictureBox1.Visible = true;
                this.labelN1.Text = this.jugadorTurno.Cartas[0].Numero.ToString();
                this.pictureBox1.Image = Image.FromFile(@$"..\..\..\Resources\{this.jugadorTurno.Cartas[0].Palo.ToString()}.png");
            }
            else
            {
                this.labelN1.Visible = false;
                this.pictureBox1.Visible = false;
            }
            if (jugadorTurno.Cartas[1].CartaTirada == false)
            {
                this.labelN2.Visible = true;
                this.pictureBox2.Visible = true;
                this.labelN2.Text = this.jugadorTurno.Cartas[1].Numero.ToString();
                this.pictureBox2.Image = Image.FromFile(@$"..\..\..\Resources\{this.jugadorTurno.Cartas[1].Palo.ToString()}.png");
            }
            else
            {
                this.labelN2.Visible = false;
                this.pictureBox2.Visible = false;
            }
            if (jugadorTurno.Cartas[2].CartaTirada == false)
            {
                this.labelN3.Visible = true;
                this.pictureBox3.Visible = true;
                this.labelN3.Text = this.jugadorTurno.Cartas[2].Numero.ToString();
                this.pictureBox3.Image = Image.FromFile(@$"..\..\..\Resources\{this.jugadorTurno.Cartas[2].Palo.ToString()}.png");
            }
            else
            {
                this.labelN3.Visible = false;
                this.pictureBox3.Visible = false;
            }
        }

        private void buttonTruco_Click(object sender, EventArgs e)
        {
            if (this.trucoCantado == false)
            {
                int numeroJugador;
                int numeroJugadorRival;
                if (this.jugadorTurno == this.jugador1)
                {
                    numeroJugador = 1;
                    numeroJugadorRival = 2;
                }
                else
                {
                    numeroJugador = 2;
                    numeroJugadorRival = 1;
                }

                FormTruco formTruco = new FormTruco(0);//Canta truco
                this.richTextBox1.Text += $"\n¡El jugador {numeroJugador} ha cantado truco!";
                switch (formTruco.ShowDialog())
                {
                    case DialogResult.OK://Quiere 
                        this.puntosTruco = 1;                        
                        this.richTextBox1.Text += $"\n¡El jugador {numeroJugadorRival} ha aceptado el truco!";
                        break;

                    case DialogResult.Yes://Quiere re truco

                        FormTruco formReTruco = new FormTruco(1);//El contrario Canta re truco
                        this.richTextBox1.Text += $"\n¡El jugador {numeroJugadorRival} ha cantado re truco!";
                        switch (formReTruco.ShowDialog())
                        {
                            case DialogResult.OK://Quiere
                                this.puntosTruco = 2;                                
                                this.richTextBox1.Text += $"\n¡El jugador {numeroJugador} ha aceptado el re truco!";
                                break;

                            case DialogResult.Yes://Quiere vale 4
                                FormTruco formValeCuatro = new FormTruco(2);//Canta vale 4
                                this.richTextBox1.Text += $"\n¡El jugador {numeroJugador} ha cantado vale 4!";
                                switch (formValeCuatro.ShowDialog())
                                {
                                    case DialogResult.OK://Quiere
                                        this.richTextBox1.Text += $"\n¡El jugador {numeroJugadorRival} ha aceptado el vale 4!";
                                        this.puntosTruco = 3;                                        
                                        break;
                                    case DialogResult.Cancel://No quiere vale 4
                                        this.richTextBox1.Text += $"\n¡El jugador {numeroJugadorRival} ha rechazado el vale 4!";
                                        if (numeroJugador == 1)
                                        {
                                            this.jugador1.Puntos += 3;
                                            this.manosGanadasJugador1 = 2;
                                            this.mano = 4;
                                            this.ActualizarRondas();
                                        }
                                        else
                                        {
                                            this.jugador2.Puntos += 3;
                                            this.manosGanadasJugador2 = 2;
                                            this.mano = 4;
                                            this.ActualizarRondas();
                                        }
                                        break;
                                }
                                break;

                            case DialogResult.Cancel://No quiere re truco
                                this.richTextBox1.Text += $"\n¡El jugador {numeroJugador} ha rechazado el re truco!";
                                if (numeroJugador == 1)
                                {
                                    this.jugador2.Puntos += 2;
                                    this.manosGanadasJugador2 = 2;
                                    this.mano = 4;
                                    this.ActualizarRondas();

                                }
                                else
                                {
                                    this.jugador1.Puntos += 2;
                                    this.manosGanadasJugador1 = 2;
                                    this.mano = 4;
                                    this.ActualizarRondas();

                                }

                                break;
                        }
                        break;

                    case DialogResult.Cancel://No quiere truco
                        this.richTextBox1.Text += $"\n¡El jugador {numeroJugadorRival} ha rechazado el truco!";
                        if (numeroJugador == 1)
                        {
                            this.jugador1.Puntos += 1;
                            this.manosGanadasJugador1 = 2;
                            this.mano = 4;
                            this.ActualizarRondas();

                        }
                        else
                        {
                            this.jugador2.Puntos += 1;
                            this.manosGanadasJugador2 = 2;
                            this.mano = 4;
                            this.ActualizarRondas();
                        }
                        break;                        
                }
                this.trucoCantado = true;

            }
            else
            {
                MessageBox.Show("¡El truco ya fue cantado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEnvido_Click(object sender, EventArgs e)
        {
            if (this.mano == 1)
            {
                if(this.envidoCantado == false)
                {
                    if (this.trucoCantado == false)
                    {
                        int numeroJugador;
                        int numeroJugadorRival;
                        if (this.jugadorTurno == this.jugador1)
                        {
                            numeroJugador = 1;
                            numeroJugadorRival = 2;
                        }
                        else
                        {
                            numeroJugador = 2;
                            numeroJugadorRival = 1;
                        }
                        FormEnvido formEnvido = new FormEnvido(0);//Canta envido
                        this.envidoCantado = true;
                        this.richTextBox1.Text += $"\n¡El jugador {numeroJugador} ha cantado envido";
                        switch (formEnvido.ShowDialog())
                        {
                            case DialogResult.OK://Quiere
                                this.puntosEnvido = 2;
                                this.richTextBox1.Text += $"\n¡El jugador {numeroJugadorRival} ha aceptado el envido!";
                                this.AnunciarGanadorEnvido();
                                break;
                            case DialogResult.Yes://El contrario canta falta envido
                                FormEnvido formFaltaEnvido = new FormEnvido(1);//Canta falta envido
                                this.richTextBox1.Text += $"\n¡El jugador {numeroJugadorRival} ha cantado falta envido!";
                                switch (formFaltaEnvido.ShowDialog())
                                {
                                    case DialogResult.OK:
                                        this.richTextBox1.Text += $"\n¡El jugador {numeroJugador} ha aceptado el falta envido!";
                                        this.faltaEnvido = true;
                                        break;
                                    case DialogResult.Cancel:
                                        this.richTextBox1.Text += $"\n¡El jugador {numeroJugador} ha rechazado el falta envido!";
                                        break;

                                }
                                break;
                            case DialogResult.Cancel://No quiere envido
                                this.richTextBox1.Text += $"\n¡El jugador {numeroJugadorRival} ha rechazado el envido!";
                                if (numeroJugador == 1)
                                {
                                    this.jugador1.Puntos += 1;

                                }
                                else
                                {
                                    this.jugador2.Puntos += 1;

                                }
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("¡El envido solo puede ser cantado antes del truco!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("¡El envido ya fue cantado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("¡El envido solo puede ser cantado en la primer mano!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }  
        /// <summary>
        /// Compara los envidos de cada uno de los jugadores y anuncia al ganador. En caso de que ambos jugadores tengan el mismo envido gana
        /// el que haya sido mano en esa ronda.
        /// </summary>
        private void AnunciarGanadorEnvido()
        {
            if(this.jugador1.Envido > this.jugador2.Envido)
            {
                MessageBox.Show($"¡El jugador 1 ha ganado el envido con {this.jugador1.Envido}!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (this.jugador2.Envido > this.jugador1.Envido)
                {
                    MessageBox.Show($"¡El jugador 2 ha ganado el envido con {this.jugador2.Envido}!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else//hay empate de envido
                {
                    if(this.ronda==2 || this.ronda == 4)
                    {
                        MessageBox.Show($"¡Hubo empate en el envido pero el jugador 2 ha ganado por mano!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"¡Hubo empate en el envido pero el jugador 1 ha ganado por mano!", "Anuncio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        /// <summary>
        /// Asigna como ganador al jugador contrario al que se haya ido al mazo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIrseMazo_Click(object sender, EventArgs e)
        {            
            this.mano = 4;
            if (this.jugadorTurno == this.jugador1)
            {                
                this.jugador2.Puntos +=  1 + this.puntosTruco + this.puntosEnvido;
                this.manosGanadasJugador2 = 2;                
            }
            else
            {
                this.jugador1.Puntos += 1 + this.puntosTruco + this.puntosEnvido;
                this.manosGanadasJugador1 = 2;                
            }
            this.labelPuntosJug1.Text = this.jugador1.Puntos.ToString();
            this.labelPuntosJug2.Text = this.jugador2.Puntos.ToString();
            this.ActualizarRondas();
        }

        private void SalaDeJuego_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.ronda!=4 && this.hayGanador==false)
            {
                int numeroJugador;
                if(this.jugadorTurno == this.jugador1)
                {
                    numeroJugador = 2;
                }
                else
                {
                    numeroJugador = 1;
                }
                if(MessageBox.Show($"Si cierra la sala de juego se dará por ganador al jugador {numeroJugador}\n¿Está seguro de que desea continuar?", "Precaución",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(numeroJugador==2)
                    {                        
                        this.AsignarGanadorPartida(2);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {                       
                        this.AsignarGanadorPartida(1);
                        this.DialogResult = DialogResult.OK;
                    }                   
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        /// <summary>
        /// Modifica el tiempo de la partida agregándole 1 segundo.
        /// </summary>
        private void AsignarDuracion()
        {            
            if (this.labelDuracion.InvokeRequired == true)
            {
                Action CambiarTiempo = new Action(this.AsignarDuracion);
                this.labelDuracion.Invoke(CambiarTiempo);
            }
            else
            {
                this.timer = this.timer.AddSeconds(1);
                this.labelDuracion.Text = $"{this.timer:T}";
            }
        }
        /// <summary>
        /// Se encarga de llamar a la función que permite modificar la duracion de la partida cada 1 segundo.
        /// </summary>
        private void CambiarDuracion()
        {
            while(true)
            {
                Thread.Sleep(1000);
                this.AsignarDuracion();
            }
        }
    }
}
