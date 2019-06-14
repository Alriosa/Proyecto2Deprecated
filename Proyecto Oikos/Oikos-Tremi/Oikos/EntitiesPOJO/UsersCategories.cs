using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class UsersCategories : BaseEntity {
        public int UsersCategoriesId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public UsersCategories() {
        }

        public UsersCategories(int usersCategoriesId, int userId, int categoryId) {
            UsersCategoriesId = usersCategoriesId;
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}