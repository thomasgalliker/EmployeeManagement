﻿using System.Data.Entity.ModelConfiguration;


namespace Employee.DataAccess.Mapping
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Model.Employee>
    {
        public EmployeeConfiguration()
        {
            this.HasKey(d => d.Id);

            this.Property(e => e.LastName).IsRequired();
            this.Property(e => e.LastName).HasMaxLength(20);

            this.Property(e => e.FirstName).IsRequired();
            this.Property(e => e.FirstName).HasMaxLength(20);

            this.Property(e => e.Birthdate).IsRequired();

            this.Property(e => e.RowVersion).IsConcurrencyToken();

            this.HasOptional(e => e.Department)
                .WithMany(d => d.Employees);
        }
    }
}