
function CrudRole() {

    this.tblRoles = 'tblRoles';
    this.service = 'role/';
    this.ctrlActions = new ControlActions();
    this.columns = "RoleId,Name,IsActive";
    this.checkboxContId = "checkboxCont";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service+"retrieveall", this.tblRoles, false, "0,2");
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service + "retrieveall", this.tblRoles, true);
        $("#viewsModal").modal('hide');
    }

    this.Create = function () {
        var roleFormData = this.ctrlActions.GetDataForm('frmRole');
        var role = { RoleId: 0, Name: roleFormData.Name, IsActive : true };
        this.ctrlActions.PostToAPIRefresh(this.service + "create", role);
        this.ReloadTable();
    }

    this.Update = function () {

        var roleFormData = this.ctrlActions.GetDataForm('frmRole');
        var role = { RoleId: roleFormData.RoleId, Name: roleFormData.Name, IsActive: roleFormData.IsActive };
        this.ctrlActions.PutToAPIRefresh(this.service + "update", role);
        this.ReloadTable();

    }

    this.Delete = function () {

        var roleFormData = this.ctrlActions.GetDataForm('frmRole');
        var role = { RoleId: roleFormData.RoleId, Name: roleFormData.Name, IsActive: roleFormData.IsActive };
        this.ctrlActions.DeleteToAPIRefresh(this.service + "delete", role);
        this.ReloadTable();

    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmRole', data);
        this.ctrlActions.UncheckCheckboxes("checkboxCont");
        this.RetrieveViewsByRole(data.RoleId);
        $('#viewsModal').modal();
    }

    this.AddViewsToRole = function () {
        var data = this.ctrlActions.GetDataForm("frmRole");
        var roleView = { UsersRolesViewsId: 0, RoleId: data.RoleId, IsActive: true, UserId: 0 };
        var lst = this.ctrlActions.GetCheckedCheckboxes("checkboxCont");
        for (var i = 0; i < lst.length; i++) {
            roleView.ViewName = lst[i];
            this.ctrlActions.PostToAPI("usersrolesviews/create", roleView);
        }
        $("#viewsModal").modal('hide');
    }

    this.RetrieveViewsByRole = function(roleId) {
        var lst = this.ctrlActions.GetToAPIAsync("usersrolesviews/retrieveallbyrole?roleId=" + roleId);
        this.CheckAssignedViews(lst.Data);
    }

    this.CheckAssignedViews = function (lst) {
        
        var inputs = this.ctrlActions.GetCheckboxes("checkboxCont");
        for (var i = 0; i < inputs.length; i++) {
            for (var j = 0; j < lst.length; j++) {
                if (inputs[i].value === lst[j].ViewName) {
                    $("#checkId"+[i]).prop("checked", true);
                }
            }
        }
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var view = new CrudRole();
    view.RetrieveAll();

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