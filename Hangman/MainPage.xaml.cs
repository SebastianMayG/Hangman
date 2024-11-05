using System.ComponentModel;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        List<string> secretWords = new List<string> {
                                    "code", "bug", "java", "script", "data",
                                    "framework", "server", "loops", "algorithm", "bit",
                                    "compile","debug","object","variable","syntax"
                                    };

        List<char> userLetters = new List<char>();
        Random random = new Random();
        String answer;

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

        private List<char> letters = new List<char>();
        public List<char> Letters
        {
            get => letters;
            set
            {
                letters = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            InitializeComponent();
            RandomWord();
            Letters.AddRange("qwertyuiopasdfghjklzxcvbnm".ToCharArray());
            MaskWord(answer, userLetters);
            BindingContext = this;
        }

        private void MaskWord(string word, List<char> letters)
        {
            var mask = word
                .Select(x => letters.IndexOf(x) >= 0 ? x : '_')
                .ToArray();
            Spotlight = string.Join(" ", mask);
        }

        public String RandomWord()
        {
            int randomIndex = random.Next(0, secretWords.Count);
            return answer = secretWords[randomIndex];
        }

    }

}
