using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingBlog.Data;
using CodingBlog.Enums;
using CodingBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;

namespace CodingBlog.Utilities
{
    public class SeedHelper
    {
        //private ApplicationDbContext _context;
        //public SeedHelper(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public static async Task SeedDataAsync(UserManager<BlogUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedAdmin(userManager);
            await SeedModerator(userManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
        }

        private static async Task SeedAdmin(UserManager<BlogUser> userManager)
        {
            if (await userManager.FindByEmailAsync("joshuapetersmail@gmail.com") == null)
            {
                var admin = new BlogUser()
                {
                    Email = "JoshuaPetersmail@gmail.com",
                    UserName = "JoshuaPetersmail@gmail.com",
                    FirstName = "Joshua",
                    LastName = "Peters",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "Abc&123!");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());

            }
        }
        private static async Task SeedModerator(UserManager<BlogUser> userManager)
        {
            if (await userManager.FindByEmailAsync("JasonTwichell@CoderFoundry.com") == null)
            {
                var moderator = new BlogUser()
                {
                    Email = "JasonTwichell@CoderFoundry.com",
                    UserName = "JasonTwichell@CoderFoundry.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(moderator, "Abc&123!");
                await userManager.AddToRoleAsync(moderator, Roles.Moderator.ToString());

            }
        }

    }
}