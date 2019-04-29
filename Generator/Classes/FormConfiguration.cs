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
    class FormConfigurationBuilder : TableFileBuilder
    {
        public static string ExecludedInputs = "CreationDate,DateAdded,ViewCounter";

        StringBuilder DynamicProperties = new StringBuilder();
        StringBuilder AdditionalProperties = new StringBuilder();
        StringBuilder FilterProperties = new StringBuilder();
        StringBuilder FormConfiguration = new StringBuilder();
        public FormConfigurationBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }
        public string CreateFormConfiguration()
        {

          string formElements = CreateFormElements();
            //--------------------------------------------------------------------------------
            //{this.CurrentTable.ProgramatlyName} save form 
            //--------------------------------------------------------------------------------
            string formCode =  $@"            
                Form form = new Form();
                form.ResourceFile = ""{this.CurrentTable.ProgramatlyName}"";
                form.Elements = new List<Input> {{
                    {formElements}
                }};
                return form;
";
            string buildFormConfiguration = CreateMethod("BuildFormConfiguration", formCode, "Form");
            return buildFormConfiguration;
        }
        public string CreateFormElements()
        {
            StringBuilder sp = new StringBuilder();
            StringBuilder hiddenFeilds = new StringBuilder();
            string t = "\t";
            string n = "\n";
            foreach (CustomColumn c in CurrentTable.Columns)
            {
                if (ExecludedInputs.ToLower().Contains(c.LowerProgramatlyName))
                {
                    continue;
                }

                if (c.InPrimaryKey && c.IsIdentity)
                {
                    hiddenFeilds.Append($@"{n}{t}{t}{t}{t}new HiddenInput {{ Name = ""{c.ProgramatlyName}"" }},");
                    continue;
                }
                switch (c.InputType)
                {
                    case EnumInputType.undefined:
                        break;
                    case EnumInputType.textarea:
                        sp.Append($@"{n}{t}{t}{t}{t}new TextAreaInput {{ Name = ""{c.ProgramatlyName}"", StringLength = {c.MaxLength}, ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.select:
                        sp.Append($@"{n}{t}{t}{t}{t}new SelectInput {{ Name = ""{c.ProgramatlyName}"", DataSource= ""{c.ProgramatlyName}_Options"" , ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.checkbox:
                        sp.Append($@"{n}{t}{t}{t}{t}new CheckBoxInput {{ Name = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.date:
                        sp.Append($@"{n}{t}{t}{t}{t}new TextInput {{ Name = ""{c.ProgramatlyName}"", StringLength = {c.MaxLength}, ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.email:
                        sp.Append($@"{n}{t}{t}{t}{t}new TextInput {{ Name = ""{c.ProgramatlyName}"", StringLength = {c.MaxLength}, ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.file:
                        break;
                    case EnumInputType.UploadedServerfileWithJustNameInDB:
                        hiddenFeilds.Append($@"{n}{t}{t}{t}{t}new HiddenInput {{ Name = ""{c.ProgramatlyName}"" }},");
                        sp.Append($@"{n}{t}{t}{t}{t}new FileInput   {{ Name = ""{c.ProgramatlyName}PostedFile"",FileSize = (5*1024*1024), Extension = """", FileSizeError = ""Maximum allowed file size is 5 MB"" }},");
                        break;
                    case EnumInputType.UploadedServerPhotoWithThumbnailAndJustNameInDB:
                        hiddenFeilds.Append($@"{n}{t}{t}{t}{t}new HiddenInput {{ Name = ""{c.ProgramatlyName}"" }},");
                        sp.Append($@"{n}{t}{t}{t}{t}new FileInput   {{ Name = ""{c.ProgramatlyName}PostedFile"",FileSize = (5*1024*1024), Extension = ""jpg,jpeg,png,gif"", FileSizeError = ""Maximum allowed file size is 5 MB"" }},");
                        sp.Append($@"{n}{t}{t}{t}{t}new ThumbInput   {{ Name = ""{c.ProgramatlyName}ThumUrl"" }},");
                        break;
                    case EnumInputType.hidden:
                        hiddenFeilds.Append($@"{n}{t}{t}{t}{t}new HiddenInput {{ Name = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.number:
                        sp.Append($@"{n}{t}{t}{t}{t}new NumberInput {{ Name = ""{c.ProgramatlyName}"", StringLength = {c.MaxLength}, ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.password:
                        sp.Append($@"{n}{t}{t}{t}{t}new PasswordInput {{ Name = ""{c.ProgramatlyName}"", StringLength = {c.MaxLength}, ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.radio:
                        sp.Append($@"{n}{t}{t}{t}{t}new RadioInput {{ Name = ""{c.ProgramatlyName}"", StringLength = {c.MaxLength}, ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.text:
                        sp.Append($@"{n}{t}{t}{t}{t}new TextInput {{ Name = ""{c.ProgramatlyName}"", StringLength = {c.MaxLength}, ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.url:
                        sp.Append($@"{n}{t}{t}{t}{t}new TextInput {{ Name = ""{c.ProgramatlyName}"", StringLength = {c.MaxLength}, ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.tel:
                        sp.Append($@"{n}{t}{t}{t}{t}new TextInput {{ Name = ""{c.ProgramatlyName}"", StringLength = {c.MaxLength}, ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.checkboxList:
                        sp.Append($@"{n}{t}{t}{t}{t}new SelectInput {{ Name = ""{c.ProgramatlyName}"", DataSource= ""{c.ProgramatlyName}_Options"" , ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.button:
                        sp.Append($@"{n}{t}{t}{t}{t}new ButtonInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.color:
                        sp.Append($@"{n}{t}{t}{t}{t}new ColorInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.datetime:
                        sp.Append($@"{n}{t}{t}{t}{t}new DateTimeInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.image:
                        sp.Append($@"{n}{t}{t}{t}{t}new ImageInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.month:
                        sp.Append($@"{n}{t}{t}{t}{t}new MonthInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.range:
                        sp.Append($@"{n}{t}{t}{t}{t}new RangeInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.reset:
                        sp.Append($@"{n}{t}{t}{t}{t}new ResetInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.search:
                        sp.Append($@"{n}{t}{t}{t}{t}new SearchInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.submit:
                        sp.Append($@"{n}{t}{t}{t}{t}new SubmitInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.time:
                        sp.Append($@"{n}{t}{t}{t}{t}new TimeInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.week:
                        sp.Append($@"{n}{t}{t}{t}{t}new WeekInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.postalCode:
                        sp.Append($@"{n}{t}{t}{t}{t}new PostalCodeInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    case EnumInputType.creditCard:
                        sp.Append($@"{n}{t}{t}{t}{t}new CreditCardInput {{ Name = ""{c.ProgramatlyName}"", ResourceText = ""{c.ProgramatlyName}"" }},");
                        break;
                    default:
                        break;
                }
            }
            sp.Append($@"{n}{t}{t}{t}{t}new SubmitInput {{ Name = ""Send"", ResourceText = ""Send"" }}");
            string code = hiddenFeilds.ToString() + sp.ToString();
            return code;
        }
    }
}
