using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using System.Windows.Media.Imaging;

namespace JuegoGeografia
{
    public partial class Preguntas : Window
    {
        private MySqlConnection conn;
        private string paisSeleccionado;
        private string respuestaCorrecta = string.Empty;
        private int preguntaActual = 0;
        private const int totalPreguntas = 15;
        private int puntos = 0;
        private DispatcherTimer timer;
        private int tiempoRestante = 20;
        private bool transicionEnProgreso = false;
        private readonly List<int> preguntasUsadas = new List<int>();
        private double posicionVerticalInicial; // Posición inicial del personaje
        private double posicionPersonaje = 0;
        private const double avance = 70;
        private const double retroceso = 55;
        private int tiempoTotalEmpleado = 0;
        private string usuario;
        private string avatarSeleccionado;
        private bool juegoTerminado = false;
        private double puntosAcumulados = 0;

        public Preguntas(string pais, string usuario, string avatarSeleccionado)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.avatarSeleccionado = avatarSeleccionado;
            paisSeleccionado = pais;

            string connStr = "Server=localhost;Database=JuegoGeografia;Uid=root;Pwd=gralos;"; // Tu cadena de conexión
            conn = new MySqlConnection(connStr);

            CargarPreguntas(paisSeleccionado);
            ActualizarCabecera();
            ActualizarBotonSonido();
            InicializarTemporizador();
        }

