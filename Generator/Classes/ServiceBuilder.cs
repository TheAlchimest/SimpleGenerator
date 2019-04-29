using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SMOSampleInConsol.IO;
using System.IO;
namespace SMOSampleInConsol
{
    class ApplicationServiceBuilder : TableFileBuilder
    {

        string populateFilesRegionTemplateForCreateUpdate = @"
            dto.$FileProp$UploadedFile = UploadedFileHelper.Populate$FileProp$(dto);
            dto.$FileProp$ = dto.$FileProp$UploadedFile.FileServerName; ";
        string populateFilesRegionTemplateForDelete = @"
            dto.$FileProp$UploadedFile = UploadedFileHelper.Populate$FileProp$(dto);";

        string saveFileRegionTemplate = "dto.$FileProp$UploadedFile.SaveFiles();";
    string deleteFileRegionTemplate = "dto.$FileProp$UploadedFile.DeleteFiles();";
        public ApplicationServiceBuilder()
        {
        }

        public ApplicationServiceBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }

        public static void Create(CustomTable t)
        {
            ApplicationServiceBuilder builder = new ApplicationServiceBuilder(t);
            
            string fileCode = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Classes/Service.cs");
            string generatedCode = builder.GenerateAllCode();
            string tempCode = fileCode.Replace("$code$", generatedCode);
            string finalCode = builder.Prepare(tempCode);

            FileManager.SaveFile(".cs", builder.CurrentTable.PathOfApplicatioServiceFile, finalCode);

        }

