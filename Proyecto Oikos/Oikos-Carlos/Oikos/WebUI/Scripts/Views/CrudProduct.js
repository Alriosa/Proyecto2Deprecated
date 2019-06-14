var txtSellingPrice;
var txtTaxPercentage;
var pId;
var productStockQuantity;

function Crud() {

    this.tblProducts = "tblProducts";
    this.service = "product";
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function() {
        this.ctrlActions.FillTable(this.service + '/RetrieveAll', this.tblProducts, false, "0,1,4,5,6,7");
    }

    this.ReloadTable = function() {
        this.ctrlActions.FillTable(this.service + '/RetrieveAll', this.tblProducts, true);
        $("#updateModal").modal('hide');
        $(".reset").click(function() {
            $(this).closest('frmProduct').find("input[type=text], textarea").val("");
        });
    }
    this.ProductStockRedirect = function () {
        sessionStorage.setItem("productId", $("#txtProductId").val());
        window.location.href = 'http://localhost:57619/ListProductInventory';
    }
    this.AddStock = function () {
        sessionStorage.setItem("productId", $("#txtProductId").val());
        window.location.href = 'http://localhost:57619/AddStock';
    }

    this.RetrieveAllByStoreId = function() {
        this.ctrlActions.FillTable(this.service + '/RetrieveStoreProducts?storeId=' + getActiveStoreId(),
            this.tblProducts,
            false,
            "0,1,4,5,6,7");
    }

    this.RetrieveByProductId = function() {
        pId = getAllUrlParams().productid;
        if (pId === undefined) {
            window.location.href = 'http://localhost:57619/';
        } else {
            tempData = this.ctrlActions.GetToAPIAsync(this.service + '/RetrieveById?pId=' + pId).Data;
            LoadProduct();
        }
    }

    this.ReloadTableByStoreId = function() {
        this.ctrlActions.FillTable(this.service + '/RetrieveStoreProducts?storeId=', this.tblProducts, true);
        $("#updateModal").modal('hide');
        $(".reset").click(function() {
            $(this).closest('frmProduct').find("input[type=text], textarea").val("");
        });
    }

    this.Create = function() {
        var entityData = this.ctrlActions.GetDataForm('frmProduct');
        var data = {
            StoreId: getActiveStoreId(),
            Name: entityData.Name,
            Description: entityData.Description,
            TaxPercentage: AutoNumeric.getNumber('#txtTaxPercentage'),
            SellingPrice: txtSellingPrice.getNumber('#txtTSellingPrice'),
            ProductProviderId: entityData.ProductProviderId,
            IsActive: true
        };

        this.ctrlActions.PostToAPI('Product' + '/create', data);

        // Creates product media for the product
        var lastProductId = this.ctrlActions.GetToAPIAsync('Product' + '/RetrieveLastProductId').Data;

        for (i = 0; i < mediaQuantity; i++) {
            var data2 = {
                ProductId: lastProductId,
                Url: mediaList[i],
                IsActive: true
            };
            this.ctrlActions.PostToAPI('ProductMedia' + '/create', data2);
        }

        this.ctrlActions.AddCategoriesToObject("product", "checkboxCont",lastProductId);

//        window.history.back();
        window.location.href = 'http://localhost:57619/ListProducts';
    }

    this.Update = function() {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmProduct');
        var data = {
            ProductId: entityData.ProductId,
            StoreId: entityData.StoreId,
            Name: entityData.Name,
            Description: entityData.Description,
            TaxPercentage: AutoNumeric.getNumber('#txtTaxPercentage'),
            SellingPrice: txtSellingPrice.getNumber('#txtTSellingPrice'),
            ProductProviderId: entityData.ProductProviderId,
            IsActive: entityData.IsActive
        };
        this.ctrlActions.PutToAPI(this.service + '/update', data);

        SoftDeleteMedia();

        var productId = getActiveProduct();

        for (i = 0; i < newMediaToRegister.length; i++) {
            var data2 = {
                ProductId: productId,
                Url: newMediaToRegister[i],
                IsActive: true
            }
            this.ctrlActions.PostToAPI('ProductMedia' + '/create', data2);
        }
        mediaToDeleteList = new Array();
        newMediaToRegister = new Array();
        this.ReloadTable();
    }

    this.Delete = function() {

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
                    var entityData = {};
                    entityData = this.ctrlActions.GetDataForm('frmProduct');
                    var data = {
                        ProductId: entityData.ProductId
                    };
                    this.ctrlActions.DeleteToAPI(this.service + '/delete', data);
                    this.ReloadTable();
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

    this.BindFields = function(data) {
        $("#updateModal").modal();

        this.ctrlActions.BindFields('frmProduct', data);

        document.querySelector('#txtCurrency').value = getActiveStoreCurrencyCode();
        document.querySelector('#txtProductProviderName').value = getActiveProductProvider().Name;
        LoadProductMediaInForm();

        txtSellingPrice.set(data.SellingPrice);
        txtTaxPercentage.set(data.TaxPercentage);
    }

    this.DisplayProductInfo = function() {

    }

    this.Back = function() {
        window.history.back();
    }

    this.Validate = function() {

        if (validateAll() == true) {
            if ((document.querySelector('#whoAmI').innerHTML === "update") ||
                (document.querySelector('#whoAmI').innerHTML === "list")) {

                // Sweet alert
                const swalWithBootstrapButtons = swal.mixin({
                    confirmButtonClass: 'btn btn-success ml-2',
                    cancelButtonClass: 'btn btn-danger mr-2 ',
                    buttonsStyling: false
                });

                swalWithBootstrapButtons({
                    title: '¿Desea modificar este producto?',
                    type: 'warning',
                    showCancelButton: true,
                    text: "No podrá revertir este cambio.",
                    confirmButtonText: '¡Sí, continuar!',
                    cancelButtonText: '¡No, cancelar!',
                    reverseButtons: true
                }).then((result) => {
                    if (result.value) {

                        try {
                            this.Update();
                        } catch (err) {
                            {
                                swalWithBootstrapButtons(
                                    '¡Error!',
                                    err.message,
                                    'El producto no ha sido modificado.',
                                    'error'
                                );
                            }
                        }
                        swalWithBootstrapButtons(
                            '¡Modificado!',
                            'El producto fue modificado con éxito.',
                            'success'
                        );
                    } else if (
                        // Read more about handling dismissals
                        result.dismiss === swal.DismissReason.cancel
                    ) {
                        swalWithBootstrapButtons(
                            'Cancelado',
                            'El producto no ha sido modificado.',
                            'error'
                        );
                    }
                });
                // End sweet alert
            }
            if (document.querySelector('#whoAmI').innerHTML === "create") {
                this.Create();
            }
        }
    }
}


//ON DOCUMENT READY
$(document).ready(function() {
    var entity = new Crud();
    var cats = new Categories();
    cats.GenerateDropdown(getActiveStoreId());
    if (document.querySelector('#forWhomAmI') == null) {
        entity.RetrieveAll();
    } else if (document.querySelector('#forWhomAmI').innerHTML === "store") {
        entity.RetrieveAllByStoreId();
    }
});

// Loads in form product in localStorage
window.onload = function() {
    var entity = new Crud();

    if (document.querySelector('#whoAmI').innerHTML === "update") {
        initializeAutonumeric();
        LoadForm();
    }

    if (document.querySelector('#whoAmI').innerHTML === "list") {
        initializeAutonumeric();
    }

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        initializeAutonumeric();
        document.querySelector('#txtCurrency').value = getActiveStoreCurrencyCode();
    }

    if (document.querySelector('#whoAmI').innerHTML === "details") {
        initializeAutonumericSellingPrice();
        entity.RetrieveByProductId();
        validateIfProductIsInCart();
    };
}

