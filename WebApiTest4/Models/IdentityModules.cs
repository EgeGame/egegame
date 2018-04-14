using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Models
{
    //drive required class with int KEY
    public class UserRoleIntPk : IdentityUserRole<int>
    {
        public virtual User User { get; set; }
    }

    public class UserClaimIntPk : IdentityUserClaim<int>
    {
        public virtual User User { get; set; }
    }

    public class UserLoginIntPk : IdentityUserLogin<int>
    {
        public virtual User User { get; set; }
    }

    public class RoleIntPk : IdentityRole<int, UserRoleIntPk>
    {
        public RoleIntPk() { }
        public RoleIntPk(string name) { Name = name; }
    }

    public class UserStoreIntPk : UserStore<User, RoleIntPk, int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public UserStoreIntPk(ExamAppDbContext context)
            : base(context)
        {
        }
    }

    public class RoleStoreIntPk : RoleStore<RoleIntPk, int, UserRoleIntPk>
    {
        public RoleStoreIntPk(ExamAppDbContext context)
            : base(context)
        {
        }
    }

    
    //// You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class User : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    //{
    //    public async ExamTask<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    //public class ExamAppDbContext : IdentityDbContext<User, RoleIntPk, int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    //{
    //    public ExamAppDbContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    public static ExamAppDbContext Create()
    //    {
    //        return new ExamAppDbContext();
    //    }
    //}

}