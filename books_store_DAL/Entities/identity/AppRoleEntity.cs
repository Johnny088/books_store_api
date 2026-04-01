using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities.identity
{
    public class AppRoleEntity: IdentityRole
    {
        public  ICollection<AppUserRoleEntity> UserRoles { get; set; } = [];
        public  ICollection<AppRoleClaimEntity> RoleClaims { get; set; } = [];
    }
}
