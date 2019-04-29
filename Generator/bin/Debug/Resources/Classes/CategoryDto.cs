using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using $NameSpace$.Models.Core;
using $NameSpace$.Models.Utilities;
using $NameSpace$.Models.Utilities.Extensions;
using $NameSpace$.Models.ViewModels;

namespace $NameSpace$.Models.Dto
{
    public  class $Entity$Dto
    {

        public int $Entity$ID { get; set; }

        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Please enter the Arabic name.")]
        [StringLength(128, ErrorMessage = "The Arabic name must be less than {1} characters.")]
        [Display(Name = "Arabic name:")]
        public string NameAr { get; set; }

        public SelectList Deparments { get; set; }

        [Required(ErrorMessage = "Please enter the  English name.")]
        [StringLength(128, ErrorMessage = "The  English name must be less than {1} characters.")]
        [Display(Name = " English name:")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "Please enter the Identifier.")]
        [StringLength(128, ErrorMessage = "The Identifier must be less than {1} characters.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Display(Name = "Identifier")]
        public string Identifier { get; set; }

     
        public string $Entity$Image { get; set; }

        public int ViewsCounter { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Please enter the ClassName.")]
        [StringLength(128, ErrorMessage = "The ClassName must be less than {1} characters.")]
        [Display(Name = "ClassName")]
        public string ClassName { get; set; }
        //[StringLength(AbpUserBase.MaxUserNameLength)]

        public DateTime CreationDate { get; set; }
        //---------------------------------
        //PhotoPostedFile
        //---------------------------------3
        //[FileExtensions(Extensions = (".png", ".jpg", ".jpeg", and ".gif")",.jpg,.jpeg,.png,.gif", ErrorMessage = "error Extensions")]
        [FileType("jpg,jpeg,png,gif")]
        [AllowFileSize(FileSize = 5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 MB")]
        public HttpPostedFileBase $Entity$ImagePostedFile { get; set; }
        public UploadedFile $Entity$ImageUploadedFile { get; set; }
        //---------------------------------

        #region ------PhotoUrl------
        //---------------------------------
        //PhotoUrl
        //---------------------------------
        private string _PhotoUrl;
        public string PhotoUrl
        {
            get
            {
                if (_PhotoUrl == null && !string.IsNullOrEmpty(this.$Entity$Image))
                {
                    _PhotoUrl = string.Format("{0}{1}/{2}", SiteSettings.$Entity$UploadVirtualPath, this.$Entity$ID, this.$Entity$Image);
                }
                return _PhotoUrl;
            }

        }
        //---------------------------------
        #endregion


        #region ------ThumUrl------
        //---------------------------------
        //ThumUrl
        //---------------------------------
        public string ThumUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.$Entity$Image))
                {
                    return "/$Entity$/Thumbnail/" + this.$Entity$ID;
                }
                else
                    return "/Content/Limitless_2.0.1/Bootstrap 4/Template/global_assets/images/placeholders/placeholder.jpg";
            }

        }
        //---------------------------------
        #endregion


    }

    public class $Entity$ListFilter : PiginationData
    {
        public int DepartmentID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

    }

}