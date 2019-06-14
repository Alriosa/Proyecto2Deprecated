function getShoppingCartCount() {
    try {
        service = "ShoppingCart";
        ctrlActions = new ControlActions();

        userId = JSON.parse(localStorage.getItem('Credentials')).UserId;
        var count = ctrlActions.GetToAPIAsync(service + '/RetrieveItemsCount?userId=' + userId).Data;

        if (count > 0) {
            document.querySelector('#cartCount').classList.remove('d-none');
            document.querySelector('#cartCount').innerHTML = count;
        }

    } catch (e) {
        console.log(e);
    } 
}

$(document).ready(function () {
    getShoppingCartCount();
});