        #region ----------------GenerateAllCode----------------
        //---------------------------------------------
        //GenerateAllCode
        //---------------------------------------------
        public string GenerateAllCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(GenerateConstructor());
            builder.AppendLine(GenerateCreateMethod());
            builder.AppendLine(GenerateUpdateMethod());
            builder.AppendLine(GenerateDeleteMethod());
            builder.AppendLine(GenerateGetEntityMethod());
            builder.AppendLine(GenerateGetList());
            builder.AppendLine(GenerateGetPagedList());
            builder.AppendLine(GenerateGetIndexViewData());
            builder.AppendLine(GenerateGetEntityPagedList());
            builder.AppendLine(GeneratePrepareFormView());
           return  builder.ToString();
        }
        //---------------------------------------------
        #endregion

        #region ----------------GenerateConstructor----------------
        //---------------------------------------------
        //GenerateConstructor
        //---------------------------------------------
        /*
        public void GetRelatedTable(ForeignKeyCollection fkc,CustomColumn clmn)
        {
            foreach (ForeignKey relation in fkc)
            {
                foreach (Column c in relation.Columns)
                {
                    if (c.Name == clmn.dbObject.Name)
                    {
                        relation.Parent.Name();
                    }
                }
            }
        }
        */
        public string GenerateConstructor()
        {
            string forienKeysServicesDecleration = "";
            string forienKeysServicesParameters = "";
            string forienKeysServicesAssignments = "";
            string forienKeysServicesNewing = "";

            foreach (var fkc in CurrentTable.ForeignKeys) 
            {
                forienKeysServicesDecleration += "\t\tprivate $Prop$Service _service$Prop$;\n".Replace("$Prop$", fkc.ReferanceTable.ProgramatlyName);
                forienKeysServicesNewing += "\t\t\t_service$Prop$ = new $Prop$Service();\n".Replace("$Prop$", fkc.ReferanceTable.ProgramatlyName);
                forienKeysServicesParameters += ", $Prop$Service service$Prop$".Replace("$Prop$", fkc.ReferanceTable.ProgramatlyName);
                forienKeysServicesAssignments += "\t\t\t_service$Prop$ = service$Prop$;\n".Replace("$Prop$", fkc.ReferanceTable.ProgramatlyName);
                
            }
            string method = $@"
        private UnitOfWork _unitOfWork;
{forienKeysServicesDecleration}
        

        #region --------------Constructor--------------
        //---------------------------------------------------------------------
        //Constructor
        //---------------------------------------------------------------------
        public $Entity$Service()
        {{
            _unitOfWork = new UnitOfWork();
{forienKeysServicesNewing}
        }}
        public $Entity$Service(UnitOfWork unitOfWork{forienKeysServicesParameters})
        {{
            _unitOfWork = unitOfWork;
{forienKeysServicesAssignments}
        }}
        //---------------------------------------------------------------------
        #endregion
              
       ";
            return method;
        }
        //---------------------------------------------
        #endregion

        #region --------------GenerateCreateMethod--------------
        //---------------------------------------------------------------------
        //GenerateCreateMethod
        //---------------------------------------------------------------------
        public string GenerateCreateMethod()
        {
            string populateFilesRegion = "";
            string saveFileRegion = "";
            if (CurrentTable.AllFilesColumns.Count > 0)
            {
                populateFilesRegion = "\n\t\t\t// Populate uploaded files data";
                saveFileRegion = "\n\t\t\t//save uploaded files";
            }
            //
            foreach (var c in CurrentTable.AllFilesColumns)
            {
                populateFilesRegion += "\n\t\t\t" + populateFilesRegionTemplateForCreateUpdate.Replace("$FileProp$", c.ProgramatlyName);
                saveFileRegion += "\n\t\t\t" + saveFileRegionTemplate.Replace("$FileProp$", c.ProgramatlyName);
            }
            //
            if (!string.IsNullOrEmpty(populateFilesRegion))
            {
                populateFilesRegion += "\n\t\t\t//--------------------------------------";
            }


            string method = $@"
        #region --------------Add$Entity$--------------
        //----------------------------------------------
        //Add$Entity$
        //----------------------------------------------
        public void Add$Entity$($Entity$Dto dto)
        {{{populateFilesRegion}
            // save $Entity_smallCase$ data 
            _unitOfWork.$Entity$Repo.Add$Entity$(dto);
            _unitOfWork.Commit();{saveFileRegion}
        }}
        //----------------------------------------------
        #endregion
       ";
            
            return method;
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------GenerateUpdateMethod--------------
        //---------------------------------------------------------------------
        //GenerateUpdateMethod
        //---------------------------------------------------------------------
        public string GenerateUpdateMethod()
        {
            string populateFilesRegion = "";
            string saveFileRegion = "";
            if (CurrentTable.AllFilesColumns.Count > 0)
            {
                populateFilesRegion = "\n\t\t\t// Populate uploaded files data";
                saveFileRegion = "\n\t\t\t//save uploaded files";
            }
            //
            foreach (var c in CurrentTable.AllFilesColumns)
            {
                populateFilesRegion += "\n\t\t\t" + populateFilesRegionTemplateForCreateUpdate.Replace("$FileProp$", c.ProgramatlyName);
                saveFileRegion += "\n\t\t\t" + saveFileRegionTemplate.Replace("$FileProp$", c.ProgramatlyName);
            }
            //
            if (!string.IsNullOrEmpty(populateFilesRegion))
            {
                populateFilesRegion += "\n";
            }


            string method = $@"
        #region --------------Update$Entity$--------------
        //----------------------------------------------
        //Update$Entity$
        //----------------------------------------------
        public void Update$Entity$($Entity$Dto dto)
        {{{populateFilesRegion}
            // save $Entity_smallCase$ updated data 
            _unitOfWork.$Entity$Repo.Update$Entity$(dto);
            _unitOfWork.Commit();{saveFileRegion}
        }}
        //----------------------------------------------
        #endregion
       ";
            
            return method;
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------GenerateDeleteMethod--------------
        //---------------------------------------------------------------------
        //GenerateDeleteMethod
        //---------------------------------------------------------------------
        public string GenerateDeleteMethod()
        {
            string populateFilesRegion = "";
            string deleteFileRegion = "";
            if (CurrentTable.AllFilesColumns.Count > 0)
            {
                populateFilesRegion = "\n\t\t\t// Populate uploaded files data";
                deleteFileRegion = "\n\t\t\t//deleted uploaded files";
            }
            //
            foreach (var c in CurrentTable.AllFilesColumns)
            {
                populateFilesRegion += "\n\t\t\t" + populateFilesRegionTemplateForDelete.Replace("$FileProp$", c.ProgramatlyName);
                deleteFileRegion += "\n\t\t\t" + deleteFileRegionTemplate.Replace("$FileProp$", c.ProgramatlyName);
            }
            //
            if (!string.IsNullOrEmpty(populateFilesRegion))
            {
           
                populateFilesRegion = 
                    "$Entity$Dto dto = this.Get$Entity$(id);"+ 
                    populateFilesRegion+ 
                    "\n";
            }


            string method = $@"
        //
        #region --------------Delete$Entity$--------------
        //---------------------------------------------------------------------
        //Delete$Entity$
        //---------------------------------------------------------------------
        public void Delete$Entity$(int id)
        {{{populateFilesRegion}
            //delete data
            _unitOfWork.$Entity$Repo.Delete$Entity$(id);
            _unitOfWork.Commit();
{deleteFileRegion}
        }}
        //---------------------------------------------------------------------
        #endregion

       ";
            
            return method;
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------GenerateGetMethod--------------
        //---------------------------------------------------------------------
        //GenerateGetMethod
        //---------------------------------------------------------------------
        public string GenerateGetEntityMethod()
        {

            string method = $@"
        #region --------------Get$Entity$--------------
        //---------------------------------------------------------------------
        //Get$Entity$
        //---------------------------------------------------------------------
        /// <summary>
        /// get $Entity_smallCase$ by Id
        /// </summary>
        /// <param name=""$Entity_smallCase$""></param>
        public $Entity$Dto Get$Entity$(int $Entity_smallCase$Id)
        {{
            return _unitOfWork.$Entity$Repo.Get$Entity$($Entity_smallCase$Id);
        }}
        //---------------------------------------------------------------------
        #endregion

       ";
            return method;
        }
        //---------------------------------------------------------------------
        #endregion

        #region ----------------GenerateGetList----------------
        //---------------------------------------------
        //GenerateGetList
        //---------------------------------------------
        public string GenerateGetList()
        {

            string method = $@"
        #region --------------Get$Entity$List--------------
        //---------------------------------------------------------------------
        //Get$Entity$List
        //---------------------------------------------------------------------
        public List<$Entity$Dto> Get$Entity$List($Entity$ListFilter filter)
        {{
            List<$Entity$> entityList = _unitOfWork.$Entity$Repo.Get$Entity$List(filter);
            return ObjectMapper.GetEntityList<$Entity$Dto>(entityList);
        }}
        //---------------------------------------------------------------------
        #endregion
       ";
            return method;
        }
        //---------------------------------------------
        #endregion

        #region ----------------GenerateGetPagedList----------------
        //---------------------------------------------
        //GenerateGetPagedList
        //---------------------------------------------
        public string GenerateGetPagedList()
        {

            string method = $@"
        #region --------------GetPagedList--------------
        //----------------------------------------------
        //GetPagedList
        //----------------------------------------------
        public IPagedList<$Entity$> GetPagedList($Entity$ListFilter filter, int pageNumber, int pageSize, string searchValue, string sortColumnName, string sortDirection)
        {{
            return _unitOfWork.$Entity$Repo.GetPagedList(filter, pageNumber, pageSize, searchValue, sortColumnName, sortDirection);
        }}
        //----------------------------------------------
        #endregion             
       ";
            return method;
        }
        //---------------------------------------------
        #endregion

        #region ----------------GenerateGetIndexPagedViewData----------------
        //---------------------------------------------
        //GenerateGetIndexPagedViewData
        //---------------------------------------------
        public string GenerateGetIndexViewData()
        {
            string forienKeysServicesCall = "";

            foreach (var fkc in CurrentTable.ForeignKeys)
            {
                forienKeysServicesCall += "\t\t\tlistDto.$Prop$Filter = _service$Prop$.Get$Prop$List().To$Prop$SelectList();\n".Replace("$Prop$", fkc.ReferanceTable.ProgramatlyName);
            }
            string method = $@"
        #region --------------GetIndexPagedViewData--------------
        //----------------------------------------------
        //GetIndexPagedViewData
        //----------------------------------------------
        public $Entity$ListDto GetIndexPagedViewData(string data)
        {{
            $Entity$ListDto listDto = this.Get$Entity$PagedList(data);
            {forienKeysServicesCall}
            return listDto;
        }}
        //----------------------------------------------
        #endregion
       ";
            return method;
        }
        //---------------------------------------------
        #endregion
        #region ----------------GenerateGetEntityPagedList----------------
        //---------------------------------------------
        //GenerateGetEntityPagedList
        //---------------------------------------------
        public string GenerateGetEntityPagedList()
        {

            string method = $@"
        #region --------------Get$Entity$PagedList--------------
        //----------------------------------------------
        //Get$Entity$PagedList
        //----------------------------------------------
        public $Entity$ListDto Get$Entity$PagedList(string data)
        {{
            #region --------------Filter Data --------------
            //---------------------------------------------------------------------
            //get filter data 
            //---------------------------------------------------------------------
            CommonParameters filterObject = null;
            $Entity$ListFilter $Entity_smallCase$ListFilter = null;
            if (data != null)
            {{
                filterObject = JsonConvert.DeserializeObject<CommonParameters>(data);
                $Entity_smallCase$ListFilter = ObjectMapper.GetEntity<$Entity$ListFilter>(filterObject);
            }}
            else
            {{
                filterObject = new CommonParameters();
                $Entity_smallCase$ListFilter = new $Entity$ListFilter();
            }}
            //----------------------------------------------------------
            //---------------------------------------------------------------------
            #endregion
            //----------------------------------------------------------
            // get data 
            IPagedList<$Entity$> list = this.GetPagedList($Entity_smallCase$ListFilter, filterObject.PageNo, filterObject.PageSize, null, null, null);
            ElsPaging pagination = new ElsPaging(filterObject.PageNo, list.TotalItemCount, filterObject.PageSize, filterObject.PaginationItems, """", """");
            // prepare view model
            $Entity$ListDto listDto = new $Entity$ListDto();
            listDto.SatrtItemNo = ((filterObject.PageNo - 1) * filterObject.PageSize) + 1;
            listDto.ItemList = ObjectMapper.GetEntityList<$Entity$Dto>(list.ToList());
            listDto.Pagination = pagination;
            return listDto;
        }}
        //----------------------------------------------
        #endregion
       ";
            return method;
        }
        //---------------------------------------------
        #endregion
        #region ----------------GeneratePrepareFormView----------------
        //---------------------------------------------
        //GeneratePrepareFormView
        //---------------------------------------------
        public string GeneratePrepareFormView()
        {

            string t = "\t", n = "\n";
            string forienKeysServicesCall = "";
            string forienKeyPluralName = "";
            foreach (var fkc in CurrentTable.ForeignKeys)
            {
                forienKeyPluralName = fkc.ReferanceTable.PluralCase;
                forienKeysServicesCall += $@"{t}{t}{t}//get $Prop_smallCase$ options{n}".Replace("$Prop_smallCase$", fkc.ReferanceTable.SmallSinglerCase);
                forienKeysServicesCall += $@"{t}{t}{t}dto.{fkc.ProgramatlyName}_Options = _service$Prop$.Get$Prop$List().To$Prop$SelectList(dto.{fkc.ProgramatlyName});{n}".Replace("$Prop$", fkc.ReferanceTable.ProgramatlyName);
            }

            string method = $@"
        #region --------------PrepareFormView--------------
        //---------------------------------------------------------------------
        //PrepareFormView
        //---------------------------------------------------------------------
        public $Entity$Dto PrepareFormView(int id = 0)
        {{
            $Entity$Dto dto = null;
            if (id > 0)
            {{
                dto = this.Get$Entity$(id);
            }}
            else
            {{
                dto = new $Entity$Dto();
            }}
            {forienKeysServicesCall}
            return dto;
        }}

        //---------------------------------------------------------------------
        #endregion                     
       ";
            return method;
        }
        //---------------------------------------------
        #endregion


        
    }

}
