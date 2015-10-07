using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Employee.Service.Contracts.DataContracts
{
    [DataContract]
    public class DepartmentDto
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public EmployeeDto Leader { get; set; }

        [DataMember]
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}