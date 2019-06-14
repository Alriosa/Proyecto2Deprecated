using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class ProductProvider : BaseEntity
    {
        public int ProductProviderId { get; set; }//0
        public int StoreId { get; set; }//1
        public string Name { get; set; }//2
        public string ProviderId { get; set; }//3
        public string Address { get; set; }//4
        public int ProviderType { get; set; }//5
        public string Phone { get; set; }//6
        public string Email { get; set; }//7

        public ProductProvider()
        {
        }


        public ProductProvider(int productProviderId, int storeId, string name, string providerId, string address, int providerType, string phone, string email)
        {
            ProductProviderId = productProviderId;
            StoreId = storeId;
            Name = name;
            ProviderId = providerId;
            Address = address;
            ProviderType = providerType;
            Phone = phone;
            Email = email;
        }

        public ProductProvider(int storeId, string name, string providerId, string address, int providerType, string phone, string email)
        {
            
            StoreId = storeId;
            Name = name;
            ProviderId = providerId;
            Address = address;
            ProviderType = providerType;
            Phone = phone;
            Email = email;
        }
    }


}
