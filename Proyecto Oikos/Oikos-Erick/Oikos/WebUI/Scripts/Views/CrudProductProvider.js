function Crud() {

    this.tblProductProvider = "tblProductProvider";
    this.service = "ProductProvider";
    this.ctrlActions = new ControlActions();
    this.columns =
        "ProductProviderId,StoreId,Name,ProviderId,Address,ProviderType,Phone,Email";
    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + '/RetrieveAll', this.tblProductProvider, false, "0,3");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + '/RetrieveAll', this.tblProductProvider, true);
        $("#updateModal").modal('hide'); 
        $(".reset").click(function () {
            $(this).closest('frmProductProvider').find("input[type=text], textarea").val("");
        });
    }

    this.Create = function () {
        var entityData = this.ctrlActions.GetDataForm('frmProductProvider');
        var data = {
            StoreId: getActiveStoreId(),
            Name: entityData.Name,
            ProviderId: entityData.ProviderId,
            Address: entityData.Address,
            ProviderType: entityData.ProviderType,
            Phone:entityData.Phone,
            Email: entityData.Email
        };
        alert("Creacion exitosa");
        console.log(data);
        this.ctrlActions.PostToAPI(this.service + '/create', data);
        this.ReloadTable();
    }

    this.Update = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmProductProvider');
        var data = {
            ProductProviderId: entityData.ProductProviderId,
            StoreId: entityData.StoreId,
            ProviderId: entityData.ProviderId,
            Name: entityData.Name,
            ProviderType: entityData.ProviderType,
            Phone: entityData.Phone,
            Address: entityData.Address,
            Email: entityData.Email
        };
        console.log(data);

        this.ctrlActions.PutToAPI(this.service + '/update', data);
        this.ReloadTable();

    }

    this.Delete = function () {


        // Sweet alert
        const swalWithBootstrapButtons = swal.mixin({
            confirmButtonClass: 'btn btn-success ml-2',
            cancelButtonClass: 'btn btn-danger mr-2 ',
            buttonsStyling: false
        });

        swalWithBootstrapButtons({
            title: '¿Desea eliminar este proveedor?',
            type: 'warning',
            showCancelButton: true,
            text: "No podrá revertir este cambio.",
            confirmButtonText: '¡Sí, continuar!',
            cancelButtonText: '¡No, cancelar!',
            reverseButtons: true
        }).then((result) => {
            if (result.value) {

                try {
                    var entityData = {};
                    entityData = this.ctrlActions.GetDataForm('frmProductProvider');
                    var data = {
                        ProductProviderId: entityData.ProductProviderId
                    };
                    this.ctrlActions.DeleteToAPI(this.service + '/delete', data);
                    this.ReloadTable();
                } catch (err) {
                    {
                        swalWithBootstrapButtons(
                            '¡Error!',
                            err.message,
                            'El proveedor no ha sido eliminado.',
                            'error'
                        );
                    }
                }
                swalWithBootstrapButtons(
                    '¡Eliminado!',
                    'El proveedor fue eliminado con éxito.',
                    'success'
                );
            } else if (
                // Read more about handling dismissals
                result.dismiss === swal.DismissReason.cancel
            ) {
                swalWithBootstrapButtons(
                    'Cancelado',
                    'El proveedor no ha sido eliminado.',
                    'error'
                );
            }
        });
        // End sweet alert
    }

    function getActiveUserId() {
        return JSON.parse(localStorage.getItem('Credentials')).UserId;
    }

    this.Suspend = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmProductProvider');
        var data = {
            ProductProviderId: entityData.ProductProviderId
        };
        this.ctrlActions.DeleteToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmProductProvider', data);
    }

 
    this.Validate = function () {

        if (validateAll() == true) {
            if ((document.querySelector('#whoAmI').innerHTML === "update") ||
                (document.querySelector('#whoAmI').innerHTML === "list")) {

                // Sweet alert
                const swalWithBootstrapButtons = swal.mixin({
                    confirmButtonClass: 'btn btn-success ml-2',
                    cancelButtonClass: 'btn btn-danger mr-2 ',
                    buttonsStyling: false
                });

                swalWithBootstrapButtons({
                    title: '¿Desea modificar este proveedor?',
                    type: 'warning',
                    showCancelButton: true,
                    text: "No podrá revertir este cambio.",
                    confirmButtonText: '¡Sí, continuar!',
                    cancelButtonText: '¡No, cancelar!',
                    reverseButtons: true
                }).then((result) => {
                    if (result.value) {

                        try {
                            this.Update();
                        } catch (err) {
                            {
                                swalWithBootstrapButtons(
                                    '¡Error!',
                                    err.message,
                                    'El proveedor no ha sido modificado.',
                                    'error'
                                );
                            }
                        }
                        swalWithBootstrapButtons(
                            '¡Modificado!',
                            'El proveedor fue modificado con éxito.',
                            'success'
                        );
                    } else if (
                        // Read more about handling dismissals
                        result.dismiss === swal.DismissReason.cancel
                    ) {
                        swalWithBootstrapButtons(
                            'Cancelado',
                            'El proveedor no ha sido modificado.',
                            'error'
                        );
                    }
                });
                // End sweet alert
            }
            if (document.querySelector('#whoAmI').innerHTML === "create") {
                this.Create();
            }
        }
    }
}



