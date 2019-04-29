using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace SMOSampleInConsol
{
    class CustomTable_Old
    {
        string _SqlDataPrviderClass = "{0}SqlDataPrvider";
        string _ModelClass = "{0}Model";
        string _FactoryClass = "{0}Factor";
        string _ControllerClass = "{0}Controller";
        string _ModelObject = "{0}Obj";
        string _ModelList = "{0}List";
        string _DialogBox = "{0}Dialog";
        string _DialogBoxTitle = "Add / Edit {0}";
        string _DialogBoxFormID = "{0}NewsForm";

        string _ProcedureInsert = "[dbo].[{0}_Create]";
        string _ProcedureUpdate = "[dbo].[{0}_Update]";
        string _ProcedureDelete = "[dbo].[{0}_Delete]";
        string _ProcedureGetPageByPage = "[dbo].[{0}_GetPageByPage]";
        string _ProcedureGet = "[dbo].[{0}_Get]";

        string _MethodSave = "Save";
        string _MethodCreate = "Create";
        string _MethodUpdat = "Update";
        string _MethodDelete = "Delete";
        string _DeleteGroupofObjects = "DeleteGroupofObjects";
        string _MethodGetPageByPage = "GetPageByPage";
        string _MethodGet = "Get";
        string _MethodGetObject = "GetObject";
        string _MethodCreateFlexiJson = "CreateFlexiJson";
        
        string _FlixyGridID = "{0}List";

        string _ModelID ="";
        string _ModelIdDotNetType = "";
        string _ModelIDNullValue = "null";

        private int primaryKeyIndex=-1;
        private int firstStringColumnIndex = -1;
        private string _FirstStringColumnName = "";

        string _JsObject = "{0}Post";
        string _JsForm = "{0}Form";

        string _PathOfStoredProcerduresFile = ProjectBuilder.ProjectFolder + @"\db\{0}Proc";
        string _PathOfControllerClass = ProjectBuilder.ProjectFolder + @"\Controllers\{0}";

        string _PathOfSqlDataPrviderClass = ProjectBuilder.ProjectFolder + @"\Models\{0}\{1}";
        string _PathOfFactoryClass = ProjectBuilder.ProjectFolder + @"\Models\{0}\{1}";
        string _PathOfModelClass = ProjectBuilder.ProjectFolder + @"\Models\{0}\{1}";

        string _PathOfIndexView = ProjectBuilder.ProjectFolder + @"\Views\{0}\Index";
        string _PathOfDialogBoxView = ProjectBuilder.ProjectFolder + @"\Views\{0}\{0}Dialog";

        #region --------------NameInDatabase--------------
        /// <summary>
        /// Gets model NameInDatabase. 
        /// </summary>
        public string NameInDatabase
        {
            get { return _dbObject.Name; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------ProgramatlyName--------------
        private string _ProgramatlyName;
        /// <summary>
        /// Gets model ProgramatlyName. 
        /// </summary>
        public string ProgramatlyName
        {
            get { return _ProgramatlyName; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------dbObject--------------
        private Table _dbObject;
        /// <summary>
        /// Gets model dbObject. 
        /// </summary>
        public Table dbObject
        {
            get { return _dbObject; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Columns--------------
        private List<CustomColumn> _Columns;
        /// <summary>
        /// Gets or sets model Columns. 
        /// </summary>
        public List<CustomColumn> Columns
        {
            get { return _Columns; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------ID--------------
        //------------------------------------------------------------------------------------------------------
        //ID
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets table ID. 
        /// </summary>
        public CustomColumn ID
        {
            get {
                if (primaryKeyIndex >= 0)
                    return Columns[primaryKeyIndex];
                else 
                    return null;
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------FirstStringColumn--------------
        //------------------------------------------------------------------------------------------------------
        //ID
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets table FirstStringColumn. 
        /// </summary>
        public CustomColumn FirstStringColumn
        {
            get
            {
                if (firstStringColumnIndex >= 0)
                    return Columns[firstStringColumnIndex];
                else
                    return null;
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
        #region --------------FirstStringColumnName--------------
        //------------------------------------------------------------------------------------------------------
        //ID
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets table FirstStringColumnName. 
        /// </summary>
        public string FirstStringColumnName
        {
            get
            {
                return _FirstStringColumnName;
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
        #region TextsParameters
        public string SqlDataPrviderClass { get { return _SqlDataPrviderClass; } }
        public string ModelClass { get { return _ModelClass; } }
        public string FactoryClass { get { return _FactoryClass; } }
        public string ControllerClass { get { return _ControllerClass; } }
        public string ModelObject { get { return _ModelObject; } }
        public string ModelList { get { return _ModelList; } }
        

        public string ModelID { get { return _ModelID; } }
        public string ModelIdDotNetType { get { return _ModelIdDotNetType; } }
        public string ModelIDNullValue { get { return _ModelIDNullValue; } }

        public string ProcedureInsert { get { return _ProcedureInsert; } }
        public string ProcedureUpdate { get { return _ProcedureUpdate; } }
        public string ProcedureDelete { get { return _ProcedureDelete; } }
        public string ProcedureGetPageByPage { get { return _ProcedureGetPageByPage; } }
        public string ProcedureGet { get { return _ProcedureGet; } }

        public string MethodSave { get { return _MethodSave; } }
        public string MethodCreate { get { return _MethodCreate; } }
        public string MethodUpdat { get { return _MethodUpdat; } }
        public string MethodDelete { get { return _MethodDelete; } }
        public string DeleteGroupofObjects { get { return _DeleteGroupofObjects; } }
        public string MethodGetPageByPage { get { return _MethodGetPageByPage; } }
        public string MethodGet { get { return _MethodGet; } }
        public string MethodGetObject { get { return _MethodGetObject; } }
        public string MethodCreateFlexiJson { get { return _MethodCreateFlexiJson; } }
        public string FlixyGridID { get { return _FlixyGridID; } }

        public string DialogBox { get { return _DialogBox; } }
        public string DialogBoxTitle { get { return _DialogBoxTitle; } }
        public string DialogBoxFormID { get { return _DialogBoxFormID; } }
        public string JsObject { get { return _JsObject; } }
        public string JsForm { get { return _JsForm; } }

        public string PathOfStoredProcerduresFile { get { return _PathOfStoredProcerduresFile; } }

        public string PathOfSqlDataPrviderClass { get { return _PathOfSqlDataPrviderClass; } }
        public string PathOfFactoryClass { get { return _PathOfFactoryClass; } }
        public string PathOfModelClass { get { return _PathOfModelClass; } }

        public string PathOfControllerClass { get { return _PathOfControllerClass; } }
        public string PathOfIndexView { get { return _PathOfIndexView; } }
        public string PathOfDialogBoxView { get { return _PathOfDialogBoxView; } }

        #endregion

        public CustomTable_Old(Table table)
        {
            _dbObject = table;
            _ProgramatlyName = Globals.GetProgramatlyName(NameInDatabase);
            _Columns=new List<CustomColumn>();
            int index = 0;
            CustomColumn temp=null;
            foreach (Column c in _dbObject.Columns)
            {
                temp=new CustomColumn(c, NameInDatabase, _ProgramatlyName);
                _Columns.Add(temp);
                if (c.InPrimaryKey)
                {
                    primaryKeyIndex = index;
                }
                if (firstStringColumnIndex < 0 && temp.DotNetType == typeof(string))
                {
                    firstStringColumnIndex = index;
                    _FirstStringColumnName = temp.ProgramatlyName;
                }
                index++;
            }
            if (firstStringColumnIndex < 0)
            {
                firstStringColumnIndex = 0;
                Column c = _dbObject.Columns[0];
                temp = new CustomColumn(c, NameInDatabase, _ProgramatlyName);
                _FirstStringColumnName = temp.ProgramatlyName;
            }
            _SqlDataPrviderClass = string.Format(_SqlDataPrviderClass, _ProgramatlyName);
            _ModelClass = string.Format(_ModelClass, _ProgramatlyName);
            _FactoryClass = string.Format(_FactoryClass, _ProgramatlyName);
            _ControllerClass = string.Format(_ControllerClass, _ProgramatlyName);
            _ModelObject = string.Format(_ModelObject, _ProgramatlyName);
            _ModelList = string.Format(_ModelList, _ProgramatlyName);
            
            _ProcedureInsert = string.Format(_ProcedureInsert, _ProgramatlyName);
            _ProcedureUpdate = string.Format(_ProcedureUpdate, _ProgramatlyName);
            _ProcedureDelete = string.Format(_ProcedureDelete, _ProgramatlyName);
            _ProcedureGetPageByPage = string.Format(_ProcedureGetPageByPage, _ProgramatlyName);
            _ProcedureGet = string.Format(_ProcedureGet, _ProgramatlyName);

            _FlixyGridID = string.Format(_FlixyGridID, _ProgramatlyName);
            if (ID != null)
            {
                _ModelID = ID.ProgramatlyName;
                _ModelIdDotNetType = ID.DotNetTypeAlias;

                if (ID.DotNetType == typeof(int) ||
                        ID.DotNetType == typeof(short) ||
                        ID.DotNetType == typeof(long))
                {
                    _ModelIDNullValue = "-1";
                }
            }

            _DialogBox = string.Format(_DialogBox, _ProgramatlyName);
            _DialogBoxTitle = string.Format(_DialogBoxTitle, _ProgramatlyName);
            _DialogBoxFormID = string.Format(_DialogBoxFormID, _ProgramatlyName);

            _JsObject = string.Format(_JsObject, _ProgramatlyName);
            _JsForm = string.Format(_JsForm, _ProgramatlyName);

            _PathOfModelClass = string.Format(_PathOfModelClass, _ProgramatlyName, _ModelClass);
            _PathOfFactoryClass = string.Format(_PathOfFactoryClass, _ProgramatlyName, _FactoryClass);
            _PathOfSqlDataPrviderClass = string.Format(_PathOfSqlDataPrviderClass, _ProgramatlyName, _SqlDataPrviderClass);

            _PathOfControllerClass = string.Format(_PathOfControllerClass, _ControllerClass);
            _PathOfStoredProcerduresFile = string.Format(_PathOfStoredProcerduresFile, _ProgramatlyName);
            _PathOfIndexView = string.Format(_PathOfIndexView, _ProgramatlyName);
            _PathOfDialogBoxView = string.Format(_PathOfDialogBoxView, _ProgramatlyName);
        }

        public string SqlDataPrvider { get { return _ProgramatlyName+"SqlDataPrvider"; } }

		
    }
}
