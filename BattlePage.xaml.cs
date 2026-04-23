using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using HeroArena.Models;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeroArena
{
    public partial class BattlePage : Page
    {

        public Models.Hero? FirstHero { get; set; }
        public BattlePage()
        {
            InitializeComponent();
            CreateHero();

            FirstHero = GetFisrtHero();
            this.DataContext = this;
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
            // var heros = db.Heros.AsNoTracking().ToList(); pas trop comprit le asnotracking mais utile dans certain cas ce documenter
        }
    }
}
