using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace SMOSampleInConsol
{
    class CustomTable
    {
        string _ModelID = "";
        string _ModelIdDotNetType = "";
        string _ModelIDNullValue = "null";

        private int primaryKeyIndex = -1;
        private int firstStringColumnIndex = -1;
        private string _FirstStringColumnName = "";

        private string _pluralCase = "";
        private string _smallSinglerCase = "";
        public string PluralCase { get { return _pluralCase; } }
        public string SmallSinglerCase { get { return _smallSinglerCase; } }


        string _PathOfStoredProcerduresFile = ProjectBuilder.ProjectFolder + @"\db\{0}Proc";
        string _PathOfControllerFile = ProjectBuilder.ProjectFolder + @"\Controllers\{0}Controller";
        string _PathOfApplicatioServiceFile = ProjectBuilder.ProjectFolder + @"\Services\{0}Service";
        string _PathOfRepositoryFile = ProjectBuilder.ProjectFolder + @"\Repositories\{0}Repository";


        string _PathOfModelClass = ProjectBuilder.ProjectFolder + @"\Models\{0}Model";
        string _PathOfDtoClass = ProjectBuilder.ProjectFolder + @"\Models\{0}Dto";

        string _PathOfIndexView = ProjectBuilder.ProjectFolder + @"\Views\{0}\Index";
        string _PathOfDialogBoxView = ProjectBuilder.ProjectFolder + @"\Views\{0}\{0}Dialog";
        public string PathOfStoredProcerduresFile { get { return _PathOfStoredProcerduresFile; } }

        public string PathOfRepositoryFile { get { return _PathOfRepositoryFile; } }
        public string PathOfApplicatioServiceFile { get { return _PathOfApplicatioServiceFile; } }
        public string PathOfControllerFile { get { return _PathOfControllerFile; } }

        public string PathOfModelClass { get { return _PathOfModelClass; } }//
        public string PathOfDtoClass { get { return _PathOfDtoClass; } }//

        public string PathOfIndexView { get { return _PathOfIndexView; } }
        public string PathOfDialogBoxView { get { return _PathOfDialogBoxView; } }


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


        public bool HasFile { get; set; }
        public bool HasPhoto { get; set; }

        public List<CustomColumn> FilesColumns { get; set; } = new List<CustomColumn>();
        public List<CustomColumn> PhotosColumns { get; set; } = new List<CustomColumn>();
        public List<CustomColumn> AllFilesColumns { get; set; } = new List<CustomColumn>();
        public List<CustomColumn> ForeignKeys { get; set; } = new List<CustomColumn>();

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

        public string ModelID { get { return _ModelID; } }
        public string ModelIdDotNetType { get { return _ModelIdDotNetType; } }
        public string ModelIDNullValue { get { return _ModelIDNullValue; } }

        #endregion

        public CustomTable(Table table)
        {
            _dbObject = table;
            _ProgramatlyName = Globals.GetProgramatlyName(NameInDatabase);
            PluralizationService pluralizationService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
            _pluralCase = pluralizationService.Pluralize(_ProgramatlyName);
            _smallSinglerCase = Globals.GetProgramatlyCamelCase(_ProgramatlyName); 

            _Columns =new List<CustomColumn>();
            int index = 0;
            CustomColumn customColumn=null;
            foreach (Column c in _dbObject.Columns)
            {
                customColumn=new CustomColumn(c,this, NameInDatabase, _ProgramatlyName);
                _Columns.Add(customColumn);
                if (c.InPrimaryKey)
                {
                    primaryKeyIndex = index;
                }

                if (firstStringColumnIndex < 0 && customColumn.DotNetType == typeof(string))
                {
                    firstStringColumnIndex = index;
                    _FirstStringColumnName = customColumn.ProgramatlyName;
                }
                if (c.IsForeignKey)
                {
                    this.ForeignKeys.Add(customColumn);
                }
                index++;
            }
            if (firstStringColumnIndex < 0)
            {
                firstStringColumnIndex = 0;
                Column c = _dbObject.Columns[0];
                customColumn = new CustomColumn(c, this, NameInDatabase, _ProgramatlyName);
                _FirstStringColumnName = customColumn.ProgramatlyName;
            }
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
            //pathes
            _PathOfControllerFile = string.Format(_PathOfControllerFile, _ProgramatlyName);
            _PathOfApplicatioServiceFile = string.Format(_PathOfApplicatioServiceFile, _ProgramatlyName);
            _PathOfStoredProcerduresFile = string.Format(_PathOfStoredProcerduresFile, _ProgramatlyName);
            _PathOfRepositoryFile = string.Format(_PathOfRepositoryFile, _ProgramatlyName);

            _PathOfModelClass = string.Format(_PathOfModelClass, _ProgramatlyName);
            _PathOfDtoClass = string.Format(_PathOfDtoClass, _ProgramatlyName);
            
            _PathOfIndexView = string.Format(_PathOfIndexView, _ProgramatlyName);
            _PathOfDialogBoxView = string.Format(_PathOfDialogBoxView, _ProgramatlyName);
        }
    }
}
