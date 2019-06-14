using DataAccess.Dao;
using EntitiesPOJO;

namespace DataAccess.Mapper {
    public interface ISqlStatements {
        SqlOperation GetCreateStatement(BaseEntity entity, EntityTypes itemType);
        SqlOperation GetRetrieveStatement(BaseEntity entity, EntityTypes itemType);
        SqlOperation GetRetrieveAllStatement(BaseEntity entity, EntityTypes itemType);
        SqlOperation GetUpdateStatement(BaseEntity entity, EntityTypes itemType);
        SqlOperation GetDeleteStatement(BaseEntity entity, EntityTypes itemType);
    }
}