using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Employee.Service.Contracts.DataContracts
{
    [DebuggerDisplay("Id={Id}, Name={Name}")]
    [DataContract]
    public class DepartmentDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public EmployeeDto Leader { get; set; }

        [DataMember]
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}