function LogIn() {
    this.service = 'login';
    this.ctrlActions = new ControlActions();
    this.sessionMng = new SessionManager();


    this.LogIn = function() {
        var credentials = this.ctrlActions.GetDataForm('frmLogin');

        if (credentials.Email === "" || credentials.Password === "") {
            Swal(
                'Error',
                'Digite un correo y una contraseña validas para ingresar.',
                'error'
            )
        } else {
            var pUser = {
                "Password": credentials.Password,
                "Email": credentials.Email,
            };
            this.service += "/login";
            this.ctrlActions.PostToAPIForSession(this.service, pUser);

            if (this.ctrlActions.getSession().UserId==0) {
                Swal(
                    'Error',
                    'Digite un correo y una contraseña validas para ingresar.',
                    'error'
                )
                
            } else {
                this.sessionMng.setView();
            }

            
        }     
    }

    this.Validate = function() {
        var credentials = this.ctrlActions.GetDataForm('frmLogin');
        var bool = false;
        if (credentials.Email === "" || credentials.Password === "") {
            Swal(
                'Error',
                'Digite un correo y una contraseña validas para ingresar.',
                'error'
            );
        }
    }

    this.CreateUserRedirect = function() {
        sessionStorage.setItem("newEmail", $("#txtRegisterEmail").val());
        sessionStorage.setItem("newFlag", true);
        window.location.href = 'http://localhost:57619/CreateUser';
    }
}

$(document).ready(function() {
    localStorage.clear();
});