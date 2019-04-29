using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace SMOSampleInConsol
{
    class CustomDatabase
    {
        #region --------------NameInDatabase--------------
        /// <summary>
        /// Gets entity NameInDatabase. 
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
        /// Gets entity ProgramatlyName. 
        /// </summary>
        public string ProgramatlyName
        {
            get { return _ProgramatlyName; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------dbObject--------------
        private Database _dbObject;
        /// <summary>
        /// Gets entity dbObject. 
        /// </summary>
        public Database dbObject
        {
            get { return _dbObject; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Tables--------------
        private List<CustomTable> _Tables;
        public List<CustomTable> Tables
        {
            get { return _Tables; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion


        public CustomDatabase(Database db)
        {
            _dbObject = db;
            _ProgramatlyName = Globals.GetProgramatlyName(NameInDatabase);
            //ProjectBuilder.Init(_ProgramatlyName);
            _Tables = new List<CustomTable>();
            CustomTable table = null;
            //string requiredTables = @"LK_BudgetCategories ";
            
            foreach (Table t in _dbObject.Tables)
            {
                //if (!requiredTables.Contains(t.Name))
                //    continue;
                if (!t.IsSystemObject)
                {
                    table = new CustomTable(t);
                    Tables.Add(table);
                }
            }
            //read relation
            foreach (CustomTable ct in this.Tables)
            {
                foreach (CustomColumn c in ct.ForeignKeys)
                {
                    GetRelatedTable(ct.dbObject.ForeignKeys, c);
                }
            }
        }

        public void GetRelatedTable(ForeignKeyCollection fkc, CustomColumn clmn)
        {
            foreach (ForeignKey relation in fkc)
            {
                foreach (ForeignKeyColumn c in relation.Columns)
                {
                    if (c.Name == clmn.dbObject.Name)
                    {
                        clmn.ReferanceTable= GetTable(relation.ReferencedTable);
                    }
                }
            }
        }

        public CustomTable GetTable(string nameInDb)
        {
            foreach (CustomTable t in Tables)
            {
                if (t.NameInDatabase == nameInDb)
                    return t;
            }
            return null;
        }

		
    }
}
