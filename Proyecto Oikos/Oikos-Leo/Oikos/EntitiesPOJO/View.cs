using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class View : BaseEntity{
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        /*
         * Constructor of the View class
         *
         * @author Leonardo Mora
         */
        public View() {
        }

        /*
         * Constructor of the View class
         *
         * @author Leonardo Mora
         * @param string name - Name of the view.
         */
        public View(string name, string type, string description) {
            Name = name;
            Type = type;
            Description = description;
        }
    }
}
