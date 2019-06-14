using System.Web;

namespace WebUI.Models.Controls {
    public class CtrlBaseModel {

        public string Id { get; set; }
        public string ViewName { get; set; }

        private string ReadFileText() {
            string path = HttpContext.Current.Server.MapPath("~/Models/Controls/");
            string fileName = this.GetType().Name + ".html";

            path = path + fileName;

            string text = System.IO.File.ReadAllText(path);

            return text;
        }

        public string GetHtml() {
            var html = ReadFileText();

            foreach (var prop in this.GetType().GetProperties()) {
                if (prop != null) {
                    var value = prop.GetValue(this, null).ToString();

                    var tag = string.Format("-#{0}-", prop.Name);
                    html = html.Replace(tag, value);
                }
            }
            return html;
        }
    }
}