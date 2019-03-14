using System.ComponentModel.DataAnnotations;

namespace APP2000V_DesktopApp_g11.Models.Database
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


    }
}
