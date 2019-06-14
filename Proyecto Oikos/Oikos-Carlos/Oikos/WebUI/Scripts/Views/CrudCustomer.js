function CrudCustomer() {


    this.contactService = "usercontacts";
    this.locationService = "userlocation";
    this.mediaService = "usermedia";
    this.ctrlActions = new ControlActions();


    this.Create = function () {

        this.CreateUserPic();
        this.CreateUserIdMedia();
        this.CreateUserContact();
        this.CreateUserLocation();


        window.location.href = "index";


    }
    this.CreateUserContact = function (){
        var contactInfo = this.ctrlActions.GetDataForm('frmUserContact');
        var data = {
            UserId: getActiveUserId(),
            ContactTypeCode: $("#drpContactType").val(),
            ContactValue: contactInfo.ContactValue
    };
        console.log(data);
        this.ctrlActions.PostToAPI(this.contactService + '/create', data);
    }

   
    this.CreateUserIdMedia = function () {
        var data = {
            UserId: getActiveUserId(),           
            Url: $("#txtIdPic").val(),
            UsersMediaTypeCode: "UMEDIA02",
            IsActive: true
        };
        console.log(data);
        this.ctrlActions.PostToAPI(this.mediaService + '/create', data);
    }
    this.CreateUserPic = function () {
        var data = {
            UserId: getActiveUserId(),
            Url: $("#txtCustomerPhoto").val(),
            UsersMediaTypeCode: "UMEDIA01",
            IsActive: true
        };
        console.log(data);
        this.ctrlActions.PostToAPI(this.mediaService + '/create', data);
    }

    this.CreateUserLocation = function() {
        var locationInfo = this.ctrlActions.GetDataForm('frmUserLocation');
        var data = {
            UserId: getActiveUserId(),
            Latitude: locationInfo.Latitude,
            Longitude: locationInfo.Longitude,
            Address: locationInfo.Address,
            Province: $("#sltProvince").val(),
            IsActive: true
    };
        console.log(data);
        this.ctrlActions.PostToAPI(this.locationService + '/create', data);
    }
}
function getActiveUserId() {
    return JSON.parse(localStorage.getItem('Credentials')).UserId;
}
