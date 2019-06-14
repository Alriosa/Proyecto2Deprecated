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

    this.GenerateDropdown = function(storeId) {
        var lst = this.ctrlActions.GetFromApi("productprovider/retrieveallbystoreid?storeId=" + storeId);
        var options = "";
        var niceOptions = "";

        for (var cat of lst){
            options += "<option value='" + cat.ProductProviderId + "'>" + " " + cat.Name + "</option>";
            niceOptions += "<li data-value=\"" + cat.ProductProviderId + "\" class=\"option\">" + cat.Name+"</li>";
        }
        $("#drpProvider").append(options);
        $("#drpProviderWrapper > div.nice-select > ul.list").append(niceOptions);
    }


    /*
     * <li><a href="Product?CategoryId=2"><i></i>Deporte</a></li>
     */
    this.GenerateCatMenu = function() {
        var lst = this.ctrlActions.GetFromApi("category/retrieveallalphabetically");
        console.log(lst);
        var html = "";
        for (var cat of lst) {
            html += "<li><a href=\"Product?CategoryId=" + cat.CategoryId + "\"><i class=\"\"></i>" + cat.Name +"</a></li>";
        }
        $("#catmenu > nav.catmenu-body > ul").append(html);
    }
}