let tempWarehouse;
let tempCommission;

function CrudProductMedia() {

    this.tblProductMedias = "tblProductMedias";
    this.service = "product";
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + '/RetrieveAll', this.tblProductMedias, false, "0,3,4,5,6,7,9,10");
    }

    this.Create = function () {
        var entityData = this.ctrlActions.GetDataForm('frmProductMedia');
        var data = {
            StoreId: getActiveStoreId(),
            Name: entityData.Name,
            Description: entityData.Description,
            TaxPercentage: entityData.TaxPercentage,
            SellingPrice: entityData.SellingPrice,
            ProductMediaProviderId: 1,
            IsActive: true
        };

        this.ctrlActions.PostToAPI(this.service + '/create', data);
        this.ReloadTable();
    }

    this.Update = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmProductMedia');
        var data = {
            ProductMediaId: entityData.ProductMediaId,
            StoreId: getActiveStoreId(),
            Name: entityData.Name,
            Description: entityData.Description,
            TaxPercentage: entityData.TaxPercentage,
            SellingPrice: entityData.SellingPrice,
            ProductMediaProviderId: entityData.ProductMediaProviderId,
            IsActive: entityData.IsActive
        };
        this.ctrlActions.PutToAPI(this.service + '/update', data);
        this.ReloadTable();
    }

    this.Delete = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmProductMedia');
        var data = {
            ProductMediaId: entityData.ProductMediaId
        };
        this.ctrlActions.DeleteToAPI(this.service + '/delete', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmProductMedia', data);
    }

    this.Back = function () {
        window.history.back();
    }

}

//ON DOCUMENT READY
$(document).ready(function () {

    var entity = new Crud();
    entity.RetrieveAll();
});

// Loads in form productd in localStorage
window.onload = function () {
//    LoadForm();
};

function getActiveStoreId() {
    return JSON.parse(localStorage.getItem('activeStore')).StoreId;
}

function getActiveStoreCurrencyCode() {
    return JSON.parse(localStorage.getItem('activeStore')).CurrencyCode;
}

function activateContainer(divId) {
    document.getElementById(divId).classList.add("active");
}

// Loads data for product user to edit its product
function LoadForm() {

    //    activeStore quemado en localStorage QUITAR!!!!
//    var product = {
//        StoreId: 1,
//        Identification: "2-202-20000",
//        Name: "Tienda 202",
//        Owner: 1,
//        Logo: "https://res.cloudinary.com/oikos-product/image/upload/v1542384791/oikos/nh6rgo0jwzsppgie27t6.jpg",
//        WarehouseLatitude: 15.89104951847282,
//        WarehouseLongitude: -89.36093949999997,
//        Description: "desc",
//        CurrencyCode: "CAD",
//        StoreStatusCode: "STS01",
//        Commission: 0.05,
//        CreationDate: "2018-11-17T00:00:00"
//    }
//    localStorage.setItem('activeStore', JSON.stringify(product));
//
//    tempData = JSON.parse(localStorage.getItem('activeStore'));
//    ctrlActions = new ControlActions();
//
//    document.querySelector('#txtCurrency').value = getActiveStoreCurrencyCode();
//    document.querySelector('#txtStoreId').value = getActiveStoreId();
//    document.querySelector('#txtIsActive').value = true;

    //    ctrlActions.BindFields('frmProductMedia', tempData);
    //    document.querySelector('#imgStoreLogo').src = tempData.Logo;

}

// VALIdATIONS

function validateInput(id) {
    var e = document.getElementById(id);
    switch (e.getAttribute("format")) {
        case "phone":
            validatePhoneInput(e);
            break;
        case "identification":
            validateIdentificationInput(e);
            break;
        default:
            break;
    }
}

function validatePhoneInput(e) {
    var regex = /(\+\d{3})*\d{4}\-\d{4}/;
    var res = regex.test(e.value);
    res
        ? setValidInput(e)
        : setInvalidInput(e);
}

function validateIdentificationInput(e) {
    var regex = /\d(-\d{4}){2}/;
    var res = regex.test(e.value);
    res
        ? setValidInput(e)
        : setInvalidInput(e);
}

function setValidInput(e) {
    e.classList.remove("is-invalid");
    e.classList.add("is-valid");
}

function setInvalidInput(e) {
    e.classList.remove("is-valid");
    e.classList.add("is-invalid");
}
