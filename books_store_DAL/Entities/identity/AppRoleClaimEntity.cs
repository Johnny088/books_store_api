using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities.identity
{
    public class AppRoleClaimEntity: IdentityRoleClaim<string>
    {
        public  AppRoleEntity? Role { get; set; }
    }
}
