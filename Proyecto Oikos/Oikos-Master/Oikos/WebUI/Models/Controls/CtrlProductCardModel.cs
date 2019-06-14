using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using EntitiesPOJO;
using Exceptions;
using Newtonsoft.Json;

namespace WebUI.Models.Controls {
    public class CtrlProductCardModel : CtrlBaseModel {
        private string Html { get; set; }
        public int StoreId { get; set; }

        public CtrlProductCardModel() {
            ViewName = "";
            Id = "";
        }

        public string GenerateContent {
            get {
                if (StoreId == 0) {
                    GetAllProducts();
                }
                else {
                    GetAllProductsByStore();
                }

                return Html;
            }
        }

        private void GetAllProducts() {
            try {
                var cli = new WebClient {Encoding = Encoding.UTF8};
                var response = cli.DownloadString(ConfigurationManager.AppSettings["RetrieveAllProducts"]);
                var reqRespLst = JsonConvert.DeserializeObject<List<Product>>(GetResponseData(response));
                var prodMediaLst = GetProductMedia(reqRespLst, cli);
                GenerateHtml(reqRespLst, prodMediaLst);
            }
            catch (BusinessException e) {
                throw;
            }
        }

        private void GetAllProductsByStore() {
            try {
                var cli = new WebClient { Encoding = Encoding.UTF8 };
                var response = cli.DownloadString(ConfigurationManager.AppSettings["RetrieveAllProductsByStore"]);
                var reqRespLst = JsonConvert.DeserializeObject<List<Product>>(GetResponseData(response));
                var prodMediaLst = GetProductMedia(reqRespLst, cli);
                GenerateHtml(reqRespLst, prodMediaLst);
            }
            catch (BusinessException e) {
                throw;
            }
            
        }

        private List<ProductMedia> GetProductMedia(List<Product> lstProducts, WebClient cli) {
            var prodMediaLst = new List<ProductMedia>();

            try {
                var response = cli.DownloadString(ConfigurationManager.AppSettings["RetrieveAllProductMedia"]);
                prodMediaLst = JsonConvert.DeserializeObject<List<ProductMedia>>(GetResponseData(response));
            }
            catch (BusinessException e) {
                throw;
            }

            return prodMediaLst;
        }

        private void GenerateHtml(List<Product> productLst, List<ProductMedia> prodMediaLst) {
            foreach (var obj in productLst) {
                string productUrl = "https://res.cloudinary.com/oikos-store/image/upload/v1542776897/product_placeholder.svg";
                string sellingPrice = String.Format(new CultureInfo("en-CR"), "{0:C}", obj.SellingPrice);

                for (int i = 0; i < prodMediaLst.Count; i++) {
                        if (obj.ProductId == prodMediaLst[i].ProductId) {
                            productUrl = prodMediaLst[i].Url;
                            i = prodMediaLst.Count;
                    }
                }
                
                Html += "<!-- Single Product Beginning -->" +
                        "<div class=\"col-lg-3 col-md-4 col-sm-6 col-12\">" +

                        "<article class=\"hoproduct store-product-card\">" +
                            "<div class=\"hoproduct-image\">" + 
                                "<a class=\"hoproduct-thumb\" href =\"Product?productId=" + obj.ProductId + "\">" +
                                    "<img class=\"hoproduct-frontimage store-product-image\" src =\"" + productUrl + "\" alt =\"" + obj.Name + "\">" + 
                                "</a>" +
                                "<ul class=\"hoproduct-actionbox\">" +
                                    "<li><a href =\"Product?productId=" + obj.ProductId + "\" class=\"quickview-trigger\"><i class=\"lnr lnr-eye\"></i></a></li>" + 
                                "</ul>"+ 
                             "</div>" +
                                "<div class=\"hoproduct-content store-card-product-name\">" +
                                    "<h5 class=\"hoproduct-title store-card-product-name\">" +
                                    "<a href=\"product-details.html\">" + 
                                        "<span class=\"store-card-product-name\">" + obj.Name + "</span>" + 
                                    "</a>" + 
                                    "</h5>" +
                                "<div class=\"hoproduct-pricebox\">" +
                                "<div class=\"pricebox\">" +
                                    "<span class=\"price\">" + sellingPrice + "</span>" +
                                "</div>" + 
                                "</div>" + 
                            "</div>" + 
                        "</article>" +
                        "</div>" +
                        "<!--// Single Product End-->";
            }
        }

        private string GetResponseData(string response) {
            var regex = new Regex(@"(\[.*\])", RegexOptions.Multiline);
            var match = regex.Match(response);
            return match.Value;
        }

        private string GetResponseDataSingleObject(string response) {
            var regex = new Regex(@"""Data"":({|\[)?({.*})(}|])?", RegexOptions.Multiline);
            var match = regex.Match(response);
            var masd = match.Groups[2].Value;
            return masd;
        }
    }
}