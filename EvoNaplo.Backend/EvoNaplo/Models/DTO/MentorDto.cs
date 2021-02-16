using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoNaplo.Models.DTO
{
    public class MentorDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Role UserRole = Role.Mentor;

        public MentorDto()
        {

        }

        //Constructor with phone number
        public MentorDto(string email, string password, string firstName, string lastName, string phoneNumber)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        //Constructor without phone number
        public MentorDto(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return ($"{Email}-{Password}-{FirstName}-{LastName}-{PhoneNumber}-{UserRole}");
        }
    }
}
