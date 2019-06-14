function CrudStock() {


    this.service = 'inventory';
    this.tableInventory = 'tblInventory';
    this.ctrlActions = new ControlActions();
    this.tableProdInv = 'tblProdInventory';
    this.columns = 'ProductId,PriceBought,SerialNumber';


    this.AddStock = function () {
        var entityData = this.ctrlActions.GetDataForm('frmStock');
        var data = {
            ProductId: entityData.ProductId,
            PriceBought: entityData.PriceBought,
            PurchaseDatetime: entityData.PurchaseDatetime,
            SerialNumber: entityData.SerialNumber,
            Quantity: entityData.Quantity
        };

        this.ctrlActions.PostToAPI(this.service + '/addstock', data);
        location.reload();

    }

    this.RetrieveAll = function () {
        var storeId = JSON.parse(localStorage.getItem('Credentials')).StoreId;

        this.ctrlActions.FillTable(this.service + '/RetrieveStoreInventory?pStoreId=' + storeId, this.tableInventory, false);
    }

    this.ProductStockRedirect = function () {
        sessionStorage.setItem("productId", $("#txtProductId").val());
        window.location.href = 'http://localhost:57619/AddStock';
    }

    this.GetProductStockCount = function (productId) {
        return this.ctrlActions.GetFromApi(this.service + "/RetrieveProductInventoryCount?pProductId=" + productId);
    }

    this.RetrieveAllProduct = function () {
        var productId = JSON.parse(sessionStorage.getItem('productId'));
        this.ctrlActions.FillTable(this.service + '/RetrieveProductUnits?pProductId=' + productId, this.tableProdInv, false);
    }

}

$(document).ready(function () {
    var entity = new CrudStock();

    if (document.querySelector('#whoAmI').innerHTML === "prodList") {
        entity.RetrieveAllProduct();
    } else if (document.querySelector('#whoAmI').innerHTML === "addstock") {
        $("#nProductId").val(sessionStorage.getItem("productId"));
    } else if (document.querySelector('#whoAmI').innerHTML === "list"){
        entity.RetrieveAll();
    }
}); 