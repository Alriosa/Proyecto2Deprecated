using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using EntitiesPOJO;
using Newtonsoft.Json;

namespace WebUI.Models.Controls {
    public class CtrlCheckboxModel : CtrlBaseModel {
        public string ContainerId { get; set; }
        public string ColMdSize { get; set; }
        public string ColSmSize { get; set; }
        public EntityTypes CheckboxType { get; set; }
        public string ViewApiAction { get; set; }

        public string GenerateCheckboxes {
            get {
                var finalHtml = "";
                var lst = GetLstFromApi();
                var i = 0;
                foreach (var obj in lst) {
                    finalHtml += "<div class=\"" + ColMdSize + " " + ColSmSize + "\"><input value=\"" + obj.Value +
                                 "\" type=\"checkbox\" id=\"" + Id + i + "\"><label for=\"" + Id + i + "\">" +
                                 obj.Label + "</label></div>";
                    i++;
                }

                return finalHtml;
            }
        }

        public CtrlCheckboxModel() {
            ViewName = "";
        }

        /*
         * This method is in charge of retrieving the list of checkboxes with a given entity type.
         *
         * @author Leonardo Mora
         * @return The list of checkboxes used in the dropdown.
         */
        private List<Checkbox> GetLstFromApi() {
            var objLst = new List<Checkbox>();
            switch (CheckboxType) {
                case EntityTypes.View:
                    DeserializeList<View>(objLst);
                    break;
                case EntityTypes.Category:
                    DeserializeList<Category>(objLst);
                    break;
            }

            return objLst;
        }

        /*
         * This method is in charge of retrieving all of the objects in the database in the given entity. Once it has the objects, it deserializes the list and returns it.
         *
         * @author Leonardo Mora
         * @param WebClient client - The WebClient used to contact the API.
         * @return The list of deserialized objects.
         */
        private void DeserializeList<T>(List<Checkbox> objLst) {
            var client = new WebClient {Encoding = Encoding.UTF8};
            string response;
            List<T> lst;
            switch (CheckboxType) {
                case EntityTypes.View:
                    response = client.DownloadString(ConfigurationManager.AppSettings["RetrieveAllViews"] + ViewApiAction);
                    lst = JsonConvert.DeserializeObject<List<T>>(GetResponseData(response));
                    foreach (var obj in lst)
                        objLst.Add(new CheckboxFactory().CreateCheckbox((BaseEntity) (object) obj, CheckboxType));
                    break;
                case EntityTypes.Category:
                    response = client.DownloadString(ConfigurationManager.AppSettings["RetrieveAllCategories"]);
                    lst = JsonConvert.DeserializeObject<List<T>>(GetResponseData(response));
                    foreach (var obj in lst)
                        objLst.Add(new CheckboxFactory().CreateCheckbox((BaseEntity)(object)obj, CheckboxType));
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