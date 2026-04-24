using HeroArena.Views;
using HeroArena.ViewsModels;
using System;
using System.Collections.Generic;
using HeroArena.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace HeroArena.ViewModels
{
    class RegisterVM : BaseVM
    {
        public ICommand RegisterCommand { get; }
        public ICommand NavigateLogin{ get; }


        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private NavigationService _navService;

        public RegisterVM(NavigationService navService)
        {
            _navService = navService;
            NavigateLogin = new RelayCommand(LoginLink);
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register(object parameter)
        {

            string pseudo = Username;
            var container = parameter as StackPanel;
            if (container == null)
            {
                return;
            }
                var pass = container.Children.OfType<PasswordBox>().FirstOrDefault(x => x.Name == "TxtPassword");
                var confirmPass = container.Children.OfType<PasswordBox>().FirstOrDefault(x => x.Name == "TxtConfirmPassword");

                string mdp = pass?.Password;
                string mdpConfirm = confirmPass?.Password;

                if (mdp != mdpConfirm) 
                {
                    System.Windows.MessageBox.Show("Les mots de passe ne correspondent pas");
                    return;
                }
            

            if (string.IsNullOrWhiteSpace(pseudo))
            {
                MessageBox.Show("Veuillez entrer un Pseudo");
                return;
            }

            if (string.IsNullOrWhiteSpace(mdp) && mdp.Length >= 6)
            {
                System.Windows.MessageBox.Show("Veuillez entrer un mot de pass d'au moin 6 caractère");
                return;
            }

            CreateUser(pseudo, mdp);

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
                _navService.Navigate(new LoginPage());
            }
        }

        private void LoginLink(object parameter)
        {
            _navService.Navigate(new LoginPage());
        }
    }
}
