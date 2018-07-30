using GameStore.Domain.Helper;
using GameStore.Domain.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        private AppUserStore _store;
        private string _membership = "";
        public AppUserManager(AppUserStore store)
            : base(store)
        {
            _store = store;
        }

        public override Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            try
            {
                if (!user.Membership.Equals("Regular") && !user.Membership.Equals("Advanced"))
                {
                    return Task.FromResult(IdentityResult.Failed("Invalid membership!"));
                }
                AppUser existUser = _store.Users.Where(u => u.Email == user.Email).FirstOrDefault();
                if (existUser != null)
                {
                    return Task.FromResult(IdentityResult.Failed("User with email ["+ user.Email + "] already exists!"));
                }

                GameStoreDBContext context = (GameStoreDBContext)_store.Context;
                var newUser = context.Users.Create();
                newUser.Email = user.Email;
                newUser.UserName = user.UserName;
                newUser.PasswordHash = PasswordHasher.HashPassword(password);
                newUser.PhoneNumber = user.PhoneNumber;
                newUser.Membership = user.Membership;

                var role = context.Roles.Where(r => r.Name == user.Membership).First();
                newUser.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = newUser.Id });
                context.Users.Add(newUser);

                context.SaveChanges();

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed("DB Error"));
            }

        }
        public override Task<ClaimsIdentity> CreateIdentityAsync(AppUser user, string authenticationType)
        {
            ClaimsIdentity identity = new ClaimsIdentity(authenticationType);
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var roles = _store.GetRolesAsync(user).Result;
            foreach (string role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            identity.AddClaims(claims);

            return Task.FromResult(identity);

        }
        public override Task<AppUser> FindAsync(string userName, string password)
        {

            string hashedPassword = PasswordHasher.HashPassword(password);
            return _store.Users.Where(u => u.Email == userName && u.PasswordHash == hashedPassword).FirstOrDefaultAsync();

        }

        public override Task<AppUser> FindByEmailAsync(string email)
        {
            return _store.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(new AppUserStore(context.Get<GameStoreDBContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordHasher = new GameStorePasswordHasher();

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }

        public void SetMembership(string membership)
        {
            _membership = membership;
        }
    }
}
