using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Configuration;

namespace SMOSampleInConsol
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sql Authentication
            //ServerConnection conn = new ServerConnection(".", "sa", "banana");
            //Windows Authentication
            string DatabaseServer = ConfigurationSettings.AppSettings["DatabaseServer"];
            string DatabaseUser = ConfigurationSettings.AppSettings["DatabaseUser"];
            string DatabasePasswoed = ConfigurationSettings.AppSettings["DatabasePasswoed"];
            ServerConnection conn = new ServerConnection(DatabaseServer, DatabaseUser, DatabasePasswoed);
            Database db =null;
            List<CustomTable> dbCustomTables= new List<CustomTable>();
            //try
            //{
                Server srv = new Server(conn);
                Console.WriteLine("Server: " + srv.Name);
                //Console.WriteLine("Edition: " + srv.Information.Edition);
                foreach (Database tempdb in srv.Databases)
                {
                    Console.WriteLine(tempdb.Name);
                }
                while (db==null)
                {
                    Console.Write("Enter db name you want to generate its' code: ");
                    string dbName = Console.ReadLine();
                    db = srv.Databases[dbName];
               /* foreach (StoredProcedure sp in db.StoredProcedures)
                {
                    Console.WriteLine("Name:{0}",sp.Name);
                    foreach (StoredProcedureParameter p in sp.Parameters)
                    {
                        Console.WriteLine("Name:{0}", p.Name);
                        Console.WriteLine("DataType:{0}", p.DataType); 
                        Console.WriteLine("IsOutputParameter:{0}", p.IsOutputParameter); 
                        Console.WriteLine("IsReadOnly:{0}", p.IsReadOnly); 
                        Console.WriteLine("IsCursorParameter:{0}", p.IsCursorParameter); 
                        Console.WriteLine("ID:{0}", p.ID); 
                        Console.WriteLine("DefaultValue:{0}", p.DefaultValue);
                        Console.WriteLine("----------------------------------------------");
                        //sp.ExecuteWithModes(SqlExecutionModes.;
                    }
                    // 
                    Console.WriteLine("Enter for Next");
                    Console.ReadLine();
                    Console.WriteLine("Name:{0}", sp.Parameters);
                    
                }
                */
                    if (db == null)
                    {
                        Console.WriteLine("invalid db name ");
                    }
                }

            //ProjectBuilder.Init(
                ProjectBuilder.Build(db);
                
               //CreateDatabase(srv, "testikoSMO", "C:\\");
               // Database dbIndexers = srv.Databases[5];
               //Table indexers =dbIndexers.Tables["Indexers"];
               //ColumnCollection cl = indexers.Columns;
               //Database dbIndexers = (new System.Linq.SystemCore_EnumerableDebugView(srv.Databases[5].Tables)).Items[0]
               //BackupDatabase(srv, "Indexers", @"C:\Test\Indexers.bak");
               /*
                foreach (Database db in srv.Databases)
                {
                    Console.WriteLine(db.Name);
                    foreach (FileGroup fg in db.FileGroups)
                    {
                        Console.WriteLine("   " + fg.Name);
                        foreach (DataFile df in fg.Files)
                        {
                            Console.WriteLine("      " + df.Name + " " + df.FileName);
                        }
                    }
                    foreach (Property prop in db.Properties)
                    {
                        Console.WriteLine(prop.Name + " " + prop.Value);
                    }
                }
               */
                conn.Disconnect();
            //}
            //catch (Exception err)
            //{
            //    Console.WriteLine(err.Message);
            //}
        }

        

        #region /*--------- CreateDatabase ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static void CreateDatabase(Server srv, string name, string filesPath)
        {
            string mdfPath = filesPath + name + @".mdf";
            string ldfPath = filesPath + name + @".ldf";
            Database database = new Database(srv, name);
            //database.FileGroups.Add(new FileGroup(database, "PRIMARY"));
            //DataFile dtPrimary = new DataFile(database.FileGroups["PRIMARY"], "PriValue", mdfPath);
            //dtPrimary.Size = 77.0 * 1024.0;
            //dtPrimary.GrowthType = FileGrowthType.KB;
            //dtPrimary.Growth = 1.0 * 1024.0;
            //database.FileGroups["PRIMARY"].Files.Add(dtPrimary);

            //LogFile logFile = new LogFile(database, "Log", ldfPath);
            //logFile.Size = 7.0 * 1024.0;
            //logFile.GrowthType = FileGrowthType.Percent;
            //logFile.Growth = 10.0;

            //database.LogFiles.Add(logFile);
            database.Create();
            database.Refresh();
        }
        //------------------------------------------------------------------------------------------------------
        #endregion


        #region /*--------- BackupDatabase ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static void BackupDatabase(Server srv,string databaseName,string backupPath)
        {
            Database database = srv.Databases[databaseName];
            Backup backup = new Backup();
            backup.Action = BackupActionType.Database;
            backup.Database = database.Name;
            backup.Devices.AddDevice(backupPath, DeviceType.File);
            backup.PercentCompleteNotification = 10;
            backup.PercentComplete += new PercentCompleteEventHandler(ProgressEventHandler);
            backup.SqlBackup(srv);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        static void ProgressEventHandler(object sender, PercentCompleteEventArgs e)
        {
            Console.WriteLine(e.Percent);
        }
    }
}
