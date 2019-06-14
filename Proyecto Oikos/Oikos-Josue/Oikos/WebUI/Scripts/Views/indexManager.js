$(document).ready(function () {
    var entity = new Crud();

    if (document.querySelector('#forWhomAmI') == null) {
        entity.RetrieveAll();
    } else if (document.querySelector('#forWhomAmI').innerHTML === "store") {
        entity.RetrieveAllByStoreId();
    }
});

