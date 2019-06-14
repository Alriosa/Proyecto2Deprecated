function vCoupons() {

    this.tblShippingProvidersId = 'tblCoupons';
    this.service = 'coupon';
    this.ctrlActions = new ControlActions();
    this.columns =
        "Maker,ValueType,Value";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblShippingProvidersId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + "/retrieveall", this.tblShippingProvidersId, true);
    }

    this.Create = function() {
        var couponData = this.ctrlActions.GetDataForm('frmCoupon');
        var data = {
            CouponId: 2,
            MakerCode: couponData.MakerCode,
            ValueTypeCode: couponData.ValueTypeCode,
            Value: couponData.Value,
            MadeBy: getActiveUserId()
        };

        console.log(data);
        this.service += '/create';
                
        this.ctrlActions.PostToAPIRefresh(this.service, data, this.tableId);

        this.ReloadTable();
    }

    this.Update = function() {
        var couponData = this.ctrlActions.GetDataForm('frmCoupon');
        var data = {
            CouponId: 2,
            MakerCode: couponData.MakerCode,
            ValueTypeCode: couponData.ValueTypeCode,
            Value: couponData.Value,
            MadeBy: getActiveUserId()
        }

        console.log(data);
        this.service += '/update';

        this.ctrlActions.PostToAPIRefresh(this.service, data, this.tableId);

        this.ReloadTable();
    }

    this.Delete = function() {
        var shipProviderData = this.ctrlActions.GetDataForm('frmShippingProvider');

        this.service += '/delete';

        this.ctrlActions.DeleteToAPI(this.service, shipProviderData);

        this.ReloadTable();
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmShippingProvider', data);
    }
}

$(document).ready(function () {
    var vcoupon = new vCoupons();
    vcoupon.RetrieveAll();
});

function activateTabContainers(id) {
    document.getElementById(id).classList.add("active");
}

function validateInput(id) {
    console.log("validated");
}

function getActiveUserId() {
    //    activeUser quemado en sessionStorage
    var user = { UserId: 3 }
    localStorage.setItem('activeUser', JSON.stringify(user));
    return JSON.parse(localStorage.getItem('activeUser')).UserId;
}