using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesPOJO;

namespace EntitiesPOJO
{
    public class ShippingProvider : BaseEntity
    {
        
        public int ShippingProviderId { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public string Logo { get; set; }
        public double BaseFare { get; set; }
        public string CurrencyCode { get; set; }
        public double Commission { get; set; }
        public string ShippingProviderStatusCode { get; set; }
        public double AreaLatitude { get; set; }
        public double AreaLongitude { get; set; }        
        public double AreaRadius { get; set; }

        /*
         * default class constructor
         *
         * @author: Josué Quirós
         */
        public ShippingProvider()
        {

        }


        /*
         * main class constructor
         *
         * @author: Josué Quirós
         *
         * @param spId: shipping provider id number
         * @param name: shipping provider name
         * @param owner: shipping provider owner
         * @param bFare: base fare for first km
         * @param currCode: currency code for shipping provider currency of choice
         * @param comm: commision for platform
         * @param spStatusCode: status (active/inactive) of shipping provider
         * @param areaLng: longitude coordinate for area center
         * @param areaLat: latitude coordinate for area center
         * @param areaRad: area radius
         *
         */
        public ShippingProvider(int spId, string name, int owner, string logo,
            double bFare, string currCode, double comm, string spStatusCode, double areaLat, double areaLng, double areaRad)
        {
            ShippingProviderId = spId;
            Name = name;
            Owner = owner;
            Logo = logo;
            BaseFare = bFare;
            CurrencyCode = currCode;
            Commission = comm;
            ShippingProviderStatusCode = spStatusCode;
            AreaLatitude = areaLat;
            AreaLongitude = areaLng;
            AreaRadius = areaRad;
        }
    }   
}
