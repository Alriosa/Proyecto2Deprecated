using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class UserContacts : BaseEntity
    {

        
        public int UserContactId { get; set; }
        public int UserId { get; set; }
        public string ContactTypeCode { get; set; }
        public string ContactValue { get; set; }


        /*
         * @author: Carlos Rios Sanchez
         *
         * default: Class Constructor
         */
        public UserContacts()
        {

        }

        /*
         * @author: Carlos Rios Sanchez
         * Main class UserContacts
         *
         * @param userContactId: User contact ID , type integer
         * @param userId: user ID , type integer 
         * @param contactTypeCode: Contact Type Code , type string
         * @para contactValue: Contact value , type string
         *
         */

        public UserContacts( int userContactId , int userId , string contactTypeCode , string contactValue)
        {
            UserContactId = userContactId;
            UserId = userId;
            ContactTypeCode = contactTypeCode;
            ContactValue = contactValue;
        }

        /*
        * @author: Carlos Rios Sanchez
        * Main class UserContacts
        *
        * @param userId: user ID , type integer 
        * @param contactTypeCode: Contact Type Code , type string
        * @para contactValue: Contact value , type string
        *
        */

        public UserContacts( int userId, string contactTypeCode, string contactValue)
        {
 
            UserId = userId;
            ContactTypeCode = contactTypeCode;
            ContactValue = contactValue;
        }
    }
}
