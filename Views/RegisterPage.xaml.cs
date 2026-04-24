
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace HeroArena.Views
{
    public partial class RegisterPage : BasePage
    {

        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override object CreateViewModel()
        {
            return new ViewModels.RegisterVM(this.NavigationService);
        }
    }
}
