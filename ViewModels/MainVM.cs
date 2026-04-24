using HeroArena.Models;
using HeroArena.ViewsModels;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HeroArena.ViewModels
{
    public class MainViewModel : BaseVM
    {
        public Login CurrentUser { get; set; }
        public Player CurrentPlayer { get; set; }

        public NavigationService Navigation { get; set; }

        public void ExecuteNavigation(Page nextPage)
        {
            Navigation?.Navigate(nextPage);
        }
    }
}
