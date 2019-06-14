using System;

namespace EntitiesPOJO {
    public class CheckboxFactory {
        /*
         * This factory is in charge of creating a new checkbox based on a given entity.
         *
         * @author Leonardo Mora
         * @param BaseEntity obj - The object to be converted to a checkbox.
         * @param EntityTypes objType - The entity type of the object.
         * @return The checkbox to be added to the list.
         */
        public Checkbox CreateCheckbox(BaseEntity obj, EntityTypes objType) {
            switch (objType) {
                case EntityTypes.View:
                    var view = (View) Convert.ChangeType(obj, typeof(View));
                    return new Checkbox {Value = view.Name, Label = view.Description};
                case EntityTypes.Category:
                    var cat = (Category) Convert.ChangeType(obj, typeof(Category));
                    return new Checkbox {Value = cat.CategoryId+"", Label = cat.Name};
            }

            return new Checkbox();
        }
    }
}