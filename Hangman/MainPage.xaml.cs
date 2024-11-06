using System.ComponentModel;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private List<string> palabras = new List<string> {
                                    "code", "bug", "java", "script", "data",
                                    "framework", "server", "loops", "algorithm", "bit",
                                    "compile","debug","object","variable","syntax"
                                    };

        private List<char> letrasUsuario = new List<char>();
        private Random random = new Random();
        private String respuesta;

        private string spotlight = "";
        public String Spotlight
        {
            get => spotlight;
            set
            {
                spotlight = value;
                OnPropertyChanged();
            }
        }

        private List<char> letras = new List<char>();
        public List<char> Letras
        {
            get => letras;
            set
            {
                letras = value;
                OnPropertyChanged();
            }
        }

        private string mensaje;
        public string Mensaje
        {
            get => mensaje;
            set
            {
                mensaje = value;
                OnPropertyChanged();
            }
        }
        
        private String estado;
        public String Estado
        {
            get => estado;
            set
            {
                estado = value;
                OnPropertyChanged();
            }
        }

        private string imagen;
        public string Imagen
        {
            get => imagen;
            set
            {
                imagen = value;
                OnPropertyChanged();
            }
        }

        private int errores;
        public int Errores
        {
            get => errores;
            set
            {
                errores = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            InitializeComponent();
            RandomWord();
            Letras.AddRange("qwertyuiopasdfghjklzxcvbnm".ToCharArray());
            EnmascararPalabra(respuesta, letrasUsuario);
            ActualizarEstado();
            BindingContext = this;
        }

        private void EnmascararPalabra(string palabra, List<char> letras)
        {
            var mascara = palabra
                .Select(x => letras.IndexOf(x) >= 0 ? x : '_')
                .ToArray();
            Spotlight = string.Join(" ", mascara);
        }

        public String RandomWord()
        {
            int randomIndex = random.Next(0, palabras.Count);
            return respuesta = palabras[randomIndex];
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            char letra = boton.Text[0];
            boton.IsEnabled = false;
            ManejarLetras(letra);
        }

        private void ManejarLetras(char letra)
        {
            if(letrasUsuario.IndexOf(letra) == -1)
            {
                letrasUsuario.Add(letra);
            }
            if(respuesta.IndexOf(letra) >= 0)
            {
                EnmascararPalabra(respuesta, letrasUsuario);
                RevisarSiGano();
            }
            else
            {
                errores++;
                ActualizarEstado();
            }
        }

        private void RevisarSiGano()
        {
            if(Spotlight.Replace(" ", "") == respuesta)
            {
                Mensaje = "Ganaste!";
            }
        }

        private void ActualizarEstado()
        {
            Estado = $"Errores: {errores} de 6";
            Imagen = $"img{errores}.jpg";

            if(errores == 6)
            {
                Mensaje = "Perdiste!";
            }
        }

        private void btnReiniciar_Clicked(object sender, EventArgs e)
        {
            letrasUsuario.Clear();
            errores = 0;
            Mensaje = string.Empty;
            respuesta = RandomWord();
            EnmascararPalabra(respuesta, letrasUsuario);
            ActualizarEstado();

            foreach (var child in LetrasLayout.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = true;
                }
            }
        }
    }
}