        private void InicializarTemporizador()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante--;
                tiempoTotalEmpleado++;
                ActualizarCabecera();
            }
            else
            {
                if (!transicionEnProgreso)
                {
                    transicionEnProgreso = true;
                    timer.Stop();
                    // No se suma puntos por no responder
                    MostrarRetroalimentacion(null, Brushes.Red);
                    MoverPersonaje(false);
                    CargarSiguientePreguntaConRetraso();
                }
            }
        }

        private void CargarPreguntas(string pais)
        {
            try
            {
                conn.Open();

                // Asegura de que todas las preguntas se carguen sin saltos
                string query = "SELECT * FROM Preguntas WHERE pais = @pais";
                if (preguntasUsadas.Count > 0)
                {
                    query += $" AND id NOT IN ({string.Join(",", preguntasUsadas)})";
                }
                query += " ORDER BY id LIMIT 1"; // Ordenar por id en lugar de RAND() para secuencia

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pais", pais);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtPregunta.Text = reader["pregunta"].ToString();
                    opcion1.Content = reader["opcion1"].ToString();
                    opcion2.Content = reader["opcion2"].ToString();
                    opcion3.Content = reader["opcion3"].ToString();
                    respuestaCorrecta = reader["respuesta_correcta"].ToString();

                    int preguntaId = Convert.ToInt32(reader["id"]);
                    preguntasUsadas.Add(preguntaId);

                    preguntaActual++;
                }
                else
                {
                    MostrarGameOver();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la pregunta: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            ActualizarCabecera();
        }

        //Personaje

        private void MoverPersonaje(bool respuestaCorrecta)
        {
            double distanciaTotal = 800;
            double desplazamiento = distanciaTotal / totalPreguntas;

            if (respuestaCorrecta)
            {
                posicionPersonaje += desplazamiento;
            }
            else
            {
                posicionPersonaje -= desplazamiento / 2; // Retroceso menor para errores
            }

            // Limita la posición horizontal dentro de los límites permitidos
            if (posicionPersonaje < 0)
            {
                posicionPersonaje = 0; // Evita valores negativos
            }
            else if (posicionPersonaje > distanciaTotal)
            {
                posicionPersonaje = distanciaTotal; // Límite derecho (bandera)
            }

            // Mantiene la posición vertical fija
            double posicionVerticalFija = 91; // Valor fijo para mantener a Pikachu en línea recta
            AnimatedImage.Margin = new Thickness(posicionPersonaje, 0, 0, 91);

            // Verifica si ha llegado a la bandera
            if (posicionPersonaje >= distanciaTotal)
            {
                MessageBox.Show("¡Felicidades, has llegado a la meta!");
            }
        }

        

        private void VerificarRespuesta(string opcionSeleccionada)
        {
            if (transicionEnProgreso) return;

            transicionEnProgreso = true;
            timer.Stop();

            bool esCorrecta = string.Equals(opcionSeleccionada, respuestaCorrecta, StringComparison.OrdinalIgnoreCase);
            if (esCorrecta)
            {
                // Acumula puntos como double
                puntosAcumulados += 100.0 / totalPreguntas;
                MostrarRetroalimentacion(opcionSeleccionada, Brushes.Green);
                MoverPersonaje(true);
            }
            else
            {
                MostrarRetroalimentacion(opcionSeleccionada, Brushes.Red);
                MoverPersonaje(false);
            }

            CargarSiguientePreguntaConRetraso();
        }

        // Muestra la retroalimentación visual para la respuesta seleccionada
        private void MostrarRetroalimentacion(string opcionSeleccionada, Brush? color = null)
        {
            if (opcionSeleccionada != null && color != null)
            {
                foreach (Button btn in new[] { opcion1, opcion2, opcion3 })
                {
                    if (btn.Content.ToString() == opcionSeleccionada)
                    {
                        btn.Background = color;
                    }
                }
            }
        }

        

        // Carga la siguiente pregunta si quedan disponibles
        private void CargarSiguientePreguntaConRetraso()
        {
            if (preguntasUsadas.Count >= totalPreguntas)
            {
                MostrarGameOver();
                return;
            }

            DispatcherTimer delayTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            delayTimer.Tick += (s, e) =>
            {
                delayTimer.Stop();
                ReiniciarEstadoPregunta();
                CargarPreguntas(paisSeleccionado);
            };
            delayTimer.Start();
        }

        // Reinicia el estado para la siguiente pregunta
        private void ReiniciarEstadoPregunta()
        {
            ReiniciarBotones();
            tiempoRestante = 20;
            transicionEnProgreso = false;
            ActualizarCabecera();
            timer.Start();
        }

        // Actualiza la cabecera con la información actual
        private void ActualizarCabecera()
        {
            txtNumeroPreguntas.Text = $"{preguntaActual}/{totalPreguntas}";
            // Asegúrate de que estás mostrando 'puntosAcumulados' si es lo que acumulas
            txtPuntos.Text = $"Puntos: {(int)Math.Round(puntosAcumulados)}";
            txtTiempo.Text = $"Tiempo: {tiempoRestante} seg";
        }


        // Maneja el evento de clic en las opciones de respuesta
        private void Opcion_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button boton && boton.Content != null)
            {
                VerificarRespuesta(boton.Content.ToString());
            }
        }

        // Regresa al mapa de selección de país
        private void btnSelectCountry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MapaSudamerica mapaSudamerica = new MapaSudamerica(usuario, avatarSeleccionado);
                mapaSudamerica.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al regresar al mapa: " + ex.Message);
            }
        }

        // Muestra la ventana de Game Over
        private void MostrarGameOver()
        {
            if (juegoTerminado) return;
            juegoTerminado = true;

            timer.Stop();

            // Redondea el puntaje final aquí
            int puntosFinales = (int)Math.Round(puntosAcumulados);

            GameOver ventanaGameOver = new GameOver(puntosFinales, tiempoTotalEmpleado, paisSeleccionado, usuario, avatarSeleccionado);
            ventanaGameOver.Show();
            this.Close();
        }

        // Alterna el sonido en la aplicación
        private void btnSound_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.ToggleSound();
            ActualizarBotonSonido();
        }

        // Actualiza el botón de sonido según el estado actual
        private void ActualizarBotonSonido()
        {
            string soundImagePath = SoundManager.IsSoundOn() ? "Images/Sonido_ON.png" : "Images/Sonido_OFF.png";
            btnSound.Template = CreateButtonTemplate(soundImagePath);
        }

        // Crea una plantilla para el botón de sonido
        private ControlTemplate CreateButtonTemplate(string imagePath)
        {
            var template = new ControlTemplate(typeof(Button));
            var imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.SourceProperty, new BitmapImage(new Uri(imagePath, UriKind.Relative)));
            imageFactory.SetValue(Image.StretchProperty, Stretch.Uniform);
            template.VisualTree = imageFactory;
            return template;
        }

        // Reinicia los estilos y estados de los botones de opciones a su estado original
        private void ReiniciarBotones()
        {
            foreach (Button btn in new[] { opcion1, opcion2, opcion3 })
            {
                btn.Background = Brushes.Orange;
                btn.IsEnabled = true;
            }
        }

    }
}
