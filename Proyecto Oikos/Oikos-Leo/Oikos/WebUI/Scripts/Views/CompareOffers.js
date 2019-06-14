function CompareOffers() {

    this.service = 'productrequestresponses/';
    this.ctrlActions = new ControlActions();

    this.acceptOffer = function(requestResponseId) {

        const swalWithBootstrapButtons = swal.mixin({
            confirmButtonClass: 'btn btn-success ml-2',
            cancelButtonClass: 'btn btn-danger mr-2 ',
            buttonsStyling: false
        });

        swalWithBootstrapButtons({
            title: '¿Desea aceptar esta oferta?',
            type: 'warning',
            showCancelButton: true,
            text: "Una vez seleccionada una oferta, la solicitud se terminará.",
            confirmButtonText: '¡Sí, continuar!',
            cancelButtonText: '¡No, cancelar!',
            reverseButtons: true
        }).then((result) => {
            if (result.value) {

                try {
                    var resp = this.ctrlActions.GetFromApi(this.service + "retrieve?id=" + requestResponseId);
                    var req = this.ctrlActions.GetFromApi("productrequest/retrievebyid?id=" + resp.ProductRequestId);
                    this.ctrlActions.DeleteToAPI("productrequest/delete", req);
                    resp.IsAccepted = true;
                    this.ctrlActions.PutToAPI(this.service + "update", resp);
                    this.checkoutOffer(resp);
                } catch (err) {
                    {
                        swalWithBootstrapButtons(
                            '¡Error!',
                            err.message,
                            'No se ha podido aceptar la oferta.',
                            'error'
                        );
                    }
                }
                swalWithBootstrapButtons(
                    '¡Felicidades!',
                    'La oferta fue aceptada con éxito.',
                    'success'
                );
            } else if (
                result.dismiss === swal.DismissReason.cancel
            ) {
                swalWithBootstrapButtons(
                    'Cancelado',
                    'La oferta no ha sido aceptada.',
                    'error'
                );
            }
        });
        return false;
    }

    this.checkoutOffer = function(resp) {
        var credentials = JSON.parse(localStorage.getItem("Credentials"));
        console.log(credentials);
        var shoppCart = {
            ShoppingCartId: 0,
            UserId: credentials.UserId,
            ProductId: resp.ProductId,
            Quantity: resp.Quantity,
            IsActive: true,
            Price: resp.Price,
            IsOffer: true
        };
        console.log("por meter al carrito");
        console.log(shoppCart);
        this.ctrlActions.PostToAPI("shoppingcart/create", shoppCart);
        window.location.href = 'http://localhost:57619/ShoppingCart';
        localStorage.removeItem("prodReqId");
    }

    this.generateOffers = function(prodReqId) {
        var responsesLst = this.getResponsesLst(prodReqId);
        var productsLst = this.getProductsLst(responsesLst);
        var prodMediaLst = this.getProdMediaLst(productsLst);
        var html = this.generateHtml(responsesLst, productsLst, prodMediaLst);
        $("#offersContainer").append(html);
    }

    this.getResponsesLst = function(prodReqId) {
        return this.ctrlActions.GetFromApi(
            "productrequestresponses/retrieveallbyrequest?productRequestId=" + prodReqId);
    }

    this.getProductsLst = function(responsesLst) {
        var values = "";
        responsesLst.forEach(obj => {
            values += obj.ProductId + ",";
        });
        return this.ctrlActions.GetFromApi("product/retrievemultiple?ids=" + values.substr(0, values.length - 1));
    }

    this.getProdMediaLst = function(prodLst) {
        var prodMediaLst = [];
        var response = this.ctrlActions.GetFromApi("productmedia/retrieveall");
        for (var prod of prodLst) {
            for (var media of response) {
                if (prod.ProductId !== media.ProductId) continue;
                prodMediaLst.push(media);
                break;
            }          
        }
        return prodMediaLst;
    }

    this.generateHtml = function(responsesLst, prodLst, prodMediaLst) {
        var html = "";
        var i = 0;
        for (var resp of responsesLst) {
            var prod = prodLst[i];
            var media = prodMediaLst[i];
            html += " <div class=\"col-lg-3 col-md-4 col-sm-6 mb-30\" > " +
                "<div class=\"card\">" +
                "<div class=\"card-header\">" +
                "<img class=\"card-img-top h-100\" src=\"" +
                media.Url +
                "\" alt=\"Imagen del producto\">" +
                "</div>" +
                "<div class=\"card-body\">" +
                "<h5 class=\"card-title\">" +
                prod.Name +
                "</h5>" +
                "<p class=\"card-text\">" +
                resp.Description +
                "</p>" +
                "<p class=\"card-text text-right\">" +
                resp.Price +
                "</p>" +
                "</div>" +
                "<div class=\"card-footer\">" +
                "<a productRequestResponseId=\"" +
                resp.ProductRequestResponseId +
                "\" id=\"compCardId" +
                i +
                "\" href=\"#\" class=\"ho-button ho-button-sm\">" +
                "<span>Aceptar oferta</span>" +
                "</a>" +
                "</div>" +
                "</div>" +
                "</div>";
            i++;
        }
        return html;
    }
}

//ON DOCUMENT READY
$(document).ready(function () {
    if (localStorage.getItem("Credentials") !== null) {
        var offers = new CompareOffers();
        var prodReqId = JSON.parse(localStorage.getItem("prodReqId"));
        offers.generateOffers(prodReqId);
    }
});