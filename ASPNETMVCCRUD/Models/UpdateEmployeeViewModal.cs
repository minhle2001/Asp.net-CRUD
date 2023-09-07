namespace ASPNETMVCCRUD.Models
{
    public class UpdateEmployeeViewModal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public Boolean Sex { get; set; }
    }
}