function getActiveStoreId() {
    //    activeUser quemado en sessionStorage QUITAR!!!!
    var store = { StoreId: 1 }
    localStorage.setItem('activeProductProvider', JSON.stringify(store));
    return JSON.parse(localStorage.getItem('activeProductProvider')).UserId;
}

//PRODUCTPROVIDER UTILIZA ACTIVE STORE. 
// Loads data for store user to edit its store

//ON DOCUMENT READY
$(document).ready(function () {

    var entity = new Crud();
    entity.RetrieveAll();
    console.log("hola ready");

    
    //if (document.getElementById('#whoAmI').innerHTML == "update") {
    //    LoadForm();
    //}
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



function validateIdentificationInput(e) {
    var regex = /\d(-\d{4}){2}/;
    var res = regex.test(e.value);
    res
        ? setValidInput(e)
        : setInvalidInput(e);
}


function LoadForm() {

    //    activeProductProvider quemado en localStorage QUITAR!!!!
    var productProvider = {
        ProductProviderId: 1,
        StoreId: 1,
        Name: "Carlos Augusto",
        ProviderId: "182648263",
        Address: "Urb el villa",
        ProviderType: 1,
        Phone: "84928543",
        Email: "email@gmail.com"
    }
    console.log(productProvider);
    localStorage.setItem('activeProductProvider', JSON.stringify(productProvider));

    tempData = JSON.parse(localStorage.getItem('activeProductProvider'));
    ctrlActions = new ControlActions();

    ctrlActions.BindFields('frmProductProvider', tempData);



}

function validateAll() {
    var valid = false;

    visualValidation();

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if ((document.querySelector('#txtName').value != "") &&
            (document.querySelector('#txtId') != null) &&
            (document.querySelector('#txtLocation').value != "") &&
            (document.querySelector('#drpProviderType').value != "") &&
            (document.querySelector('#txtPhone').value != "") &&
            (document.querySelector('#txtEmail').value != "")) {
            valid = true;
            document.querySelector('#alertMessage').classList.add('d-none');
        }
    }

    if ((document.querySelector('#whoAmI').innerHTML === "list") ||
        (document.querySelector('#whoAmI').innerHTML === "update")) {
        if ((document.querySelector('#txtName').value != "") &&
            (document.querySelector('#txtId') != null) &&
            (document.querySelector('#txtLocation').value != "") &&
            (document.querySelector('#drpProviderType').value != "") &&
            (document.querySelector('#txtPhone').value != "") &&
            (document.querySelector('#txtEmail').value != "")) {
            valid = true;
            document.querySelector('#alertMessage').classList.add('d-none');
        }
    }

    if (valid == false) {
        document.querySelector('#alertMessage').classList.remove('d-none');
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
        if (document.querySelector('#txtId').value != null) {
            setValidInput('#txtId');
        } else {
            setInvalidInput('#txtId');
        }
    }

    if (document.querySelector('#txtLocation').value != "") {
        setValidInput('#txtLocation');
    } else {
        setInvalidInput('#txtLocation');
    }

    if (document.querySelector('#drpProviderType').value != "") {
        setValidInput('#drpProviderType');
    } else {
        setInvalidInput('#drpProviderType');
    }

    if (document.querySelector('#txtPhone').value != "") {
        setValidInput('#txtPhone');
    } else {
        setInvalidInput('#txtPhone');
    }

    if (document.querySelector('#txtEmail').value != "") {
        setValidInput('#txtEmail');
    } else {
        setInvalidInput('#txtEmail');
    }

}

this.ReloadTableByProductProviderId = function () {
    this.ctrlActions.FillTable(this.service + '/Retrieve?"StoreId"=', this.tblProductProvider, true);
    $("#updateModal").modal('hide');
    $(".reset").click(function () {
        $(this).closest('frmProductProvider').find("input[type=text], textarea").val("");
    });
}



//No Negative number Validation

//txtPhone.onkeydown = function (e) {
//    if (!((e.keyCode > 95 && e.keyCode < 106)
//        || (e.keyCode > 47 && e.keyCode < 58)
//        || e.keyCode == 8)) {
//        return false;
//    }

 

    //document.getElementById("txtPhone").onkeyup = function () { myFunction() };

    //function myFunction() {
    //    var x = document.getElementById("txtPhone");
    //    x.value = x.value.toUpperCase();
    //}
//End No negative number Validation

$(document).ready(function () {


    $('#txtPhone').keyup(function () {
        if (!this.value.match(/[0-9]/)) {
            this.value = this.value.replace(/[^0-9]/g, '');
        }
    });

    $('#txtId').keyup(function () {
        if (!this.value.match(/[0-9]/)) {
            this.value = this.value.replace(/[^0-9]/g, '');
        }
    });
    


    //if (document.getElementById('#whoAmI').innerHTML == "update") {
    //    LoadForm();
    //}
});

function Back() {
    window.history.back();
}


function setValidInput(e) {
    document.querySelector(e).classList.remove("invalidInput");
}

function setInvalidInput(e) {
    document.querySelector(e).classList.add("invalidInput");
}