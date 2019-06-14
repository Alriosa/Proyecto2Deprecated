$().ready(function() {

    $("#frmShippingMethod").validate({
        //Aqui coloquen los nombre de los ID de los campos 
        rules: {
            txtName: {
                required: true,
                minlength: 2,
                lettersonly: true
            },
            txtShippingProviderId: {
                required: true,
                minlength: 1,
                number: true
            },
            txtBaseCost:{
                required: true,
                minglength: 1,
                number: true
            },
            txtAdditionalCost: {
                required: true,
                minglength: 1,
                number: true
            },
            txtDescription: {
                required: true,
                minlength: 4
            },
            drpIsActive: {
                required: true
            }
        },
        messages: {
            txtName: "Introduzca un nombre valido",
            txtShippingProviderId: "Introduzca un ID valido",
            txtAdditionalCost: "Introduzca un Costo adicional valido",
            txtDescription: "Introduce una descripcion",
            drpIsActive: "Seleccione su actividad"
        }
    }); //Fin del validate
});
