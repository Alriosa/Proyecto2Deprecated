using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
public class ProductRequestsCategories : BaseEntity {
    public int ProductRequestsCategoriesId { get; set; }
    public int ProductRequestId { get; set; }
    public int CategoryId { get; set; }

    public ProductRequestsCategories() {
    }

    public ProductRequestsCategories(int usersCategoriesId, int userId, int categoryId) {
        ProductRequestsCategoriesId = usersCategoriesId;
        ProductRequestId = userId;
        CategoryId = categoryId;
    }
}
}
