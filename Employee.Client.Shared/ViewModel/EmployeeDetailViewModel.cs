using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CrossPlatformLibrary.Extensions;
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

        private DepartmentDto selecteDepartmentDto;
        private EmployeeDto originalEmployeeDto;
        private bool buttonVisibility;
        private int id;
        private string firstName;
        private string lastName;
        private DateTime birthdate;
        private int departmentId;
        private RelayCommand saveEmployeeDtoRelayCommand;
        private RelayCommand cancelEmployeeEditingRelayCommand;
        private RelayCommand employeeDtoValueChangedCommand;
        private RelayCommand getAllDepartmentRelayCommand;
        private bool isFirstComboboxChange;

        //Public properties
        public ObservableCollection<DepartmentDto> Departments { get; private set; }
        public DepartmentDto SelecteDepartmentDto
        {
            get { return selecteDepartmentDto; }
            set
            {
                selecteDepartmentDto = value;
                RaisePropertyChanged(() => this.SelecteDepartmentDto);
            }
        }
        public EmployeeDto OriginalEmployeeDto
        {
            get { return originalEmployeeDto; }
        }
        public bool ButtonVisibility
        {
            get { return buttonVisibility; }
            set
            {
                buttonVisibility = value;
                RaisePropertyChanged(() => this.ButtonVisibility);
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged(() => this.Id);
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                RaisePropertyChanged(() => this.FirstName);
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                RaisePropertyChanged(() => this.LastName);
            }
        }
        public DateTime Birthdate
        {
            get { return birthdate; }
            set
            {
                birthdate = value;
                RaisePropertyChanged(() => this.Birthdate);
            }
        }
        public int DepartmentId
        {
            get { return departmentId; }
            set
            {
                departmentId = value;
                if (Departments != null)
                {
                    SelecteDepartmentDto = this.Departments.SingleOrDefault(d => d.Id == this.DepartmentId);
                }
                RaisePropertyChanged(() => this.DepartmentId);
            }
        }
        
        

        //Constructors
        public EmployeeDetailViewModel(IEmployeeServiceClient employeeServiceClient, EmployeeDto employeeDto)
        {
            this.employeeServiceClient = employeeServiceClient;

            this.Id = employeeDto.Id;
            this.FirstName = employeeDto.FirstName;
            this.LastName = employeeDto.LastName;
            this.Birthdate = employeeDto.Birthdate;
            this.DepartmentId = employeeDto.Department.Id;

            this.originalEmployeeDto = employeeDto;

            this.Departments = new ObservableCollection<DepartmentDto>();

            this.GetAllDepartments().ContinueWith(ct =>
            {
                if (ct.Status == TaskStatus.RanToCompletion)
                {
                    SelecteDepartmentDto = this.Departments.SingleOrDefault(d => d.Id == this.DepartmentId);
                }
            });

            ButtonVisibility = false;
            isFirstComboboxChange = true;
        }

        //Methods
        //  Private
        private bool ChangedEmployeeDto()
        {
            if (originalEmployeeDto.LastName == LastName
                && originalEmployeeDto.FirstName == FirstName
                && originalEmployeeDto.Birthdate == Birthdate
                && originalEmployeeDto.Department.Id == DepartmentId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private async Task GetAllDepartments()
        {
            var departmentDtos = await employeeServiceClient.GetAllDepartments();
            Departments.Clear();
            foreach (DepartmentDto departmentDto in departmentDtos)
            {
                Departments.Add(departmentDto);
            }
        }
        private void ResetEmployeeDtoValues()
        {
            FirstName = OriginalEmployeeDto.FirstName;
            LastName = OriginalEmployeeDto.LastName;
            Birthdate = OriginalEmployeeDto.Birthdate;
            DepartmentId = OriginalEmployeeDto.Department.Id;
        }

        //  Public
        public RelayCommand SaveEmployeeDtoRelayCommand
        {
            get
            {
                return saveEmployeeDtoRelayCommand ?? new RelayCommand(() =>
                {
                    if (ChangedEmployeeDto())
                    {
                        //TODO: Save EmployeeDto to database
                    }
                });
            }
        }
        public RelayCommand CancelEmployeeEditingRelayCommand
        {
            get
            {
                return cancelEmployeeEditingRelayCommand ?? new RelayCommand(() =>
                {
                    ResetEmployeeDtoValues();
                    ButtonVisibility = false;
                });
            }
        }
        public RelayCommand EmployeeDtoValueChangedCommand
        {
            get
            {
                return employeeDtoValueChangedCommand ?? new RelayCommand(() =>
                {
                    if (!isFirstComboboxChange)
                    {
                        ButtonVisibility = true;
                    }
                    else
                    {
                        isFirstComboboxChange = false;
                    }
                });
            }
        }
    }
}
