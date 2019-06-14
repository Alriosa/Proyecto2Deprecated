var productLst;
var productMediaLst;
var itemsToShowPerPage = 12;
var itemsCount = 0;
var itemsToShow = 0;
var pagesToShow = 0;
var sId;

function calculateItemsPagination() {
    itemsCount = productLst.length;
    itemsToShow = itemsCount;

    if (itemsCount <= itemsToShowPerPage) {
        pagesToShow++;
    } if (itemsCount > itemsToShowPerPage) {
        pagesToShow = Math.ceil(itemsCount / itemsToShowPerPage);
    }
}

//function paginateItems() {
//    itemsLeftToShow = itemsCount;
//
//    if (itemsCount <= itemsToShowPerPage) {
//        itemsShown += pagesShown * itemsToShowPerPage;
//        itemsToShow -= itemsShown;
//        itemsLeftToShow -= itemsLeftToShow;
//    } else if (itemsCount > itemsToShowPerPage) {
//        itemsShown += pagesShown * itemsToShowPerPage;
//        itemsLeftToShow = 
//            itemsToShow -= itemsShown;
//        itemsLeftToShow -= itemsLeftToShow;
//    } else if (itemsCount > itemsToShowPerPage && itemsLeftToShow <= 12) {
//        itemsToShow -= itemsShown;
//        itemsLeftToShow -= itemsLeftToShow; 
//    }
//    printPagination();
//    pagesShown++;
//    pagesToShow--;
//}

function printPagination() {
    console.log("Mostrando " + itemsShown + " productos de " + itemsCount);
}

function alertNoProducts() {
    var alert = "<div class=\"alert alert-info\" role=\"alert\">\r\n            <h4 class=\"alert-heading\">\u00A1A\u00FAn no hay productos!<\/h4>\r\n            <p>Si sos el administrador de la tienda, por favor proced\u00E9 a crear nuevos productos.<\/p>\r\n            <hr>\r\n            <p class=\"mb-0\">Si necesit\u00E1s asistencia contactanos a <a href=\"mailto:ayuda@oikos.com\">ayuda@oikos.com<\/a>.<\/p>\r\n        <\/div>";
    document.querySelector('#alert').innerHTML = alert;
    document.querySelector('#loadMore').classList.add("d-none");
}
function validateProductExistence() {
    if (productLst.length == 0 || productLst == null) {
        return false;
    } else {
        return true;
    }
}

function Storefront() {
    this.service1 = "Product";
    this.service2 = "ProductMedia";
    this.service3 = "Store";
    this.ctrlActions = new ControlActions();

    this.RetrieveAllProducts = function () {
        document.querySelector('#divIsStore').classList.add('d-none');
        productLst = this.ctrlActions.GetToAPIAsync(this.service1 + '/RetrieveAll').Data;
        DrawProducts();
    }

    this.RetrieveAllStoreProducts = function () {
        productLstDb = this.ctrlActions.GetToAPIAsync(this.service1 + '/RetrieveStoreProducts?storeId=' + sId);
        productLst = productLstDb.Data;
        storeDb = this.ctrlActions.GetToAPIAsync(this.service3 + '/Retrieve?storeId=' + sId);
        store = storeDb.Data;
        document.querySelector('#divIsStore').classList.remove('d-none');
        document.querySelector('#imgStorefrontLogo').src = store.Logo;
        document.querySelector('#lblStoreName').src = store.Logo;

        if (validateProductExistence() === true) {
            DrawProducts();
        } else {
            alertNoProducts();
        }
    }

    this.RetrieveAllProductMedia = function() {
        productMediaLst = this.ctrlActions.GetToAPIAsync(this.service2 + '/RetrieveAll').Data;
    }

    this.shouldShowMore = function() {
        if (pagesToShow <= 0) {
            document.querySelector('#loadMore').classList.add("d-none");
        }
    }

//    this.RetrieveAllProductMediaByProductId = function() {
//        productLst = this.ctrlActions.GetToAPIAsync(this.service1 + '/RetrieveAllByProductId?productID=productID');
//        DrawProducts();
//    }

//    this.RetrieveAllProductByCategory = function () {
//        productLst = this.ctrlActions.GetToAPIAsync(this.service1);
//    }

//    this.RetrieveAllProductByCategoryByStoreId = function () {
//        productLst = this.ctrlActions.GetToAPIAsync(this.service1);
//    }

}

