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
    class DtoBuilder : TableFileBuilder
    {
        

        StringBuilder DynamicProperties = new StringBuilder();
        StringBuilder AdditionalProperties = new StringBuilder();
        StringBuilder FilterProperties = new StringBuilder();

        public DtoBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }
        private void init()
        {
            string t = "\t";
            string n = "\n";
            StringBuilder dataAnnotations = new StringBuilder();
            EnumInputType inputType = EnumInputType.undefined;
            foreach (CustomColumn c in CurrentTable.Columns)
            {

                //inputType = Globals.GetElementInputType(c);
                if (!c.IsNullable)
                {
                    dataAnnotations.AppendLine($@"{t}{t}[Required(ErrorMessage = ""Please enter the {c.ProgramatlyName}."")]");
                }
                if (c.DotNetType == typeof(string))
                {
                    dataAnnotations.AppendLine($@"{t}{t}[StringLength({c.MaxLength}, ErrorMessage = ""The {c.ProgramatlyName} must be less than {{1}} characters."")]");
                }


                //if (c.DataTypeAttr != System.ComponentModel.DataAnnotations.DataType.Text)
                //{
                //    dataAnnotations.AppendLine($@"{t}{t}[DataType(DataType.{c.DataTypeAttr})]");
                //}
                if (c.HasDefaultValue)
                {
                }
                if (c.DotNetType == typeof(string))
                {
                }
                if (c.IsIdentifier)
                {
                    dataAnnotations.AppendLine($@"{t}{t}[RegularExpression(@""^\S*$"", ErrorMessage = ""No white space allowed"")]");
                }
                //dataAnnotations.AppendLine($@"{t}{t}[Display(Name = ""{c.ProgramatlyName}: "")]");
                if (!c.IsIdentity && !FormConfigurationBuilder.ExecludedInputs.ToLower().Contains(c.LowerProgramatlyName))
                {
                    DynamicProperties.Append(dataAnnotations.ToString());
                }

                //DynamicProperties.Append("\n\t\tpublic " + c.DotNetTypeAlias + optionalprifex + " " + c.ProgramatlyName + " { get; set; }\n");
                DynamicProperties.Append($@"{n}{t}{t}public {c.DotNetTypeAlias} {c.ProgramatlyName} {{ get; set; }}");
                //Here we will check the default value to add it in the dto
                if (c.HasDefaultValue)
                {
                    //formElementInfoAttribute += $", InitialValue = {c.DefaultValue}";
                    if (c.DotNetType == typeof(bool))
                    {
                        if (c.DefaultValue == "0")
                        {
                            DynamicProperties.Append(" = false;");
                        }
                        if (c.DefaultValue == "1")
                        {
                            DynamicProperties.Append(" = true;");
                        }
                    }
                    else if (c.DotNetType == typeof(DateTime))
                    {
                        if (c.DefaultValue.ToLower().IndexOf("getdate") > -1)
                        {
                            DynamicProperties.Append(" = DateTime.Now;");
                        }
                    }
                    else if (c.DotNetType == typeof(string))
                    {
                        DynamicProperties.Append($@" = ""{c.DefaultValue}"";");
                    }
                    else
                    {
                        DynamicProperties.Append($@" = {c.DefaultValue};");
                    }

                }
                if (c.IsForeignKey)
                {
                    FilterProperties.AppendLine($@"{n}{t}public {c.DotNetTypeAlias} {c.ProgramatlyName} {{ get; set; }}");
                    DynamicProperties.AppendLine($@"{n}{t}{t}public SelectList {c.ProgramatlyName}_Options {{ get; set; }}");
                }
                if (c.IsFile || c.IsPhoto)
                {
                    string extension = "";
                    if (c.IsPhoto)
                    { extension = "jpg,jpeg,png,gif"; }
                    AdditionalProperties.AppendLine(
                   $@"
        //---------------------------------
        //{c.ProgramatlyName}PostedFile
        //---------------------------------
        [FileType(""{extension}"")]
        [AllowFileSize(FileSize = 5 * 1024 * 1024, ErrorMessage = ""Maximum allowed file size is 5 MB"")]
        public HttpPostedFileBase {c.ProgramatlyName}PostedFile {{ get; set; }}
        public UploadedFile {c.ProgramatlyName}UploadedFile {{ get; set; }}
        //---------------------------------

        #region ------{c.ProgramatlyName}Url------
        //---------------------------------
        //{c.ProgramatlyName}Url
        //---------------------------------
        private string _{c.ProgramatlyName}Url;
        public string {c.ProgramatlyName}Url
        {{
            {t}get
            {{
                if (_{c.ProgramatlyName}Url == null && !string.IsNullOrEmpty(this.{c.ProgramatlyName}))
                {{
                    _{c.ProgramatlyName}Url = string.Format(""{{0}}{{1}}/{{2}}"", SiteSettings.{c.ProgramatlyName}VirtualPath, this.{c.ParentTable.ID.ProgramatlyName}, this.{c.ProgramatlyName});
                }}
                return _{c.ProgramatlyName}Url;
            }}

        }}
        //---------------------------------
        #endregion
       
    ");
                }
                if (c.IsPhoto)
                {
                    AdditionalProperties.AppendLine(
                   $@"

        #region ------{c.ProgramatlyName}ThumUrl------
        //---------------------------------
        //{c.ProgramatlyName}ThumUrl
        //---------------------------------
        public string {c.ProgramatlyName}ThumUrl
        {{
            get
            {{
                if (!string.IsNullOrEmpty(this.{c.ProgramatlyName}))
                {{
                    return ""/{c.ParentTable.ProgramatlyName}/Thumbnail/"" + this.{c.ParentTable.ID.ProgramatlyName};
                }}
                else
                    return ""/Content/Limitless_2.0.1/Bootstrap 4/Template/global_assets/images/placeholders/placeholder.jpg"";
            }}

        }}
        //---------------------------------
        #endregion
    ");
                }


                //----------------------------
                DynamicProperties.AppendLine("\n");
                if (c.IsFile || c.IsPhoto)//has additional props
                {
                    AdditionalProperties.AppendLine("\n");
                }
                //clear data annotation
                dataAnnotations.Clear();
            } 


        }

        public static void Create(CustomTable t)
        {
            DtoBuilder builder = new DtoBuilder(t);

            string EntityFile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Classes/Dto.cs");

            string genEntityFile = builder.Prepare(EntityFile);

            FileManager.SaveFile(".cs", builder.CurrentTable.PathOfDtoClass, genEntityFile);

        }
        public new string Prepare(string temp )
        {
            StringBuilder codes = new StringBuilder(base.Prepare(temp));
            //codes.Replace(ResourcesParameters.DynamicParameters, DynamicParameters.ToString());
            //codes.Replace(ResourcesParameters.DynamicPopulatedParameters, DynamicPopulatedParameters.ToString());
            codes.Replace("$DynamicProperties$", DynamicProperties.ToString());
            codes.Replace("$AdditionalProperties$", AdditionalProperties.ToString());
            codes.Replace("$FilterProperties$", FilterProperties.ToString());
            FormConfigurationBuilder fcb = new FormConfigurationBuilder(this.CurrentTable);
            string fcbCode=  fcb.CreateFormConfiguration();
            codes.Replace("$FormConfiguration$", fcbCode);
            
            return codes.ToString();
        }
    }
}
