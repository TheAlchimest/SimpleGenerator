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
    class NavigationLinksBuilder //: TableFileBuilder
    {
        public CustomDatabase CurrentDB { get; set; }

        StringBuilder MenuLinks = new StringBuilder();
        StringBuilder HomeLinks = new StringBuilder();

        public NavigationLinksBuilder(CustomDatabase db)
        {
            CurrentDB = db;
            init();
        }
        
        private void init()
        {

            string menuLinkPattern = "\n\t\t\t\t\t\t\t\t<li>@Html.ActionLink(\"{0}\", \"Index\", \"{0}\")</li>";
            string homeLinkPattern = "\n\t\t<li>@Html.ActionLink(\"{0}\", \"Index\", \"{0}\")</li>";
            foreach (CustomTable t in CurrentDB.Tables)
            {
                MenuLinks.AppendFormat(menuLinkPattern, new string[] { t.ProgramatlyName });
                HomeLinks.AppendFormat(homeLinkPattern, new string[] { t.ProgramatlyName });
            }
        }

        public static void Create(CustomDatabase db)
        {
            NavigationLinksBuilder ourGeneratedFile = new NavigationLinksBuilder(db);

            string _LayoutTemplatefile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Views/_Layout.cshtml");
            string _HomeIndexTemplatefile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Views/Home.cshtml");

            string genratedLayout = ourGeneratedFile.Prepare(_LayoutTemplatefile);
            FileManager.SaveFile(".cshtml", ProjectBuilder.ProjectFolder + @"\Views\Shared\_Layout", genratedLayout);
            string genratedHomeIndex = ourGeneratedFile.Prepare(_HomeIndexTemplatefile);
            FileManager.SaveFile(".cshtml", ProjectBuilder.ProjectFolder + @"\Views\Home\Index", genratedHomeIndex);

        }
        public new string Prepare(string temp )
        {
            StringBuilder codes = new StringBuilder(temp);
            codes.Replace(ResourcesParameters.MenuLinks, MenuLinks.ToString());
            codes.Replace(ResourcesParameters.HomeLinks, HomeLinks.ToString());
            return codes.ToString();
        }
        
         
    }
    
}
