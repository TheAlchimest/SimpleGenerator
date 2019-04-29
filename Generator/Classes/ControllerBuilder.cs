using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMOSampleInConsol.IO;
using System.IO;
namespace SMOSampleInConsol
{
    class ControllerBuilder : TableFileBuilder
    {
        
        StringBuilder ForignkeysCode = new StringBuilder();

        public ControllerBuilder(CustomTable t)
        {
            CurrentTable = t;
            Init();
        }
        private void Init()
        {
        }

        public static void Create(CustomTable t)
        {
            ControllerBuilder builder = new ControllerBuilder(t);

            string templatefile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Classes/Controller.cs");

            string genratedCode = builder.Prepare(templatefile);

            FileManager.SaveFile(".cs", builder.CurrentTable.PathOfControllerFile, genratedCode);

        }
        public new string Prepare(string temp )
        {
            StringBuilder codes = new StringBuilder(base.Prepare(temp));
            return codes.ToString();
        }
        
         
    }
    
}
