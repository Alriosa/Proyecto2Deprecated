

function vPaymentMethod() {

    this.tblPaymentMethodId = 'tblPaymentMethod';
    this.service = 'paymentmethod';
    this.ctrlActions = new ControlActions();
    this.columns = "PaymentMethodId,PaymentTypeCode,Value,UserId,IsActive";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblPaymentMethodId, false, "0,3,4");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblPaymentMethodId, true);
    }

    this.Create = function () {

        var paymethodData = this.ctrlActions.GetDataForm('frmPaymentMethod');
        var data = {
            PaymentMethodId: 0,
            PaymentTypeCode: paymethodData.PaymentTypeCode,
            Value: paymethodData.Value,
            UserId: getActiveUserId(),
            IsActive: true
        }

        console.log(data);
        this.service += '/create';

        this.ctrlActions.PostToAPI(this.service, data);

        this.ReloadTable();


    }

    this.Update = function () {

        var paymethodData = this.ctrlActions.GetDataForm('frmPaymentMethod');
        var data = {
            PaymentMethodId: 0,
            PaymentTypeCode: paymethodData.PaymentTypeCode,
            Value: paymethodData.Value,
            UserId: getActiveUser(),
            IsActive: true
        }

        this.service += '/update';

        this.ctrlActions.PutToAPIRefresh(this.service, data, this.tableId);
        this.ReloadTable();
        $('#updateModal').modal('hide');


    }

    this.Delete = function () {
        var payMethodData = {};
        payMethodData= this.ctrlActions.GetDataForm('frmPaymentMethod');

        this.service += '/delete';

        this.ctrlActions.DeleteToAPI(this.service, payMethodData);

        this.ReloadTable();
    }

    this.Suspend = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmPaymentMethod');
        var data = {
            Identification: entityData.Identification
        };
        this.ctrlActions.PutToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmPaymentMethod', data);
        document.querySelector('#imgShippingProviderLogo').src = tempData.Logo

        document.querySelector('#suspend').classList.add("invisible");
        if ("STS02" == tempData.ShippingProviderStatusCode) {
            document.querySelector('#suspend').classList.remove("invisible");
        };
    }

    this.Back = function () {
        window.history.back();
    }
}


$(document).ready(function () {
    var vpaymethod = new vPaymentMethod();
    vpaymethod.RetrieveAll();
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

    var tempPayMethod = this.ctrlActions.GetToAPIAsync('paymentmethod/RetrieveById?id=' + userId);

    ctrlActions.BindFields('frmPaymentMethod', tempPayMethod.Data);
    document.querySelector('#imgPaymentMethodLogo').src = tempShippingProvider.Data.Logo;

    document.querySelector('#suspend').classList.add("invisible");
    if (tempPayMethod.Data.IsActive ===  false) {
        document.querySelector('#suspend').classList.remove("invisible");
    };
}