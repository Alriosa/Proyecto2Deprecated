using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        public string AreaCovered { get; set; }
        public double BaseFare { get; set; }
        public string CurrencyCode { get; set; }
        public double Commission { get; set; }
        public string ShippingProviderStatusCode { get; set; }

        public ShippingProvider()
        {

        }

        public ShippingProvider(int spId, string name, int owner, 
            string aCover, double bFare, string currCode, double comm, string spStatusCode)
        {
            ShippingProviderId = spId;
            Name = name;
            Owner = owner;
            AreaCovered = aCover;
            BaseFare = bFare;
            CurrencyCode = currCode;
            Commission = comm;
            ShippingProviderStatusCode = spStatusCode;
        }
    }   
}
