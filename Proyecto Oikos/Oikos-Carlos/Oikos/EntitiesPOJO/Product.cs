using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class Product:BaseEntity
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal SellingPrice { get; set; }
        public int ProductProviderId { get; set; }
        public bool IsActive { get; set; }

        public Product(){

        }

        public Product(int productId, int storeId, string name, string description, decimal taxPercentage, decimal sellingPrice, int productProviderId, bool isActive)
        {
            ProductId = productId;
            StoreId = storeId;
            Name = name;
            Description = description;
            TaxPercentage = taxPercentage;
            SellingPrice = sellingPrice;
            ProductProviderId = productProviderId;
            IsActive = isActive;
        }

        public Product(int storeId, string name, string description, decimal taxPercentage, decimal sellingPrice, int productProviderId) {
            StoreId = storeId;
            Name = name;
            Description = description;
            TaxPercentage = taxPercentage;
            SellingPrice = sellingPrice;
            ProductProviderId = productProviderId;
            IsActive = true;
        }
    }
}
