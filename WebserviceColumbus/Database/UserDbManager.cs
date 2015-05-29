using System;
using System.Linq;
using WebserviceColumbus.Models.Other;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Database
{
    public class UserDbManager : DbManager<User>
    {
        public bool ValidateUser(string username)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    User user = db.Users.First(a => a.Username.Equals(username));
                    return user != null;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to FIND User in database with corresponding username", true);
                return false;
            }
        }

        public bool ValidateUser(string username, int userID)
        {
            User user = GetEntity(userID);
            if(user != null && user.Username != null) {
                return user.Username.Equals(username);
            }
            return false;
        }

        public bool ValidateUser(string username, string password)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    User user = db.Users.First(a => a.Username.Equals(username));
                    return user.Password.Equals(password);
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to FIND User in database with corresponding username", true);
                return false;
            }
        }
    }
}