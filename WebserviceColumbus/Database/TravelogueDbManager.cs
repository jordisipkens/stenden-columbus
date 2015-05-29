using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Models.Travel;
using WebserviceColumbus.Models.Travel.Travelogue;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Database
{
    public class TravelogueDbManager : DbManager<Travelogue>
    {
        /// <summary>
        /// Gets the travelogue by ID and icludes the necessary files.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Travelogue GetTravelogue(int id)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Travelogues.Include("Paragraphs").Include("Ratings").Where(a => a.ID == id).First();
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET Travelogue in database with ID #" + id, true);
                return null;
            }
        }

        /// <summary>
        /// Gets the travelogue by ID and icludes the necessary files.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Travelogue> GetTravelogues()
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Travelogues.Include("Paragraphs").Include("Ratings").ToList();
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET all Travelogues in database", true);
                return null;
            }
        }

        /// <summary>
        /// Gets a collection of travelogues sorted by a specific type.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<Travelogue> Display(SearchType filter = SearchType.Latest, int offset = 0, int limit = 20)
        {
            if(limit < 0 || limit > 100) {
                limit = 100;
            }

            if(filter == SearchType.Best) {
                return GetBest(offset, limit);
            }
            else {
                return GetLatest(offset, limit);
            }
        }

        #region Display

        /// <summary>
        /// Gets a collection of Travelogues sorted by latest first.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static List<Travelogue> GetLatest(int offset, int limit)
        {
            List<Travelogue> travelogues = GetTravelogues().OrderBy(t => t.PublishedTime).ToList();
            if(offset + limit > travelogues.Count) {
                limit = travelogues.Count - offset;
            }
            return travelogues.GetRange(offset, limit);
        }

        /// <summary>
        /// Gets a collection of Travelogues sorted by best first.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static List<Travelogue> GetBest(int offset, int limit)
        {
            List<Travelogue> travelogues = GetTravelogues();    //TODO
            if(offset + limit > travelogues.Count) {
                limit = travelogues.Count - offset;
            }
            return travelogues.GetRange(offset, limit);
        }

        #endregion Display

        /// <summary>
        /// Searches all travelogues for a specific value and returns the found entities.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<Travelogue> Search(string value, int limit = 20)
        {
            if(limit < 0 || limit > 100) {
                limit = 100;
            }
            try {
                List<Travelogue> foundTravelogues;
                using(var db = new ColumbusDbContext()) {
                    foundTravelogues = db.Travelogues.Where(t => t.Published == true && t.Paragraphs.Any(p => p.Header.Contains(value) || p.Text.Contains(value))).ToList();
                }

                if(foundTravelogues != null && foundTravelogues.Count > limit) {
                    return foundTravelogues.GetRange(0, limit);
                }
                return foundTravelogues;
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Error occurred while searching travelogues with value: " + value, true);
                return null;
            }
        }

        /// <summary>
        /// Tries to update or insert the travelogue. The action is determined by the value of the ID.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The new object with the new ID</returns>
        public static Travelogue UpdateOrInsert(Travelogue travelogue)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    if(travelogue.ID == 0) {
                        db.Entry(travelogue).State = EntityState.Added;
                    }
                    else {
                        db.Entry(travelogue).State = EntityState.Modified;
                        foreach(Paragraph paragraph in travelogue.Paragraphs) {
                            db.Entry(paragraph).State = EntityState.Modified;
                        }
                    }
                    db.SaveChanges();
                    return travelogue;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to Insert or Add travelogue in database", true);
                return null;
            }
        }

        #region Publish

        /// <summary>
        /// Published a Travelogue. Also checks if the publish request is valid.
        /// </summary>
        /// <param name="travelOgueID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Publish(int travelOgueID, bool value = true)
        {
            Travelogue travelogue = TravelogueDbManager.GetTravelogue(travelOgueID);
            Travel correspondingTravel = TravelDbManager.GetTravel(travelogue.TravelID);
            if(travelogue != null && correspondingTravel != null && UserDbManager.ValidateUser(TokenManager.GetUsernameFromToken(), correspondingTravel.UserID)) {
                return TravelogueDbManager.SetPublish(travelOgueID, value);
            }
            return false;
        }

        /// <summary>
        /// Publishes the given travelogue
        /// </summary>
        /// <param name="travelogue"></param>
        /// <returns></returns>
        private static bool SetPublish(Travelogue travelOgue, bool value)
        {
            return SetPublish(travelOgue.ID, value);
        }

        /// <summary>
        /// Publishes the given travelogue
        /// </summary>
        /// <param name="travelogue"></param>
        /// <returns></returns>
        private static bool SetPublish(int id, bool value)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    Travelogue entity = db.Travelogues.Find(id);
                    if(entity != null) {
                        entity.Published = value;
                        entity.PublishedTime = DateTime.Now;
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Error while publishing travelogue", true);
                return false;
            }
        }

        #endregion Publish
    }

    public enum SearchType
    {
        Best,
        Latest
    }
}