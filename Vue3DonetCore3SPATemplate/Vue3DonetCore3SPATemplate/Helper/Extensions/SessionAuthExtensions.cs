using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Vue3DonetCore3SPATemplate.Helper.Extensions
{
    public static class SessionAuthExtensions
    {
        /// <summary>
        /// return null if not log in
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static int? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            string idString = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = -1;

            if (!Int32.TryParse(idString, out id))
            {
                return null;
            }

            return id;
        }
    }
}
