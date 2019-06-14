using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using EntitiesPOJO;
using Newtonsoft.Json;
using Exceptions;

namespace WebUI.Models.Controls {
    public class CtrlComparativeTableModel : CtrlBaseModel {
        public int ProductRequestId { get; set; }
        private string Html { get; set; }

        public CtrlComparativeTableModel() {
            ViewName = "";
            Html = "";
        }

        public string GenerateContent {
            get {
                GetRequestResponsesLists();
                return Html;
            }
        }

        private void GetRequestResponsesLists() {
            var cli = new WebClient {Encoding = Encoding.UTF8};
            var response = cli.DownloadString(ConfigurationManager.AppSettings["RetrieveAllResponsesByRequest"] +
                                              "?productRequestId=" + ProductRequestId);
            var reqRespLst = JsonConvert.DeserializeObject<List<ProductRequestResponses>>(GetResponseData(response));
            var prodLst = GetProductList(reqRespLst, cli);
            var prodMediaLst = GetProductMedia(prodLst, cli);
            GenerateHtml(reqRespLst, prodLst, prodMediaLst);
        }

        private List<ProductMedia> GetProductMedia(List<Product> lstProducts, WebClient cli) {
            var prodMediaLst = new List<ProductMedia>();
            try {
                var response = cli.DownloadString(ConfigurationManager.AppSettings["RetrieveAllProductMedia"]);
                var data = JsonConvert.DeserializeObject<List<ProductMedia>>(GetResponseData(response));
                foreach (var obj in lstProducts) {
                    foreach (var media in data) {
                        if (obj.ProductId != media.ProductId) continue;
                        prodMediaLst.Add(media);
                        break;
                    }
                }
            } catch (BusinessException e) {
                throw;
            }

            return prodMediaLst;
        }

        private List<Product> GetProductList(List<ProductRequestResponses> reqRespLst, WebClient cli) {
            var prodLst = new List<Product>();
            foreach (var obj in reqRespLst) {
                var response = cli.DownloadString(ConfigurationManager.AppSettings["RetrieveProductById"] + "?pId=" +
                                                  obj.ProductId);
                var data = GetResponseDataSingleObject(response);
                prodLst.Add(JsonConvert.DeserializeObject<Product>(data));
            }

            return prodLst;
        }

        private void GenerateHtml(List<ProductRequestResponses> reqRespLst, List<Product> prodLst, List<ProductMedia> prodMediaLst) {
            var i = 0;
            foreach (var obj in reqRespLst) {
                var prod = prodLst[i];
                var media = prodMediaLst[i];
                Html += " <div class=\"col-lg-3 col-md-4 col-sm-6 mb-30\" > " + "<div class=\"card\">" +
                        "<div class=\"card-header\">" +
                        "<img class=\"card-img-top h-100\" src=\"https://res.cloudinary.com/oikos-store/image/upload/ar_1:1c,_pad,h_800,q_auto:good,r_0,w_800/v1543686466/products/xhyb7r3kaqvcfhxbdfrm.jpg\" alt=\"Imagen del producto\">" +
                        "</div>" + "<div class=\"card-body\">" + "<h5 class=\"card-title\">" + prod.Name + "</h5>" +
                        "<p class=\"card-text\">" + obj.Description + "</p>" + "<p class=\"card-text text-right\">" +
                        obj.Price + "</p>" + "</div>" + "<div class=\"card-footer\">" +
                        "<a productRequestResponseId=\"" + obj.ProductRequestResponseId + "\" id=\"compCardId" + i +
                        "\" href=\"#\" class=\"ho-button ho-button-sm\">" + "<span>Aceptar oferta</span>" + "</a>" +
                        "</div>" + "</div>" + "</div>";
                i++;
            }
        }

        private string GetResponseData(string response) {
            var regex = new Regex(@"(\[.*\])", RegexOptions.Multiline);
            var match = regex.Match(response);
            return match.Value;
        }

        private string GetResponseDataSingleObject(string response) {
            var regex = new Regex(@"""Data"":({|\[)?({.*})(}|])+", RegexOptions.Multiline);
            var match = regex.Match(response);
            var group = match.Groups[2].Value;
            return group;
        }
    }
}