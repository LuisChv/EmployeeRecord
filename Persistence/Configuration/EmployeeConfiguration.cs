using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder.Property(e => e.EmployeeId)
                .HasColumnName("EmployeeID ");

            builder.Property(e => e.EmployeeFirstName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.EmployeeLastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.EmployeePhone)
                .HasMaxLength(14)
                .IsUnicode(false);

            builder.Property(e => e.EmployeeZip)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.HireDate)
                .HasColumnType("date")
                .HasColumnName("HireDate ");
        }
    }
}
