using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataAccess.Dao;
using EntitiesPOJO;
using Newtonsoft.Json;

namespace DataAccess.Mapper {
    public class MasterMapper : EntityMapper, ISqlStatements, IObjectMapper {
        public JsonTextReader ObjectToJsonAndReader(BaseEntity entity) {
            var serializedEntity = JsonConvert.SerializeObject(entity);
            return new JsonTextReader(new StringReader(serializedEntity));
        }

        public string ToUnderscoreCase(string str) {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()))
                .ToLower();
        }

        public SqlOperation GetCreateStatement(BaseEntity entity, EntityTypes itemType) {
            var procedureName = "CRE_GENERAL_PR";
            return GetOperationMultipleParams2(entity, procedureName, itemType);
        }

        public SqlOperation GetRetrieveStatement(BaseEntity entity, EntityTypes itemType) {
            var procedureName = "RET_GENERAL_PR";
            return GetOperationSingleParams2(entity, procedureName, itemType);
        }

        public SqlOperation GetRetrieveAllStatement(BaseEntity entity, EntityTypes itemType) {
            var procedureName = "RET_ALL_GENERAL_PR";
            return GetOperationSingleParams2(entity, procedureName, itemType);
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity, EntityTypes itemType) {
            var procedureName = "UPD_GENERAL_PR";
            return GetOperationMultipleParams2(entity, procedureName, itemType);
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity, EntityTypes itemType) {
            var procedureName = "DEL_GENERAL_PR";
            return GetOperationSingleParams2(entity, procedureName, itemType);
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows, EntityTypes itemType) {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows) {
                var obj = BuildObject(row, itemType);
                lstResults.Add(obj);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row, EntityTypes itemType) {
            return new EntityFactory().CreateEntity(row, itemType);
        }

        private SqlOperation
            GetOperationMultipleParams2(BaseEntity entity, string procedureName, EntityTypes itemType) {
            var operation = new SqlOperation {ProcedureName = procedureName};
            var reader = ObjectToJsonAndReader(entity);
            var colNames = "";
            var colValues = "";

            while (reader.Read()) {
                if (reader.TokenType.ToString() != "PropertyName") continue;

                var colName = reader.Value.ToString();
                reader.Read();
                var colValue = reader.Value.ToString();

                colNames += (ToUnderscoreCase(colName) + ",");
                colValues += ("'" + colValue + "',");
            }

            operation.AddVarcharParam("TABLE_NAME", itemType.GetDescription());
            operation.AddVarcharParam("KEYS", colNames.Remove(colNames.Length - 1));
            operation.AddVarcharParam("VALUES", colValues.Remove(colValues.Length - 1));

            return operation;
        }

        private SqlOperation GetOperationSingleParams2(BaseEntity entity, string procedureName, EntityTypes itemType) {
            var operation = new SqlOperation {ProcedureName = procedureName};

            var reader = ObjectToJsonAndReader(entity);
            var colNames = "";
            var colValues = "";

            while (reader.Read()) {
                if (reader.TokenType.ToString() != "PropertyName") continue;

                var colName = reader.Value.ToString();
                reader.Read();
                var colValue = reader.Value.ToString();

                colNames += (ToUnderscoreCase(colName) + ",");
                colValues += ("'" + colValue + "',");

                break;
            }

            try {
                operation.AddVarcharParam("COLUMN_NAME", colNames.Remove(colNames.Length - 1));
                operation.AddVarcharParam("VALUE", colValues.Remove(colValues.Length - 1));
            } catch (Exception) {
                operation.AddVarcharParam("COLUMN_NAME", "");
                operation.AddVarcharParam("VALUE", "");
            }

            operation.AddVarcharParam("TABLE_NAME", itemType.GetDescription());

            return operation;
        }

        public SqlOperation GetMaxIdStatement(BaseEntity entity, EntityTypes itemType) {
            var procedureName = "MAX_ID_GENERAL_PR";
            return GetOperationSingleParams2(entity, procedureName, itemType);
        }
    }
}