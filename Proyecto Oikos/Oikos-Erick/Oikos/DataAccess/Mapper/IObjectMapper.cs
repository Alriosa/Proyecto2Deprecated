using EntitiesPOJO;
using System.Collections.Generic;

namespace DataAccess.Mapper {
    interface IObjectMapper {
        List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows, EntityTypes itemType);
        BaseEntity BuildObject(Dictionary<string, object> row, EntityTypes itemType);
    }
}