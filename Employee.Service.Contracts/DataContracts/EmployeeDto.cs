using System;
using System.Runtime.Serialization;

namespace Employee.Service.Contracts.DataContracts
{
    [DataContract]
    public class EmployeeDto
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime Birthdate { get; set; }
    }
}