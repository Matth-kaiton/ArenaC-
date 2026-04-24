using HeroArena.ViewModels;
using System.Windows.Controls;


namespace HeroArena.Views
{
    /// <summary>
    /// Logique d'interaction pour SpellsPage.xaml
    /// </summary>
    public partial class SpellsPage : BasePage
    {
        public SpellsPage()
        {
            InitializeComponent();
        }

        override protected object CreateViewModel()
        {
            return new SpellsVM();
        }
    }
}
