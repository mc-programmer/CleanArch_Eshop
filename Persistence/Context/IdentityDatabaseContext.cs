using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) : IdentityDbContext<User>(options)
{
    #region Fluent api

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityUser<string>>().ToTable("Users","identity");
        builder.Entity<IdentityUser<string>>().ToTable("Roles" , "identity");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims" , "identity");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims" , "identity");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins" , "identity");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles" , "identity");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens" , "identity");

        builder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });
        builder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });
        builder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

        //base.OnModelCreating(builder);
    }

    #endregion
}