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
    class IndexViewBuilder : TableFileBuilder
    {
        

        StringBuilder javaScriptObjectParameters = new StringBuilder();
        StringBuilder javaScriptFormParameters = new StringBuilder();
        StringBuilder jsFormLoadObjectFunctionParams = new StringBuilder();
        StringBuilder jsFormClearFunctionParams = new StringBuilder();
        StringBuilder jsLoadControlsFunctionParams = new StringBuilder();
        
        public IndexViewBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }
        
        private void init()
        {

            string javaScriptObjectPattern = "\n\t\t\t{0}: null";
            string javaScriptFormPattern = "\n\t\t\t{0}: $(\"#{0}\"),";

            string jsFormLoadObjectPattern = "\n\t\t\t\t{0}.{1} = this.{1}.val();";
            string jsFormLoadObjectPatternBoolean = "\n\t\t\t\t{0}.{1} = this.{1}.is(\":checked\");";

            string jsFormClearFunctionParamsPattern = "\n\t\t\t\tthis.{0}.val(\"\");";
            //string jsFormClearFunctionParamsPatternIntegerID = "\n\t\t\t\tthis.{0}.val(\"0\");";
            string jsFormClearFunctionParamsPatternBoolean = "\n\t\t\t\tthis.{0}.prop('checked', false);";

            string jsLoadControlsFunctionParamsPattern = "\n\t\t\t\t\tthis.{0}.val(data.{0});";
            string jsLoadControlsFunctionParamsPatternDate = "\n\t\t\t\t\tthis.{0}.val(FormController.FormatJsonDate(data.{0}));";
            string jsLoadControlsFunctionParamsPatternBoolean = "\n\t\t\t\t\tthis.{0}.prop('checked', data.{0});";
            
            int i = -1;
            foreach (CustomColumn c in CurrentTable.Columns)
            {
                //javaScriptObjectParameters
                javaScriptObjectParameters.AppendFormat(javaScriptObjectPattern, new string[] { c.ProgramatlyName });
                if (++i < CurrentTable.Columns.Count - 1)
                    javaScriptObjectParameters.Append(",");
                //javaScriptFormParameters
                javaScriptFormParameters.AppendFormat(javaScriptFormPattern, new string[] { c.ProgramatlyName });
                //jsFormLoadObject
                if (c.DotNetType != typeof(Boolean))
                {
                    //jsFormLoadObjectFunctionParams.AppendFormat(jsFormLoadObjectPattern, new string[] { CurrentTable.JsObject, c.ProgramatlyName });
                }
                else
                {
                   // jsFormLoadObjectFunctionParams.AppendFormat(jsFormLoadObjectPatternBoolean, new string[] { CurrentTable.JsObject, c.ProgramatlyName });
                }
                //jsFormClearFunctionParams
                if (c.DotNetType != typeof(Boolean))
                {
                    jsFormClearFunctionParams.AppendFormat(jsFormClearFunctionParamsPattern, new string[] { c.ProgramatlyName });
                }
                else
                {
                    jsFormClearFunctionParams.AppendFormat(jsFormClearFunctionParamsPatternBoolean, new string[] { c.ProgramatlyName });
                }
                //jsLoadControlsFunctionParams
                if (c.DotNetType == typeof(Boolean))
                {
                    jsLoadControlsFunctionParams.AppendFormat(jsLoadControlsFunctionParamsPatternBoolean, new string[] { c.ProgramatlyName });
                }
                else if (c.DotNetType == typeof(DateTime))
                {
                    jsLoadControlsFunctionParams.AppendFormat(jsLoadControlsFunctionParamsPatternDate, new string[] { c.ProgramatlyName });
                }
                else
                {
                    jsLoadControlsFunctionParams.AppendFormat(jsLoadControlsFunctionParamsPattern, new string[] { c.ProgramatlyName });
                }
            }
            
        }

        public static void Create(CustomTable t)
        {
            IndexViewBuilder ourGeneratedFile = new IndexViewBuilder(t);

            string templatefile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Views/Index.cshtml");

            string genratedCode = ourGeneratedFile.Prepare(templatefile);

            FileManager.SaveFile(".cshtml", ourGeneratedFile.CurrentTable.PathOfIndexView, genratedCode);

        }
        public new string Prepare(string temp )
        {
            StringBuilder codes = new StringBuilder(base.Prepare(temp));
            codes.Replace(ResourcesParameters.javaScriptObjectParameters, javaScriptObjectParameters.ToString());
            codes.Replace(ResourcesParameters.javaScriptFormParameters, javaScriptFormParameters.ToString());
            codes.Replace(ResourcesParameters.jsFormLoadObjectFunctionParams, jsFormLoadObjectFunctionParams.ToString());
            codes.Replace(ResourcesParameters.jsFormClearFunctionParams, jsFormClearFunctionParams.ToString());
            codes.Replace(ResourcesParameters.jsLoadControlsFunctionParams, jsLoadControlsFunctionParams.ToString());
            return codes.ToString();
        }
        
         
    }
    
}
