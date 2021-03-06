﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Words_learning_app_thing.Helpers;

namespace Words_learning_app_thing.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Progress { get; set; } = 0;
        public TrybWybieraniaSesji PrefferedMode { get; set; } = TrybWybieraniaSesji.Automatyczny;
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}