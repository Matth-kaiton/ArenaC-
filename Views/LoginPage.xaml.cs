
using System.Windows;
using System.Windows.Controls;
using HeroArena.ViewModels;
using System.Windows.Navigation;

namespace HeroArena.Views
{
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();
        }


        protected override object CreateViewModel()
        {
            return new ViewModels.LoginVM(this.NavigationService);
        }

    }
}
