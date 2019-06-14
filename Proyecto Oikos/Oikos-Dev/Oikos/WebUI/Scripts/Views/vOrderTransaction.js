function vOrderTransaction() {
    this.tblOrderTransactionId = 'tblOrderTransaction';
    this.service = 'ordertransaction';
    this.ctrlActions = new ControlActions();
    this.columns = "OrderTransactionId,OrderId,TransactionId";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblPaymentMethodId, false, "0,3,4");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblPaymentMethodId, true);
    }

    this.Create = function () {

        var paymethodData = this.ctrlActions.GetDataForm('frmOrderTransaction');
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

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmOrderTransaction', data);

    }
}

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
