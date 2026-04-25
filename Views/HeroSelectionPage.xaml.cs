using HeroArena.Commands;
using HeroArena.Models;
using HeroArena.ViewModels;
using HeroArena.ViewsModels;

namespace HeroArena.Views
{
    /// <summary>
    /// Logique d'interaction pour HeroSelectionPage.xaml
    /// </summary>
    public partial class HeroSelectionPage : BasePage
    {
        public HeroSelectionPage()
        {
            InitializeComponent();
        }

        override protected object CreateViewModel()
        {
            return new HeroSelectionVM();
        }

    }
}
