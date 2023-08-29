using Domain.Enum;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        //FIXME this should be enum
        public AccountStatus AccountStatus { get; set; }
        //FIXME this should be enum
        public AccountRole AccountRole { get; set; }
    }
}
