using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class UserLocation : BaseEntity {

        public int UserLocationsId { get; set; }
        public int UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public bool IsActive { get; set; }

        /*
         * Default constructor of the Location class
         *
         * @author Erick Garro
         */
        public UserLocation() {
        }

        /*
         * Constructor of the Location class
         *
         * @author Erick Garro
         *
         * @param int userId - user id to which a location is associated
         * @param double latitude - latitude coordiantes
         * @param double longitude - longitude coordiantes
         * @param string address - full address
         * @param string distrito - distrito
         * @param string canton - canton
         * @param string provincia - provincia
         * @param bool isActive - status for soft delete
         */
        public UserLocation(int userId, double latitude, double longitude, string address, 
             string provincia, bool isActive)
        {
            UserId = userId;
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            Province = provincia;
            IsActive = isActive;
        }

        /*
         * Constructor of the Location class
         *
         * @author Erick Garro
         *
         * @param int userLocationsId - id for the location
         * @param int userId - user id to which a location is associated
         * @param double latitude - latitude coordiantes
         * @param double longitude - longitude coordiantes
         * @param string address - full address
         * @param string distrito - distrito
         * @param string canton - canton
         * @param string provincia - provincia
         * @param bool isActive - status for soft delete
         */
        public UserLocation(int userLocationsId, int userId, double latitude, double longitude, string address, string provincia, bool isActive) {
        UserLocationsId = userLocationsId;
        UserId = userId;
        Latitude = latitude;
        Longitude = longitude;
        Address = address;
        Province = provincia;
        IsActive = isActive;
        }
    }

}

