namespace EntitiesPOJO {
    public class Role : BaseEntity {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        /*
         * Constructor of the Role class
         *
         * @author Leonardo Mora
         */
        public Role() {
        }

        /*
         * Constructor of the Role class
         *
         * @author Leonardo Mora
         * @param int roleId - Id of the role.
         * @param string name - Name of the role.
         */
        public Role(int roleId, string name, bool isActive) {
            RoleId = roleId;
            Name = name;
            IsActive = isActive;
        }
    }
}
