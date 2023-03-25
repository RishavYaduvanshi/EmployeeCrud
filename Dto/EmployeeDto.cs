namespace EmployeeDetails.Dto
{
    public class EmployeeDto
    {
        public EmployeeDto() { }
         public int? Id { get; set; }
         public string? FirstName { get; set; }
         public string? LastName { get; set; }
            
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
