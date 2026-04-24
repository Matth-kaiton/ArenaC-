using HeroArena.Models;
using HeroArena.ViewsModels;
using HeroArena.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace HeroArena.ViewModels
{
    class SpellsVM : BaseVM
    {
        private Hero _selectedHeroFilter;
        private Spell _selectedSpell;
        private List<Spell> _allSpellsDatabase;
        public ObservableCollection<Hero> AllHeroes { get; set; }

        public Hero SelectedHeroFilter
        {
            get => _selectedHeroFilter;
            set
            {
                if (SetProperty(ref _selectedHeroFilter, value))
                {
                    OnPropertyChanged(nameof(FilteredSpells));
                }
            }
        }


        public Spell SelectedSpell
        {
            get => _selectedSpell;
            set => SetProperty(ref _selectedSpell, value);
        }

        public IEnumerable<Spell> FilteredSpells
        {
            get
            {
                if (SelectedHeroFilter == null || SelectedHeroFilter.Id == 0)
                {
                    return _allSpellsDatabase;
                }

                return SelectedHeroFilter.Spells;
            }
        }

        public ICommand NavigateMainMenu { get; }

        public SpellsVM()
        {

            NavigateMainMenu = new RelayCommand(GoToMainMenu);
            using (var db = new Models.ExerciceHeroContext())
            {

                _allSpellsDatabase = db.Spells.ToList();

                var heroesFromDb = db.Heroes.Include(h => h.Spells).ToList();

                var allOption = new Hero { Id = 0, Name = "--- Tous les sorts ---" };

                AllHeroes = new ObservableCollection<Hero>();
                AllHeroes.Add(allOption);
                foreach (var h in heroesFromDb) AllHeroes.Add(h);

                SelectedHeroFilter = allOption;
            }
        }

        private void GoToMainMenu()
        {
            MainVM.ExecuteNavigation(new MainMenuPage());
        }
    }
}
