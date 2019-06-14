function Views() {

    this.service = 'view';
    this.ctrlActions = new ControlActions();

    this.RetrieveViews = function () {
        this.ctrlActions.GetViews(this.service + "/retrievestoreviews");
        var checks = sessionStorage.getItem("checkboxList");
        checks = JSON.parse(checks);
        console.log("getSess: " + checks);
        var string = "";
        for (var i = 0; i < checks.length;i++) {
            console.log(checks[i]);
            string += checks[i].Name + ",";
        }
        string = string.slice(0, -1);
        sessionStorage.setItem("checkString",string);
    }
}

////ON DOCUMENT READY
//$(document).ready(function () {
//    var views = new Views();
//    views.RetrieveViews();
//});