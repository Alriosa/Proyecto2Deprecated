using EntitiesPOJO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Configuration;
using WebUI.Models.Controls;

namespace WebUI.Models.Controls {
    public class CtrlDropDown2Model : CtrlBaseModel{

        public string Label { get; set; }
        public string ListId { get; set; }
        public string DivSize { get; set; }
        public string ColumnDataName { get; set; }
        public string DisableThis { get; set; }

        private string URL_API_LIST = WebConfigurationManager.AppSettings["OptionListApiRetrieveAllUri"];

        public CtrlDropDown2Model() {
            ViewName = "";
        }

        public string ListOptions {
            get {
                var htmlOptions = "";
                var lst = GetOptionsFromAPI();

                foreach (var option in lst) {
                    htmlOptions += "<option value='" + option.Value + "'>" +  option.Description + "</option>";
                }
                return htmlOptions;
            }
            set {

            }
        }

        private List<OptionList> GetOptionsFromAPI() {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var response = client.DownloadString(URL_API_LIST + ListId);
            var options = JsonConvert.DeserializeObject<List<OptionList>>(response);
            return options;
        }
        
    }
}