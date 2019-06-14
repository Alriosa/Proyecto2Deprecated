using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class StoresCategories : BaseEntity {
        public int StoresCategoriesId { get; set; }
        public int StoreId { get; set; }
        public int CategoryId { get; set; }

        public StoresCategories() {
        }

        public StoresCategories(int usersCategoriesId, int userId, int categoryId) {
            StoresCategoriesId = usersCategoriesId;
            StoreId = userId;
            CategoryId = categoryId;
        }
    }
}
