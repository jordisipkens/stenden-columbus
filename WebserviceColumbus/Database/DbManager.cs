﻿using System;
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
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetEntity(int id)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Set<T>().Find(id);
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET " + typeof(T) + " in database with ID #" + id, true);
            }
            return null;
        }

        /// <summary>
        /// Gets all Entities of the given Type.
        /// </summary>
        /// <returns></returns>
        public static List<T> GetEntities()
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return db.Set<T>().ToList();
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to GET all " + typeof(T) + "s in database", true);
            }
            return null;
        }

        #endregion Getters

        #region Setters

        /// <summary>
        /// Adds a new entity to its corresponding table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Value if insert was succesfull</returns>
        public static bool AddEntity(T entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    db.Set<T>().Add(entity);
                    return db.SaveChanges() == 1;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to CREATE " + typeof(T) + " in database", true);
            }
            return false;
        }

        /// <summary>
        /// Adds a collection of entities to its corresponding table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Value if inserts were succesfull</returns>
        public static bool AddEntities(ICollection<T> entities)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    foreach(T entity in entities) {
                        db.Set<T>().Add(entity);
                    }
                    return db.SaveChanges() == entities.Count;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to CREATE list of " + typeof(T) + "s in database", true);
            }
            return false;
        }

        #endregion Setters

        #region Update

        /// <summary>
        /// Updates the given entity. Tries to update related entities(childs) aswell.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool UpdateEntity(T entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    db.Set<T>().Attach(entity);
                    db.Entry<T>(entity).State = EntityState.Modified;
                    return db.SaveChanges() == 1;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to UPDATE " + typeof(T) + " in database with ID #" + entity.ID, true);
            }
            return false;
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool DeleteEntity(T entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    db.Set<T>().Attach(entity);
                    db.Entry<T>(entity).State = EntityState.Deleted;
                    return db.SaveChanges() == 1;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to DELETE " + typeof(T) + " in database with ID #" + entity.ID, true);
            }
            return false;
        }

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteEntity(int id)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    return DeleteEntity(db.Set<T>().Find(id));
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to FIND " + typeof(T) + " in database with ID #" + id, true);
            }
            return false;
        }

        #endregion Delete

        /// <summary>
        /// Tries to update or insert an entity. The action is determined by the value of the ID.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The new object with the new ID</returns>
        public static T UpdateOrInsertEntity(T entity)
        {
            try {
                using(var db = new ColumbusDbContext()) {
                    db.Set<T>().Attach(entity);
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
            }
            return default(T);
        }
    }
}