using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities.identity
{
    public class AppUserRoleEntity: IdentityUserRole<string>
    {
        public  AppUserEntity? User { get; set; }
        public  AppRoleEntity? Role { get; set; }
    }
}
