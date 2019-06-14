function CreateUser() {


    this.service = 'users';
    this.ctrlActions = new ControlActions();


    this.Create = function() {

        if (validateAll()) {
            var entityData = this.ctrlActions.GetDataForm('frmUser');
            var data = {
                Id: entityData.Id,
                Password: entityData.Password,
                Email: entityData.Email,
                Name: entityData.Name,
                UserStatusCode: '1',
                CurrencyCode: 'CRC'
            };

            this.ctrlActions.PostToAPI(this.service + '/create', data);

            var userId = this.ctrlActions.GetFromApi(this.service + '/GetLastId');

            var creds = {
                UserId: userId,
                Id: "",
                Password: "",
                Email: entityData.Email,
                Name: entityData.Name,
                UserStatusCode: '1',
                CurrencyCode: 'CRC'
            };

            localStorage.setItem('Credentials', JSON.stringify(creds));

            this.SetForm();
        }


    }
    this.SetForm = function() {
        var type = $("#drpUserCode").val();


        switch (type) {
        case "STORE":
            window.location.href = '../CreateStore';
            break;
        case "SHIPPINGPROVIDER":
            window.location.href = '../CreateShippingProvider';
            break;
        case "CLIENT":
            window.location.href = '../CustomerCreate';
            break;

        }

    }

    this.UserTypeRedirect = function() {
        sessionStorage.setItem("newUserId", $("#txtRegisterEmail").val());
        sessionStorage.setItem("newUserFlag", true);
        window.location.href = 'http://localhost:57619/CreateUser';
    }
    this.Validate = function() {
        var isValid = true;

    }
}

// VALIdATIONS

function validateAll() {
    var valid = false;

    visualValidation();

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if ((document.querySelector('#txtName').value != "") &&
            (document.querySelector('#drpUserCode') != null) &&
            (document.querySelector('#txtIdentification').value != "") &&
            (document.querySelector('#txtPw').value != "") &&
            (testPassword($('#txtPw').val())) &&
            (document.querySelector('#txtEmail').value != "")) {
            valid = true;
            document.querySelector('#alertMessage').classList.add("d-none");
        }
    }

    if (valid == false) {
        document.querySelector('#alertMessage').classList.remove("d-none");
        document.querySelector('#pwAlertMessage').classList.remove("d-none");
    } 

    return valid;
}

function testPassword(pwString) {
    var isValid = false;
    var pattern = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
    if (pattern.test(pwString)) {
        isValid = true;
    }
    return isValid;
}

function visualValidation() {

    if (document.querySelector('#txtName').value != "") {
        setValidInput('#txtName');
    } else {
        setInvalidInput('#txtName');
    }


    if (document.querySelector('#drpUserCode').value != "") {
        setValidInput('#drpUserCode');
    } else {
        setInvalidInput('#drpUserCode');
    }


    if (document.querySelector('#txtIdentification').value != "") {
        setValidInput('#txtIdentification');
    } else {
        setInvalidInput('#txtIdentification');
    }

    if (document.querySelector('#txtPw').value != "" && testPassword(document.querySelector('#txtPw').value)) {
        setValidInput('#txtPw');
    } else {
        setInvalidInput('#txtPw');
    }

    if (document.querySelector('#txtEmail').value != "") {
        setValidInput('#txtEmail');
    } else {
        setInvalidInput('#txtEmail');
    }


}

function setValidInput(e) {
    document.querySelector(e).classList.remove("invalidInput");
}

function setInvalidInput(e) {
    document.querySelector(e).classList.add("invalidInput");
}


$(document).ready(function() {
    if (sessionStorage.getItem("newFlag")) {
        $("#txtEmail").val(sessionStorage.getItem("newEmail"));
    }
});