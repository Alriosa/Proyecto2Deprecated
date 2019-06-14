using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;
using EntitiesPOJO;
using WebUI.Models.Controls;

namespace WebUI.Models.Helpers {
    public static class ControlExtensions {
        public static HtmlString CtrlInput(this HtmlHelper html, string divSize, string id, string type, string format,
            string label, string placeHolder = "", string columnDataName = "") {
            var ctrl = new CtrlInputModel {
                Id = id,
                DivSize = divSize,
                Type = type,
                Format = format,
                Label = label,
                PlaceHolder = placeHolder,
                ColumnDataName = columnDataName
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlButton(this HtmlHelper html, string viewName, string id, string style,
            string label, string onClickFunction = "", string buttonType = "primary", string parameters = "") {
            var ctrl = new CtrlButtonModel {
                ViewName = viewName,
                Id = id,
                Style = style,
                Label = label,
                FunctionName = onClickFunction,
                ButtonType = buttonType,
                Params = parameters
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlTable(this HtmlHelper html, string viewName, string id, string title,
            string columnsTitle, string ColumnsDataName, string onSelectFunction) {
            var ctrl = new CtrlTableModel {
                ViewName = viewName,
                Id = id,
                Title = title,
                Columns = columnsTitle,
                ColumnsDataName = ColumnsDataName,
                FunctionName = onSelectFunction
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlDropDown(this HtmlHelper html, string id, string label, string listId,
            string divSize, EntityTypes entityType, string columnDataName) {
            var ctrl = new CtrlDropDownModel {
                Id = id,
                Label = label,
                ListId = listId.ToUpper(),
                DivSize = divSize,
                ListType = entityType,
                ColumnDataName = columnDataName
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlDropDown2(this HtmlHelper html, string id, string label, string listId,
            string style, string columnDataName, string disabledThis = "") {
            var ctrl = new CtrlDropDown2Model {
                Id = id,
                Label = label,
                ListId = listId.ToUpper(),
                DivSize = style,
                ColumnDataName = columnDataName,
                DisableThis = " " + disabledThis
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlTextArea(this HtmlHelper html, string divSize, string id, string type,
            string format,
            string label, string placeHolder = "", string columnDataName = "") {
            var ctrl = new CtrlTextAreaModel {
                Id = id,
                DivSize = divSize,
                Type = type,
                Format = format,
                Label = label,
                PlaceHolder = placeHolder,
                ColumnDataName = columnDataName
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlCheckbox(this HtmlHelper html, string id, string containerId, EntityTypes type,
            string colMdSize = "col-md-4", string colSmSize = "col-sm-12", string viewApiAction = "") {
            var ctrl = new CtrlCheckboxModel {
                Id = id,
                ContainerId = containerId,
                CheckboxType = type,
                ColMdSize = colMdSize,
                ColSmSize = colSmSize,
                ViewApiAction = viewApiAction
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlProductCard(this HtmlHelper html, int storeId) {
            var ctrl = new CtrlProductCardModel {
                StoreId = storeId
            };
            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlComparativeTable(this HtmlHelper html, int prodReqId, string id = "") {
            var ctrl = new CtrlComparativeTableModel {
                Id = id,
                ProductRequestId = prodReqId
            };
            return new HtmlString(ctrl.GetHtml());
        }
    }
}

