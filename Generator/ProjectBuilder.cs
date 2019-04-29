using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using  SMOSampleInConsol.Views;
namespace SMOSampleInConsol
{
    class ProjectBuilder
    {
        #region --------------db--------------
        //------------------------------------------------------------------------------------------------------
        //dbOwner
        //--------------------------------------------------------------------
        private static CustomDatabase _db = null;
        public static CustomDatabase db
        {
            get { return _db; }
            set {
                _db = value;
                _NameSpace = _db.ProgramatlyName;
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        static string _NameSpace = "";
        static string _ProjectFolder = "";
        public static string NameSpace { get { return _NameSpace; } }
        public static string ProjectFolder { get { return _ProjectFolder; } }
        public static void Build(Database _dbObject)
        {

            //------------------------------------------------------
            _NameSpace = Globals.GetProgramatlyName(_dbObject.Name);
            //------------------------------------------------------
            _ProjectFolder = AppDomain.CurrentDomain.BaseDirectory + _NameSpace;
            if (Directory.Exists(_ProjectFolder))
            {
                Directory.Delete(_ProjectFolder,true);
            }
            Directory.CreateDirectory(_ProjectFolder);
            Directory.CreateDirectory(_ProjectFolder + @"\db\");
            Directory.CreateDirectory(_ProjectFolder + @"\Controllers\");
            Directory.CreateDirectory(_ProjectFolder + @"\Models\");
            Directory.CreateDirectory(_ProjectFolder + @"\Services\");
            Directory.CreateDirectory(_ProjectFolder + @"\Repositories\");
            Directory.CreateDirectory(_ProjectFolder + @"\Views\");
            //------------------------------------------------------
            db = new CustomDatabase(_dbObject);

            foreach (CustomTable table in db.Tables)
            {
                DtoBuilder.Create(table);//first one because it reads additional meta data and set them into custom table object
                ModelBuilder.Create(table);
                StoredProcedureBuilder.Create(table);
                Directory.CreateDirectory(_ProjectFolder + @"\Models\" + table.ProgramatlyName);
                RepositoryBuilder.Create(table);
                ApplicationServiceBuilder.Create(table);
                ControllerBuilder.Create(table);
                Directory.CreateDirectory(_ProjectFolder + @"\Views\" + table.ProgramatlyName);
                IndexViewBuilder.Create(table);
                DialogBoxViewBuilder.Create(table);
            }
            Directory.CreateDirectory(_ProjectFolder + @"\Views\Shared\");
            Directory.CreateDirectory(_ProjectFolder + @"\Views\Home\");
            NavigationLinksBuilder.Create(db);
        }
    }
}
