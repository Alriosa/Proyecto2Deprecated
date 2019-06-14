using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class ShippingMethods : BaseEntity
    {
        public int ShippingMethodId { get; set; } //0
        public string Name { get; set; } //1
        public int ShippingProviderId { get; set; } //2
        public decimal BaseCost { get; set; }//3
        public decimal AdditionalCost { get; set; } //4
        public string Description { get; set; }//5
        public bool IsActive { get; set; } //6
        public int DaysToDeliver { get; set; }//7

        /*
         * @author: Carlos Rios Sanchez
         *
         * default: Class Constructor
         */
        public ShippingMethods()
        {
        }

        /*
         *@author Carlos Rios Sanchez
         * Main Class ShippingMethods
         *
         * @param shippingMethodID: ID from the shipping method, type int
         * @param shippingProviderID: ID from the shipping provider, type int
         * @param name: name of the shipping method, type string 
         * @param additionalCost: Aditional cost , type decimal
         * @param description: description of the shipping method , type string 
         * @param isActive: Status of the shipping method, (True/False) type boolean
         */
        public ShippingMethods(int shippingMethodId, string name,  int shippingProviderId, decimal baseCost,   decimal additionalCost , string description , bool isActive, int daysToDeliver)
        {
            ShippingMethodId = shippingMethodId;
            Name = name;
            ShippingProviderId = shippingProviderId;
            BaseCost = baseCost;
            AdditionalCost = additionalCost;
            Description = description;
            IsActive = isActive;
            DaysToDeliver = daysToDeliver;
        }


        /*
        *@author Carlos Rios Sanchez
        * Main Class ShippingMethods
        *
        * @param shippingProviderID: ID from the shipping provider, type int
        * @param name: name of the shipping method, type string 
        * @param additionalCost: Aditional cost , type decimal
        * @param description: description of the shipping method , type string 
        * @param isActive: Status of the shipping method, (True/False) type boolean
        */
        public ShippingMethods(string name, int shippingProviderId, decimal baseCost, decimal additionalCost, string description, bool isActive , int daysToDeliver)
        {
         
            Name = name;
            ShippingProviderId = shippingProviderId;
            BaseCost = baseCost;
            AdditionalCost = additionalCost;
            Description = description;
            IsActive = isActive;
            DaysToDeliver = daysToDeliver;
        }
    }
}
