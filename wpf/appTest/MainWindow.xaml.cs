using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
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

namespace appTest;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{

    private int _lastNumber = 0;
    private int _number = 0;
    public int Number
    {
        get => _number;
        set
        {
            _number = value;
            OnPropertyChanged();
        }
    }

    private int _result = 0;
    public int Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddCommand { get; }

    public string _operation = "";
    public string _lastOp = "";
    public string Operation
    {
        get => _operation;
        set
        {
            _operation = value;
            OnPropertyChanged();
        }
    }
    public ICommand OperationCommand { get; } 
    public ICommand CalculateCommand { get; }
    public ICommand ResetCommand { get; }


    public MainWindow()
    {
        InitializeComponent();

        AddCommand = new RelayCommand(d => AddNumber(d));
        OperationCommand = new RelayCommand(d => AddOperator(d));
        CalculateCommand = new RelayCommand(obj => Calculate());
        ResetCommand = new RelayCommand(obj => Reset());



        this.DataContext = this;
    }

    public void AddNumber(object parameter)
    {
        if (Operation != "" )
        {
            Number = 0;
        }

        if (int.TryParse(parameter.ToString(), out int num))
        {
            if(Number == 0)
            {
                Number += num;
                _lastOp = Operation;
                Operation = "";
            }
            else
            {
                Number *= 10;
                Number += num;
            }
        }
    }

    public void AddOperator(object parameter)
    {
        if(parameter != null){
            _lastNumber = Number;
            Operation = parameter.ToString();
        }
    }

    public void Calculate()
    {
        switch (_lastOp)
        {
            case "+":
                Result = Number + _lastNumber;
                break;

            case "-":
                Result = _lastNumber - Number;
                break;

            case "*":
                Result = Number * _lastNumber;
                break;

            case "/":
                Result = _lastNumber / Number;
                break;

            default:
                break;

        }
        _lastNumber = 0;
        Number = 0;

    }

    public void Reset()
    {
        Number = 0;
        _lastNumber = 0;
        Operation = "";
        _lastOp = "";
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}