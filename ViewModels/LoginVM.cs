using HeroArena.Commands;
using HeroArena.ViewsModels;
using HeroArena.Views;
using System.Windows.Input;
using System.Windows.Navigation;

namespace HeroArena.ViewModels
{
    public class LoginVM : BaseVM
    {
        public ICommand LoginCommand { get; }
        public ICommand NavigateRegister { get; }

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }


        private NavigationService _navService;


        public LoginVM(NavigationService navService)
        {
            _navService = navService;
            LoginCommand = new RelayCommand(OnLogin);
            NavigateRegister = new RelayCommand(RegisterLink);
        } 

        private void OnLogin(object parameter)
        {

            string pseudo = Username;
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            string password = passwordBox?.Password;

            if (string.IsNullOrEmpty(pseudo) || string.IsNullOrEmpty(password))
            {
                System.Windows.MessageBox.Show("Remplissez toutes les informations");
                return;
            }

            using (var db = new Models.ExerciceHeroContext())
            {


                var user = db.Logins
                         .FirstOrDefault(u => u.Username == pseudo.ToLower());

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    System.Windows.MessageBox.Show($"tu t'est bien connecté {user.Username}");

                    var player = db.Players.FirstOrDefault(p => p.LoginId == user.Id);
                    if (player != null)
                    {
                        MainVM.CurrentUser = user;
                        MainVM.CurrentPlayer = player;

                        MainVM.ExecuteNavigation(new MainMenuPage());
                    }
                    else
                    {
                        MainVM.CurrentUser = user;
                        _navService.Navigate(new Views.AddPlayerPage());
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Accés refuser manant");
                }
            }
        }

        private void RegisterLink(object parameter)
        {
            _navService.Navigate(new Views.RegisterPage());
        }

    }
}
