using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class Category : BaseEntity {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        /*
         * Constructor of the Category class
         *
         * @author Leonardo Mora
         */
        public Category() {
        }

        /*
         * Constructor of the Category class
         *
         * @author Leonardo Mora
         * @param int categoryId - Id of the category.
         * @param string name - Name of the category.
         * @param string description - Description of the category.
         * @param bool isActive - Status of the category.
         */
        public Category(int categoryId, string name, string description, bool isActive) {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            IsActive = isActive;
        }
    }
}