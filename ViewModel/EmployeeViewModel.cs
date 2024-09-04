using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module02Exercise01.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
namespace Module02Exercise01.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged

    {
        private Employee _employee;

        private string _fullName;

        public EmployeeViewModel()
        {
            _employee = new Employee { FirstName = "John", LastName = "Doe", Position = "Manger", Department = "CCS", IsActive = true };

            LoadEmployeetDataCommand = new Command(async () => await LoadEmployeeDataAsync());

            Employees = new ObservableCollection<Employee>
            {
                new Employee {FirstName = "CJ", LastName = "Nuguid", Position = "Student", Department = "IT", IsActive = false },
                new Employee {FirstName = "BJ", LastName = "Suguid", Position = "Student", Department = "CS", IsActive = true },
                new Employee {FirstName = "Richard", LastName = "Darwin", Position = "Student", Department = "BMMA", IsActive = true },
                new Employee {FirstName = "Andrei", LastName = "Agbisit", Position = "Student", Department = "CS", IsActive = false }

            };
        }
        public ObservableCollection<Employee> Employees { get; set; }

        public string FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
        public ICommand LoadEmployeetDataCommand { get; }

        private async Task LoadEmployeeDataAsync()
        {
            await Task.Delay(100);
            FullName = $"The People In The Department Are {_employee.FirstName} {_employee.LastName} {_employee.Position} {_employee.Department} {_employee.IsActive}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}