using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using CrossPlatformLibrary.Extensions;

using Employee.Client.Shared.Service;
using Employee.Service.Contracts.DataContracts;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Guards;

namespace Employee.Client.Shared.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IEmployeeServiceClient employeeServiceClient;

        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand getAllEmployeesCommand;

        private EmployeeDto selectedEmployeeDto;
        public EmployeeDto SelectedEmployeeDto
        {
            get { return selectedEmployeeDto; }
            set
            {
                selectedEmployeeDto = value;
                OnPropertyChanged("SelectedEmployeeDto");
            }
        }

        public MainViewModel(IEmployeeServiceClient employeeServiceClient)
        {
            Guard.ArgumentNotNull(() => employeeServiceClient);

            this.employeeServiceClient = employeeServiceClient;

            this.Employees = new ObservableCollection<EmployeeDto>();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<EmployeeDto> Employees { get; private set; }

        public RelayCommand GetAllEmployeesCommand
        {
            get
            {
                return this.getAllEmployeesCommand ?? new RelayCommand(async () =>
                {
                    //await this.employeeServiceClient.CreateEmployee(new EmployeeDto{FirstName = "hulululu", LastName = "balblbl", Birthdate = DateTime.Now, Department = new DepartmentDto{Id = 1}});
                    var employeeDtos = await this.employeeServiceClient.GetAllEmployees();
                    this.Employees.Clear();
                    employeeDtos.ForEach(this.Employees.Add);
                });
            }
        }
    }
}