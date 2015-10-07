using System.Data.Entity.ModelConfiguration;

namespace Employee.DataAccess.Mapping
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Model.Department>
    {
        public DepartmentConfiguration()
        {
            this.HasKey(d => new {d.Id, d.Name});
            this.Property(d => d.Name).IsRequired();
            this.Property(d => d.Name).HasMaxLength(20);

            this.HasMany(d => d.Employees)
                .WithOptional(e => e.Department);

            this.HasRequired(d => d.Leader);
        }
    }
}