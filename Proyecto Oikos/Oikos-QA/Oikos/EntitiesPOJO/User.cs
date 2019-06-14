using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class User:BaseEntity
    {
        public int UserId;
        public string Password;
        public string Id;
        public string Name;
        public string Email;
        public string UserStatusCode;
        public string CurrencyCode;

        public User()
        {

        }

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
