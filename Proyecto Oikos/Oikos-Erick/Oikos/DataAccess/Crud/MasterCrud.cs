using System;
using System.Collections.Generic;
using DataAccess.Mapper;
using DataAccess.Dao;
using EntitiesPOJO;

namespace DataAccess.Crud {
    public class MasterCrudFactory : CrudFactory {
        private readonly MasterMapper mapper;

        public MasterCrudFactory() {
            mapper = new MasterMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity, EntityTypes itemType) {
            var sqlOperation = mapper.GetCreateStatement(entity, itemType);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(BaseEntity entity, EntityTypes itemType) {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveStatement(entity, itemType));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0) {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic, itemType);
                return (T) Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>(EntityTypes itemType) {
            var lst = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement(new BaseEntity(), itemType));
            if (lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult, itemType);
                foreach (var obj in objs) {
                    lst.Add((T) Convert.ChangeType(obj, typeof(T)));
                }
            }

            return lst;
        }

        public override void Update(BaseEntity entity, EntityTypes itemType) {
            dao.ExecuteProcedure(mapper.GetUpdateStatement(entity, itemType));
        }

        public override void Delete(BaseEntity entity, EntityTypes itemType) {
            dao.ExecuteProcedure(mapper.GetDeleteStatement(entity, itemType));
        }

        public int GetMaxId(BaseEntity entity, EntityTypes itemType) {
            var result = dao.ExecuteQueryProcedure(mapper.GetMaxIdStatement(entity, itemType));
            return (int) result[0]["MAX_ID"];
        }

        public int GetNextId(BaseEntity entity, EntityTypes itemType) {
            var result = dao.ExecuteQueryProcedure(mapper.GetMaxIdStatement(entity, itemType));
            return (int)result[0]["MAX_ID"] + 1;
        }
    }
}