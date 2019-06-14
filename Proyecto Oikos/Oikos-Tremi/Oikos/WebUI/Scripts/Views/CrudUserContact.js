function Crud() {

    this.tblUserContact = "tblUserContact";
    this.service = "userContacts";
    this.ctrlActions = new ControlActions();
    this.columns =
        "UserContactId,UserId,ContactTypeCode,ContactValue";
    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + '/retrieveall', this.tblUserContact, false, "0,3");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + '/retrieveall', this.tblUserContact, true);
        $("#updateModal").modal('hide');
        $(".reset").click(function () {
            $(this).closest('frmUserContact').find("input[type=text], textarea").val("");
        });
    }

    this.Create = function () {
        var entityData = this.ctrlActions.GetDataForm('frmUserContact');
        var data = {
            UserId: getActiveUserId(),
            ContactTypeCode: $("#drpContactType").val(),
            ContactValue: entityData.ContactValue
        };
        alert("Creacion exitosa");
        this.ctrlActions.PostToAPI(this.service + '/create', data);
        this.ReloadTable();
    }

    this.Update = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmUserContact');
        var data = {
            UserContactId: 1,
            UserId: 1,
            ContactTypeCode: entityData.ContactTypeCode,
            ContactValue: entityData.ContactValue
        };
        alert("Update exitoso");
        this.ctrlActions.PutToAPI(this.service + '/update', data);
        this.ReloadTable();

    }

    this.Delete = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmUserContact');
        var data = {
            UserContactId: entityData.UserContactId
        };
        this.ctrlActions.DeleteToAPI(this.service + '/delete', data);
        this.ReloadTable();
        alert("Elimnacion exitosa");
    }

    this.Suspend = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmUserContact');
        var data = {
            UserContactId: entityData.UserContactId
        };
        this.ctrlActions.DeleteToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmUserContact', data);
    }
}

function getActiveUserId() {
    //    activeUser quemado en sessionStorage QUITAR!!!!
    return JSON.parse(localStorage.getItem('Credentials')).UserId;
}


//USERCONTACT UTILIZA ACTIVE USER. 
// Loads data for store user to edit its store
function LoadForm() {

    //    userContact quemado en localStorage QUITAR!!!!
    var userContact = {
        UserContactId: 1,
        UserId: 3,
        ContactTypeCode: "3",
        ContactValue: "3"
    }
    localStorage.setItem('userContact', JSON.stringify(userContact));

    tempData = JSON.parse(localStorage.getItem('userContact'));
    ctrlActions = new ControlActions();

    ctrlActions.BindFields('frmUserContact', tempData);


    document.querySelector('#suspend').classList.add("invisible");
    if ("STS02" == tempData.StoreStatusCode) {
        document.querySelector('#suspend').classList.remove("invisible");
    };
}

//ON DOCUMENT READY
$(document).ready(function () {

    var entity = new Crud();
    entity.RetrieveAll();
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
