using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class OptionList : BaseEntity {
        public string Value { get; set; }
        public string ListId { get; set; }
        public string Description { get; set; }

        /*
         * Default class constructor
         *
         * @author Erick Garro
         */
        public OptionList() {
        }

        /*
         * Class constructor used by API to retrieve all options form an specific list
         *
         * @author Erick Garro
         *
         * @param listId: name of the option list
         */
        public OptionList(string listId) {
            ListId = listId.ToUpper();
        }

        /*
         * Main class constructor
         *
         * @author Erick Garro
         *
         * @param listId: name of the option list
         * @param value: value of the option
         */
        public OptionList(string listId, string value) {
            ListId = listId.ToUpper();
            Value = value.ToUpper();
        }

        /*
         * Main class constructor
         *
         * @author Erick Garro
         *
         * @param listId: name of the option list
         * @param value: value of the option
         * @param description: description of the option
         */
        public OptionList(string listId, string value, string description) { 
            ListId = listId.ToUpper();
            Value = value.ToUpper();
            Description = description;
        }
    }
}
