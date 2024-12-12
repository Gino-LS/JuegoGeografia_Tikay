using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Media;
using System.Windows.Media;

namespace JuegoGeografia
{
    public partial class SeleccionAvatar : Window
    {
        private string usuario; // Nombre del usuario
        private string avatarSeleccionado; // Ruta del avatar seleccionado

        public SeleccionAvatar(string usuario)
        {
            
            InitializeComponent();
            ActualizarBotonSonido(); // Actualizar la imagen del botón según el estado actual
            this.usuario = usuario;
            avatarSeleccionado = string.Empty;
        }

        /// <summary>
        /// Evento cuando se selecciona un avatar
        /// </summary>
        private void AvatarSelected(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Verifica si el objeto es una imagen antes de usarlo
                if (sender is Image imagen)
                {
                    avatarSeleccionado = imagen.Source.ToString();
                    MessageBox.Show($"¡Avatar seleccionado correctamente!\n{avatarSeleccionado}", "Selección de Avatar");
                }
                else
                {
                    MessageBox.Show("Error al seleccionar el avatar. Inténtalo nuevamente.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Devuelve la ruta del avatar seleccionado o null si no hay selección
        /// </summary>
        private string? GetSelectedAvatarPath()
        {
            return string.IsNullOrEmpty(avatarSeleccionado) ? null : avatarSeleccionado;
        }

        /// <summary>
        /// Evento del botón Continuar
        /// </summary>
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            string? rutaAvatar = GetSelectedAvatarPath();

            if (string.IsNullOrEmpty(rutaAvatar))
            {
                MessageBox.Show("Por favor, selecciona un avatar antes de continuar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Abrir la ventana MapaSudamerica con los datos necesarios
            MapaSudamerica mapaSudamerica = new MapaSudamerica(usuario, rutaAvatar);
            mapaSudamerica.Show();

            // Cerrar esta ventana
            this.Close();
        }

        /// <summary>
        /// Evento del botón "Atrás"
        /// </summary>
        private void Atras_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private bool isSoundOn = false; // Estado inicial del sonido apagado

        private void btnSound_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.ToggleSound(); // Alternar el sonido
            ActualizarBotonSonido();    // Actualizar la imagen del botón
        }

        private void ActualizarBotonSonido()
        {
            if (btnSound == null)
            {
                Console.WriteLine("El botón de sonido (btnSound) no está inicializado.");
                return;
            }

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
