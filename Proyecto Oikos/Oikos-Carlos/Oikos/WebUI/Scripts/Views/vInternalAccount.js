function vInternalAccount() {
    this.tblInternalAccountId = 'tblInternalAccount';
    this.service = 'internalaccount';
    this.ctrlActions = new ControlActions();
    this.columns =
        "InternalAccountId,UserId,DestinationAccount,Balance";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblInternalAccountId, false, "0,1");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblInternalAccountId, true);
    }

    this.Create = function () {

        var internalAcctData = this.ctrlActions.GetDataForm('frmInternalAccount');
        var data = {
            InternalAccountId: 0,
            UserId: getActiveUserId(),
            DestinationAccount: internalAcctData.DestinationAccount,
            Balance: internalAcctData.Balance
    }

        console.log(data);
        this.service += '/create';

        this.ctrlActions.PostToAPIRefresh(this.service, data, this.tableId);

        this.ReloadTable();


    }

    this.Update = function () {

        var internalAcctData = this.ctrlActions.GetDataForm('frmInternalAccount');
        var data = {
            InternalAccountId: internalAcctData.InternalAccountId,
            UserId: getActiveUserId(),
            DestinationAccount: internalAcctData.DestinationAccount,
            Balance: internalAcctData.Balance
        }

        this.service += '/update';

        this.ctrlActions.PutToAPIRefresh(this.service, data, this.tableId);
        this.ReloadTable();
      //  $('#updateModal').modal('hide');


    }

    this.Delete = function () {
        var internalAcctData = this.ctrlActions.GetDataForm('frmInternalAccount');

        this.service += '/delete';

        this.ctrlActions.DeleteToAPI(this.service, internalAcctData);

        this.ReloadTable();
    }

    this.Suspend = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmInternalAcctData');
        var data = {
            Identification: entityData.Identification
        };
        this.ctrlActions.PutToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmInternalAcctData', data);

        document.querySelector('#suspend').classList.add("invisible");
       /* if ("STS02" == tempData.ShippingProviderStatusCode) {
            document.querySelector('#suspend').classList.remove("invisible");
        };*/
    }

    this.Back = function () {
        window.history.back();
    }
}

$(document).ready(function () {
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