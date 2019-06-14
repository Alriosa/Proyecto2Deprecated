using System.Collections.Generic;
using EntitiesPOJO;
using DataAccess.Dao;

namespace DataAccess.Crud {
    public abstract class CrudFactory {
        protected SqlDao dao;

        public abstract void Create(BaseEntity entity, EntityTypes itemType);
        public abstract T Retrieve<T>(BaseEntity entity, EntityTypes itemType);
        public abstract List<T> RetrieveAll<T>(EntityTypes itemType);
        public abstract void Update(BaseEntity entity, EntityTypes itemType);
        public abstract void Delete(BaseEntity entity, EntityTypes itemType);
    }
}