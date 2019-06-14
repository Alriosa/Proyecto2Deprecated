namespace WebUI.Models.Controls {
    public class CtrlMoneyInputModel : CtrlBaseModel {
        public string Type { get; set; }
        public string DivSize { get; set; }
        public string Format { get; set; }
        public string Label { get; set; }
        public string PlaceHolder { get; set; }
        public string ColumnDataName { get; set; }

        public CtrlMoneyInputModel() {
            ViewName = "";
        }
    }
}