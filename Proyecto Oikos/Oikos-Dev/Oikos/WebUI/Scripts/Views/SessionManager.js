function SessionManager() {

    this.setView = function() {
        var user = JSON.parse(localStorage.getItem('Credentials'));
        var adminCode = 'STS00';
        var storeAdminCode = 'STS01';

        

        if (user.StoreId != 0) {
            window.location.href = 'StoreUserDashboard';
        } else if (user.ShippingProviderId != 0) {
            window.location.href = 'ProviderUserDashboard';
        } else if (user.UserStatusCode != adminCode && user.ShippingProviderId == 0 && user.StoreId == 0) {
            if (JSON.parse(localStorage.getItem('previousRef') != null)) {
                window.location.href = JSON.parse(localStorage.getItem('previousRef'));
            } else {
                window.location.href = 'index';
            }
            
        }
        if (user.UserStatusCode==adminCode) {
            window.location.href = 'AdminMenu';
        }
        if (user.UserStatusCode == storeAdminCode) {
            window.location.href = 'StoreUserDashboard';
        }
    }

    this.verifyView = function(viewName) {
        var user = JSON.parse(localStorage.getItem('Credentials'));
        var isValid = false;
        user.RolesViewsList.forEach(function(obj) {
            if (obj.ViewName.localeCompare(viewName)) {
                isValid = true;
            }
        });

    }

    this.LogOut = function() {
        localStorage.clear();
        window.location.href = 'LogInRegister';
    }
}