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
        /// <param name="ID"></param>
        /// <returns></returns>
        public override Travelogue GetEntity(int ID)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    Travelogue travelogue = db.Travelogues.Include("Paragraphs").Include("Ratings").Where(a => a.ID == ID).First();
                    travelogue.Author = new UserDbManager().GetUsername(travelogue.TravelID);
                    return travelogue;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET Travelogue in database with ID #" + ID, true);
                return null;
            }
        }

        /// <summary>
        /// Gets a collectio of all travelogues
        /// </summary>
        /// <returns></returns>
        public override List<Travelogue> GetEntities()
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    List<Travelogue> travelogues = db.Travelogues.Include("Paragraphs").Include("Ratings").ToList();
                    UserDbManager dbManager = new UserDbManager();
                    foreach(Travelogue travelogue in travelogues) {
                        travelogue.Author = dbManager.GetUsername(travelogue.TravelID);
                    }
                    return travelogues;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET all Travelogues in database", true);
                return null;
            }
        }

        /// <summary>
        /// Gets all travelogues from the User.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Travelogue> GetEntities(int userID)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    List<int> travelIDs = db.Travels.Where(t => t.UserID == userID).Select(t => t.ID).ToList();
                    return db.Travelogues.Include("Paragraphs").Include("Ratings").Where(t => travelIDs.Contains(t.TravelID)).ToList();
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Error while retrieving all Travelogues by userID", true);
                return null;
            }
        }

        /// <summary>
        /// Updates the given entity. Tries to update related entities(childs) aswell.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool UpdateEntity(Travelogue entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    if(entity.Paragraphs != null) {
                        foreach(Paragraph paragraph in entity.Paragraphs) {
                            if(paragraph.ID == 0) {
                                db.Entry(paragraph).State = EntityState.Added;
                            }
                            else {
                                db.Entry(paragraph).State = EntityState.Modified;
                            }
                        }
                    }
                    if(entity.Ratings != null) {
                        foreach(Rating rating in entity.Ratings) {
                            if(rating.ID == 0) {
                                db.Entry(rating).State = EntityState.Added;
                            }
                            else {
                                db.Entry(rating).State = EntityState.Modified;
                            }
                        }
                    }
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to UPDATE Travelogue in database with ID #" + entity.ID, true);
                return false;
            }
        }

        public string TempUpdateOrInsertEntity(Travelogue travelogue)
        {
            try {
                if(travelogue.ID == 0) {
                    using(var db = new ColumbusDbContext()) {
                        db.Entry(travelogue).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else {
                    return TempUpdateEntity(travelogue);
                }
                return "TOPPIE MAIN";
            }
            catch(Exception ex) {
                return ex.Message + "|" + ex.InnerException.Message;
            }
        }

        public string TempUpdateEntity(Travelogue entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    if(entity.Paragraphs != null) {
                        foreach(Paragraph paragraph in entity.Paragraphs) {
                            if(paragraph.ID == 0) {
                                db.Entry(paragraph).State = EntityState.Added;
                            }
                            else {
                                db.Entry(paragraph).State = EntityState.Modified;
                            }
                        }
                    }
                    if(entity.Ratings != null) {
                        foreach(Rating rating in entity.Ratings) {
                            if(rating.ID == 0) {
                                db.Entry(rating).State = EntityState.Added;
                            }
                            else {
                                db.Entry(rating).State = EntityState.Modified;
                            }
                        }
                    }
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return "TOPPIE UPDATE";
                }
            }
            catch(Exception ex) {
                return ex.Message + "|" + ex.InnerException.Message;
            }
        }

        /// <summary>
        /// Tries to update or insert the travelogue. The action is determined by the value of the ID.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The new object with the new ID</returns>
        public override Travelogue UpdateOrInsertEntity(Travelogue travelogue)
        {
            try {
                if(travelogue.ID == 0) {
                    using(var db = new ColumbusDbContext()) {
                        db.Entry(travelogue).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else {
                    UpdateEntity(travelogue);
                }
                return travelogue;
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to INSERT or UPDATE Travelogue in database", true);
                return null;
            }
        }

        /// <summary>
        /// Searches all travelogues for a specific value and returns the found entities.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Travelogue> Search(string value, int limit = 20)
        {
            if(limit < 0 || limit > 100) {                                      
                limit = 100;
            }
            try {
                List<Travelogue> foundTravelogues = GetEntities();
                List<Travelogue> filteredTravelogues = new List<Travelogue>();
                foreach(Travelogue travelogue in foundTravelogues) {
                    if(travelogue.Title != null && travelogue.Title.Contains(value)) {
                        filteredTravelogues.Add(travelogue);
                        continue;
                    }

                    foreach(Paragraph paragraph in travelogue.Paragraphs) {
                        if((paragraph.Header != null && paragraph.Header.ToLower().Contains(value)) || (paragraph.Text != null && paragraph.Text.ToLower().Contains(value))) {
                            filteredTravelogues.Add(travelogue);
                        }
                    }
                }
                foundTravelogues = filteredTravelogues;

                if(foundTravelogues != null && foundTravelogues.Count > limit) {
                    foundTravelogues = foundTravelogues.GetRange(0, limit);
                }
                return foundTravelogues;
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Error occurred while searching Travelogues with value: " + value, true);
                return null;
            }
        }

        /// <summary>
        /// Rates the travelogue
        /// </summary>
        /// <param name="travelogueID"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        public bool Rate(Rating rating, int travelogueID)
        {
            Travelogue travelogue = GetEntity(travelogueID);
            if(travelogue != null) {
                travelogue.Ratings.Add(rating);
                return UpdateEntity(travelogue);
            }
            return false;
        }

        /// <summary>
        /// Published a Travelogue. Also checks if the publish request is valid.
        /// </summary>
        /// <param name="travelOgueID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Publish(int travelOgueID, bool value = true)
        {
            Travelogue travelogue = GetEntity(travelOgueID);
            if(travelogue != null) {
                Travel correspondingTravel = new TravelDbManager().GetEntity(travelogue.TravelID);
                if(correspondingTravel != null && new UserDbManager().ValidateUser(TokenManager.GetUsernameFromToken(), correspondingTravel.UserID)) {
                    travelogue.Published = value;
                    travelogue.PublishedTime = DateTime.Now;        
                    return UpdateEntity(travelogue);
                }
            }
            return false;
        }

        /// <summary>
        /// Gets a collection of travelogues sorted by a specific type.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Travelogue> Display(SearchType filter = SearchType.Latest, int offset = 0, int limit = 20)
        {
            if(limit < 0 || limit > 100) {
                limit = 100;
            }

            if(filter == SearchType.Best) {
                return GetBest(offset, limit);
            }
            else if(filter == SearchType.Oldest) {
                return GetByTime(offset, limit, false);
            }
            else {
                return GetByTime(offset, limit, true);
            }
        }

        #region Display

        /// <summary>
        /// Gets a collection of Travelogues sorted by latest first.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private List<Travelogue> GetByTime(int offset, int limit, bool decending)
        {
            List<Travelogue> travelogues = GetEntities().Where(t => t.Published.Equals(true)).OrderByDescending(t => t.PublishedTime).ToList();
            if(!decending) {
                travelogues.Reverse();
            }

            if(travelogues != null) {
                if(offset + limit > travelogues.Count) {
                    limit = travelogues.Count - offset;
                }
                return travelogues.GetRange(offset, limit);
            }
            return null;
        }

        /// <summary>
        /// Gets a collection of Travelogues sorted by best first.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private List<Travelogue> GetBest(int offset, int limit)
        {
            List<Travelogue> travelogues = GetEntities().Where(t => t.Published.Equals(true)).ToList();
            foreach(Travelogue travelogue in travelogues) {
                TimeSpan diff = DateTime.Now - travelogue.PublishedTime;
                travelogue.RatingFactor = Math.Pow(travelogue.TotalRating / diff.TotalHours, 1.5);
            }
            travelogues = travelogues.OrderByDescending(t => t.RatingFactor).ToList();

            if(travelogues != null) {
                if(offset + limit > travelogues.Count) {
                    limit = travelogues.Count - offset;
                }
                return travelogues.GetRange(offset, limit);
            }
            return null;
        }

        #endregion Display
    }

    public enum SearchType
    {
        Best,
        Latest,
        Oldest
    }
}
