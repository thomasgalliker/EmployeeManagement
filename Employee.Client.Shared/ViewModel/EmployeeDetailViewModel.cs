using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Employee.Client.Shared.Service;
using Employee.Service.Contracts.DataContracts;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Employee.Client.Shared.ViewModel
{
    public class EmployeeDetailViewModel : ViewModelBase
    {
        //Private fields
        private readonly IEmployeeServiceClient employeeServiceClient;

        private DepartmentDto selecteDepartment;
        private bool buttonVisibility;
        private int id;
        private string firstName;
        private string lastName;
        private DateTime birthdate;
        private int departmentId;
        private RelayCommand saveEmployeeCommand;
        private RelayCommand cancelChangesCommand;
        private RelayCommand editEmployeeCommand;
        private readonly EditMode editMode;

        public EmployeeDetailViewModel(IEmployeeServiceClient employeeServiceClient, int employeeId, EditMode editMode)
        {
            this.employeeServiceClient = employeeServiceClient;
            this.editMode = editMode;

            this.Departments = new ObservableCollection<DepartmentDto>();

            this.LoadEmployeeDetail(employeeId);
        }

        public ObservableCollection<DepartmentDto> Departments { get; private set; }

        public DepartmentDto SelecteDepartment
        {
            get { return this.selecteDepartment; }
            set
            {
                this.selecteDepartment = value;
                this.RaisePropertyChanged(() => this.SelecteDepartment);
            }
        }

        public bool IsAddButtonVisible
        {
            get { return this.editMode == EditMode.Add; }
        }

        public bool IsSaveButtonVisible
        {
            get { return this.editMode == EditMode.Edit; }
        }

        public int Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                this.RaisePropertyChanged(() => this.Id);
            }
        }

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                this.firstName = value;
                this.RaisePropertyChanged(() => this.FirstName);
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                this.lastName = value;
                this.RaisePropertyChanged(() => this.LastName);
            }
        }

        public DateTime Birthdate
        {
            get { return this.birthdate; }
            set
            {
                this.birthdate = value;
                this.RaisePropertyChanged(() => this.Birthdate);
            }
        }

        public int DepartmentId
        {
            get { return this.departmentId; }
            set
            {
                this.departmentId = value;
                if (this.Departments != null)
                {
                    this.SelecteDepartment = this.Departments.SingleOrDefault(d => d.Id == this.DepartmentId);
                }
                this.RaisePropertyChanged(() => this.DepartmentId);
            }
        }

        private async void LoadEmployeeDetail(int employeeId)
        {
            await this.LoadAllDepartments();
            await this.LoadEmployeeById(employeeId);
        }

        private async Task LoadAllDepartments()
        {
            try
            {
                var departmentDtos = await this.employeeServiceClient.GetAllDepartments();
                this.Departments.Clear();
                foreach (DepartmentDto departmentDto in departmentDtos)
                {
                    this.Departments.Add(departmentDto);
                }
            }
            catch (Exception ex)
            {
                // TODO Show callout with exception
            }
        }

        private async Task LoadEmployeeById(long employeeId)
        {
            try
            {
                var employeeDto = await this.employeeServiceClient.GetEmployeeById(employeeId);

                this.Id = employeeDto.Id;
                this.FirstName = employeeDto.FirstName;
                this.LastName = employeeDto.LastName;
                this.Birthdate = employeeDto.Birthdate;
                this.DepartmentId = employeeDto.Department.Id;

                this.SelecteDepartment = this.Departments.SingleOrDefault(d => d.Id == this.DepartmentId);
            }
            catch (Exception ex)
            {
                // TODO Show callout with exception
            }
        }

        public RelayCommand AddEmployeeCommand
        {
            get
            {
                return this.editEmployeeCommand ?? (this.editEmployeeCommand = new RelayCommand(() =>
                {

                }));
            }
        }

        public RelayCommand SaveEmployeeCommand
        {
            get
            {
                return this.saveEmployeeCommand ?? (this.saveEmployeeCommand = new RelayCommand(() =>
                {
                    
                }));
            }
        }

        public RelayCommand CancelChangesCommand
        {
            get
            {
                return this.cancelChangesCommand ?? (this.cancelChangesCommand = new RelayCommand(() =>
                {
                    //TODO: Ask to save/discard changes
                    this.LoadEmployeeDetail(this.Id);
                }));
            }
        }
    }
}
