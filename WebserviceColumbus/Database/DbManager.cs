using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebserviceColumbus.Model;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Database
{
    public class DbManager<T> where T : class, iDbEntity
    {
        #region Getters

        /// <summary>
        /// Gets an Entity by ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual T GetEntity(int ID)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Set<T>().Find(ID);
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET " + typeof(T) + " in database with ID #" + ID, true);
                return null;
            }
        }

        /// <summary>
        /// Gets all Entities of the given Type.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetEntities()
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Set<T>().ToList();
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET all " + typeof(T) + "s in database", true);
                return null;
            }
        }

        #endregion Getters

        #region Setters

        /// <summary>
        /// Adds a new entity to its corresponding table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Value if insert was succesfull</returns>
        public virtual T AddEntity(T entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    db.Set<T>().Add(entity);
                    db.SaveChanges();
                    return entity;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to CREATE " + typeof(T) + " in database", true);
                return null;
            }
        }

        /// <summary>
        /// Adds a collection of entities to its corresponding table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Value if inserts were succesfull</returns>
        public virtual ICollection<T> AddEntities(ICollection<T> entities)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    foreach(T entity in entities) {
                        db.Set<T>().Add(entity);
                    }
                    db.SaveChanges();
                    return entities;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to CREATE list of " + typeof(T) + "s in database", true);
                return default(ICollection<T>);
            }
        }

        #endregion Setters

        #region Update

        /// <summary>
        /// Updates the given entity. Tries to update related entities(childs) aswell.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool UpdateEntity(T entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    db.Entry<T>(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to UPDATE " + typeof(T) + " in database with ID #" + entity.ID, true);
                return false;
            }
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool DeleteEntity(T entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    db.Entry<T>(entity).State = EntityState.Deleted;
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to DELETE " + typeof(T) + " in database with ID #" + entity.ID, true);
                return false;
            }
        }

        #endregion Delete

        #region Other

        /// <summary>
        /// Tries to update or insert an entity. The action is determined by the value of the ID.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The new object with the new ID</returns>
        public virtual T UpdateOrInsertEntity(T entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    if(entity.ID == 0) {
                        db.Entry<T>(entity).State = EntityState.Added;
                    }
                    else {
                        db.Entry<T>(entity).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    return entity;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET all " + typeof(T) + "s in database", true);
                return null;
            }
        }

        #endregion Other
    }
}