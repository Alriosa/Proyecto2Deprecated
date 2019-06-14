using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class UsersRolesViews:BaseEntity {
        public int UsersRolesViewsId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string ViewName { get; set; }

        public UsersRolesViews(){

        }

        public UsersRolesViews(int usersRolesViewsId, int userId, int roleId, string viewId) {
            UsersRolesViewsId=usersRolesViewsId;
            UserId=userId;
            RoleId=roleId;
            ViewName=viewId;
        }
    }
}
