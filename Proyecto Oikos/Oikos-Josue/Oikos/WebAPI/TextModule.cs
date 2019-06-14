using System;
using EntitiesPOJO;

namespace WebAPI {
    public class TextModule {
        public BaseEntity AdaptObject(BaseEntity entity, EntityTypes type, bool toDataBase) {
            switch (type) {
                case EntityTypes.Category:
                    var cat = (Category) Convert.ChangeType(entity, typeof(Category));
                    LoopThroughObject(cat, toDataBase);
                    return cat;
                case EntityTypes.ProductRequestResponses:
                    var response =
                        (ProductRequestResponses) Convert.ChangeType(entity, typeof(ProductRequestResponses));
                    LoopThroughObject(response, toDataBase);
                    return response;
                case EntityTypes.ProductMedia:
                    var pm = (ProductMedia)Convert.ChangeType(entity, typeof(ProductMedia));
                    LoopThroughObject(pm, toDataBase);
                    return pm;
                case EntityTypes.Product:
                    var p = (Product)Convert.ChangeType(entity, typeof(Product));
                    LoopThroughObject(p, toDataBase);
                    return p;
                case EntityTypes.Store:
                    var s = (Store)Convert.ChangeType(entity, typeof(Store));
                    LoopThroughObject(s, toDataBase);
                    return s;
                case EntityTypes.Coupon:
                    var c = (Coupon)Convert.ChangeType(entity, typeof(Coupon));
                    LoopThroughObject(c, toDataBase);
                    return c;
                case EntityTypes.CustomerServiceType:
                    var cst = (Coupon)Convert.ChangeType(entity, typeof(CustomerServiceType));
                    LoopThroughObject(cst, toDataBase);
                    return cst;
                case EntityTypes.CustomerServiceRequest:
                    var csr = (CustomerServiceRequest)Convert.ChangeType(entity, typeof(CustomerServiceRequest));
                    LoopThroughObject(csr, toDataBase);
                    return csr;
                case EntityTypes.Inventory:
                    var i = (Inventory)Convert.ChangeType(entity, typeof(Inventory));
                    LoopThroughObject(i, toDataBase);
                    return i;
                case EntityTypes.OptionList:
                    var ol = (OptionList)Convert.ChangeType(entity, typeof(OptionList));
                    LoopThroughObject(ol, toDataBase);
                    return ol;
                case EntityTypes.OrdersTransactions:
                    var ot = (OrdersTransactions)Convert.ChangeType(entity, typeof(OrdersTransactions));
                    LoopThroughObject(ot, toDataBase);
                    return ot;
                case EntityTypes.PaymentMethod:
                    var pym = (PaymentMethod)Convert.ChangeType(entity, typeof(PaymentMethod));
                    LoopThroughObject(pym, toDataBase);
                    return pym;
                case EntityTypes.ProductProvider:
                    var pp = (ProductProvider)Convert.ChangeType(entity, typeof(ProductProvider));
                    LoopThroughObject(pp, toDataBase);
                    return pp;
                case EntityTypes.ShippingMethods:
                    var sm = (ShippingMethods)Convert.ChangeType(entity, typeof(ShippingMethods));
                    LoopThroughObject(sm, toDataBase);
                    return sm;
                case EntityTypes.ShippingProvider:
                    var sp = (ShippingProvider)Convert.ChangeType(entity, typeof(ShippingProvider));
                    LoopThroughObject(sp, toDataBase);
                    return sp;
                case EntityTypes.Transaction:
                    var t = (Transaction)Convert.ChangeType(entity, typeof(Transaction));
                    LoopThroughObject(t, toDataBase);
                    return t;
                case EntityTypes.UserContacts:
                    var uc = (UserContacts)Convert.ChangeType(entity, typeof(UserContacts));
                    LoopThroughObject(uc, toDataBase);
                    return uc;
                case EntityTypes.UserLocation:
                    var ul = (UserLocation)Convert.ChangeType(entity, typeof(UserLocation));
                    LoopThroughObject(ul, toDataBase);
                    return ul;
                case EntityTypes.Users:
                    var u = (UserLocation)Convert.ChangeType(entity, typeof(UserLocation));
                    LoopThroughObject(u, toDataBase);
                    return u;
                case EntityTypes.ProductRequest:
                    var request =
                        (ProductRequest)Convert.ChangeType(entity, typeof(ProductRequest));
                    LoopThroughObject(request, toDataBase);
                    return request;
                default:
                    return default(BaseEntity);
            }
        }

        private void LoopThroughObject<T>(T obj, bool toDataBase) {
            foreach (var property in obj.GetType().GetProperties()) {
                if (property.PropertyType != typeof(string)) continue;
                var value = (string) property.GetValue(obj, null);
                property.SetValue(obj, toDataBase ? InsertVerticalBarsInString(value) : InsertCommasInString(value),
                    null);
            }
        }

        public string InsertVerticalBarsInString(string givenString) {
            return givenString.Replace(',', '|');
        }

        public string InsertCommasInString(string givenString) {
            return givenString.Replace('|', ',');
        }
    }
}
