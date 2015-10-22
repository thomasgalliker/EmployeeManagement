using System.Collections.ObjectModel;

using CrossPlatformLibrary.Extensions;

using Employee.Client.Shared.Service;
using Employee.Service.Contracts.DataContracts;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Guards;

namespace Employee.Client.Shared.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEmployeeServiceClient employeeServiceClient;

        private RelayCommand getAllEmployeesCommand;

        public MainViewModel(IEmployeeServiceClient employeeServiceClient)
        {
            Guard.ArgumentNotNull(() => employeeServiceClient);

            this.employeeServiceClient = employeeServiceClient;

            this.Employees = new ObservableCollection<EmployeeDto>();
        }

        public ObservableCollection<EmployeeDto> Employees { get; private set; }

        public RelayCommand GetAllEmployeesCommand
        {
            get
            {
                return this.getAllEmployeesCommand ?? new RelayCommand(async () =>
                {
                    var employeeDtos = await this.employeeServiceClient.GetAllEmployees();
                    this.Employees.Clear();
                    employeeDtos.ForEach(this.Employees.Add);
                });
            }
        }
    }
}