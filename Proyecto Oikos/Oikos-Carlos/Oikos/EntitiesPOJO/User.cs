using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class User:BaseEntity
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserStatusCode { get; set; }
        public string CurrencyCode { get; set; }

        /*
         * Constructor of the User class
         *
         * @author Tremi
         * empty constructor
         */
        public User()
        {

        }

        /*
         * Constructor of the User class
         *
         * @author Tremi
         * @param int userId - Id of the user
         * @param string password - Encrypted password of the user
         * @param string identification - personal identification of the user
         * @param string name - name of the user
         * @param string email - main email of the user
         * @param string statusCode - status of the user
         * @param string currencyCode - code of chosen currency
         */
        public User(int userId, string password, string identification, string name, string email, string statusCode, string currencyCode)
        {
            UserId = userId;
            Password = password;
            Id = identification;
            Name = name;
            Email = email;
            UserStatusCode = statusCode;
            CurrencyCode = currencyCode;
        }
    }
}
