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

    public partial class MainMenuPage : Page
    {

        private Models.Player _player;

        private string WelcomeText { get; set; }

        public MainMenuPage(Models.Player player)
        {
            InitializeComponent();
            _player = player;
            WelcomeText = $"Bienvenue {_player.Name}";
            this.DataContext = this;
        }

        private void BtnExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnInitDB(object sender, RoutedEventArgs e)
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
    }
}
