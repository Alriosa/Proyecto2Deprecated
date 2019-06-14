using EntitiesPOJO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WebUI.Models.Controls {
    public class CtrlDropDownModel : CtrlBaseModel {
        public string Label { get; set; }
        public string ListId { get; set; }
        public string DivSize { get; set; }
        public string ColumnDataName { get; set; }
        public EntityTypes ListType { get; set; }

        public string ListOptions {
            get {
                var htmlOptions = "";
                var lst = GetOptionsFromAPI();

                foreach (var option in lst) {
                    htmlOptions += "<option value='" + option.Value + "'>" + " " + option.Description + "</option>";
                }

                return htmlOptions;
            }
        }

        public CtrlDropDownModel() {
            ViewName = "";
        }

        /*
         * This method is in charge of retrieving the options list with a given entity type.
         *
         * @author Leonardo Mora
         * @return The list of options used in the dropdown.
         */
        private List<OptionList> GetOptionsFromAPI() {
            var optLst = new List<OptionList>();
            switch (ListType) {
                case EntityTypes.View:
                    DeserializeList<View>(optLst);
                    break;
                case EntityTypes.ProductProvider:
                    DeserializeList<ProductProvider>(optLst);
                    break;
            }

            return optLst;
        }

        /*
         * This method is in charge of retrieving all of the objects in the database in the given entity. Once it has the objects, it deserializes the list and returns it.
         *
         * @author Leonardo Mora
         * @param WebClient client - The WebClient used to contact the API.
         * @return The list of deserialized objects.
         */
        private void DeserializeList<T>(List<OptionList> objLst) {
            var client = new WebClient {Encoding = Encoding.UTF8};
            string response;
            List<T> lst;
            switch (ListType) {
                case EntityTypes.View:
                    response = client.DownloadString(ConfigurationManager.AppSettings["RetrieveAllViews"]);
                    lst = JsonConvert.DeserializeObject<List<T>>(GetResponseData(response));
                    foreach (var obj in lst)
                        objLst.Add(new OptionListFactory().CreateOption((BaseEntity) (object) obj, ListType, ListId));
                    break;
                case EntityTypes.Category:
                    response = client.DownloadString(ConfigurationManager.AppSettings["RetrieveAllCategories"]);
                    lst = JsonConvert.DeserializeObject<List<T>>(GetResponseData(response));
                    foreach (var obj in lst)
                        objLst.Add(new OptionListFactory().CreateOption((BaseEntity) (object) obj, ListType, ListId));
                    break;
                case EntityTypes.ProductProvider:
                    response = client.DownloadString(ConfigurationManager.AppSettings["RetrieveAllProductProviders"]);
                    lst = JsonConvert.DeserializeObject<List<T>>(GetResponseData(response));
                    foreach (var obj in lst)
                        objLst.Add(new OptionListFactory().CreateOption((BaseEntity) (object) obj, ListType, ListId));
                    break;
            }
        }

        //Tengo que hacer esto una interface, pero despues lo hago
        private string GetResponseData(string response) {
            var regex = new Regex(@"(\[.*\])", RegexOptions.Multiline);
            var match = regex.Match(response);
            return match.Value;
        }
    }
}