namespace ITI_API_Learn.DTO
{
    //Hide Model - More Secure for model structure and solve serilization propble cycle
    public class EmployeeDataWithDepartmentDTO
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Address { get; set; }

        public string DepartmentName { get; set; }

    }
}
