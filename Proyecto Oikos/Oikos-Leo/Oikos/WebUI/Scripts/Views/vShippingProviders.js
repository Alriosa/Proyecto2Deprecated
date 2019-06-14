var tempWarehouse;
var tempCommission;

function vShippingProviders() {

    this.tblShippingProvidersId = 'tblShippingProviders';
    this.service = 'shippingprovider';
    this.ctrlActions = new ControlActions();
    this.columns =
        "ShippingProviderId,Name,Owner,Logo,BaseFare,CurrencyCode,Commission,ShippingProviderStatusCode,AreaLatitude,AreaLongitude,AreaRadius";

    this.RetrieveAll = function() {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblShippingProvidersId, false, "0,2,3,7,8,9,10");
    }

    this.ReloadTable = function() {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblShippingProvidersId, true);
    }

    this.Create = function () {
        
        var shipProviderData = this.ctrlActions.GetDataForm('frmShippingProvider');
        var data = {
            ShippingProviderId: 0,
            Name: shipProviderData.Name,
            Owner: getActiveUserId(),
            Logo: shipProviderData.Logo,
            BaseFare: shipProviderData.BaseFare,
            CurrencyCode: shipProviderData.CurrencyCode,
            Commission: 5,
            ShippingProviderStatusCode: "STS01",
            AreaLatitude: shipProviderData.AreaLatitude,
            AreaLongitude: shipProviderData.AreaLongitude,
            AreaRadius: shipProviderData.AreaRadius
        }

        console.log(data);
        this.service += '/create';

        this.ctrlActions.PostToAPIRefresh(this.service, data, this.tableId);

        this.ReloadTable();
        
        
    }

    this.Update = function () {
        
        var shipProviderData = this.ctrlActions.GetDataForm('frmShippingProvider');
        var data = {
            ShippingProviderId: shipProviderData.ShippingProviderId,
            Name: shipProviderData.Name,
            Owner: getActiveUserId(),
            Logo: shipProviderData.Logo,
            BaseFare: shipProviderData.BaseFare,
            CurrencyCode: shipProviderData.CurrencyCode,
            Commission: shipProviderData.Commission,
            ShippingProviderStatusCode: shipProviderData.ShippingProviderStatusCode,
            AreaLatitude: shipProviderData.AreaLatitude,
            AreaLongitude: shipProviderData.AreaLongitude,
            AreaRadius: shipProviderData.AreaRadius
        }

        this.service += '/update';

        this.ctrlActions.PutToAPIRefresh(this.service, data, this.tableId);
        this.ReloadTable();
        $('#updateModal').modal('hide');
        
        
    }

    this.Delete = function() {
        var shipProviderData = {};
        shipProviderData = this.ctrlActions.GetDataForm('frmShippingProvider');

        this.service += '/delete';

        this.ctrlActions.DeleteToAPI(this.service, shipProviderData);

        this.ReloadTable();
    }

    this.Suspend = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmShippingProvider');
        var data = {
            Identification: entityData.Identification
        };
        this.ctrlActions.PutToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmShippingProvider', data);
        document.querySelector('#imgShippingProviderLogo').src = tempData.Logo;
        $('#txtAreaLatitude').html(tempData.AreaLatitude);
        $('#txtAreaLongitude').html(tempData.AreaLongitude);
        $('#txtAreaRadius').html(tempData.AreaRadius)

        document.querySelector('#suspend').classList.add("invisible");
        if ("STS02" == tempData.ShippingProviderStatusCode) {
            document.querySelector('#suspend').classList.remove("invisible");
        };
    }

    this.Back = function () {
        window.history.back();
    }
}


$(document).ready(function() {
    var vshipprovider = new vShippingProviders();
    vshipprovider.RetrieveAll();
});

function activateTabContainers(id) {
    document.getElementById(id).classList.add("active");
}

function validateInput(id) {
    console.log("validated");
}
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

    var tempShippingProvider = this.ctrlActions.GetToAPIAsync('shippingProvider/RetrieveByOwnerId?ownerId=' + userId);

    ctrlActions.BindFields('frmShippingProvider', tempShippingProvider.Data);
    document.querySelector('#imgShippingProviderLogo').src = tempShippingProvider.Data.Logo;
    circle.center.lat = tempShippingProvider.Data.AreaLatitude;
    circle.center.lng = tempShippingProvider.Data.AreaLongitude;
    circle.radiuse = tempShippingProvider.Data.AreaRadius;

    document.querySelector('#suspend').classList.add("invisible");
    if ("STS02" == tempShippingProvider.Data.ShippingProviderStatusCode) {
        document.querySelector('#suspend').classList.remove("invisible");
    };
}