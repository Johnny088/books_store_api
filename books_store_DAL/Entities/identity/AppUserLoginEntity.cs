using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities.identity
{
    public class AppUserLoginEntity: IdentityUserLogin<string>
    {
        public AppUserEntity? User { get; set; }
    }
}
