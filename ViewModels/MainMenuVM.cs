using HeroArena.Commands;
using HeroArena.Models;
using HeroArena.Views;
using HeroArena.ViewsModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Input;

namespace HeroArena.ViewModels
{
    public class MainMenuVM : BaseVM
    {
        private string _welcomeText = string.Empty;
        public string WelcomeText
        {
            get => _welcomeText;
            set => SetProperty(ref _welcomeText, value);
        }

        private Models.Player _player;
        public Models.Player Player
        {
            get => _player;
            set => SetProperty(ref _player, value);
        }

        public ICommand Exit { get; }
        public ICommand DBInit { get; }
        public ICommand NavigateProfile { get; }
        public ICommand NavigateSpells { get; }

        public ICommand NavigatePlay { get; }


        public MainMenuVM()
        {
            WelcomeText = $"Bienvenue {MainVM.CurrentPlayer.Name}";
            Player = MainVM.CurrentPlayer;
            Exit = new RelayCommand(CLose);
            DBInit = new RelayCommand(Initialise);
            NavigateProfile = new RelayCommand(GoToProfile);
            NavigateSpells = new RelayCommand(GoToSpells);
            NavigatePlay = new RelayCommand(GoToHeroSelection);
        }


        private void CLose(object parameter)
        {
            Application.Current.Shutdown();
        }


        private void Initialise(object parameter)
        {
            ResetDB();
            CreateHeroesSpells();
            MessageBox.Show("Base de donnée initialiser");
        }


        private void ResetDB()
        {
            using (var db = new Models.ExerciceHeroContext())
            {
                var allHeroes = db.Heroes
                                .Include(h => h.Spells)
                                .Include(h => h.Players)
                                .ToList();

                var allSpells = db.Spells.ToList();

                foreach (var hero in allHeroes)
                {

                    hero.Spells.Clear();
                    hero.Players.Clear();
                }


                db.SaveChanges();

                db.Spells.RemoveRange(allSpells);
                db.Heroes.RemoveRange(allHeroes);

                db.SaveChanges();
            }
        }

        private void CreateHeroesSpells()
        {
            using (var db = new Models.ExerciceHeroContext())
            {

                var s = new Dictionary<string, Spell>
                {
                    ["Strike"] = new Spell { Name = "Strike", Damage = 15, Description = "Vous frapper votre cible et infliger 15 point de dégat" },
                    ["Heavy"] = new Spell { Name = "Heavy strike", Damage = 30, Description = "Frappe de plein fouet, chances de rater" },
                    ["Contre"] = new Spell { Name = "Contre", Damage = 20, Description = "Contre la prochaine attaque" },
                    ["Armee"] = new Spell { Name = "Armé des mort", Damage = 60, Description = "Déchaîne l'armée des parias" },
                    ["Sprint"] = new Spell { Name = "Sprinteur", Damage = 0, Description = "Jouer 2 tours après cette compétence" },
                    ["Biere"] = new Spell { Name = "Bierre Naine", Damage = 0, Description = "Soigne de 40 points de vie" },
                    ["Aigle"] = new Spell { Name = "Yeux de l'aigle", Damage = 0, Description = "Ne rate pas pendant 2 tours, rejouer" },
                    ["Jambes"] = new Spell { Name = "Jeux de jambes", Damage = 0, Description = "Plus de chances d'esquiver" },
                    ["Frimer"] = new Spell { Name = "Frimer", Damage = 10, Description = "Fait perdre 10 PV (30 si c'est Gimli)" },
                    ["PasseraPas"] = new Spell { Name = "Vous ne passerez pas", Damage = 30, Description = "Bloque et inflige 30 dégâts" },
                    ["Break"] = new Spell { Name = "Break Dance", Damage = 30, Description = "Danse spectaculaire pour distraire" },
                    ["Celine"] = new Spell { Name = "Céline Dion Reprise", Damage = 80, Description = "Dévastateur par l'émotion" }
                };


                var heroes = new List<Hero>
                {
                    new Hero {
                        Name = "Aragorn", Health = 150, ImageUrl = "/images/aragorn.jpg",
                        Spells = new List<Spell> { s["Strike"], s["Heavy"], s["Contre"], s["Armee"] }
                    },
                    new Hero {
                        Name = "Gimli", Health = 180, ImageUrl = "images/gimli.jpg",
                        Spells = new List<Spell> { s["Strike"], s["Heavy"], s["Sprint"], s["Biere"] }
                    },
                    new Hero {
                        Name = "Legolas", Health = 100, ImageUrl = "images/legolas.png",
                        Spells = new List<Spell> { s["Strike"], s["Aigle"], s["Jambes"], s["Frimer"] }
                    },
                    new Hero {
                        Name = "Gandalf", Health = 120, ImageUrl = "images/gandalf.png",
                        Spells = new List<Spell> { s["Strike"], s["PasseraPas"], s["Break"], s["Celine"] }
                    }
                };

                db.Heroes.AddRange(heroes);
                db.SaveChanges();
            }
        }

        private void GoToProfile(object parameter)
        {
            using (var db = new Models.ExerciceHeroContext())
            {
                if (Player != null)
                {
                    var login = db.Logins.Find(Player.LoginId);
                    MainVM.ExecuteNavigation(new Views.ProfileSelectionPage());
                }
                else
                {
                    MessageBox.Show("Le player est pas renseigné");
                }
            }
        }

        private void GoToSpells(object parameter)
        {
            MainVM.ExecuteNavigation(new SpellsPage());
        }

        private void GoToHeroSelection(object parameter)
        {
            MainVM.ExecuteNavigation(new HeroSelectionPage());
        }
    }
}
