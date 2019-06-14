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
                    return new PaymentMethod(Int32.Parse(attrs[0]), attrs[1], attrs[2], int.Parse(attrs[3]), bool.Parse(attrs[4]));
                case EntityTypes.Transaction:
                    return new Transaction(int.Parse(attrs[0]), attrs[1], attrs[2], double.Parse(attrs[3]), int.Parse(attrs[4]), int.Parse(attrs[5]), int.Parse(attrs[6]));
                case EntityTypes.OrdersTransactions:
                    return new OrdersTransactions(int.Parse(attrs[0]), int.Parse(attrs[1]), int.Parse(attrs[2]));
                case EntityTypes.Store:
                    return new Store(int.Parse(attrs[0]), attrs[1], attrs[2], int.Parse(attrs[3]),
                    attrs[4], attrs[5], attrs[6], attrs[7], attrs[8], double.Parse(attrs[9]), DateTime.Parse(attrs[10]));
                case EntityTypes.Category:
                    return new Category(int.Parse(attrs[0]), attrs[1], attrs[2], bool.Parse(attrs[3]));
                case EntityTypes.Users:
                    return  new User(int.Parse(attrs[0]), attrs[1], attrs[2],attrs[3],attrs[4],attrs[5],attrs[6]);
                case EntityTypes.Currency:
                    return new Currency(attrs[0],attrs[1],attrs[2]);
                case EntityTypes.ShippingProvider:
                    return new ShippingProvider(int.Parse(attrs[0]), attrs[1], int.Parse(attrs[2]), attrs[3], double.Parse(attrs[4]), attrs[5], double.Parse(attrs[6]), attrs[7]);
            }

            return myObject;
        }
    }
}