function validateIfProductIsInCart() {
    var ctrlActions = new ControlActions();
    var result = ctrlActions.GetToAPIAsync('ShoppingCart/IsProductInCart?productId=' + productId + '&userId=' + getActiveUserId()).Data;
    if (result == true) {
        toggleCartButton();
    }
}

function toggleCartButton() {
    $("#btnAddToCart").toggleClass("d-none");
    $("#pdetailsQuantity").toggleClass("d-none");
    $("#btnGoToCart").toggleClass("d-none");
}

function GoToCart() {
    window.location.href = 'http://localhost:57619/ShoppingCart/';
}

function GoToLogIn() {
    window.location.href = 'http://localhost:57619/loginregister';
}

function retrieveStoreInfo() {
    var ctrlActions = new ControlActions();
    return this.ctrlActions.GetToAPIAsync('product/RetrieveStoreInfo?productId=' + tempData.ProductId).Data;
}

function initializeAutonumericSellingPrice() {
    txtSellingPrice = new AutoNumeric('#txtSellingPrice',
        {
            negative: false,
            alwaysAllowDecimalCharacter: true,
            outputFormat: 'number',
            currencySymbol: '₡',
            minimumValue: '0'
        });
}

function initializeAutonumeric() {
    txtSellingPrice = new AutoNumeric('#txtSellingPrice',
        {
            negative: false,
            alwaysAllowDecimalCharacter: true,
            outputFormat: 'number',
            minimumValue: '0'
        });

    txtTaxPercentage = new AutoNumeric('#txtTaxPercentage', AutoNumeric.getPredefinedOptions().percentageUS2decPos);
}

