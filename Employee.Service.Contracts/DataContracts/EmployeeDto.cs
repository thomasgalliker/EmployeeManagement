using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Employee.Service.Contracts.DataContracts
{
    [DebuggerDisplay("Id={Id}, FirstName={FirstName}, LastName={LastName}")]
    [DataContract]
    public class EmployeeDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime Birthdate { get; set; }

        [DataMember]
        public DepartmentDto Department { get; set; }
    }
}