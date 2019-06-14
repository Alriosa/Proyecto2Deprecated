function Crud() {

    this.tblShippingMethod = "tblShippingMethod";
    this.service = "ShippingMethod";
    this.ctrlActions = new ControlActions();
    this.columns =
        "ShippingMethodId,Name,ShippingProviderId,BaseCost,AdditionalCost,Description,IsActive,DaysToDeliver";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + '/retrieveall', this.tblShippingMethod, false, "0,3");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + '/retrieveall', this.tblShippingMethod, true);
        $("#updateModal").modal('hide');
        $(".reset").click(function () {
            $(this).closest('frmShippingMethod').find("input[type=text], textarea").val("");
        });
    }

    this.Create = function () {
        var entityData = this.ctrlActions.GetDataForm('frmShippingMethod');
        var data = {
            ShippingMethodId: entityData.ShippingMethodId,
            Name: entityData.Name,
            ShippingProviderId: entityData.ShippingProviderId,
            BaseCost : entityData.BaseCost,
            AdditionalCost: entityData.AdditionalCost,
            Description: entityData.Description,
            IsActive: true,
            DaysToDeliver : entityData.DaysToDeliver
            
        };
        
        console.log(data);
        this.ctrlActions.PostToAPI(this.service + '/create', data);
  
        this.ReloadTable();
    }

    this.Update = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmShippingMethod');
        var data = {
            ShippingMethodId: entityData.ShippingMethodId,
            Name: entityData.Name,
            ShippingProviderId: 1,
            BaseCost: entityData.BaseCost,
            AdditionalCost: entityData.AdditionalCost,
            Description: entityData.Description,
            IsActive: entityData.IsActive,
            DaysToDeliver: entityData.DaysToDeliver
        };
        
        this.ctrlActions.PutToAPI(this.service + '/update', data);
        this.ReloadTable();
        console.log(data);
    }

    this.Delete = function () {
        // Sweet alert
        const swalWithBootstrapButtons = swal.mixin({
            confirmButtonClass: 'btn btn-success ml-2',
            cancelButtonClass: 'btn btn-danger mr-2 ',
            buttonsStyling: false
        });

        swalWithBootstrapButtons({
            title: '¿Desea eliminar este Metodo?',
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
                    entityData = this.ctrlActions.GetDataForm('frmShippingMethod');
                    var data = {
                        ShippingMethodId: entityData.ShippingMethodId
                    };
                    this.ctrlActions.DeleteToAPI(this.service + '/delete', data);
                    this.ReloadTable();
                } catch (err) {
                    {
                        swalWithBootstrapButtons(
                            '¡Error!',
                            err.message,
                            'El metodo no ha sido eliminado.',
                            'error'
                        );
                    }
                }
                swalWithBootstrapButtons(
                    '¡Eliminado!',
                    'El Metodo fue eliminado con éxito.',
                    'success'
                );
            } else if (
                // Read more about handling dismissals
                result.dismiss === swal.DismissReason.cancel
            ) {
                swalWithBootstrapButtons(
                    'Cancelado',
                    'El Metodo no ha sido eliminado.',
                    'error'
                );
            }
        });
        // End sweet alert

    }
    

    this.Suspend = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmShippingMethod');
        var data = {
            ShippingMethodId: entityData.ShippingMethodId
        };
        this.ctrlActions.DeleteToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmShippingMethod', data);

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
                    title: '¿Desea modificar este metodo?',
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
                                    'El metodo no ha sido modificado.',
                                    'error'
                                );
                            }
                        }
                        swalWithBootstrapButtons(
                            '¡Modificado!',
                            'El metodo fue modificado con éxito.',
                            'success'
                        );
                    } else if (
                        // Read more about handling dismissals
                        result.dismiss === swal.DismissReason.cancel
                    ) {
                        swalWithBootstrapButtons(
                            'Cancelado',
                            'El metodo no ha sido modificado.',
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

function Back() {
    window.history.back();
}

function getActiveUserId() {
    return JSON.parse(localStorage.getItem('Credentials')).UserId;
}




//SHIPPINGMETHOD UTILIZA ACTIVE PRODUCTPROVIDER. 
// Loads data for store user to edit its store
function LoadForm() {

    //    activeStore quemado en localStorage QUITAR!!!!
    var shippingM = {
        ShippingMethodId: 6,
        Name: "Envío Exprés",
        ShippingProviderId: 1,
        BaseCost: 2000,
        AdditionalCost: 500,
        Description: "Servicio de entrega en 24 hrs",
        IsActive: true

    }
    localStorage.setItem('activeProductProvider', JSON.stringify(shippingM));

    tempData = JSON.parse(localStorage.getItem('activeProductProvider'));
    ctrlActions = new ControlActions();

    ctrlActions.BindFields('frmShippingMethod', tempData);
 
    document.querySelector('#suspend').classList.add("invisible");
    if ("STS02" == tempData.IsActive) {
        document.querySelector('#suspend').classList.remove("invisible");
    };
}

//ON DOCUMENT READY
$(document).ready(function () {

    var entity = new Crud();
    entity.RetrieveAll();
});

function getActiveUserId() {
    var user = { UserId: 1 }
    localStorage.setItem('activeUser', JSON.stringify(user));
    return JSON.parse(localStorage.getItem('activeUser')).UserId;
}

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



function validateAll() {
    var valid = false;

    visualValidation();

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if ((document.querySelector('#txtShippingMethodId').value != "")
            (document.querySelector('#txtName').value != "") &&
            (document.querySelector('#txtShippingProviderId') != '') &&
            (document.querySelector('#txtBaseCost').value != '') &&
            (document.querySelector('#txtAdditionalCost').value != '') &&
            (document.querySelector('#txtDescription').value != "") &&
            (document.querySelector('#txtDaysToDeliver').value != '')) {
            valid = true;
            document.querySelector('#alertMessage').classList.add('d-none');
        }
    }

    if ((document.querySelector('#whoAmI').innerHTML === "list") ||
        (document.querySelector('#whoAmI').innerHTML === "update")) {
        if ((document.querySelector('#txtShippingMethodId').value != "") &&
            (document.querySelector('#txtName').value != "") &&
            (document.querySelector('#txtShippingProviderId') != '') &&
            (document.querySelector('#txtBaseCost').value != '') &&
            (document.querySelector('#txtAdditionalCost').value != '') &&
            (document.querySelector('#txtDescription').value != "") &&
            (document.querySelector('#txtDaysToDeliver').value != '')) {
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

    
        if (document.querySelector('#txtShippingMethodId').value != '') {
            setValidInput('#txtShippingMethodId');
        } else {
            setInvalidInput('#txtShippingMethodId');
        }
    

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if (document.querySelector('#txtShippingProviderId').value != '') {
            setValidInput('#txtShippingProviderId');
        } else {
            setInvalidInput('#txtShippingProviderId');
        }
    }

    if (document.querySelector('#txtBaseCost').value != '') {
        setValidInput('#txtBaseCost');
    } else {
        setInvalidInput('#txtBaseCost');
    }

    if (document.querySelector('#txtAdditionalCost').value != '') {
        setValidInput('#txtAdditionalCost');
    } else {
        setInvalidInput('#txtAdditionalCost');
    }

    if (document.querySelector('#txtDescription').value != "") {
        setValidInput('#txtDescription');
    } else {
        setInvalidInput('#txtDescription');
    }

    if (document.querySelector('#txtDaysToDeliver').value != '') {
        setValidInput('#txtDaysToDeliver');
    } else {
        setInvalidInput('#txtDaysToDeliver');
    }

}

function setValidInput(e) {
    document.querySelector(e).classList.remove("invalidInput");
}

function setInvalidInput(e) {
    document.querySelector(e).classList.add("invalidInput");
}

$(document).ready(function () {


    $('#txtBaseCost').keyup(function () {
        if (!this.value.match(/[0-9]/)) {
            this.value = this.value.replace(/[^0-9]/g, '');
        }
    });

    $('#txtAdditionalCost').keyup(function () {
        if (!this.value.match(/[0-9]/)) {
            this.value = this.value.replace(/[^0-9]/g, '');
        }
    });

    $('#txtDaysToDeliver').keyup(function () {
        if (!this.value.match(/[0-9]/)) {
            this.value = this.value.replace(/[^0-9]/g, '');
        }
    });



    //if (document.getElementById('#whoAmI').innerHTML == "update") {
    //    LoadForm();
    //}
});
