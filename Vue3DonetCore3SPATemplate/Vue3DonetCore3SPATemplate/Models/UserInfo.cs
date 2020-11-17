using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Vue3DonetCore3SPATemplate.Models
{
    public class UserInfo
    {
        //public List<SimpleClaim> Claims { get; set; } = new List<SimpleClaim>();

        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();

        public UserInfo(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                string type = claim.Type.Split("/").Last();
                string value = claim.Value;

                this.Claims[type] = value;
            }
        }
    }
}
