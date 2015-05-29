using System.Collections.Generic;
using WebserviceColumbus.Model;

namespace WebserviceColumbus.Database
{
    internal interface iDatabaseManager
    {
        iDbEntity GetEntity(int id);

        List<iDbEntity> GetEntities();

        iDbEntity UpdateOrInsert(iDbEntity entity);

        bool DeleteEntity(int id);
    }
}