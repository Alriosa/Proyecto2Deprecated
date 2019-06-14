using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class UserRoleView : BaseEntity {
        public int UsersRolesViewsId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string ViewName { get; set; }
        public bool IsActive { get; set; }

        /*
         * Constructor of the UserRoleView class
         *
         * @author Leonardo Mora
         */
        public UserRoleView() {
        }

        /*
         * Constructor of the UserRoleView class
         *
         * @author Leonardo Mora
         * @param int usersRolesViewsId - Id of the object.
         * @param int userId - Foreign key to an existing user.
         * @param int roleId - Foreign key to an existing role.
         * @param string viewName - Foreign key to an existing view.
         * @param bool isActive  - Status of the object.
         */
        public UserRoleView(int usersRolesViewsId, int userId, int roleId, string viewName, bool isActive) {
            UsersRolesViewsId = usersRolesViewsId;
            UserId = userId;
            RoleId = roleId;
            ViewName = viewName;
            IsActive = isActive;
        }
    }
}
