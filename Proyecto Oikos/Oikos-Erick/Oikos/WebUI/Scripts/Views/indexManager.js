$(document).ready(function () {

    var cred = JSON.parse(localStorage.getItem('Credentials'));

    if (cred == null) {
        document.querySelector('#onlineOptions').classList.add('d-none');
        document.querySelector('#onlineOpt').classList.add('d-none');
        document.querySelector('#offlineOptions').classList.remove('d-none');
        document.querySelector('#offlineOpt').classList.remove('d-none');

    } else {
        document.querySelector('#onlineOptions').classList.remove('d-none');
        document.querySelector('#offlineOptions').classList.add('d-none');
        document.querySelector('#onlineOpt').classList.remove('d-none');
        document.querySelector('#offlineOpt').classList.add('d-none');
        var span = document.getElementById('txtUserName');
        while (span.firstChild) {
            span.removeChild(span.firstChild);
        }
        
        span.appendChild(document.createTextNode(cred.Name));
    }
        

});

