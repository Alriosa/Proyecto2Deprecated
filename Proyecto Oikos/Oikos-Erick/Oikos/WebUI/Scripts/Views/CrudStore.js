var tempWarehouse;
var tempCommission;
var valid = true;
var txtCommission;

function Crud() {

    this.tblStores = "tblStores";
    this.service = "store";
    this.columns = "StoreId,Identification,Name,Owner,WarehouseLatitude,WarehouseLongitude,Logo,Description,CurrencyCode,StoreStatusCode,Commission,CreationDate";

    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function() {
        this.ctrlActions.FillTable(this.service + '/RetrieveAll', this.tblStores, false, "0,3,4,5,6,7,9,10");
    }

    this.ReloadTable = function() {
        this.ctrlActions.FillTable(this.service + '/RetrieveAll', this.tblStores, true);
        $("#updateModal").modal('hide');
        $(".reset").click(function() {
            $(this).closest('frmStore').find("input[type=text], textarea").val("");
        });
    }

    this.Create = function() {
        var entityData = this.ctrlActions.GetDataForm('frmStore');
        var data = {
            Identification: entityData.Identification,
            Name: entityData.Name,
            Owner: getActiveUserId(),
            WarehouseLatitude: entityData.WarehouseLatitude,
            WarehouseLongitude: entityData.WarehouseLongitude,
            Logo: entityData.Logo,
            Description: entityData.Description,
            CurrencyCode: entityData.CurrencyCode
        };
        mapLat = 0.0;
        mapLng = 0.0;

        if (validateAll() == true) {
            this.ctrlActions.PostToAPI(this.service + '/create', data);
        }
    }

    this.Update = function() {
        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmStore');
        var data = {
            StoreId: entityData.StoreId,
            Identification: entityData.Identification,
            Name: entityData.Name,
            Owner: entityData.Owner,
            WarehouseLatitude: entityData.WarehouseLatitude,
            WarehouseLongitude: entityData.WarehouseLongitude,
            Logo: entityData.Logo,
            Description: entityData.Description,
            CurrencyCode: entityData.CurrencyCode,
            StoreStatusCode: entityData.StoreStatusCode,
            Commission: AutoNumeric.getNumber('#txtCommission'),
            CreationDate: entityData.CreationDate
        };
        mapLat = 0.0;
        mapLng = 0.0;

        this.ctrlActions.PutToAPI(this.service + '/update', data);
        this.ReloadTable();
    }

    this.Delete = function() {
        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmStore');
        var data = {
            Identification: entityData.Identification
        };
        this.ctrlActions.DeleteToAPI(this.service + '/delete', data);
        this.ReloadTable();
    }

    this.Suspend = function() {
        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmStore');
        var data = {
            Identification: entityData.Identification
        };
        this.ctrlActions.PutToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        initializeAutonumericCommission();
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmStore', data);
        document.querySelector('#imgStoreLogo').src = tempData.Logo;
        mapLat = tempData.WarehouseLatitude;
        mapLng = tempData.WarehouseLongitude;
        document.querySelector('#txtWarehouseCoordinates').value = "Latitud: " + mapLat + "  |  Longitud: " + mapLng;
        txtCommission.set(data.Commission);

        document.querySelector('#suspend').classList.add("invisible");
        if ("STS02" === tempData.StoreStatusCode) {
            document.querySelector('#suspend').classList.remove("invisible");
        };
    }

    this.Validate = function() {
        if (validateAll() === true) {
            if ((document.querySelector('#whoAmI').innerHTML === "update") ||
                (document.querySelector('#whoAmI').innerHTML === "list")) {

                // Sweet alert
                const swalWithBootstrapButtons = swal.mixin({
                    confirmButtonClass: 'btn btn-success ml-2',
                    cancelButtonClass: 'btn btn-danger mr-2 ',
                    buttonsStyling: false
                });

                swalWithBootstrapButtons({
                    title: '¿Desea modificar la tienda?',
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
                                    'La tienda no ha sido modificada.',
                                    'error'
                                );
                            }
                        }
                        swalWithBootstrapButtons(
                            '¡Modificada!',
                            'La tienda fue modificada con éxito.',
                            'success'
                        );
                        window.location.href = '/StoreUserDashboard';
                    } else if (
                        // Read more about handling dismissals
                        result.dismiss === swal.DismissReason.cancel
                    ) {
                        swalWithBootstrapButtons(
                            'Cancelado',
                            'El producto no ha sido modificado.',
                            'error'
                        );
                    }
                });
                // End sweet alert
            }
            if (document.querySelector('#whoAmI').innerHTML === "create") {
                try {
                    this.Create();
                    Swal({
                        position: 'center',
                        type: 'success',
                        title: '¡Tienda creada con éxito!',
                        showConfirmButton: true
                    });
                    window.location.href = '/StoreUserDashboard';
                } catch (e) {
                    console.log(e);
                }

            }

        }
    }
}

