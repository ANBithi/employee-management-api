using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Designation { get; set; }
        public string ReportsTo { get; set; }
        public string ProfileStatus { get; set; }
        public DateTime LastLogin { get; set; }
    }

    public class CreateUserRequest
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Designation { get; set; }
        public string ReportsTo { get; set; }
        public string ProfileStatus { get; set; }
        public DateTime AccountCreated { get; set; }
    }
    

}
