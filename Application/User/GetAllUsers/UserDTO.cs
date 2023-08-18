using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.GetAllUsers
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public AccountRole AccountRole { get; set; }
    }
}
