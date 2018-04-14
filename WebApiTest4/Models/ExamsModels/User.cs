using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApiTest4.Models.ExamsModels
{
    public class User : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public DateTime? CreatedAt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        [DefaultValue(0)]
        public int Points { get; set; }
        [DefaultValue(0)]
        public int UsePoints { get; set; }

        public int? TeacherId { get; set; }
        public virtual User Teacher { get; set; }
        public virtual List<User> Students { get; set; }

        public virtual Exam CurrentExam { get; set; }

        public virtual School School { get; set; }

        public virtual List<Train> Trains{ get; set; }

        public virtual List<Badge> Badges { get; set; }

        public virtual IEnumerable<UserManualCheckingTaskAttempt> CheckedAttempts { get; set; }
    }

    static class UserExtesion
    {
        public static IQueryable<User> OfRole(this IQueryable<User> source, string role)
        {
            return source.Where(x => x.Claims.Any(c => c.ClaimType.Equals(ClaimTypes.Role)
                                                       && c.ClaimValue.Equals(role)));
        }
    }
}