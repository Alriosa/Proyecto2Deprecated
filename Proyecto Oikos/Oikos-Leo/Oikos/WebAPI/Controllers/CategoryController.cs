using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using WebAPI.Models;
using Exceptions;

namespace WebAPI.Controllers {
    public class CategoryController : ApiController {
        private ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        [HttpGet]
        /*
         *This method is in charge of retrieving all the categories registered in the database.
         *
         * @author Leonardo Mora
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var data = mng.RetrieveAll<Category>(EntityTypes.Category);
            var filteredLst = new List<Category>();
            foreach (var category in data) if (category.IsActive) filteredLst.Add(category);
            foreach (var cat in filteredLst) textMod.AdaptObject(cat, EntityTypes.Category, false);
            apiResp.Data = filteredLst;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult RetrieveAllAlphabetically() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var data = mng.RetrieveAll<Category>(EntityTypes.Category);
            var filteredLst = new List<Category>();
            foreach (var category in data) if (category.IsActive) filteredLst.Add(category);
            foreach (var cat in filteredLst) textMod.AdaptObject(cat, EntityTypes.Category, false);
            apiResp.Data = filteredLst.OrderBy(cat => cat.Name).ToList();
            return Ok(apiResp);
        }

        [HttpPost]
        /*
         *This method is in charge of retrieving a category registered in the database.
         *
         * @author Leonardo Mora
         * @param Category cat - An instance of the Category class with the id to search for.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Retrieve(Category cat) {
            try {
                var mng = new MasterManager();
                cat = mng.Retrieve<Category>(cat, EntityTypes.Category);
                textMod.AdaptObject(cat, EntityTypes.Category, false);
                apiResp = new ApiResponse() {Data = cat};
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        /*
         *This method is in charge of registering a new category in the database.
         *
         * @author Leonardo Mora
         * @param Category cat - An instance of the Category class with the information of the category to be registered.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Create(Category cat) {
            try {
                var mng = new MasterManager();
                if (cat.CategoryId == 0) cat.CategoryId = mng.GetMaxId(cat,EntityTypes.Category)+1;
                cat.IsActive = true;
                textMod.AdaptObject(cat, EntityTypes.Category, true);
                mng.Create<Category>(cat, EntityTypes.Category);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpPut]
        /*
         *This method is in charge of updating the information of a registered category in the database.
         *
         * @author Leonardo Mora
         * @param Category cat - An instance of the Category class with the information to be updated.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Update(Category cat) {
            try {
                var mng = new MasterManager();
                textMod.AdaptObject(cat, EntityTypes.Category, true);
                mng.Update(cat, EntityTypes.Category);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpDelete]
        /*
         *This method is in charge of deleting a category registered in the database.
         *
         * @author Leonardo Mora
         * @param Category cat - An instance of the Category class with the id to be deleted.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Delete(Category cat) {
            try {
                var mng = new MasterManager();
                cat = mng.Retrieve<Category>(cat, EntityTypes.Category);
                cat.IsActive = false;
                textMod.AdaptObject(cat, EntityTypes.Category, true);
                mng.Update(cat, EntityTypes.Category);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}