using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public CommandBinding bind;
        public RoutedCommand Command { get; set; } = new RoutedCommand();
        private static int CBIndex = -1;
        private bool CheckStart = true;

        public ViewModel()
        {
            bind = new CommandBinding(Command);
            bind.Executed += Command_Executed;
        }

        public void Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Result"));
        }

        public string Result
        {
            get => GetResult(Model.FirstOperand.Text, Model.SecondOperand.Text);
            set
            {
            }
        }

        private string GetResult(string oneString, string twoString)
        {
            try
            {
                double one = Convert.ToDouble(oneString);
                double two = Convert.ToDouble(twoString);

                return CBIndex == 2 && two == 0 ? "Ошибка деления на 0" : Calculation(one, two);
            }
            catch
            {
                if (CheckStart)
                {
                    CheckStart = false;
                    return "";
                }
                return "Введите числа корректно";
            }
        }

        private string Calculation(double one, double two)
        {
            switch (CBIndex)
            {
                case 0:
                    return (one + two).ToString();
                case 1:
                    return (one - two).ToString();
                case 2:
                    return (one / two).ToString();
                case 3:
                    return (one * two).ToString();
                default:
                    return "Действие не выбрано";
            }
        }


        public int SelectedIndex
        {
            set
            {
                CBIndex = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OperationSymbol"));
            }
        }

        public string OperationSymbol => CBIndex == -1 ? "" : Model.ArifmeticalSign[CBIndex];
    }
}