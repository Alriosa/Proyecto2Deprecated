using System;
using System.Collections.Generic;
using DataAccess.Crud;
using EntitiesPOJO;
using Exceptions;

namespace CoreAPI {
    public class MasterManager {
        private MasterCrudFactory crud;

        public MasterManager() {
            crud = new MasterCrudFactory();
        }

        public List<T> 
            RetrieveAll<T>(EntityTypes itemType) {
            return crud.RetrieveAll<T>(itemType);
        }

        public T Retrieve<T>(BaseEntity entity, EntityTypes itemType) {
            try {
                var en = crud.Retrieve<T>(entity, itemType);
                if (en == null) {
                    throw new BusinessException(1);
                } else {
                    return (T) en;
                }
            } catch (Exception e) {
                ExceptionManager.GetInstance().Process(e);
            }

            return default(T);
        }

        public void Create<T>(BaseEntity entity, EntityTypes itemType) {
            try {
                var result = crud.Retrieve<T>(entity, itemType);

                if (result != null) {
                    throw new BusinessException(2);
                } else {
                    crud.Create(entity, itemType);
                }
            } catch (Exception e) {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void Update(BaseEntity entity, EntityTypes itemType) {
            crud.Update(entity, itemType);
        }

        public void Delete(BaseEntity entity, EntityTypes itemType) {
            crud.Delete(entity, itemType);
        }

        public int GetMaxId(BaseEntity entity, EntityTypes itemType) {
            return crud.GetMaxId(entity, itemType);
        }
    }
}