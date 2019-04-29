using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMOSampleInConsol.IO;
using System.IO;
namespace SMOSampleInConsol.Views
{
    class DialogBoxViewBuilder : TableFileBuilder
    {


        StringBuilder htmlTags = new StringBuilder();
        StringBuilder AdditionsScripts = new StringBuilder();

        public DialogBoxViewBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }
        
        private void init()
        {

            string inputPatern = "\n\t\t<div class=\"editor-label\">\n\t\t\t@Html.LabelFor(model => model.{0})\n\t\t</div>\n\t\t<div class=\"editor-field\">\n\t\t\t@Html.EditorFor(model => model.{0})\n\t\t\t@Html.ValidationMessageFor(model => model.{0})\n\t\t</div>";
            string primaryKeyInputPatern = "\n\t\t\t@Html.HiddenFor(model => model.{0})";
            string forignKeyInputPatern = "\n\t\t<div class=\"editor-label\">\n\t\t\t@Html.LabelFor(model => model.{0})\n\t\t</div>\n\t\t<div class=\"editor-field\">\n\t\t\t@Html.DropDownListFor(x => x.{0},  (IEnumerable<SelectListItem>)ViewData[\"{0}\"],\"Choose\")\n\t\t\t@Html.ValidationMessageFor(model => model.{0})\n\t\t</div>";

            string dateTimePicker = "\n\t\t$(\"#{0}\").val(\"\"); $(\"#{0}\").datepicker();";

            foreach (CustomColumn c in CurrentTable.Columns)
            {
                //---------------------------------------------------------------------------------------
                if (c.DotNetType == typeof(DateTime))
                {
                    AdditionsScripts.AppendFormat(dateTimePicker, new string[] { c.ProgramatlyName });
                }
                //---------------------------------------------------------------------------------------
                if (c.InPrimaryKey&&c.IsIdentity)
                {
                    htmlTags.AppendFormat(primaryKeyInputPatern, new string[] { c.ProgramatlyName });
                }
                else if (c.IsForeignKey)
                {
                    htmlTags.AppendFormat(forignKeyInputPatern, new string[] { c.ProgramatlyName });
                }
                else
                {
                    htmlTags.AppendFormat(inputPatern, new string[] { c.ProgramatlyName });
                }
            }
            
        }

        public static void Create(CustomTable t)
        {
            DialogBoxViewBuilder ourGeneratedFile = new DialogBoxViewBuilder(t);

            string templatefile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Views/DialogBox.cshtml");

            string genratedCode = ourGeneratedFile.Prepare(templatefile);

            FileManager.SaveFile(".cshtml", ourGeneratedFile.CurrentTable.PathOfDialogBoxView, genratedCode);

        }
        public new string Prepare(string temp )
        {
            StringBuilder codes = new StringBuilder(base.Prepare(temp));
            codes.Replace(ResourcesParameters.HtmlTags, htmlTags.ToString());
            if (AdditionsScripts.Length > 0)
            {
                codes.Replace(ResourcesParameters.AdditionsScripts, "<script>\n\t$(function () {" + AdditionsScripts.ToString() + "\n\t});\n</script>");

            }
            else {

                codes.Replace(ResourcesParameters.AdditionsScripts, "");
            }
            return codes.ToString();
        }
        
         
    }
    
}
