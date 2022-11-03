namespace Core.DTO
{
    public class EmployeeCreateDto
    {
        public string EmployeeLastName { get; set; } = null!;
        public string EmployeeFirstName { get; set; } = null!;
        public string EmployeePhone { get; set; } = null!;
        public string EmployeeZip { get; set; } = null!;
        public DateTime HireDate { get; set; }
    }
}