function getProductUnitsQuantity() {
    ctrlActions = new ControlActions();
    productStockQuantity = ctrlActions.GetToAPIAsync('inventory/RetrieveProductUnitsCount?productId=' + productId).Data;
    document.querySelector('#txtQuantity').max = productStockQuantity;
    if (productStockQuantity > 1) {
        document.querySelector("#txtStockQuantity").innerHTML = productStockQuantity + " unidades disponibles";
    } else if (productStockQuantity == 1) {
        document.querySelector("#txtStockQuantity").innerHTML = productStockQuantity + " unidad disponible";
    } else {
        document.querySelector("#badgeOutOfStock").classList.remove('d-none');
        document.querySelector("#pdetailsQuantity").classList.add('d-none');
        document.querySelector("#txtStockQuantity").innerHTML = "Más unidades vienen en camino." +
            "<br>" + "Reabastecimiento esperado: " + getRestockDate();
    }
}

function getRestockDate() {
    var today = new Date();

    var date = new Date(today);
    var newdate = new Date(date);

    newdate.setDate(newdate.getDate() + 8);

    var dd = newdate.getDate();
    var mm = newdate.getMonth() + 1;
    var y = newdate.getFullYear();

    return mm + '/' + dd + '/' + y;
}

function LoadProduct() {
    document.querySelector("#badgeOutOfStock").classList.add('d-none');
    var productMediaLst = getProductMedia(tempData.ProductId);

    document.querySelector("#liBreadcrumbProductName").innerHTML = tempData.Name;
    document.querySelector('#txtName').innerHTML = tempData.Name;
    txtSellingPrice.set(tempData.SellingPrice);
    document.querySelector('#txtDescription').innerHTML = tempData.Description;

    storeURL = document.createElement('a');
    storeURL.href = '/Store?storeId=' + tempData.StoreId;
    storeURL.innerHTML = retrieveStoreInfo().Name;
    document.querySelector('#liBreadcrumbStoreName').appendChild(storeURL);
    getProductUnitsQuantity();

    for (j = 0; j < productMediaLst.length; j++) {
        if (tempData.ProductId == productMediaLst[j].ProductId) {

            switch (j) {
            case 0:
                document.querySelector('#divProductMedia1').setAttribute('data-src', productMediaLst[j].Url);
                document.querySelector('#imgProductMediaFull1').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaThumb1').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaFull1').alt = tempData.Name;
                document.querySelector('#imgProductMediaThumb1').alt = tempData.Name;
                break;
            case 1:
                document.querySelector('#divProductMedia2').setAttribute('data-src', productMediaLst[j].Url);
                document.querySelector('#imgProductMediaFull2').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaThumb2').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaFull2').alt = tempData.Name;
                document.querySelector('#imgProductMediaThumb2').alt = tempData.Name;
                break;
            case 2:
                document.querySelector('#divProductMedia3').setAttribute('data-src', productMediaLst[j].Url);
                document.querySelector('#imgProductMediaFull3').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaThumb3').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaFull3').alt = tempData.Name;
                document.querySelector('#imgProductMediaThumb3').alt = tempData.Name;
                break;
            case 3:
                document.querySelector('#divProductMedia4').setAttribute('data-src', productMediaLst[j].Url);
                document.querySelector('#imgProductMediaFull4').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaThumb4').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaFull4').alt = tempData.Name;
                document.querySelector('#imgProductMediaThumb4').alt = tempData.Name;
                break;
            case 4:
                document.querySelector('#divProductMedia5').setAttribute('data-src', productMediaLst[j].Url);
                document.querySelector('#imgProductMediaFull5').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaThumb5').src = productMediaLst[j].Url;
                document.querySelector('#imgProductMediaFull5').alt = tempData.Name;
                document.querySelector('#imgProductMediaThumb5').alt = tempData.Name;
                break;
            }
        }
    }
}

