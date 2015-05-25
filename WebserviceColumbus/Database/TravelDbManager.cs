using System;
using System.Collections.Generic;
using System.Linq;
using WebserviceColumbus.Models.Travel;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Database
{
    public class TravelDbManager : DbManager<Travel>
    {
        public static List<Travel> GetAllTravels(int userID, int offset = 0, int limit = 20)
        {
            if(limit > 100) {
                limit = 100;
            }
            using(var db = new ColumbusDbContext()) {
                try {
                    List<Travel> travels = db.Travels.Where(a => a.UserID == userID).ToList();

                    if(offset + limit > travels.Count) {
                        limit = travels.Count - offset;
                    }
                    return travels.GetRange(offset, limit);
                }
                catch(Exception ex) {
                    new ErrorHandler(ex, "Failed to GET a collection of Travels in the database", true);
                    return null;
                }
            }
        }
    }
}