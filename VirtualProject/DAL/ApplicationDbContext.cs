using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VirtualProject.DAL.Entities;
using VirtualProject.Models.Identity.ApplicationsIdentity;
using VirtualProject.DAL.Entities.Hr;

namespace VirtualProject.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,

       IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            // Rename Table of identity and add new schema
            b.Entity<ApplicationUser>().ToTable("Users", "rllfrr");
            b.Entity<ApplicationRole>().ToTable("Roles", "rllfrr");

            b.Entity<ApplicationUserRole>().ToTable("UserRoles", "rllfrr");
            b.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "rllfrr");
            b.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "rllfrr");
            b.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "rllfrr");
            b.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "rllfrr");

            b.Entity<ApplicationUser>().HasMany<ApplicationUserRole>().WithOne().HasForeignKey(e => e.UserId).HasPrincipalKey(e => e.Id);
               

            b.Entity<ApplicationRole>().HasMany<ApplicationUserRole>().WithOne().HasForeignKey(e => e.RoleId).HasPrincipalKey(e => e.Id);

            b.Entity<HrEntity>().HasKey(e => e.Hrr);
            //b.Entity<TaskHr>().Property(e=>e.CvName).IsRequired();
           
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<HrEntity> hrs { get; set; }
        public DbSet<TaskHr> tasks { get; set; }
        

    }
}
