using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity
{
    public partial class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EmployeeId { get; set; }
        public string EmployeeLastName { get; set; } = null!;
        public string EmployeeFirstName { get; set; } = null!;
        public string EmployeePhone { get; set; } = null!;
        public string EmployeeZip { get; set; } = null!;
        public DateTime HireDate { get; set; }
    }
}
