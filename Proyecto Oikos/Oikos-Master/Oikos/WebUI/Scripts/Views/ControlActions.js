var tempData;

function ControlActions() {
    this.URL_API = "http://localhost:60713/api/";

    this.GetViews = function(service) {
        $.get(this.GetUrlApiService(service),
            function(data) {
                data = JSON.stringify(data.Data);
                sessionStorage.setItem("checkboxList", data);
            });
    }

    this.GetUrlApiService = function(service) {
        return this.URL_API + service;
    }

    this.GetTableColumsDataName = function(tableId) {
        var val = $('#' + tableId).attr("ColumnsDataName");

        return val;
    }

    this.CleanForm = function(formId) {

    }

    this.GetCheckedCheckboxes = function(containerId) {
        var lst = [];
        $("#" + containerId + " input:checkbox:checked").each(function(i, v) {
            lst.push(v.value);
        });
        return lst;
    }

    this.GetSelectedDropdownValue = function(dropdownId) {
        return $("#" + dropdownId).find("ul").find(".selected").data().value;
    }

    this.GetCheckboxes = function(containerId) {
        var lst = [];
        $("#" + containerId + " input:checkbox").each(function(i, v) {
            lst.push(v);
        });
        return lst;
    }

    this.UncheckCheckboxes = function(containerId) {
        $("#" + containerId + " input:checkbox").each(function(i, v) {
            $("#" + v.id).prop("checked", false);
        });
    }

    this.GetCheckedCheckboxesAmount = function (containerId) {
        var amount = 0;
        $("#" + containerId + " input:checkbox:checked").each(function (i, v) {
            amount++;
        });
        return amount;;
    }

    this.AddCategoriesToObject = function(objectType, containerId, productId) {
        var catLst = [];
        var controller = "";
        var checkboxes = this.GetCheckedCheckboxes(containerId);
        switch (objectType) {
            case "product":
                controller = "productcategories/";
                for (var checkbox of checkboxes) {
                    catLst.push({ ProductsCategoriesId: 0, ProductId: productId, CategoryId: parseInt(checkbox) });
                }
                console.log(catLst);
                break;
            case "productrequest":
                controller = "productrequestcategories/";
                for (var checkbox of checkboxes) {
                    catLst.push({ ProductRequestsCategoriesId: 0, ProductRequestId: 0, CategoryId: parseInt(checkbox) });
                }
                console.log(catLst);
                break;
        }
        this.PostToAPI(controller + "CreateMultiple", catLst);
    }

    this.FillTable = function(service, tableId, refresh, indexes = "") {
        var columnDefs = [];
        if (indexes !== "") {
            var columns = indexes.split(',').map(Number);
            columns.forEach(function(element) {
                columnDefs.push({ "visible": false, "targets": element });
            });
        }

        if (!refresh) {
            columns = this.GetTableColumsDataName(tableId).split(',');
            var arrayColumnsData = [];

            $.each(columns,
                function(index, value) {
                    var obj = {};
                    obj.data = value;
                    arrayColumnsData.push(obj);
                });

            $('#' + tableId).DataTable({
                "columnDefs": columnDefs,
                "processing": true,
                "ajax": {
                    "url": this.GetUrlApiService(service),
                    dataSrc: "Data"
                },
                "columns": arrayColumnsData
            });
        } else {
            //RECARGA LA TABLA
            $('#' + tableId).DataTable().ajax.reload();
        }
    }

    this.GetSelectedRow = function() {
        var data = sessionStorage.getItem(tableId + '_selected');

        return data;
    };

    this.BindFields = function(formId, data) {
        console.log(data);
        tempData = data;
        $('#' + formId + ' *').filter(':input').each(function(input) {
            var columnDataName = $(this).attr("ColumnDataName");
            this.value = data[columnDataName];
        });
    }

    this.GetDataForm = function(formId) {
        var data = {};

        $('#' + formId + ' *').filter(':input').each(function(input) {
            var columnDataName = $(this).attr("ColumnDataName");
            data[columnDataName] = this.value;
        });
        return data;
    }

    this.ShowMessage = function(type, message) {
        if (type == 'E') {
            $("#alert_container").removeClass("alert alert-success alert-dismissable");
            $("#alert_container").addClass("alert alert-danger alert-dismissable");
            $("#alert_message").text(message);
        } else if (type == 'I') {
            $("#alert_container").removeClass("alert alert-danger alert-dismissable");
            $("#alert_container").addClass("alert alert-success alert-dismissable");
            $("#alert_message").text(message);
        }
        $('.alert').show();
    };

    this.PostToAPI = function(service, data) {

        var jqxhr = $.post(this.GetUrlApiService(service),
                data,
                function(response) {
                    var ctrlActions = new ControlActions();
                    ctrlActions.ShowMessage('I', response.Message);
                })
            .fail(function(response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });

    };

    function setSession(data) {
        localStorage.setItem('Credentials', JSON.stringify(data));
    }

    this.getSession = function() {
        return JSON.parse(localStorage.getItem('Credentials'));
    }

    this.PostToAPIForSession = function(service, data) {

        var jqxhr = $.post(this.GetUrlApiService(service),
                data,
                function(response) {
                    var ctrlActions = new ControlActions();
                    ctrlActions.ShowMessage('I', response.Message);
                    setSession(response.Data);
                })
            .fail(function(response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });
    };

    this.GetFromApi = function(service) {
        var idk;
        var jqxhr = $.get(this.GetUrlApiService(service))
            .done(function(data) {
                idk = data;
            });
        return idk.Data;
    }

    this.PostToAPIRefresh = function(service, data, tableId) {
        var jqxhr = $.post(this.GetUrlApiService(service),
                data,
                function(response) {
                    var ctrlActions = new ControlActions();
                    ctrlActions.ShowMessage('I', response.Message);
                })
            .done(function(response) {
                $('#' + tableId).DataTable().ajax.reload();
            })
            .fail(function(response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });
    };

    this.PutToAPI = function(service, data) {
        console.log(data);
        var jqxhr = $.put(this.GetUrlApiService(service),
                data,
                function(response) {
                    var ctrlActions = new ControlActions();
                    ctrlActions.ShowMessage('I', response.Message);
                })
            .fail(function(response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });
    };

    this.PutToAPIRefresh = function(service, data, tableId) {
        console.log(data);
        var jqxhr = $.put(this.GetUrlApiService(service),
                data,
                function(response) {
                    var ctrlActions = new ControlActions();
                    ctrlActions.ShowMessage('I', response.Message);
                })
            .done(function(response) {
                $('#' + tableId).DataTable().ajax.reload();
            })
            .fail(function(response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });
    };

    this.DeleteToAPI = function(service, data) {
        console.log(data);
        var jqxhr = $.delete(this.GetUrlApiService(service),
                data,
                function(response) {
                    var ctrlActions = new ControlActions();
                    ctrlActions.ShowMessage('I', response.Message);
                })
            .fail(function(response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });
    };

    this.DeleteToAPIRefresh = function(service, data, tableId) {
        console.log(data);
        var jqxhr = $.delete(this.GetUrlApiService(service),
                data,
                function(response) {
                    var ctrlActions = new ControlActions();
                    ctrlActions.ShowMessage('I', response.Message);
                })
            .done(function() {
                $('#' + tableId).DataTable().ajax.reload();
            })
            .fail(function(response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });
    };

    this.GetToAPIAsync = function(service) {

        var respuesta = 'respuesta';
        var peticion = $.ajax({
            url: this.GetUrlApiService(service),
            type: 'GET',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            dataType: 'json',
            async: false,
            data: {
            }
        });

        peticion.done(function(response) {
            respuesta = response;
        });

        peticion.fail(function(response) {
        });

        return respuesta;
    }
}

