using HeroArena.Commands;
using HeroArena.Models;
using HeroArena.ViewsModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HeroArena.ViewModels
{
    class BattleVM : BaseVM
    {
        private Hero _playerHero;
        private Hero _enemyHero;
        private int _currentPlayerHealth;
        private int _currentEnemyHealth;
        private int _score;
        private bool _canRestart;

        public Hero PlayerHero { get => _playerHero; set => SetProperty(ref _playerHero, value); }
        public Hero EnemyHero { get => _enemyHero; set => SetProperty(ref _enemyHero, value); }
        public int CurrentPlayerHealth { get => _currentPlayerHealth; set => SetProperty(ref _currentPlayerHealth, value); }

        public int CurrentEnemyHealth { get => _currentEnemyHealth; set => SetProperty(ref _currentEnemyHealth, value); }

        public int Score { get => _score; set => SetProperty(ref _score, value); }
        public bool CanRestart { get => _canRestart; set => SetProperty(ref _canRestart, value); }

        public ICommand AttackCommand { get; }
        public ICommand RestartCommand { get; }

        public BattleVM()
        {
            PlayerHero = MainVM.CurrentHero;
            CurrentPlayerHealth = PlayerHero.Health;
            Score = 0;
            GenerateEnemy(true);

            AttackCommand = new RelayCommand(ExecuteAttack);
            RestartCommand = new RelayCommand(ExecuteRestart);
        }

        private void ExecuteAttack(object parameter)
        {
            var spell = parameter as Spell;
            if (spell == null || EnemyHero == null || CurrentEnemyHealth <= 0) return;

            CurrentEnemyHealth -= spell.Damage;

            if (CurrentEnemyHealth <= 0)
            {
                CurrentEnemyHealth = 0;
                Score++; 
                CanRestart = true; 
            }
            else
            {
                MonsterTurn();
            }
        }

        private void MonsterTurn()
        {
            var monsterSpell = EnemyHero.Spells.FirstOrDefault();

            if (monsterSpell != null)
            {
                CurrentPlayerHealth -= monsterSpell.Damage;

                if (CurrentPlayerHealth <= 0)
                {
                    CurrentPlayerHealth = 0;
                    MessageBox.Show("Vous avez été vaincu !", "Défaite", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ExecuteRestart(object parameter)
        {
            GenerateEnemy(false);
        }

        private void GenerateEnemy(bool isFirst)
        {
            if (isFirst)
            {

                EnemyHero = new Hero
                {
                    Name = "Gbelin",
                    Health = 100,
                    Spells = PlayerHero.Spells 
                };
            }
            else
            {
                var newHealth = (int)(EnemyHero.Health * 1.10);
                var improvedSpells = EnemyHero.Spells.Select(s => new Spell
                {
                    Name = s.Name,
                    Damage = (int)(s.Damage * 1.05)
                }).ToList();

                EnemyHero = new Hero
                {
                    Name = $"Ennemi Rang {Score + 1}",
                    Health = newHealth,
                    Spells = improvedSpells
                };
            }

            CurrentEnemyHealth = EnemyHero.Health;
            CanRestart = false;
        }
    }
}