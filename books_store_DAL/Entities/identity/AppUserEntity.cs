using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities.identity
{
    public class AppUserEntity: IdentityUser
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Image {  get; set; }
        //navigation properties
        public  ICollection<AppUserClaimEntity> Claims { get; set; } = [];
        public ICollection<AppUserLoginEntity> Logins { get; set; } = [];
        public  ICollection<AppUserTokenEntity> Tokens { get; set; } = [];
        public  ICollection<AppUserRoleEntity> UserRoles { get; set; } = [];
        public ICollection<RefreshTOkenEntity> Refreshtokens { get; set; } = [];

    }
}
