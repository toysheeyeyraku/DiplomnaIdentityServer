using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public UserDto()
        {

        }

        public UserDto(string id, string userName)
        {
            this.Id = id;
            this.UserName = userName;
        }
    }
}
