using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class UserMedia : BaseEntity
    {
        public int UsersMediaId { get; set; }
        public int UserId { get; set; }
        public string Url { get; set; }
        public string UsersMediaTypeCode { get; set; }
        public bool IsActive { get; set; }

        public UserMedia()
        {
        }

        public UserMedia(int userMediaId, int userId, string url, string usersMediaTypeCode, bool isActive)
        {
            UsersMediaId = userMediaId;
            UserId = userId;
            Url = url;
            UsersMediaTypeCode = usersMediaTypeCode;
            IsActive = isActive;
        }

        public UserMedia(int userId, string url, string usersMediaTypeCode, bool isActive)
        {

            UserId = userId;
            Url = url;
            UsersMediaTypeCode = usersMediaTypeCode;
            IsActive = isActive;
        }

    }
}