//ON DOCUMENT READY
$(document).ready(function() {
    var entity = new Storefront();
    sId = getAllUrlParams().storeid;
    entity.RetrieveAllProductMedia();

    if (sId == null) {
        entity.RetrieveAllProducts();
    } else {
        entity.RetrieveAllStoreProducts();
        entity.shouldShowMore();
    }
});

function showMore() {
    $(".item").slice(0, itemsToShowPerPage).show();
    $("#loadMore").on('click',
        function(e) {
            e.preventDefault();
            $(".item:hidden").slice(0, itemsToShowPerPage).slideDown();
            if ($(".item:hidden").length == 0) {
                $("#load").fadeOut('slow');
            }
            $('html,body').animate({
                    scrollTop: $(this).offset().top
                },
                1500);
            pagesToShow--;
        });
}

function DrawProducts() {

    var productsInnerHTML = "";
    var productUrl = "https://res.cloudinary.com/oikos-store/image/upload/v1542776897/product_placeholder.svg";

    for (i = 0; i < productLst.length; i++) {
        product = productLst[i];

        for (j = 0; j < productMediaLst.length; j++) {
            if (productLst[i].ProductId == productMediaLst[j].ProductId) {
                productUrl = productMediaLst[j].Url;
                j = productMediaLst.length;
            }
        }

        document.querySelector('#txtSellingPrice').innerHTML = product.SellingPrice;
//        txtSellingPrice = document.querySelector('#txtSellingPrice').getNumericString();

        new AutoNumeric('#txtSellingPrice',
            {
                negative: false,
                alwaysAllowDecimalCharacter: true,
                outputFormat: 'number',
                minimumValue: '0',
                currencySymbol: '₡'
            });

        productsInnerHTML +=
            "<!-- Single Product Beginning -->" +
            "<div class=\"item col-lg-3 col-md-4 col-sm-6 col-12\">" +
            "<article class=\"hoproduct store-product-card\">" +
            "<div class=\"hoproduct-image\">" +
            "<a class=\"hoproduct-thumb\" href =\"Product?productId=" +
            product.ProductId +
            "\">" +
            "<img class=\"hoproduct-frontimage store-product-image\" src =\"" +
            productUrl +
            "\" alt =\"" +
            product.Name +
            "\">" +
            "</a>" +
            "<ul class=\"hoproduct-actionbox\">" +
            "<li><a href =\"Product?productId=" +
            product.ProductId +
            "\" class=\"quickview-trigger\"><i class=\"lnr lnr-eye\"></i></a></li>" +
            "</ul>" +
            "</div>" +
            "<div class=\"hoproduct-content store-card-product-name\">" +
            "<h5 class=\"hoproduct-title store-card-product-name\">" +
            "<a href =\"Product?productId=" +
            product.ProductId +
            "\"  <span class=\"store-card-product-name\">" +
            product.Name +
            "</span>" +
            "</a>" +
            "</h5>" +
            "<div class=\"hoproduct-pricebox\">" +
            "<div class=\"pricebox\">" +
            "<div id=\"editableDom\" contenteditable=\"true\" class=\"price\">" +
            txtSellingPrice.innerHTML +
            "</div>" +
            "</div>" +
            "</div>" +
            "</div>" +
            "</article>" +
            "</div>" +
            "<!--// Single Product End-->";
    }
    calculateItemsPagination();
    pagesToShow--;
    document.querySelector('#storeFront').innerHTML = productsInnerHTML;
    showMore();
}