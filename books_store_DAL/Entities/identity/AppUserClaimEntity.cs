using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities.identity
{
    public class AppUserClaimEntity: IdentityUserClaim<string>
    {
        public  AppUserEntity? User { get; set; }
    }
}
