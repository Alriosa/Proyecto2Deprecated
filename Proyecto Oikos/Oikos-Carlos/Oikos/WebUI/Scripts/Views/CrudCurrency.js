
function CreateCategory() {

    this.tblCategoriesId = 'tblCurrencies';
    this.service = 'optionlist';
    this.ctrlActions = new ControlActions();
    this.columns = "listId,value,description";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + '/retrieveall', this.tblCategoriesId, false, "0,3");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + '/retrieveall', this.tblCategoriesId, true);
    }

    this.Create = function () {
        var curData = this.ctrlActions.GetDataForm('frmCurrency');
        var data = { ListId: "CURRENCY", Value: curData.Value, Description: curData.Description};
        this.ctrlActions.PostToAPIRefresh(this.service + '/create', data, this.tblCategoriesId);
        this.ReloadTable();
    }

    this.Update = function () {

        var catData = {};
        catData = this.ctrlActions.GetDataForm('frmCategory');
        var data = {
            CategoryId: catData.CategoryId,
            Name: catData.Name,
            Description: catData.Description,
            IsActive: true
        };
        this.ctrlActions.PutToAPIRefresh(this.service + '/update', data, this.tableId);
        this.ReloadTable();

    }

    this.Delete = function () {

        var catData = {};
        catData = this.ctrlActions.GetDataForm('frmCategory');
        var data = {
            CategoryId: catData.CategoryId,
            Name: catData.Name,
            Description: catData.Description,
            IsActive: false
        };
        this.ctrlActions.DeleteToAPIRefresh(this.service + '/delete', data, this.tableId);
        this.ReloadTable();

    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmCategory', data);
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var categories = new CreateCategory();
    categories.RetrieveAll();

});

function activateContainer(divId) {
    document.getElementById(divId).classList.add("active");
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