function getActiveProductMedia() {
    ctrlActions = new ControlActions();
    productId = document.querySelector('#txtProductId').value;
    return this.ctrlActions.GetToAPIAsync('productMedia/RetrieveAllByProductId?productID=' + productId).Data;
}

function getProductMedia() {
    ctrlActions = new ControlActions();
    productId = tempData.ProductId;
    return this.ctrlActions.GetToAPIAsync('productMedia/RetrieveAllByProductId?productID=' + productId).Data;
}

function getActiveProduct() {
    return document.querySelector('#txtProductId').value;
}

function getActiveStoreId() {
    ctrlActions = new ControlActions();
    userId = getActiveUserId();
    var tempStore = this.ctrlActions.GetToAPIAsync('store/RetrieveByOwnerId?ownerId=' + userId);
    return tempStore.Data.StoreId;
}

function getActiveUserId() {
    return JSON.parse(localStorage.getItem('Credentials')).UserId;
}

//cambiar al de la tienda, este es del usuario, aunque debe ser el mismo
function getActiveStoreCurrencyCode() {
    return JSON.parse(localStorage.getItem('Credentials')).CurrencyCode;
}

function getActiveProductProvider() {
    ctrlActions = new ControlActions();
    providerId = document.querySelector('#drpProvider').value;
    data = { ProductProviderId: providerId }
    return this.ctrlActions.GetToAPIAsync('ProductProvider/Retrieve/productProvider?productProviderId=' + providerId).Data;
}

function activateContainer(divId) {
    document.getElementById(divId).classList.add("active");
}

// Soft deletes product media
function SoftDeleteMedia() {
    service = "ProductMedia";
    for (i = 0; i < mediaToDeleteList.length; i++) {
        data = {
            "ProductId": getActiveProduct(),
            "Url": mediaToDeleteList[i]
        }
        ctrlActions = new ControlActions();
        ctrlActions.DeleteToAPI(this.service + '/delete', data);
    }
}

function LoadProductMediaInForm() {
    tempData = getActiveProduct();

    activeProductMedia = getActiveProductMedia();
    mediaQuantity = getActiveProductMedia().length;

    switch (mediaQuantity) {
    case 5:
        mediaList[0] = activeProductMedia[0].Url;
        mediaList[1] = activeProductMedia[1].Url;
        mediaList[2] = activeProductMedia[2].Url;
        mediaList[3] = activeProductMedia[3].Url;
        mediaList[4] = activeProductMedia[4].Url;
        break;
    case 4:
        mediaList[0] = activeProductMedia[0].Url;
        mediaList[1] = activeProductMedia[1].Url;
        mediaList[2] = activeProductMedia[2].Url;
        mediaList[3] = activeProductMedia[3].Url;
        break;
    case 3:
        mediaList[0] = activeProductMedia[0].Url;
        mediaList[1] = activeProductMedia[1].Url;
        mediaList[2] = activeProductMedia[2].Url;
        break;
    case 2:
        mediaList[0] = activeProductMedia[0].Url;
        mediaList[1] = activeProductMedia[1].Url;
        break;
    case 1:
        mediaList[0] = activeProductMedia[0].Url;
        break;
    }

    activeProductMedia = getActiveProductMedia();
    for (i = 0; i <= getActiveProductMedia().length; i++) {
        mediaQuantity = i;
        ToggleImages();
    }

    ToggleButton();
}

