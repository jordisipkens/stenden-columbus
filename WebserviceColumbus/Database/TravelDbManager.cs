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
        /// <param name="ID"></param>
        /// <returns></returns>
        public override Travel GetEntity(int ID)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Travels.Include("Locations.LocationDetails.Coordinates").Include("User").Where(a => a.ID == ID).First();
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET Travel in database with ID #" + ID, true);
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
            List<Travel> travels;
            try {
                using(var db = new ColumbusDbContext()) {
                    travels = db.Travels.Include("Locations.LocationDetails.Coordinates").Include("User").Where(a => a.UserID == userID).ToList();
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET a collection of Travels in the database", true);
                return null;
            }

            if(travels != null && offset + limit > travels.Count) {
                limit = travels.Count - offset;
                return travels.GetRange(offset, limit);
            }
            return travels;     //TODO Check if correct number of travels is returned
        }

        public override bool UpdateEntity(Travel entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    db.Entry(entity).State = EntityState.Modified;
                    foreach(Location location in entity.Locations) {
                        EntityState state;
                        if(location.ID == 0) {
                            state = EntityState.Added;
                        }
                        else {
                            state = EntityState.Modified;
                        }
                        db.Entry(location).State = state;
                        db.Entry(location.LocationDetails).State = state;
                        db.Entry(location.LocationDetails.Coordinates).State = state;
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to UPDATE Travel in database with ID #" + entity.ID, true);
                return false;
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
                if(travel.ID == 0) {
                    using(var db = new ColumbusDbContext()) {
                        db.SaveChanges();
                        db.Entry(travel).State = EntityState.Added;
                    }
                }
                else {
                    UpdateEntity(travel);
                }
                return travel;
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to INSERT or UPDATE Travel in database", true);
                return null;
            }
        }
    }
}