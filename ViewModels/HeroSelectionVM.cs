using HeroArena.Models;
using HeroArena.Views;
using HeroArena.ViewsModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using HeroArena.Commands;
using System.Windows.Input;


namespace HeroArena.ViewModels
{
    class HeroSelectionVM : BaseVM
    {
        private Hero _selectedHero;
        public ObservableCollection<Hero> AllHeroes { get; set; }

        public Hero SelectedHero
        {
            get => _selectedHero;
            set => SetProperty(ref _selectedHero, value);
        }

        public ICommand StartBattleCommand { get; }
        public ICommand NagivateMainMenu { get; }

        public HeroSelectionVM()
        {
            using (var db = new Models.ExerciceHeroContext())
            {
                var list = db.Heroes.Include(h => h.Spells).ToList();
                AllHeroes = new ObservableCollection<Hero>(list);

                SelectedHero = AllHeroes.FirstOrDefault();
            }

            StartBattleCommand = new RelayCommand(ExecuteStartBattle);
            NagivateMainMenu = new RelayCommand(GoToMainMenu);
        }

        private void ExecuteStartBattle(object parameter)
        {
            MainVM.CurrentHero = SelectedHero;

            MainVM.ExecuteNavigation(new BattlePage());
        }

        private void GoToMainMenu(object parameter)
        {
            MainVM.ExecuteNavigation(new MainMenuPage());
        }
    }
}
