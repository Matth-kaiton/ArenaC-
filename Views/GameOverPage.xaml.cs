using HeroArena.ViewModels;

namespace HeroArena.Views
{
    /// <summary>
    /// Logique d'interaction pour GameOverPage.xaml
    /// </summary>
    public partial class GameOverPage : BasePage
    {
        public GameOverPage()
        {
            InitializeComponent();
        }

        override protected object CreateViewModel()
        {
            return new GameOverVM();
        }
    }
}
