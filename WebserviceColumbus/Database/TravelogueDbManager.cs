using System;
using System.Linq;
using System.Collections.Generic;
using WebserviceColumbus.Models.Travel.Travelogue;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Database
{
    public class TravelogueDbManager : DbManager<Travelogue>
    {
        public static Travelogue GetTravelogue(int id)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Travelogues.Include("Paragraphs").Where(a => a.ID == id).First();
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET Travelogue in database with ID #" + id, true);
            }
            return null;
        }

        public static List<Travelogue> Display(SearchType filter = SearchType.Latest, int offset = 0, int limit = 20)
        {
            if(limit < 0 || limit > 100) {
                limit = 100;
            }
            return null;
        }

        public static List<Travelogue> Search(string value, int limit = 20)
        {
            if(limit < 0 || limit > 100) {
                limit = 100;
            }
            return null;
        }
    }

    public enum SearchType
    {
        Best, 
        Most_Viewed, 
        Latest
    }
}