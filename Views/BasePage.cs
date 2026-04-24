using HeroArena.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HeroArena.Views
{
    public abstract class BasePage : Page
    {

        public MainViewModel MainVM => (Window.GetWindow(this)?.DataContext as MainViewModel);
        
        public BasePage()
        {

            this.Loaded += OnPageLoaded;
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = CreateViewModel();
        }


        protected abstract object CreateViewModel();
    }
}