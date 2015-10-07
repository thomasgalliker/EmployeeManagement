using System.Collections.Generic;

namespace Employee.Model
{
    public class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Employee Leader { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}