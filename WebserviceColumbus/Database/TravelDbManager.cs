using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebserviceColumbus.Models.Travel;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Database
{
    public class TravelDbManager : DbManager<Travel>
    {
        /// <summary>
        /// Gets the travel by ID and icludes the necessary files.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Travel GetEntity(int id)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Travels.Include("Locations.LocationDetails.Coordinates").Include("User").Where(a => a.ID == id).First();
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET Travel in database with ID #" + id, true);
                return null;
            }
        }

        /// <summary>
        /// Gets all travels of the given user.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Travel> GetEntities(int userID, int offset = 0, int limit = 20)
        {
            if(limit > 100) {
                limit = 100;
            }
            using(var db = new ColumbusDbContext()) {
                try {
                    List<Travel> travels = db.Travels.Include("Locations.LocationDetails.Coordinates").Where(a => a.UserID == userID).ToList();

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

        /// <summary>
        /// Tries to update or insert the travel. The action is determined by the value of the ID.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The new object with the new ID</returns>
        public override Travel UpdateOrInsertEntity(Travel travel)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    if(travel.ID == 0) {
                        db.Entry(travel).State = EntityState.Added;
                    }
                    else {
                        db.Entry(travel).State = EntityState.Modified;
                        foreach(Location location in travel.Locations) {
                            db.Entry(location).State = EntityState.Modified;
                            db.Entry(location.LocationDetails).State = EntityState.Modified;
                            db.Entry(location.LocationDetails.Coordinates).State = EntityState.Modified;
                        }
                    }
                    db.SaveChanges();
                    return travel;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to Insert or Add travel in database", true);
                return null;
            }
        }
    }
}