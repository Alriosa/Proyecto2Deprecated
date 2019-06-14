using System;

namespace EntitiesPOJO {
    public class BaseEntity {
        public string GetEntityInformation() {
            var dump = ObjectDumper.Dump(this);
            return dump;
        }
    }
}