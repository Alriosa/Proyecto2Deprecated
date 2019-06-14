function CrudCategory() {

    this.tblCategoriesId = 'tblCategories';
    this.service = 'category';
    this.ctrlActions = new ControlActions();
    this.columns = "CategoryId,Name,Description,IsActive";

    this.RetrieveAll = function() {
        this.ctrlActions.FillTable(this.service + '/retrieveall', this.tblCategoriesId, false, "0,3");
    }

    this.ReloadTable = function() {
        this.ctrlActions.FillTable(this.service + '/retrieveall', this.tblCategoriesId, true);
    }

    this.Create = function() {
        $('#createModal').modal("hide");
        var catData = this.ctrlActions.GetDataForm('frmCreateCategory');
        var data = { CategoryId: 0, Name: catData.Name, Description: catData.Description, IsActive: true };
        this.ctrlActions.PostToAPIRefresh(this.service + '/create', data, this.tblCategoriesId);
        this.ReloadTable();
    }

    this.Update = function() {
        $('#updateModal').modal("hide");
        var catData = {};
        catData = this.ctrlActions.GetDataForm('frmUpdateCategory');
        var data = {
            CategoryId: catData.CategoryId,
            Name: catData.Name,
            Description: catData.Description,
            IsActive: true
        };
        this.ctrlActions.PutToAPIRefresh(this.service + '/update', data, this.tableId);
        this.ReloadTable();

    }

    this.Delete = function() {

        var catData = {};
        catData = this.ctrlActions.GetDataForm('frmUpdateCategory');
        var data = {
            CategoryId: catData.CategoryId,
            Name: catData.Name,
            Description: catData.Description,
            IsActive: false
        };
        this.ctrlActions.DeleteToAPIRefresh(this.service + '/delete', data, this.tableId);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmUpdateCategory', data);
        $("#inpNameUpdate").removeClass("invalidInput");
        $("#inpDescriptionUpdate").removeClass("invalidInput");
        $("#alertMessageUpdate").addClass("d-none");
        $('#updateModal').modal();
    }
}

//ON DOCUMENT READY
$(document).ready(function() {
    var categories = new CrudCategory();
    categories.RetrieveAll();

});

function validateCreation() {
    visualValidation("create");
    if ($("#inpNameCreate").val() === "" || $("#inpDescriptionCreate").val() === "") return false;
    return true;
}

function validateUpdate() {
    visualValidation("update");
    if ($("#inpNameUpdate").val() === "" || $("#inpDescriptionUpdate").val() === "") return false;
    return true;
}

function visualValidation(type) {

    switch (type) {
        case "create":
            if ($("#inpNameCreate").val() === "") {
                setInvalidInput($("#inpNameCreate"));
                $("#alertMessageCreate").removeClass("d-none");
            } else {
                setValidInput($("#inpNameCreate"));
            }

            if ($("#inpDescriptionCreate").val() === "") {
                setInvalidInput($("#inpDescriptionCreate"));
                $("#alertMessageCreate").removeClass("d-none");
            } else {
                setValidInput($("#inpDescriptionCreate"));
            }
            break;
        case "update":
            if ($("#inpNameUpdate").val() === "") {
                setInvalidInput($("#inpNameUpdate"));
                $("#alertMessageUpdate").removeClass("d-none");
            } else {
                setValidInput($("#inpNameUpdate"));
            }

            if ($("#inpDescriptionUpdate").val() === "") {
                setInvalidInput($("#inpDescriptionUpdate"));
                $("#alertMessageUpdate").removeClass("d-none");
            } else {
                setValidInput($("#inpDescriptionUpdate"));
            }
            break;
    }
}

function setValidInput(e) {
    e.removeClass("invalidInput");
}

function setInvalidInput(e) {
    e.addClass("invalidInput");
}