﻿namespace EmployeeDetails.Dto
{
    public class EmployeeDto
    {
        public EmployeeDto() { }
         public int? Id { get; set; }
         public string? Name { get; set; }
            
        public string? Email { get; set; }
        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
