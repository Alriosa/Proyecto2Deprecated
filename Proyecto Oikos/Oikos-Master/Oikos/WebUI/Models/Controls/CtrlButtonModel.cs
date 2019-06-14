using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.Controls {
    public class CtrlButtonModel : CtrlBaseModel {
        public string Style { get; set; }
        public string Label { get; set; }
        public string FunctionName { get; set; }
        public string ButtonType { get; set; }
        public string Params { get; set; }

        public CtrlButtonModel() {
            ViewName = "";
        }
    }
}