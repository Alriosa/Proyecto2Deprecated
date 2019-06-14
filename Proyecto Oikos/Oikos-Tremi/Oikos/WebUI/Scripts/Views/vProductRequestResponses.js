function vProductRequestResponses() {
    this.tblProductRequestResponseId = "tblProductRequestResponses";
    this.service = 'productrequestresponses';
    this.ctrlActions = new ControlActions();
    this.columns =
        "ProductRequestResponseId,ProductRequestId,ProductId,Price,Quantity,Description,ResponseDatetime,IsAccepted";
    this.val = new ValidateForm();

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblProductRequestId, false, "0,1,2,6,7");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblProductRequestId, true);
    }

    this.Create = function () {
        if (this.val.validate()) {
            var prodReqData = this.ctrlActions.GetDataForm('frmProductRequestResponses');
            var data = {
                ProductRequestId: prodReqData.ProductRequestId,
                UserId: getUserData(),
                Description: prodReqData.Description,
                RequestDateTime: new Date(),
                ExpirationDateTime: prodReqData.ExpirationDateTime,
                IsActive: true,
                CategoryId: prodReqData.CategoryId,
                Quantity: prodReqData.Quantity
            }


            console.log(data);
            this.service += '/create';

            this.ctrlActions.PostToAPIRefresh(this.service, data, this.tableId);

            this.ReloadTable();
        } else {
            return console.log(this.val.validate());
        }


    }

    this.Update = function () {

        var prodReqData = this.ctrlActions.GetDataForm('frmProductRequestResponses');
        var data = {
            ProductRequestId: prodReqData.ProductRequestId,
            UserId: getUserData(),
            Description: prodReqData.Description,
            RequestDateTime: prodReqData.Description,
            ExpirationDateTime: prodReqData.ExpirationDateTime,
            IsActive: true,
            CategoryId: prodReqData.CategoryId,
            Quantity: prodReqData.Quantity
        }

        this.service += '/update';

        this.ctrlActions.PutToAPIRefresh(this.service, data, this.tableId);
        this.ReloadTable();
        $('#updateModal').modal('hide');


    }

    this.Delete = function () {
        var shipProviderData = {};
        shipProviderData = this.ctrlActions.GetDataForm('frmProductRequestResponses');

        this.service += '/delete';

        this.ctrlActions.DeleteToAPI(this.service, shipProviderData);

        this.ReloadTable();
    }

    this.Suspend = function () {

        var entityData = {};
        entityData = this.ctrlActions.GetDataForm('frmProductRequestResponses');
        var data = {
            Identification: entityData.Identification
        };
        this.ctrlActions.PutToAPI(this.service + '/suspend', data);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $("#updateModal").modal();
        this.ctrlActions.BindFields('frmProductRequestResponses', data);

        document.querySelector('#suspend').classList.add("invisible");
        if (tempData.IsActive === false) {
            document.querySelector('#suspend').classList.remove("invisible");
        };
    }

    this.Back = function () {
        window.history.back();
    }
}



$(document).ready(function () {
    var vproductRequestResponses = new vProductRequestResponses();
    vproductRequestResponses.RetrieveAll();
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

    var tempProductRequestResponses = this.ctrlActions.GetToAPIAsync('ProductRequestResponses/RetrieveById?UserId=' + userId);

    ctrlActions.BindFields('frmProductRequestResponses', tempProductRequest.Data);

    document.querySelector('#suspend').classList.add("invisible");
    if (tempProductRequestResponses.Data.IsActive === false) {
        document.querySelector('#suspend').classList.remove("invisible");
    };
}

function ValidateForm() {
    $("#txtPrice").attr("name", "Price");
    $('#txtQuantity').attr('name', 'Quantity');
    $('#txtDescription').attr('name', 'Description');
    $('#txtDate').attr('name', 'Date');

    validate.extend(validate.validators.datetime, {
        // The value is guaranteed not to be null or undefined but otherwise it
        // could be anything.
        parse: function (value, options) {
            return +moment.utc(value);
        },
        // Input is a unix timestamp
        format: function (value, options) {
            var format = options.dateOnly ? "YYYY-MM-DD" : "YYYY-MM-DD hh:mm:ss";
            return moment.utc(value).format(format);
        }
    });

    this.constraints = {
        Price: {
            presence: true,
            numericality: {
                strict: true,
                greaterThanOrEqualTo: 0
            }
        },
        Quantity: {
            numericality: {
                strict: true,
                greaterThan: 0
            },
            presence: true
        },
        Description: {
            presence: true,
        },
        Date: {
            presence: true,
            datetime: {
                earliest: new Date()
            }
        }
    
    }
    

    this.validate = function() {
        var validated = false;
        if (validate($("#frmProductRequestResponses"), this.constraints) == undefined) {
            validated = true;
        } else {
            console.log(validate($("#frmProductRequestResponses"), this.constraints));
        }
        return validated;
    }


}

// Sweet alert
const swalWithBootstrapButtons = swal.mixin({
    confirmButtonClass: 'btn btn-success ml-2',
    cancelButtonClass: 'btn btn-danger mr-2 ',
    buttonsStyling: false
});

swalWithBootstrapButtons({
    title: '¿Desea modificar este producto?',
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
                    'El producto no ha sido modificado.',
                    'error'
                );
            }
        }
        swalWithBootstrapButtons(
            '¡Modificado!',
            'El producto fue modificado con éxito.',
            'success'
        );
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