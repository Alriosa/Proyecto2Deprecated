function getShoppingCartCount() {

    service = "ShoppingCart";
    ctrlActions = new ControlActions();

    if (null != localStorage.getItem('Credentials')) {
        credentials = JSON.parse(localStorage.getItem('Credentials'));

        if (credentials.StoreId == 0 && credentials.ShippingProviderId == 0 && count > 0) {
            count = ctrlActions.GetToAPIAsync(service + '/RetrieveItemsCount?userId=' + credentials.UserId).Data;
            if (count > 0)
                document.querySelector('#cartCount').classList.remove('d-none');
            document.querySelector('#cartCount').innerHTML = count;
        }
    }
}

window.onload = function () {
    document.querySelector('.header-cart').addEventListener("click", gotToMyCart);
}

$(document).ready(function() {
    getShoppingCartCount();
});

function gotToMyCart() {
    window.location.href = '/ShoppingCart';
}