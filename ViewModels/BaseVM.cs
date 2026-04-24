using HeroArena.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HeroArena.ViewsModels
{
    public class BaseVM : INotifyPropertyChanged
    {
        public MainViewModel MainVM => (App.Current.MainWindow.DataContext as MainViewModel);

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
