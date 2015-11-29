var ViewModel = function () {
    var self = this;
    self.employees = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.departments = ko.observableArray();
    self.newEmployee = {
        FirstName: ko.observable(),
        LastName: ko.observable(),
        Birthdate: ko.observable(),
        Department: ko.observable()
    }

    var employeeUri = '/api/employee/';
    var departmentUri = '/api/department/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllEmployees() {
        ajaxHelper(employeeUri, 'GET').done(function (data) {
            self.employees(data);
        });
    }

    self.getEmployeeDetail = function (item) {
        ajaxHelper(employeeUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    function getAllDepartments() {
        ajaxHelper(departmentUri, 'GET').done(function (data) {
            self.departments(data);
        });
    }


    self.addEmployee = function (formElement) {
        var employee = {
            FirstName: self.newEmployee.FirstName(),
            LastName: self.newEmployee.LastName(),
            Birthdate: self.newEmployee.Birthdate(),
            Department: self.newEmployee.Department()
        };

        ajaxHelper(employeeUri, 'POST', employee).done(function (item) {
            self.employees.push(item);
        });
    }

    // Fetch the initial data.
    getAllEmployees();
    getAllDepartments();
};

ko.applyBindings(new ViewModel());
