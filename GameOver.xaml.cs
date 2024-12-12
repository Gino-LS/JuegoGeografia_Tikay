using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JuegoGeografia
{
    public partial class GameOver : Window
    {
        private readonly int puntos; // Mantener readonly para asegurar inmutabilidad
        private readonly int tiempoTotalSegundos;
        private readonly string paisSeleccionado;
        private readonly string usuario;
        private readonly string avatarSeleccionado;

        public GameOver(int puntos, int tiempoTotalSegundos, string paisSeleccionado, string usuario, string avatarSeleccionado)
        {
            InitializeComponent();
            this.puntos = puntos; // Asigna directamente el valor recibido
            this.tiempoTotalSegundos = tiempoTotalSegundos >= 0 ? tiempoTotalSegundos : 0;
            this.paisSeleccionado = paisSeleccionado ?? "Desconocido";
            this.usuario = usuario ?? "Invitado";
            this.avatarSeleccionado = avatarSeleccionado ?? "";
            ActualizarBotonSonido(); // Actualiza la imagen del botón según el estado actual
            MostrarResultados();
        }

        
        private void MostrarResultados()
        {
            txtPuntosFinales.Text = $"Puntos: {puntos}";
            int minutos = tiempoTotalSegundos / 60;
            int segundos = tiempoTotalSegundos % 60;
            txtTiempoFinal.Text = $"Tiempo total: {minutos} min {segundos} seg";
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            // Regresar al juego para el mismo país
            Preguntas ventanaPreguntas = new Preguntas(paisSeleccionado, usuario, avatarSeleccionado);
            ventanaPreguntas.Show();
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            // Regresar a la pantalla principal
            MainWindow ventanaPrincipal = new MainWindow();
            ventanaPrincipal.Show();
            this.Close();
        }

        // Botón para alternar el sonido
        private void btnSound_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.ToggleSound();
            ActualizarBotonSonido();
        }

        private void ActualizarBotonSonido()
        {
            string soundImagePath = SoundManager.IsSoundOn() ? "Images/Sonido_ON.png" : "Images/Sonido_OFF.png";
            btnSound.Template = CreateButtonTemplate(soundImagePath);
        }

        private ControlTemplate CreateButtonTemplate(string imagePath)
        {
            var template = new ControlTemplate(typeof(Button));
            var imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.SourceProperty, new BitmapImage(new Uri(imagePath, UriKind.Relative)));
            imageFactory.SetValue(Image.StretchProperty, Stretch.Uniform);
            template.VisualTree = imageFactory;
            return template;
        }
    }
}
