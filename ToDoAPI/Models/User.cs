using System;

namespace UniFood.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Important - Read notes below!
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastLogin { get; set; } 
        public DateTime RegisteredOn { get; set; } 
    }
}
