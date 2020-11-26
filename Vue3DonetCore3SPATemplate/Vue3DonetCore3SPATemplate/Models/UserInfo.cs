using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vue3DonetCore3SPATemplate.DAL.DBModels;
using Vue3DonetCore3SPATemplate.DAL.Models;

namespace Vue3DonetCore3SPATemplate.Models
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public AccountStatus AccountStatus { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public UserInfo(User user)
        {
            this.Name = user.Name;
            this.Email = user.Email;
            this.AccountStatus = user.AccountStatus;
            this.UserRoles = user.UserRoles;
        }
    }
}
