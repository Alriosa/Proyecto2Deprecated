using System;
using System.Collections.Generic;

namespace EntitiesPOJO {
    public class EntityFactory {
        public BaseEntity CreateEntity(Dictionary<string, object> row, EntityTypes itemType) {
            var myObject = new BaseEntity();

            List<string> attrs = new List<string>();

            foreach (var att in row) {
                attrs.Add(att.Value.ToString());
            }

            switch (itemType) {
                case EntityTypes.ApplicationMessage:
                    return new ApplicationMessage(int.Parse(attrs[0]), attrs[1]);
                case EntityTypes.PaymentMethod:
                    return new PaymentMethod(Int32.Parse(attrs[0]), attrs[1], attrs[2], int.Parse(attrs[3]),
                        bool.Parse(attrs[4]));
                case EntityTypes.Transaction:
                    return new Transaction(int.Parse(attrs[0]), attrs[1], attrs[2], double.Parse(attrs[3]),
                        int.Parse(attrs[4]), int.Parse(attrs[5]), int.Parse(attrs[6]), DateTime.Parse(attrs[7]));
                case EntityTypes.OrdersTransactions:
                    return new OrdersTransactions(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]));
                case EntityTypes.Store:
                    return new Store(int.Parse(attrs[0]), attrs[1], attrs[2], int.Parse(attrs[3]), attrs[4],
                        double.Parse(attrs[5]), double.Parse(attrs[6]), attrs[7], attrs[8], attrs[9],
                        double.Parse(attrs[10]), DateTime.Parse(attrs[11]));
                case EntityTypes.Users:
                    return new User(int.Parse(attrs[0]), attrs[1], attrs[2], attrs[3], attrs[4], attrs[5], attrs[6]);
                case EntityTypes.ShippingProvider:
                    return new ShippingProvider(int.Parse(attrs[0]), attrs[1], int.Parse(attrs[2]), attrs[3],
                        double.Parse(attrs[4]), attrs[5], double.Parse(attrs[6]), attrs[7], double.Parse(attrs[8]),
                        double.Parse(attrs[9]), double.Parse(attrs[10]));
                case EntityTypes.Coupon:
                    return new Coupon(int.Parse(attrs[0]), attrs[1], attrs[2], double.Parse(attrs[3]),
                        int.Parse(attrs[4]));
                case EntityTypes.Product:
                    return new Product(int.Parse(attrs[0]), int.Parse(attrs[1]), attrs[2], attrs[3],
                        decimal.Parse(attrs[4]), decimal.Parse(attrs[5]), int.Parse(attrs[6]), bool.Parse(attrs[7]));
                case EntityTypes.CustomerServiceType:
                    return new CustomerServiceType(int.Parse(attrs[0]), attrs[1], bool.Parse(attrs[2]),
                        bool.Parse(attrs[3]));
                case EntityTypes.CustomerServiceRequest:
                    return new CustomerServiceRequest(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]),
                        attrs[3], DateTime.Parse(attrs[4]), bool.Parse(attrs[5]), bool.Parse(attrs[6]));
                case EntityTypes.ProductProvider:
                    return new ProductProvider(int.Parse(attrs[0]), int.Parse(attrs[1]), attrs[2], attrs[3], attrs[4],
                        int.Parse(attrs[5]), attrs[6], attrs[7]);
                case EntityTypes.ShippingMethods:
                    return new ShippingMethods(int.Parse(attrs[0]), attrs[1], int.Parse(attrs[2]),
                        decimal.Parse(attrs[3]), decimal.Parse(attrs[4]), attrs[5], bool.Parse(attrs[6]),
                        int.Parse(attrs[7]));
                case EntityTypes.UserContacts:
                    return new UserContacts(int.Parse(attrs[0]), int.Parse(attrs[1]), attrs[2], attrs[3]);
                case EntityTypes.Category:
                    return new Category(int.Parse(attrs[0]), attrs[1], attrs[2], bool.Parse(attrs[3]));
                case EntityTypes.Role:
                    return new Role(int.Parse(attrs[0]), attrs[1], bool.Parse(attrs[2]));
                case EntityTypes.UsersCategories:
                    return new UsersCategories(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]));
                case EntityTypes.StoresCategories:
                    return new StoresCategories(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]));
                case EntityTypes.ProductsCategories:
                    return new ProductsCategories(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]));
                case EntityTypes.ProductRequestsCategories:
                    return new ProductRequestsCategories(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]));
                case EntityTypes.OptionList:
                    return new OptionList(attrs[0], attrs[1], attrs[2]);
                case EntityTypes.UserLocation:
                    return new UserLocation(int.Parse(attrs[0]), int.Parse(attrs[1]), double.Parse(attrs[2]),
                        double.Parse(attrs[3]), attrs[4], attrs[5], bool.Parse(attrs[6]));
                case EntityTypes.View:
                    return new View(attrs[0], attrs[1], attrs[2]);
                case EntityTypes.ProductMedia:
                    return new ProductMedia(int.Parse(attrs[0]), int.Parse(attrs[1]), attrs[2], bool.Parse(attrs[3]));
                case EntityTypes.UserRoleView:
                    return new UserRoleView(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]), attrs[3],
                        bool.Parse(attrs[4]));
                case EntityTypes.UsersStores:
                    return new UsersStores(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]));
                case EntityTypes.UsersShippingProviders:
                    return new UsersShippingProviders(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]));
                case EntityTypes.ProductRequest:
                    return new ProductRequest(int.Parse(attrs[0]), int.Parse(attrs[1]), attrs[2],
                        DateTime.Parse(attrs[3]), DateTime.Parse(attrs[4]), bool.Parse(attrs[5]), int.Parse(attrs[6]));
                case EntityTypes.Inventory:
                    return new Inventory(int.Parse(attrs[0]), int.Parse(attrs[1]), decimal.Parse(attrs[2]),
                        DateTime.Parse(attrs[3]), int.Parse(attrs[4]), bool.Parse(attrs[5]));
                case EntityTypes.Customer:
                    return new User(int.Parse(attrs[0]), attrs[1], attrs[2], attrs[3], attrs[4], attrs[5], attrs[6]);
                case EntityTypes.ShoppingCart:
                    return new ShoppingCart(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]),
                        int.Parse(attrs[3]), bool.Parse(attrs[4]), double.Parse(attrs[5]), bool.Parse(attrs[6]));
                case EntityTypes.ProductRequestResponses:
                    return new ProductRequestResponses(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]),
                        double.Parse(attrs[3]), int.Parse(attrs[4]), attrs[5], DateTime.Parse(attrs[6]),
                        bool.Parse(attrs[7]));
                case EntityTypes.UserMedia:
                    return new UserMedia(int.Parse(attrs[0]), int.Parse(attrs[1]), attrs[2], attrs[3],
                        bool.Parse(attrs[4]));
                case EntityTypes.Orders:
                    return new Orders(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]),
                        int.Parse(attrs[3]), DateTime.Parse(attrs[4]), int.Parse(attrs[5]));
                case EntityTypes.OrdersProducts:
                    return new OrdersProducts(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]), double.Parse(attrs[3]), int.Parse(attrs[4]));
                case EntityTypes.OrdersProductsInventory:
                    return new OrdersProductsInventory(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]));
                case EntityTypes.OrdersStatus:
                    return new OrdersStatus(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]),DateTime.Parse(attrs[3]),bool.Parse(attrs[4]));
                case EntityTypes.InternalAccount:
                    return new InternalAccount(int.Parse(attrs[0]), int.Parse(attrs[1]), attrs[2], double.Parse(attrs[3]));
            }

            return myObject;
        }
    }
}