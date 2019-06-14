function Categories() {
    this.ctrlActions = new ControlActions();

    this.RetrieveCategoriesById = function(type, id, containerId) {
        var lst = [];
        switch (type) {
        case "product":
            lst = this.ctrlActions.GetFromApi("productcategories/retrieveallbyproduct?productId=" + id);
            break;
        case "productrequest":
            lst = this.ctrlActions.GetFromApi("productrequestcategories/retrieveallbyrequest?requestId=" + id);
            break;
        }
        this.CheckAssignedCategories(lst, containerId);
    }

    this.CheckAssignedCategories = function(lst, containerId) {
        var inputs = this.ctrlActions.GetCheckboxes(containerId);
        for (var i = 0; i < inputs.length; i++) {
            for (var j = 0; j < lst.length; j++) {
                if (inputs[i].value == lst[j].CategoryId) {
                    $("#checkId" + i).prop("checked", true);
                }
            }
        }
    }
}