//Custom jquery actions

$.get = function(url, data) {
    return $.ajax({
        url: url,
        type: 'GET',
        async: false,
        data: JSON.stringify(data)
    });
}


$.post = function(url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }
    return $.ajax({
        url: url,
        type: 'POST',
        async: false,
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}

$.put = function(url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }
    return $.ajax({
        url: url,
        type: 'PUT',
        async: false,
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}

$.delete = function(url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }
    return $.ajax({
        url: url,
        type: 'DELETE',
        success: callback,
        async: false,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}

// Function used to get parameter values from URL
function getAllUrlParams(url) {
    // get query string from url (optional) or window
    var queryString = url ? url.split('?')[1] : window.location.search.slice(1);

    // we'll store the parameters here
    var obj = {};

    // if query string exists
    if (queryString) {

        // stuff after # is not part of query string, so get rid of it
        queryString = queryString.split('#')[0];

        // split our query string into its component parts
        var arr = queryString.split('&');

        for (var i = 0; i < arr.length; i++) {
            // separate the keys and the values
            var a = arr[i].split('=');

            // set parameter name and value (use 'true' if empty)
            var paramName = a[0];
            var paramValue = typeof (a[1]) === 'undefined' ? true : a[1];

            // (optional) keep case consistent
            paramName = paramName.toLowerCase();
            if (typeof paramValue === 'string') paramValue = paramValue.toLowerCase();

            // if the paramName ends with square brackets, e.g. colors[] or colors[2]
            if (paramName.match(/\[(\d+)?\]$/)) {

                // create key if it doesn't exist
                var key = paramName.replace(/\[(\d+)?\]/, '');
                if (!obj[key]) obj[key] = [];

                // if it's an indexed array e.g. colors[2]
                if (paramName.match(/\[\d+\]$/)) {
                    // get the index value and add the entry at the appropriate position
                    var index = /\[(\d+)\]/.exec(paramName)[1];
                    obj[key][index] = paramValue;
                } else {
                    // otherwise add the value to the end of the array
                    obj[key].push(paramValue);
                }
            } else {
                // we're dealing with a string
                if (!obj[paramName]) {
                    // if it doesn't exist, create property
                    obj[paramName] = paramValue;
                } else if (obj[paramName] && typeof obj[paramName] === 'string') {
                    // if property does exist and it's a string, convert it to an array
                    obj[paramName] = [obj[paramName]];
                    obj[paramName].push(paramValue);
                } else {
                    // otherwise add the property
                    obj[paramName].push(paramValue);
                }
            }
        }
    }

    return obj;
}
