using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class OptionListFactory {
        /*
         * This factory is in charge of creating an option.
         *
         * @author Leonardo Mora
         * @param BaseEntity obj - The object to be converted to an option.
         * @param EntityTypes objType - The entity type of the object.
         * @param string listId - The Id of the option list.
         * @return The option to be added to the list.
         */
        public OptionList CreateOption(BaseEntity obj, EntityTypes objType, string listId) {
            switch (objType) {
                case EntityTypes.ProductProvider:
                var prodProv = (ProductProvider)Convert.ChangeType(obj, typeof(ProductProvider));
                return new OptionList {
                    ListId = listId,
                    Value = prodProv.ProductProviderId + "",
                    Description = prodProv.Name
                };
            }

            return new OptionList();
        }
    }
}