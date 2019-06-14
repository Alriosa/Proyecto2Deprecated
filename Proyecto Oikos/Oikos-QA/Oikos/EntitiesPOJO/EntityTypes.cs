using System.ComponentModel;

namespace EntitiesPOJO {
    public enum EntityTypes {
        [Description("EXCEPTIONS")] ApplicationMessage,
        [Description("PAYMENT_METHODS")] PaymentMethod,
        [Description("TRANSACTIONS")] Transaction,
        [Description("ORDERS_TRANSACTIONS")] OrdersTransactions,
        [Description("CATEGORIES")] Category,
        [Description("USERS_CATEGORIES")] UsersCategories,
        [Description("STORES_CATEGORIES")] StoresCategories,
        [Description("PRODUCT_CATEGORIES")] ProductsCategories,
        [Description("PRODUCT_REQUEST_CATEGORIES")] ProductRequestsCategories,
        [Description("USERS")] Users,
        [Description("CODES ")] Currency,
        [Description("STORE")] Store,
        [Description("SHIPPING_PROVIDER")] ShippingProvider,
    }
}