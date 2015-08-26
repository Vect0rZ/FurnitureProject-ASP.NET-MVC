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

        public static string GetUserImagePath(this ApplicationUserManager UserManager, string UserID)
        {
            string imagePath = string.Empty;
            var user = UserManager.GetUser(UserID);

            if (user != null)
            {
                imagePath = user.ImagePath;
            }

            if(imagePath == null)
            {
                imagePath = string.Empty;
            }

            return imagePath;
        }
    }
}