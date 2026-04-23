
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HeroArena
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
                    MessageBox.Show("tu t'est bien connecté " + user.Username);
                    NavigationService.Navigate(new MainMenuPage());
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
