using HeroArena.ViewModels;


namespace HeroArena.Views
{
    public partial class BattlePage : BasePage
    {
        public BattlePage()
        {
            InitializeComponent();
        }

        override protected object CreateViewModel()
        {
            return new BattleVM();
        }
    }
}
