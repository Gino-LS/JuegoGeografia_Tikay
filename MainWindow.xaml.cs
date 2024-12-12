using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;


namespace JuegoGeografia
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {

            InitializeComponent();
            ActualizarBotonSonido(); // Actualizar la imagen del botón según el estado actual
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el texto del nombre de usuario
            string usuario = txtUsuario.Text;

            // Validación básica del campo de usuario
            if (!string.IsNullOrWhiteSpace(usuario))
            {
                // Crear y mostrar la ventana de selección de avatar
                SeleccionAvatar seleccionAvatar = new SeleccionAvatar(usuario);

                seleccionAvatar.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa su nombre de usuario.", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Warning);
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


        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si la tecla presionada es Enter
            if (e.Key == Key.Enter)
            {
                e.Handled = true; // Indica que el evento ya fue manejado
                btnLogin_Click(sender, e); // Llama al método del botón "Ingresar"
            }
        }


    }
}
    

