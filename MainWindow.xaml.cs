using HeroArena.Models;
using HeroArena.ViewModels;
using HeroArena.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace HeroArena
{

    public partial class MainWindow : Window
    {
        public MainViewModel MainVM { get; } = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = MainVM;

            MainVM.Navigation = this.MainFrame.NavigationService;

            MainFrame.Navigate(new LoginPage());

        }

    }
}