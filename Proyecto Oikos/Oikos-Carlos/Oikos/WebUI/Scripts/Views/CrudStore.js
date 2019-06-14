var tempWarehouse;
var tempCommission;
var valid = true;
var txtCommission;

function Crud() {

    this.tblStores = "tblStores";
    this.service = "store";
    this.ctrlActions = new ControlActions();


    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + '/RetrieveAll', this.tblStores, false, "0,3,4,5,6,7,9,10");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + '/RetrieveAll', this.tblStores, true);
        $("#updateModal").modal('hide');
        $(".reset").click(function () {
            $(this).closest('frmStore').find("input[type=text], textarea").val("");
        });
    }

    this.Create = function () {
        var entityData = this.ctrlActions.GetDataForm('frmStore');
        var data = {
            Identification: entityData.Identification,
            Name: entityData.Name,
            Owner: getActiveUserId(),
            WarehouseLatitude: entityData.WarehouseLatitude,
            WarehouseLongitude: entityData.WarehouseLongitude,
            Logo: entityData.Logo,
            Description: entityData.Description,
            CurrencyCode: entityData.CurrencyCode
        };
        mapLat = 0.0;
        mapLng = 0.0;

        this.ctrlActions.PostToAPI(this.service + '/create', data);
        
        this.ReloadTable();
        window.location.href = 'StoreUserDashboard';
    }


    this.Update = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmStore');
        var data = {
            StoreId: entityData.StoreId,
            Identification: entityData.Identification,
            Name: entityData.Name,
            Owner: entityData.Owner,
            WarehouseLatitude: entityData.WarehouseLatitude,
            WarehouseLongitude: entityData.WarehouseLongitude,
            Logo: entityData.Logo,
            Description: entityData.Description,
            CurrencyCode: entityData.CurrencyCode,
            StoreStatusCode: entityData.StoreStatusCode,
            Commission: AutoNumeric.getNumber('#txtCommission'),
            CreationDate: entityData.CreationDate
        };
        mapLat = 0.0;
        mapLng = 0.0;

        this.ctrlActions.PutToAPI(this.service + '/update', data);
        this.ReloadTable();
    }

    this.Delete = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmStore');
        var data = {
            Identification: entityData.Identification
        };
        this.ctrlActions.DeleteToAPI(this.service + '/delete', data);
        this.ReloadTable();
    }

    this.Suspend = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmStore');
        var data = {
            Identification: entityData.Identification
        };
        this.ctrlActions.PutToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmStore', data);
        document.querySelector('#imgStoreLogo').src = tempData.Logo;
        mapLat = tempData.WarehouseLatitude;
        mapLng = tempData.WarehouseLongitude;
        document.querySelector('#txtWarehouseCoordinates').value = "Latitud: " + mapLat + "  |  Longitud: " + mapLng;
        txtCommission.set(data.Commission);

        document.querySelector('#suspend').classList.add("invisible");
        if ("STS02" == tempData.StoreStatusCode) {
            document.querySelector('#suspend').classList.remove("invisible");
        };
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

// Loads in form store stored in localStorage
window.onload = function () {
//    formatNumber();
    if (document.querySelector('#whoAmI').innerHTML === "update") {
        LoadForm();
//        formatNumber();
    }
    if (document.querySelector('#whoAmI').innerHTML === "create") {
//        formatNumber();
    }

    txtCommission = new AutoNumeric('#txtCommission', AutoNumeric.getPredefinedOptions().percentageUS2decPos);
};

function getActiveUserCurrency() {
    return JSON.parse(localStorage.getItem('Credentials')).CurrencyCode;
}

function getActiveUserId() {
    return JSON.parse(localStorage.getItem('Credentials')).UserId;
}

function activateContainer(divId) {
    document.getElementById(divId).classList.add("active");
}

// Loads data for store user to edit its store
function LoadForm() { 
    ctrlActions = new ControlActions();
    userId = getActiveUserId();

    var tempStore = this.ctrlActions.GetToAPIAsync('store/RetrieveByOwnerId?ownerId=' + userId);

    ctrlActions.BindFields('frmStore', tempStore.Data);
    document.querySelector('#imgStoreLogo').src = tempStore.Data.Logo;
    mapLat = tempStore.Data.WarehouseLatitude;
    mapLng = tempStore.Data.WarehouseLongitude;
    document.querySelector('#txtWarehouseCoordinates').value = "Latitud: " + mapLat + "  |  Longitud: " + mapLng;
    txtCommission.set(data.Commission);
    reloadMarker();

    document.querySelector('#suspend').classList.add("invisible");
    if ("STS02" == tempStore.Data.StoreStatusCode) {
        document.querySelector('#suspend').classList.remove("invisible");
    };
}

// VALIDATIONS
function validateAll() {

}

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
