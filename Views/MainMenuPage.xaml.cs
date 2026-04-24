
using System.Windows;
using HeroArena.ViewModels;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace HeroArena.Views
{

    public partial class MainMenuPage : BasePage
    {

        public MainMenuPage()
        {
            InitializeComponent();           
        }

        protected override object CreateViewModel()
        {
            return new ViewModels.MainMenuVM();
        }
    }
}
