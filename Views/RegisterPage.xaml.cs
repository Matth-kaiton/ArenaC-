
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace HeroArena.Views
{
    public partial class RegisterPage : Page
    {

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void LoginLink(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }


        private void BtnRegister(object sender, RoutedEventArgs e)
        {
            string pseudo = TxtUsername.Text;
            string password = TxtPassword.Password;
            string passwordConfirm = TxtConfirmPassword.Password;

            if (string.IsNullOrWhiteSpace(pseudo))
            {
                MessageBox.Show("Veuillez entrer un Pseudo");
                return;
            }

            if (string.IsNullOrWhiteSpace(password) && password.Length >= 6)
            {
                MessageBox.Show("Veuillez entrer un mot de pass d'au moin 6 caractère");
                return;
            }

            if (password != passwordConfirm)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas");
                return;
            }

            CreateUser(pseudo, password);

        }

        public void CreateUser(string pseudo, string password)
        {
            using (var db = new Models.ExerciceHeroContext())
            {
                bool exit = db.Logins.Any(u => u.Username == pseudo.ToLower());
                if (exit)
                {
                    MessageBox.Show("Le pseudo est déja pris");
                    return;
                }

                var newUser = new Models.Login
                {
                    Username = pseudo.ToLower(),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
                };

                db.Logins.Add(newUser);
                db.SaveChanges();
                MessageBox.Show("Inscription réussie !");
                NavigationService.Navigate(new LoginPage());
            }
        }
    }
}