// Loads data for product user to edit its product
function LoadForm() {

    tempData = getActiveProduct();

    activeProductMedia = getActiveProductMedia();
    mediaQuantity = getActiveProductMedia().length;

    switch (mediaQuantity) {
    case 5:
        mediaList[0] = activeProductMedia[0].Url;
        mediaList[1] = activeProductMedia[1].Url;
        mediaList[2] = activeProductMedia[2].Url;
        mediaList[3] = activeProductMedia[3].Url;
        mediaList[4] = activeProductMedia[4].Url;
        break;
    case 4:
        mediaList[0] = activeProductMedia[0].Url;
        mediaList[1] = activeProductMedia[1].Url;
        mediaList[2] = activeProductMedia[2].Url;
        mediaList[3] = activeProductMedia[3].Url;
        break;
    case 3:
        mediaList[0] = activeProductMedia[0].Url;
        mediaList[1] = activeProductMedia[1].Url;
        mediaList[2] = activeProductMedia[2].Url;
        break;
    case 2:
        mediaList[0] = activeProductMedia[0].Url;
        mediaList[1] = activeProductMedia[1].Url;
        break;
    case 1:
        mediaList[0] = activeProductMedia[0].Url;
        break;
    }

    activeProductMedia = getActiveProductMedia();
    for (i = 0; i <= getActiveProductMedia().length; i++) {
        mediaQuantity = i;
        ToggleImages();
    }

    ToggleButton();

    ctrlActions = new ControlActions();
    ctrlActions.BindFields('frmProduct', tempData);
}


// VALIDATIONS
function validateAll() {
    var valid = false;

    visualValidation();

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if ((mediaList[0] != null) &&
            (mediaList[1] != null) &&
            (mediaList[2] != null) &&
            (document.querySelector('#txtName').value != "") &&
            (document.querySelector('#drpProvider') != null) &&
            (document.querySelector('#txtDescription').value != "") &&
            (document.querySelector('#txtCurrency').value != "") &&
            (document.querySelector('#txtSellingPrice').value != "") &&
            (document.querySelector('#txtTaxPercentage').value != "")) {
            valid = true;
            document.querySelector('#alertMessage').classList.add('d-none');
        }
    }

    if ((document.querySelector('#whoAmI').innerHTML === "list") ||
        (document.querySelector('#whoAmI').innerHTML === "update")) {
        if ((mediaList[0] != null) &&
            (mediaList[1] != null) &&
            (mediaList[2] != null) &&
            (document.querySelector('#txtName').value != "") &&
            (document.querySelector('#txtDescription').value != "") &&
            (document.querySelector('#txtCurrency').value != "") &&
            (document.querySelector('#txtSellingPrice').value != "") &&
            (document.querySelector('#txtTaxPercentage').value != "")) {
            valid = true;
            document.querySelector('#alertMessage').classList.add('d-none');
        }
    }

    if (valid == false) {
        document.querySelector('#alertMessage').classList.remove('d-none');
    }

    return valid;
}

function visualValidation() {

    this.ctrlActions = new ControlActions();

    if (mediaList[0] != null) {
        setValidInput('#divImgProduct1');
    } else {
        setInvalidInput('#divImgProduct1');
    }

    if (mediaList[1] != null) {
        setValidInput('#divImgProduct2');
    } else {
        setInvalidInput('#divImgProduct2');
    }

    if (mediaList[2] != null) {
        setValidInput('#divImgProduct3');
    } else {
        setInvalidInput('#divImgProduct3');
    }

    if (document.querySelector('#txtName').value != "") {
        setValidInput('#txtName');
    } else {
        setInvalidInput('#txtName');
    }

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if (document.querySelector('#drpProvider').value !== "") {
            $("div.nice-select").removeClass("invalidInput");
        } else {
            $("div.nice-select").addClass("invalidInput");
        }
    }

    if (this.ctrlActions.GetCheckedCheckboxesAmount("checkboxCont") === 0) {
        $("#btnAddCategories").addClass("invalidInputButton");
        $("#categoriesAlertMessage").removeClass("d-none");
    } else {
        $("#btnAddCategories").removeClass("invalidInputButton");
        $("#categoriesAlertMessage").addClass("d-none");
    }

    if (document.querySelector('#txtDescription').value != "") {
        setValidInput('#txtDescription');
    } else {
        setInvalidInput('#txtDescription');
    }

    if (document.querySelector('#txtCurrency').value != "") {
        setValidInput('#txtCurrency');
    } else {
        setInvalidInput('#txtCurrency');
    }

    if (document.querySelector('#txtSellingPrice').value != "") {
        setValidInput('#txtSellingPrice');
    } else {
        setInvalidInput('#txtSellingPrice');
    }

    if (document.querySelector('#txtTaxPercentage').value != "") {
        setValidInput('#txtTaxPercentage');
    } else {
        setInvalidInput('#txtTaxPercentage');
    }

}

function setValidInput(e) {
    document.querySelector(e).classList.remove("invalidInput");
}

function setInvalidInput(e) {
    document.querySelector(e).classList.add("invalidInput");
}
