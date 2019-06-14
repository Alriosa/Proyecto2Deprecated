using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class ProductsCategories : BaseEntity {
        public int ProductsCategoriesId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public ProductsCategories() {
        }

        public ProductsCategories(int usersCategoriesId, int userId, int categoryId) {
            ProductsCategoriesId = usersCategoriesId;
            ProductId = userId;
            CategoryId = categoryId;
        }
    }
}
