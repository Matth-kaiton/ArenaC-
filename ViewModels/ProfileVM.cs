using HeroArena.Commands;
using HeroArena.Models;
using HeroArena.ViewModels;
using HeroArena.Views;
using HeroArena.ViewsModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace HeroArena.ViewModels
{
    public class ProfileVM : BaseVM
    {
        private Login _user;

        private string _usernameToAdd = string.Empty;
        public string UsernameToAdd
        {
            get => _usernameToAdd;
            set => SetProperty(ref _usernameToAdd, value);
        }

        public ICommand SelectPlayerCommand { get; }
        public ICommand NavCreateProfileCommand { get; }
        public ICommand CreateProfilCommand { get; }
        public ObservableCollection<Player> Players { get; set; }

        public ProfileVM()
        {
            _user = MainVM.CurrentUser;
            NavCreateProfileCommand = new RelayCommand(NavigateAddPlayer);
            SelectPlayerCommand = new RelayCommand(SelectedPlayer);
            CreateProfilCommand = new RelayCommand(AddPlayer);

            using (var db = new ExerciceHeroContext())
            {
                var playerList = db.Players.Where(p => p.LoginId == _user.Id).ToList();
                Players = new ObservableCollection<Player>(playerList);
            }

        }

        protected void NavigateAddPlayer(object parameter)
        {
            MainVM.ExecuteNavigation(new AddPlayerPage());
        }


        private void SelectedPlayer(object parameter)
        {
            var selectedPlayer = parameter as Player;
            if (selectedPlayer != null)
            {
                MainVM.CurrentPlayer = selectedPlayer;
                MainVM.ExecuteNavigation(new MainMenuPage());
                MessageBox.Show($"Joueur choisi : {selectedPlayer.Name}");
            }
        }

        private void AddPlayer(object parameter)
        {
            var playerName = UsernameToAdd;
            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show("Veuillez entrer un nom de joueur");
                return;
            }

            using (var db = new Models.ExerciceHeroContext())
            {
                var newPlayer = new Models.Player { Name = playerName, LoginId = _user.Id };
                db.Players.Add(newPlayer);
                db.SaveChanges();

                MainVM.CurrentPlayer = newPlayer;
                MainVM.ExecuteNavigation(new MainMenuPage());
            }

        }
    }
}
