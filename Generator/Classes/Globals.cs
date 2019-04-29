using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMOSampleInConsol.IO;

namespace SMOSampleInConsol
{
    class TableFileBuilder
    {
        string templatePath = "";

        #region --------------CurrentTable--------------
        //------------------------------------------------------------------------------------------------------
        //CurrentTable
        //--------------------------------------------------------------------
        private CustomTable _CurrentTable;
        /// <summary>
        /// Gets or sets entity CurrentTable. 
        /// </summary>
        public CustomTable CurrentTable
        {
            get { return _CurrentTable; }
            set { _CurrentTable=value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
        public TableFileBuilder()
        {
        }
        /*public TableFileBuilder(CustomTable t)
        {
            _CurrentTable = t;
            init();
        }*/
        public void init()
        {
           
        }

        public static void Create(CustomTable t)
        {

        }
        public string Prepare(string temp)
        {
            StringBuilder codes = new StringBuilder(temp);
            codes.Replace(ResourcesParameters.NameSpace, ProjectBuilder.NameSpace);
            codes.Replace(ResourcesParameters.TableName, CurrentTable.ProgramatlyName);
            codes.Replace(ResourcesParameters.ModelID, CurrentTable.ModelID);
            codes.Replace(ResourcesParameters.ModelIdDotNetType, CurrentTable.ModelIdDotNetType);
            codes.Replace(ResourcesParameters.ModelIDNullValue, CurrentTable.ModelIDNullValue);

            codes.Replace(ResourcesParameters.FirstStringColumnName, CurrentTable.FirstStringColumnName);

            codes.Replace("$NameSpace$", ProjectBuilder.NameSpace);
            codes.Replace("$Entity$", CurrentTable.ProgramatlyName);
            codes.Replace("$Entity_smallCase$", CurrentTable.SmallSinglerCase);
            codes.Replace("$Entity_PluralCase$", CurrentTable.PluralCase);
            //parameters.Replace(ResourcesParameters.FlexiGridWidthParameter, ResourcesParameters.FlexiGridWidth.ToString());
            return codes.ToString();
        }

        public string CreateMethod(string methodName, string code, string returnType="void",string parameteres="")
        {
            string method = $@"
            #region ----------------{methodName}----------------
            //---------------------------------------------
            //{methodName}
            //---------------------------------------------
            public {returnType} {methodName}({parameteres})
            {{
                {code}
            }}
            //---------------------------------------------
            #endregion
            ";
            return method;
        }




    }
}
