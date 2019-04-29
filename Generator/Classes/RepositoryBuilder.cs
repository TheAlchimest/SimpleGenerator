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
    class RepositoryBuilder : TableFileBuilder
    {

        public RepositoryBuilder()
        {
        }

        public RepositoryBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }

        public static void Create(CustomTable t)
        {
            RepositoryBuilder builder = new RepositoryBuilder(t);

            string repositoryFile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Classes/Repository.cs");

            string genFactoryFile = builder.Prepare(repositoryFile);

            FileManager.SaveFile(".cs", builder.CurrentTable.PathOfRepositoryFile, genFactoryFile);

        }
       
    }
    
}
