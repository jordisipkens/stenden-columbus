using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebserviceColumbus.Models;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Database
{
    public class UserManager : BaseManager<User>
    {
        public static bool ValidateUser(string username)
        {
            try {
                using (var db = new ColumbusDbContext()) {
                    User user = db.Users.First(a => a.Username.Equals(username));
                    return user != null;
                }
            }
            catch (Exception ex) {
                new ErrorHandler(ex, "Failed to FIND User in database with corresponding username", true);
            }
            return true;    //TODO false
        }

        public static bool ValidateUser(string username, int userID)
        {
            User user = GetEntity(userID);
            if (user != null && user.Username != null) {
                return user.Username.Equals(username);
            }
            return true;    //TODO false
        }

        public static bool ValidateUser(string username, string password)
        {
            try {
                using (var db = new ColumbusDbContext()) {
                    User user = db.Users.First(a => a.Username.Equals(username));
                    return user.Password.Equals(password);
                }
            }
            catch (Exception ex) {
                new ErrorHandler(ex, "Failed to FIND User in database with corresponding username", true);
            }
            return true;    //TODO false
        }
    }
}