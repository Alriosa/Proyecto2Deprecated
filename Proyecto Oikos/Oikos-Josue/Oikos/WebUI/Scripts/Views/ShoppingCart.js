var elements = {};
var ShoppingCartCollection;
var cartItemsHtml;
var ShoppingCartId;
var pId;
var productName;
var productUrl;
var productMediaUrl;
var unitPrice;
var selectedUnits;
var stockUnits;
var isOffer;
var total;
var taxes;
var subtotal;

function validateIfNoSessionIsActive() {
    try {
        getActiveUserId();
    } catch (e) {
        var alert =
            "<div class=\"container alert alert-info\" role=\"alert\"><h4 class=\"alert-heading\">Tu carrito está vacío.</h4>" +
                "<p>Regresá a la <a href=\"\\\">tienda</a> o <a href=\"\LoginRegister\">iniciá sesión</a> para continuar.</p> </div>";
        document.querySelector('.page-content').innerHTML = alert;
        document.querySelector('#progressBar').classList.add('d-none');
    }
}

function getActiveUserId() {
    return JSON.parse(localStorage.getItem('Credentials')).UserId;
}

function AddToCart() {
    ctrlActions = new ControlActions();

    var data = {
        UserId: getActiveUserId(),
        ProductId: pId,
        Quantity: document.querySelector('#txtQuantity').value,
        IsActive: true,
        Price: 0.00,
        IsOffer: false 
    };

    try {
        ctrlActions.PostToAPI('ShoppingCart' + '/Create', data);
        toggleCartButton();
    } catch (e) {
        throw e;
    }

}

$(document).ready(function() {
    if (document.querySelector('#whoAmI').innerHTML === "cart") {

        if (!validateIfNoSessionIsActive()) {
            //VALIDAR SI HAY CREDENCIALES ACTIVAS EN LOCALSTORAGE
            var entity = new CrudShoppingCart();

            entity.RetrieveAll();
            LoadItemsInCart();
        }

    };
});

function LoadItemsInCart() {
    isOffer = false;
    cartItemsHtml = "";
    cartElement = document.querySelector('#cartItemsList');
    cartElement.innerHTML = ""

    for (s = 0; s < ShoppingCartCollection.length; s++) {
        pShoppingCartId = ShoppingCartCollection[s].ShoppingCartId;
        pId = ShoppingCartCollection[s].ProductId;
        productUrl = ShoppingCartCollection[s].ProductUrl;
        productMediaUrl = ShoppingCartCollection[s].ProductMediaUrl;
        unitPrice = ShoppingCartCollection[s].UnitPrice;
        selectedUnits = ShoppingCartCollection[s].Units;
        stockUnits = ShoppingCartCollection[s].UnitsAvailable;
        isOffer = ShoppingCartCollection[s].IsOffer;

        if (ShoppingCartCollection[s].IsOffer) {
            productName = "(OFERTA) " + ShoppingCartCollection[s].ProductName;
        } else {
            productName = ShoppingCartCollection[s].ProductName;
        }

        cartItemsHtml += GenerateCartItemsHtml(pShoppingCartId, pId, stockUnits, selectedUnits);
    }
    cartElement.innerHTML = cartItemsHtml;

    $('[id^=txtLineSubtotal],[id^=txtUnitPrice]').each(function() {
        let autonumericElement = new AutoNumeric('#' + this.id,
            {
                negative: false,
                alwaysAllowDecimalCharacter: true,
                outputFormat: 'number',
                currencySymbol: '₡',
                minimumValue: '0'
            });

        elements[this.id] = autonumericElement;
    });
    getShoppingCartCount();
    calculateCartSubtotal();
}

function GenerateCartItemsHtml(pShoppingCartId, pId, stockUnits, selectedUnits) {

    return "<tr id=\"" +
        pId +
        "\">" +
        "<td>" +
        "<a id=\"txtProductUrl" +
        pId +
        "\" href=\"" +
        productUrl +
        "\" class=\"product-image\">" +
        "<img id=\"txtImageUrl" +
        pId +
        "\" src=\"" +
        productMediaUrl +
        "\" alt=\"product-image\">" +
        "</a>" +
        "</td>" +
        "<td>" +
        "<a id=\"txtProductUrl" +
        pId +
        "\" href=\"" +
        productUrl +
        "\" class=\"product-title cart-product-name\">" +
        productName +
        "</a>" +
        "</td>" +
        "<td id=\"txtUnitPrice" +
        pId +
        "\">" +
        unitPrice +
        "</td>" +
        "<td>" +
        GenerateSelect(pShoppingCartId, pId, stockUnits, selectedUnits, isOffer) +
        "</td>" +
        "<td>" +
        "<span id=\"txtLineSubtotal" +
        pId +
        "\" class=\"total-price subtotal" +
        pId +
        "\">" +
        (selectedUnits * unitPrice) +
        "</span>" +
        "</td>" +
        "<td>" +
        "<button id=\"btnRemoveItem" +
        pId +
        "\" class=\"remove-product\" type=\"button\" onclick='(function(){ Delete(" +
        pShoppingCartId +
        "); })();'>" +
        "<i class=\"ion ion-ios-close\"></i></button>" +
        "</td>" +
        "</tr>" +
        "<script> this.txtLineSubtotal = new AutoNumeric(\"#txtLineSubtotal" +
        pId +
        "," +
        "{" +
        "negative: false," +
        "alwaysAllowDecimalCharacter: true," +
        "outputFormat: \"number\"," +
        "currencySymbol: '₡'\"," +
        "minimumValue: '0\"" +
        "});</script>";

}

