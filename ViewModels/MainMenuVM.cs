using System;
using System.Collections.Generic;
using System.Text;

namespace HeroArena.ViewModels
{
    class MainMenuVM : BaseVM
    {
        private string _welcomeText = " ";

        public string WelcomeText
        {
            get => _welcomeText;
            set => SetProperty(ref _welcomeText, value);
        }

        public MainMenuVM(Models.Player player)
        {
            WelcomeText = $"Bienvenue {player.Name}";
        }
    }
}
