
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HeroArena.Views
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            using (var db = new Models.ExerciceHeroContext())
            {

                string pseudo = TxtUsername.Text;
                string password = TxtPassword.Password;

                var user = db.Logins
                         .FirstOrDefault(u => u.Username == pseudo.ToLower());

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    MessageBox.Show($"tu t'est bien connecté {user.Username}" );

                    var player = db.Players.FirstOrDefault(p => p.LoginId == user.Id);
                    if (player != null) 
                    {
                        NavigationService.Navigate(new MainMenuPage(player));
                    }
                    else
                    {
                        NavigationService.Navigate(new AddPlayer(user));
                    }
                }
                else
                {
                    MessageBox.Show("Accés refuser manant");
                }
            }
        }

        private void RegisterLink(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}
