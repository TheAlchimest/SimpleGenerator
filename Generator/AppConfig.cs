using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMOSampleInConsol
{
    class AppConfig
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        //Instance
        //--------------------------------------------------------------------
        private static AppConfig _Instance;
        /// <summary>
        /// Gets or sets entity Instance. 
        /// </summary>
        public static AppConfig Instance
        {
            get {
                if (_Instance == null)
                {
                    _Instance = new AppConfig();
                }
                return _Instance; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
		
        #region --------------dbOwner--------------
        //------------------------------------------------------------------------------------------------------
        //dbOwner
        //--------------------------------------------------------------------
        private string _dbOwner="dbo";
        /// <summary>
        /// Gets or sets entity dbOwner. 
        /// </summary>
        public string dbOwner
        {
            get { return _dbOwner; }
            set { _dbOwner = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
        /*
        
		*/
    }
}
