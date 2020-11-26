using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using Vue3DonetCore3SPATemplate.DAL;
using Vue3DonetCore3SPATemplate.DAL.DBModels;
using Vue3DonetCore3SPATemplate.DAL.Models;
using Vue3DonetCore3SPATemplate.Helper.Extensions;

namespace Vue3DonetCore3SPATemplate.Helper.Auth
{
    public class AuthorizeRoleAttribute : TypeFilterAttribute
    {
        public AuthorizeRoleAttribute(UserRoles userRole)
        : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { userRole };
        }
    }

    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly UserRoles _userRole;
        protected readonly IUserAuthHandler<User,int> _authHandler;
        public AuthorizeActionFilter(UserRoles userRole, IUserAuthHandler<User, int> authHandler)
        {
            this._userRole = userRole;
            this._authHandler = authHandler;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            int? id = context.HttpContext.User.GetUserId();
            if (id == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            IEnumerable<UserRole> roles = this._authHandler.FindById((int)id)?.UserRoles;

            bool isAuthorized = roles.Any(x => x.CodeName.ToLower() == this._userRole.ToString().ToLower() );

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
    
    
}
