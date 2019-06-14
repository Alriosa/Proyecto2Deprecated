
using DataAccess.Crud;
using EntitiesPOJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI {
    public class OptionListManager : MasterManager  {
        private List<OptionList> optionLists;

        public OptionListManager() {
            LoadOptionLists();
        }

        private void LoadOptionLists() {

            try {
                optionLists = RetrieveAll<OptionList>(EntityTypes.OptionList);
            }
            catch (Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }

        }

        public OptionList Retrieve(OptionList option) {
            OptionList selectedOption = new OptionList();

            try {
                foreach (var o in optionLists) {    {
                    if (option.ListId == o.ListId && option.Value == o.Value)
                        selectedOption = o;
                }}
            }
            catch (Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }
            return selectedOption;
        }

        public List<OptionList> RetrieveByListId(OptionList option) {
         List<OptionList> filteredOptionList = new List<OptionList>();

            try {
                foreach (var o in optionLists) {
                    if (option.ListId == o.ListId) 
                        filteredOptionList.Add(o);
                }
            }
            catch (Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }
            return filteredOptionList;
        }

    }
}
