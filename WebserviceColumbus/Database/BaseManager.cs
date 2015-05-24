using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebserviceColumbus.Classes;

namespace WebserviceColumbus.Database
{
    public abstract class BaseManager<T> where T : class, iDbEntity 
    {
        #region Getters
        public T getEntity(int id)
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

        public List<T> getEntities()
        {
            try {
                using (var db = new ColumbusDbContext()) {
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
        public bool addEntity(T entity)
        {
            try {
                using (var db = new ColumbusDbContext()) {
                    db.Set<T>().Add(entity);
                    return db.SaveChanges() == 1;
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to CREATE " + typeof(T) + " in database", true);
            }
            return false;
        }

        public bool addEntities(List<T> entities)
        {
            try {
                using (var db = new ColumbusDbContext()) {
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
        public bool updateEntity(T entity)
        {
            try {
                using (var db = new ColumbusDbContext()) {
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
        public bool deleteEntity(T entity)
        {
            try {
                using (var db = new ColumbusDbContext()) {
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

        public bool deleteEntity(int id)
        {
            try {
                using (var db = new ColumbusDbContext()) {
                    return deleteEntity(db.Set<T>().Find(id));
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to FIND " + typeof(T) + " in database with ID #" + id, true);
            }
            return false;
        }
        #endregion Delete
    }
}
