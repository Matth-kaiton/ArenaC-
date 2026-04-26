using HeroArena.ViewsModels;
using HeroArena.Commands;
using HeroArena.Views;
using System.Windows.Input;

namespace HeroArena.ViewModels
{
    internal class GameOverVM : BaseVM
    {

        public ICommand NavigateMainMenu { get; }


        public GameOverVM()
        {
            NavigateMainMenu = new RelayCommand(GoToMainMenu);
        }

        private void GoToMainMenu(object parameter)
        {
            MainVM.ExecuteNavigation(new MainMenuPage());
        }
    }
}
