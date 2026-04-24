using System;
using System.Collections.Generic;
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

namespace HeroArena.Views
{
    public partial class AddPlayerPage : Page
    {

        private Models.Login _login;
        public AddPlayerPage(Models.Login login)
        {
            InitializeComponent();
            _login = login;
        }


        private void BtnAddPlayer(object sender, RoutedEventArgs e)
        {
            string playerName = TxtPlayerName.Text;
            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show("Veuillez entrer un nom de joueur");
                return;
            }

            using (var db = new Models.ExerciceHeroContext())
            {
                var newPlayer = new Models.Player { Name = playerName, LoginId = _login.Id };
                db.Players.Add(newPlayer);
                db.SaveChanges();
                MessageBox.Show($"Joueur '{playerName}' ajouté avec succès !");
                NavigationService.Navigate(new MainMenuPage(newPlayer));
            }

        }
    }
}
