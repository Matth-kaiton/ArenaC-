
using HeroArena.Models;
using HeroArena.ViewModels;

namespace HeroArena.Views
{

    
    public partial class ProfileSelectionPage : BasePage
    {
        public ProfileSelectionPage()
        {

            InitializeComponent();
        }

        protected override object CreateViewModel()
        {
            return new ProfileVM();
        }
    }
}