//ON DOCUMENT READY
$(document).ready(function() {
    var entity = new Crud();

    if (document.querySelector('#whoAmI').innerHTML === "update") {
        initializeAutonumericCommission();
        LoadForm();
    }

    if (document.querySelector('#whoAmI').innerHTML === "list") {
        entity.RetrieveAll();
    }
});

function initializeAutonumericCommission() {
    txtCommission = new AutoNumeric('#txtCommission', AutoNumeric.getPredefinedOptions().percentageUS2decPos);
}


window.onload = function() {
    var entity = new Crud();
    
    if (document.querySelector('#whoAmI').innerHTML === "update") {
        initializeAutonumericCommission();
        LoadForm();
    }

    if (document.querySelector('#whoAmI').innerHTML === "list") {
        entity.RetrieveAll();
    }
};

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

    var tempStore = this.ctrlActions.GetFromApi('store/RetrieveByOwnerId?ownerId=' + userId);

    ctrlActions.BindFields('frmStore', tempStore);
    document.querySelector('#imgStoreLogo').src = tempStore.Logo;
    mapLat = tempStore.WarehouseLatitude;
    mapLng = tempStore.WarehouseLongitude;
    document.querySelector('#txtWarehouseCoordinates').value = "Latitud: " + mapLat + "  |  Longitud: " + mapLng;
    txtCommission.set(tempStore.Commission);
    reloadMarker();

    document.querySelector('#suspend').classList.add("invisible");
    if ("STS02" === tempStore.Data.StoreStatusCode) {
        document.querySelector('#suspend').classList.remove("invisible");
    };
}

// VALIDATIONS
function validateAll() {
    var valid = false;

    visualValidation();

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if ((document.querySelector('#txtIdentification').value != "") &&
            (document.querySelector('#txtName').value != "") &&
            (document.querySelector('#txtDescription').value != "") &&
            (document.querySelector('#drpCurrencyCode') != null) &&
            (document.querySelector('#txtWarehouseCoordinates').value != "") &&
            (document.querySelector('#txtLogo').value != "")) {
            document.querySelector('#alertMessage').classList.add('d-none');
            valid = true;
        }
    }

    if (document.querySelector('#whoAmI').innerHTML === "update") {
        if ((document.querySelector('#txtName').value != "") &&
            (document.querySelector('#txtDescription').value != "") &&
            (document.querySelector('#txtWarehouseCoordinates').value != "")) {
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

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if (mediaURL != "") {
            setValidInput('#divImgLogo1');
        } else {
            setInvalidInput('#divImgLogo1');
        }
    }

    if (document.querySelector('#txtIdentification').value != "") {
        setValidInput('#txtIdentification');
    } else {
        setInvalidInput('#txtIdentification');
    }

    if (document.querySelector('#whoAmI').innerHTML === "create") {
        if (document.querySelector('#drpCurrencyCode').value != "") {
            setValidInput('#drpCurrencyCodeWrapper');
        } else {
            setInvalidInput('#drpCurrencyCodeWrapper');
        }
    }

    if (document.querySelector('#txtName').value != "") {
        setValidInput('#txtName');
    } else {
        setInvalidInput('#txtName');
    }

    if (document.querySelector('#txtDescription').value != "") {
        setValidInput('#txtDescription');
    } else {
        setInvalidInput('#txtDescription');
    }

    if (document.querySelector('#txtWarehouseCoordinates').value != "") {
        setValidInput('#txtWarehouseCoordinates');
    } else {
        setInvalidInput('#txtWarehouseCoordinates');
    }

}

function setValidInput(e) {
    document.querySelector(e).classList.remove("invalidInput");
}

function setInvalidInput(e) {
    document.querySelector(e).classList.add("invalidInput");
}