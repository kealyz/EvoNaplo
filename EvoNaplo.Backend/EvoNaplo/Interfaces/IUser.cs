using EvoNaplo.Models;

namespace EvoNaplo.Interfaces
{
    interface IUser
    {
        public int Id { get; }
        public string Email { get; set; }
        public string Password { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; }
        public Role Role { get; }
    }
}
