using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Media;
using System.Windows.Controls;


namespace JuegoGeografia
{
    public partial class MapaSudamerica : Window
    {
        private string usuario;
        private string avatarSeleccionado;

        public MapaSudamerica(string usuario, string avatarSeleccionado)
        {
            
            InitializeComponent();
            ActualizarBotonSonido(); // Actualizar la imagen del botón según el estado actual
            this.usuario = usuario;
            this.avatarSeleccionado = avatarSeleccionado;
            
            MostrarUsuarioYAvatar();
        }

        /// <summary>
        /// Muestra el nombre del usuario y el avatar seleccionado en la interfaz
        /// </summary>
        private void MostrarUsuarioYAvatar()
        {
            txtUsuario.Text = usuario;
            imgAvatar.Source = new BitmapImage(new Uri(avatarSeleccionado, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Evento del botón "Atrás" para regresar a la pantalla de selección de avatar
        /// </summary>
        private void Atras_Click(object sender, MouseButtonEventArgs e)
        {
            SeleccionAvatar seleccionAvatar = new SeleccionAvatar(usuario);
            seleccionAvatar.Show();
            this.Close();
        }

        /// <summary>
        /// Evento al hacer clic en el mapa
        /// </summary>
        private void Country_Click(object sender, MouseButtonEventArgs e)
        {
            var elipse = sender as FrameworkElement;
            if (elipse != null)
            {
                string paisSeleccionado = elipse.Tag.ToString();

                // Pasa el usuario y avatar seleccionados
                Preguntas ventanaPreguntas = new Preguntas(paisSeleccionado, usuario, avatarSeleccionado);
                ventanaPreguntas.Show();
                this.Close();
            }
        }

        private bool isSoundOn = false; // Estado inicial del sonido apagado

        private void btnSound_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.ToggleSound(); // Alternar el sonido
            ActualizarBotonSonido();    // Actualizar la imagen del botón
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

