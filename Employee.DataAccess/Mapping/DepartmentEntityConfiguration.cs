using System.Data.Entity.ModelConfiguration;

namespace Employee.DataAccess.Mapping
{
    public class DepartmentEntityConfiguration : EntityTypeConfiguration<Model.Department>
    {
        public DepartmentEntityConfiguration()
        {
            this.HasKey(d => new {d.Id, d.Name});
            this.Property(d => d.Name).IsRequired();
            this.Property(d => d.Name).HasMaxLength(20);

            this.HasMany(d => d.Employees)
                .WithOptional(e => e.Department);

            this.HasOptional(d => d.Leader);
        }
    }
}