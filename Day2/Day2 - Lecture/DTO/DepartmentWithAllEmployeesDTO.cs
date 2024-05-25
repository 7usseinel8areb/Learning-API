namespace ITI_API_Learn.DTO
{
    public class DepartmentWithAllEmployeesDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentManager {  get; set; }
        public List<string> employees { get; set; }  = new List<string>();
    }
}
