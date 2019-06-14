function vProductRequest() {
    this.tableId = "tblProductRequest";
    this.service = 'productrequest';
    this.ctrlActions = new ControlActions();
    this.columns =
        "ProductRequestId,UserId,Description,RequestDatetime,ExpirationDatetime,IsActive,Quantity";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tableId, false, "0,1,3,5");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tableId, true);
    }

    this.Create = function () {

        var prodReqData = this.ctrlActions.GetDataForm('frmProductRequest');
        var data = {
            ProductRequestId: 0,
            UserId: getActiveUserId(),
            Description: prodReqData.Description,
            RequestDateTime: new Date(),
            ExpirationDateTime: prodReqData.ExpirationDateTime,
            IsActive: true,
            Quantity: prodReqData.Quantity
        }
    

        console.log(data);
        this.service += '/create';

        this.ctrlActions.PostToAPIRefresh(this.service, data, this.tableId);

        this.ReloadTable();


    }

    this.Update = function () {

        var prodReqData = this.ctrlActions.GetDataForm('frmProductRequest');
        var data = {
            ProductRequestId: prodReqData.ProductRequestId,
            UserId: getActiveUserId(),
            Description: prodReqData.Description,
            RequestDateTime: prodReqData.Description,
            ExpirationDateTime: prodReqData.ExpirationDateTime,
            IsActive: true,
            Quantity: prodReqData.Quantity
        }

        this.service += '/update';

        this.ctrlActions.PutToAPIRefresh(this.service, data, this.tableId);
        this.ReloadTable();
        $('#updateModal').modal('hide');


    }

    this.Delete = function () {
        var shipProviderData = {};
        shipProviderData = this.ctrlActions.GetDataForm('frmProductRequest');

        this.service += '/delete';

        this.ctrlActions.DeleteToAPI(this.service, shipProviderData);

        this.ReloadTable();
    }

    this.Suspend = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmProductRequest');
        var data = {
            Identification: entityData.Identification
        };
        this.ctrlActions.PutToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmProductRequest', data);

        document.querySelector('#suspend').classList.add("invisible");
        if (tempData.IsActive === false) {
            document.querySelector('#suspend').classList.remove("invisible");
        };
    }

    this.Back = function () {
        window.history.back();
    }
}



$(document).ready(function () {
    var vproductRequest = new vProductRequest();
    vproductRequest.RetrieveAll();
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

    var tempProductRequest = this.ctrlActions.GetToAPIAsync('ProductRequest/RetrieveById?id=' + userId);

    ctrlActions.BindFields('frmProductRequest', tempProductRequest.Data);    

    document.querySelector('#suspend').classList.add("invisible");
    if (tempProductRequest.Data.IsActive === false) {
        document.querySelector('#suspend').classList.remove("invisible");
    };
}