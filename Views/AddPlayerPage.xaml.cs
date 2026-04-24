using HeroArena.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace HeroArena.Views
{
    public partial class AddPlayerPage : BasePage
    {
        public AddPlayerPage()
        {
            InitializeComponent();
 
        }

        protected override object CreateViewModel()
        {
            return new ProfileVM();
        }


        
    }
}
