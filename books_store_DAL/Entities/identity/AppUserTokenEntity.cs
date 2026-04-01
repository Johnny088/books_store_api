using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities.identity
{
    public class AppUserTokenEntity: IdentityUserToken<string>
    {
        //public string? AccessId {  get; set; }
        public  AppUserEntity? User {  get; set; }
    }
}
