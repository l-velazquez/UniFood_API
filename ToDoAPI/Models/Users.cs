using System;

namespace UniFood.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UniversityId { get; set; }
        public DateTime? LastLogin { get; set; } 
        public DateTime RegisteredOn { get; set; } 
    }
}
