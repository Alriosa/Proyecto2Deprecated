function CreateUser() {


    this.service = 'users';
    this.ctrlActions = new ControlActions();


    this.Create = function() {
        var entityData = this.ctrlActions.GetDataForm('frmUser');
        var data = {
            Id: entityData.Id,
            Password:entityData.Password,
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

    this.UserTypeRedirect = function () {
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
            (document.querySelector('#drpProvider') != null) &&
            (document.querySelector('#txtDescription').value != "") &&
            (document.querySelector('#txtCurrency').value != "") &&
            (document.querySelector('#txtSellingPrice').value != "") &&
            (document.querySelector('#txtTaxPercentage').value != "")) {
            valid = true;
            document.querySelector('#alertMessage').classList.add("d-none");
        }
    }

    if ((document.querySelector('#whoAmI').innerHTML === "list") ||
        (document.querySelector('#whoAmI').innerHTML === "update")) {
        if ((mediaList[0] != null) &&
            (mediaList[1] != null) &&
            (mediaList[2] != null) &&
            (document.querySelector('#txtName').value != "") &&
            (document.querySelector('#txtDescription').value != "") &&
            (document.querySelector('#txtCurrency').value != "") &&
            (document.querySelector('#txtSellingPrice').value != "") &&
            (document.querySelector('#txtTaxPercentage').value != "")) {
            valid = true;
            document.querySelector('#alertMessage').classList.add("d-none");
        }
    }

    if (valid == false) {
        document.querySelector('#alertMessage').classList.remove("d-none");
    }

    return valid;
}

function visualValidation() {

    if (document.querySelector('#txtName').value != "") {
        setValidInput('#txtName');
    } else {
        setInvalidInput('#txtName');
    }

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if (document.querySelector('#drpProvider').value != "") {
            setValidInput('#drpProviderWrapper');
        } else {
            setInvalidInput('#drpProviderWrapper');
        }
    }

    if (document.querySelector('#txtDescription').value != "") {
        setValidInput('#txtDescription');
    } else {
        setInvalidInput('#txtDescription');
    }

    if (document.querySelector('#txtCurrency').value != "") {
        setValidInput('#txtCurrency');
    } else {
        setInvalidInput('#txtCurrency');
    }

    if (document.querySelector('#txtSellingPrice').value != "") {
        setValidInput('#txtSellingPrice');
    } else {
        setInvalidInput('#txtSellingPrice');
    }

    if (document.querySelector('#txtTaxPercentage').value != "") {
        setValidInput('#txtTaxPercentage');
    } else {
        setInvalidInput('#txtTaxPercentage');
    }

}

function setValidInput(e) {
    document.querySelector(e).classList.remove("invalidInput");
}

function setInvalidInput(e) {
    document.querySelector(e).classList.add("invalidInput");
}



$(document).ready(function () {
    if (sessionStorage.getItem("newFlag")) {
        $("#txtEmail").val(sessionStorage.getItem("newEmail"));
    }
});