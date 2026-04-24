using HeroArena.ViewsModels;
using HeroArena.Commands;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace HeroArena.ViewModels
{
    public class MainMenuVM : BaseVM
    {
        private string _welcomeText= string.Empty;
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


        public MainMenuVM()
        {
            WelcomeText = $"Bienvenue {MainVM.CurrentPlayer.Name}";
            Player = MainVM.CurrentPlayer;
            Exit = new RelayCommand(CLose);
            DBInit = new RelayCommand(Initialise);
            NavigateProfile = new RelayCommand(GoToProfile);
        }


        private void CLose(object parameter)
        {
            Application.Current.Shutdown();
        }


        private void Initialise(object parameter)
        {
            ResetDB();
        }


        private void ResetDB()
        {
            using (var db = new Models.ExerciceHeroContext())
            {
                var allHeroes = db.Heroes.ToList();
                db.Heroes.RemoveRange(allHeroes);

                var allSpells = db.Spells.ToList();
                db.Spells.RemoveRange(allSpells);
                db.SaveChanges();
            }
        }

        private void GoToProfile(object parameter)
        {
            using(var db = new Models.ExerciceHeroContext())
            {
                if(Player != null)
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
    }
}
