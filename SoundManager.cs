using System;
using System.Windows.Media;

namespace JuegoGeografia
{
    public static class SoundManager
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private static bool isSoundOn = false; // Estado inicial: sonido apagado

        public static void PlaySound()
        {
            if (!isSoundOn) return; // No reproducir si está apagado

            try
            {
                if (mediaPlayer.Source == null) // Cargar sonido solo si no está cargado
                {
                    mediaPlayer.Open(new Uri("Images/Sound.mp3", UriKind.Relative));

                    mediaPlayer.MediaEnded += (s, e) =>
                    {
                        mediaPlayer.Position = TimeSpan.Zero;
                        mediaPlayer.Play();
                    };
                }
                mediaPlayer.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al reproducir el sonido: {ex.Message}");
            }
        }

        public static void StopSound()
        {
            try
            {
                mediaPlayer.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al detener el sonido: {ex.Message}");
            }
        }

        public static void ToggleSound()
        {
            isSoundOn = !isSoundOn;
            if (isSoundOn)
            {
                PlaySound();
            }
            else
            {
                StopSound();
            }
        }

        public static bool IsSoundOn()
        {
            return isSoundOn;
        }
    }
}
