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

namespace HeroArena.Components
{
    /// <summary>
    /// Logique d'interaction pour HeroCard.xaml
    /// </summary>
    public partial class HeroCard : UserControl
    {

        public static readonly DependencyProperty HeroProperty =
            DependencyProperty.Register(
                "Hero",
                typeof(Models.Hero),
                typeof(HeroCard),
                new PropertyMetadata(null)
                );


        public Models.Hero Hero
        {
            get { return (Models.Hero)GetValue(HeroProperty);}
            set { SetValue(HeroProperty, value); }
        }

        public HeroCard()
        {
            InitializeComponent();
        }
    }
}
