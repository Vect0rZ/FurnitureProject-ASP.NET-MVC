using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using FurnitureProject.Models;

namespace FurnitureProject.Common
{
    public static class UserManagerExtensions
    {
        public static bool ChangeUserAvatar(this ApplicationUserManager UserManager, string UserID, string ImagePath)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var User = context.Users.FirstOrDefault(u => u.Id == UserID);

                User.ImagePath = ImagePath;

                if(context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public static ApplicationUser GetUser(this ApplicationUserManager UserManager, string UserID)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Users.FirstOrDefault(u => u.Id == UserID);
            }
        }

    }
}