function initializeTotalsAutoNumeric() {
    txtSubtotal = new AutoNumeric('#txtSubtotal',
        {
            negative: false,
            alwaysAllowDecimalCharacter: true,
            outputFormat: 'number',
            currencySymbol: '₡',
            minimumValue: '0'
        });

    txtTaxes = new AutoNumeric('#txtTaxes',
        {
            negative: false,
            alwaysAllowDecimalCharacter: true,
            outputFormat: 'number',
            currencySymbol: '₡',
            minimumValue: '0'
        });

    txtTotal = new AutoNumeric('#txtTotal',
        {
            negative: false,
            alwaysAllowDecimalCharacter: true,
            outputFormat: 'number',
            currencySymbol: '₡',
            minimumValue: '0'
        });

}

function calculateCartSubtotal() {
    total = 0;
    taxes = 0;
    subtotal = 0;

    for (i = 0; i < ShoppingCartCollection.length; i++) {
        total += ShoppingCartCollection[i].ItemSubtotal;
        taxes += (ShoppingCartCollection[i].ItemSubtotal * ShoppingCartCollection[i].Tax) / 100;
    }
    subtotal = total - taxes;

    initializeTotalsAutoNumeric();
    txtSubtotal.set(subtotal);
    txtTaxes.set(taxes);
    txtTotal.set(total);
}

function setLineSubtotal(id) {

    let unit = $('#txtUnits' + id).val();
    let unitPrice = elements['txtUnitPrice' + id].get();

    elements["txtLineSubtotal" + id].set(unitPrice * unit);
}

function GenerateSelect(cartId, id, stock, selectedUnits, isOffer) {
    this.txtLineSubtotal;
    var select = "<select id =\"txtUnits" + id + "\" onchange='Update(" + cartId + ", " + id + ");'>";

    if (!isOffer) {
        for (j = 1; j <= stock; j++) {
            if (j === selectedUnits) {
                select += "<option value=" + j + " selected>" + j + "</option>";
            } else {
                select += "<option value=" + j + ">" + j + "</option>";
            }
        }
    } else {
        select += "<option value=" + selectedUnits + " selected>" + selectedUnits + "</option>";
    }
    select += "</select>";

    return select;
}

function Update(pCartId, pId) {
    var service = "ShoppingCart";
    var ctrlActions = new ControlActions();

    var shoppingCartId;
    var userId;
    var productId;
    var unitPrice;
    var units;
    var isOffer;
    var isActive;
    var data;

    for (s = 0; s < ShoppingCartCollection.length; s++) {
        var entity = new CrudShoppingCart();

        if (pCartId == ShoppingCartCollection[s].ShoppingCartId && pId == ShoppingCartCollection[s].ProductId) {
            shoppingCartId = ShoppingCartCollection[s].ShoppingCartId;
            userId = ShoppingCartCollection[s].UserId;
            productId = ShoppingCartCollection[s].ProductId;
            unitPrice = ShoppingCartCollection[s].UnitPrice;
            units = $('#txtUnits' + pId).val();
            isOffer = ShoppingCartCollection[s].IsOffer;
            isActive = ShoppingCartCollection[s].IsActive;
        }
    }
            data = {
                ShoppingCartId: shoppingCartId,
                UserId: userId,
                ProductId: productId,
                Quantity: units,
                IsActive: isActive,
                Price: unitPrice,
                IsOffer: isOffer
            };
    
    ctrlActions.PutToAPI(service + '/update', data);
    entity.ReloadCart();
}

function Delete(shoppingCartId) {
    service = "ShoppingCart";
    var ctrlActions = new ControlActions();

    item = {
        ShoppingCartId: shoppingCartId
    };

    var entity = new CrudShoppingCart();

    // Sweet alert
    const swalWithBootstrapButtons = swal.mixin({
        confirmButtonClass: 'btn btn-success ml-2',
        cancelButtonClass: 'btn btn-danger mr-2 ',
        buttonsStyling: false
    });

    swalWithBootstrapButtons({
        title: '¿Desea eliminar este producto?',
        type: 'warning',
        showCancelButton: true,
        text: "No podrá revertir este cambio.",
        confirmButtonText: '¡Sí, continuar!',
        cancelButtonText: '¡No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {

            try {
                ctrlActions.DeleteToAPI(service + '/Delete?shoppingCartId=', item);
                entity.ReloadCart();
            } catch (err) {
                {
                    swalWithBootstrapButtons(
                        '¡Error!',
                        err.message,
                        'El producto no ha sido eliminado.',
                        'error'
                    );
                }
            }
            swalWithBootstrapButtons(
                '¡Eliminado!',
                'El producto fue eliminado con éxito.',
                'success'
            );
        } else if (
            // Read more about handling dismissals
            result.dismiss === swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons(
                'Cancelado',
                'El producto no ha sido eliminado.',
                'error'
            );
        }
    });
    // End sweet alert
}

function CrudShoppingCart() {

    this.service = "ShoppingCart";
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function() {
        ShoppingCartCollection = this.ctrlActions
            .GetToAPIAsync(this.service + '/RetrieveShoppingBigCart?userId=' + getActiveUserId()).Data;
        LoadItemsInCart();
    }

    this.ReloadCart = function() {
        this.RetrieveAll();
        getShoppingCartCount();
        LoadItemsInCart();
    }
}
