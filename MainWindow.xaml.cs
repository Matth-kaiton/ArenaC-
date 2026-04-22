using HeroArena.Models;
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


namespace HeroArena
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Models.Hero? FisrtHero { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            CreateHero();

            this.DataContext = this;
            FisrtHero = GetFisrtHero();
        }

        private void CreateHero()
        {
            using (var db = new Models.ExerciceHeroContext())
            {
                if (!db.Heroes.Any()) 
                {
                    db.Heroes.AddRange(
                        new Hero { Name = "Aragorn", Health = 150, ImageUrl = "https://example.com/aragorn.png" },
                        new Hero { Name = "Legolas", Health = 100, ImageUrl = "https://example.com/legolas.png" },
                        new Hero { Name = "Gimli", Health = 180, ImageUrl = "https://example.com/gimli.png" },
                        new Hero { Name = "Gandalf", Health = 120, ImageUrl = "https://example.com/gandalf.png" }
                    );

                    db.SaveChanges(); 
                }
            }
        }

        private void CreateSpells()
        {
            using var db = new Models.ExerciceHeroContext();
            if (!db.Spells.Any())
            {

            }
        }
        private Hero? GetFisrtHero()
        {
            using (var db = new Models.ExerciceHeroContext())
                if (db.Heroes.Any())
                {
                    var hero = db.Heroes.FirstOrDefault();
                    return hero;
                }
            return null;
            // var heros = db.Heros.AsNoTracking().ToList(); pas trop comprit le as no tracking mais utile dans certain cas
        }
    }
}