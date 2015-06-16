using System;
using System.Linq;
using WebserviceColumbus.Models.Other;
using WebserviceColumbus.Models.Travel;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Database
{
    public class UserDbManager : DbManager<User>
    {
        public User GetEntity(string username)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Users.First(a => a.Username.Equals(username));
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to FIND User in database with corresponding username", true);
                return null;
            }
        }

        public string GetUsername(int travelID)
        {
            Travel travel = new TravelDbManager().GetEntity(travelID);
            if(travel.User != null) {
                if(travel.User.Username != null) {
                    return travel.User.Username;
                }
            }
            return string.Empty;
        }

        public override User AddEntity(User entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    if(!db.Users.Any(u => u.Username.Equals(entity.Username))) {
                        return base.AddEntity(entity);
                    }
                    return null;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to CREATE User in database", true);
                return null;
            }
        }

        public bool ValidateUser(string username)
        {
            return GetEntity(username) != null;
        }

        public bool ValidateUser(string username, int userID)
        {
            User user = GetEntity(userID);
            return user != null && user.Username.Equals(username);
        }

        public bool ValidateUser(string username, string password)
        {
            User user = GetEntity(username);
            return user != null && user.Password.Equals(password);
        }
    }
}