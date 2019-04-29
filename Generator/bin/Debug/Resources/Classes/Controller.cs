using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using $NameSpace$.Models.Dto;
using $NameSpace$.Models.Utilities;
using $NameSpace$.Models.ViewModels;
using $NameSpace$.Services;

namespace $NameSpace$.Controllers
{
    public class $Entity$Controller : CommonController
    {

        $Entity$Service _service$Entity$;

        #region --------------Constructor--------------
        //---------------------------------------------------------------------
        //Constructor
        //---------------------------------------------------------------------
        public $Entity$Controller()
        {
            _service$Entity$ = new $Entity$Service();
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------Create[HttpGet]--------------
        //---------------------------------------------------------------------
        //Create
        //---------------------------------------------------------------------
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                $Entity$Dto dto = _service$Entity$.PrepareFormView();
                return ReturnStandardHtml("~/Views/$Entity$/Create.cshtml", dto);
            }
            catch (Exception ex)
            {
                return ReturnError(ex);
            }
        }
        //---------------------------------------------------------------------
        #endregion

        #region ----------------Create[POST]----------------

        //---------------------------------------------
        // Create $Entity$ Action[POST]
        // POST: /News/Create
        //---------------------------------------------
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create($Entity$Dto dto)
        {
            //Validate ModelStatec
            if (!ModelState.IsValid)
            {
                return ReturnModelError(this.ModelState);
            }

            try
            {
                //use service to add a new item
                _service$Entity$.Add$Entity$(dto);
                return ReturnSuccess("Added successfuly");
            }
            catch (Exception ex)
            {
                return ReturnError(ex);
            }

        }
        //---------------------------------------------
        #endregion

        #region --------------Edit[HttpGet]--------------
        //---------------------------------------------------------------------
        //$Entity$ Edit
        //---------------------------------------------------------------------
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                $Entity$Dto dto = _service$Entity$.PrepareFormView(id);
                return ReturnStandardHtml("~/Views/$Entity$/Edit.cshtml", dto);
            }
            catch (Exception ex)
            {
                return ReturnError(ex);
            }
        }
        //---------------------------------------------------------------------
        #endregion

        #region ----------------Edit[POST]----------------

        //---------------------------------------------
        // Edit $Entity$ Action[POST]
        // POST: /$Entity$/Edit
        //---------------------------------------------
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit($Entity$Dto dto)
        {
            //Validate ModelStatec
            if (!ModelState.IsValid)
            {
                return ReturnModelError(this.ModelState);
            }

            try
            {
                //use service to update a new item
                _service$Entity$.Update$Entity$(dto);
                // return operation result
                return ReturnSuccess("Updated successfuly");
            }
            catch (Exception ex)
            {
                return ReturnError(ex);
            }

        }
        //---------------------------------------------
        #endregion

        #region --------------Index--------------
        //---------------------------------------------------------------------
        //Index
        //---------------------------------------------------------------------
        [HttpGet]
        public ActionResult Index(string data)
        {
            try
            {
                $Entity$ListDto vm = _service$Entity$.GetIndexViewData(data);
                return ReturnStandardHtml("~/Views/$Entity$/Index.cshtml", vm);
            }
            catch (Exception ex)
            {
                return ReturnError(ex);
            }
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------GetList--------------
        //---------------------------------------------------------------------
        //GetList
        //---------------------------------------------------------------------
        [HttpGet]
        public ActionResult GetList(string data)
        {
            try
            {
                $Entity$ListDto vm = _service$Entity$.Get$Entity$PagedList(data);
                return ReturnStandardHtml("~/Views/$Entity$/Indexgrid.cshtml", vm);
            }
            catch (Exception ex)
            {
                return ReturnError(ex);
            }
        }
        //---------------------------------------------------------------------
        #endregion

        #region ----------------Thumbnail----------------
        //---------------------------------------------------------------------
        //Thumbnail Action
        //Get: /News/Thumbnail/5
        //---------------------------------------------------------------------
        public ActionResult Thumbnail(int id, string image)
        {
            try
            {
                //string filePath = string.Format("/App_Files/$Entity$/{0}/Photo.jpg", idm);
                string filePath = "/App_Files/$Entity$/$Entity$Image_4d8cd0da-163b-44d3-b859-54475b52d566.jpg";
                byte[] imageContent = ThumbnailsBuilder.GetThumbnail(filePath, 200, 200);
                return File(imageContent, "image/jpeg");
            }
            catch (Exception ex)
            {
                return ReturnError(ex);
            }
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //---------------------------------------------------------------------
        //Delete
        //---------------------------------------------------------------------
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _service$Entity$.Delete$Entity$(id);
                return ReturnSuccess("deleted successfuly");
            }
            catch (Exception ex)
            {
                return ReturnError(ex);
            }
        }
        //---------------------------------------------------------------------
        #endregion

    }
}