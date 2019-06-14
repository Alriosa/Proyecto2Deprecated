

function vTransactions() {

    this.tblTransactionsId = 'tblTransactions';
    this.service = 'transaction';
    this.ctrlActions = new ControlActions();
    this.columns = "TransactionId,TransactionTypeCode,Detail,Amount,OriginInternalAccountId,DestinationInternalAccountId,PaymentMethodId,TransactionDateTime";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblShippingProvidersId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblShippingProvidersId, true);
    }

    this.Create = function () {

        var transactionData = this.ctrlActions.GetDataForm('frmTransaction');
        var data = {
            TransactionId: 0,
            TransactionTypeCode: transactionData.TransactionTypeCode,
            Detail: transactionData.Detail,
            Amount: transactionData.Amount,
            OriginInternalAccountId: transactionData.OriginInternalAccountId,
            DestinationInternalAccountId: transactionData.DestinationInternalAccountId,
            PaymentMethodId: transactionData.PaymentMethodId,
            TransactionDateTime: new Date()
    }

        console.log(data);
        this.service += '/create';

        this.ctrlActions.PostToAPIRefresh(this.service, data, this.tableId);

        this.ReloadTable();


    }
    

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmTransaction', data);

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

    var tempTransaction = this.ctrlActions.GetToAPIAsync('transaction/RetrieveByOwnerId?ownerId=' + userId);

    ctrlActions.BindFields('frmTransaction', tempTransaction.Data);
    document.querySelector('#suspend').classList.add("invisible");
    
}