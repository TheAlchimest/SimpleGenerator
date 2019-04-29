using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace SMOSampleInConsol
{
    class CustomColumn
    {

        private Column _dbObject;
       public EnumInputType InputType { get; set; }

        public Column dbObject{get { return _dbObject; }}

        public string NameInDatabase { get { return _dbObject.Name; } }

        public string ProgramatlyName { get; set; }
        public string LowerProgramatlyName { get; set; }
        public string FriendlyName { get; set; }
    
        

        public Microsoft.SqlServer.Management.Smo.DataType SmoDataType { get { return _dbObject.DataType; } }

        public bool HasDefaultValue { get; set; }

        public bool IsComputed { get { return _dbObject.Computed; } }

        public bool IsIdentity { get { return _dbObject.Identity; } }

        public bool InPrimaryKey { get { return _dbObject.InPrimaryKey; } }

        public bool IsForeignKey { get { return _dbObject.IsForeignKey; } }
        public CustomTable ReferanceTable { get; set; }
        public bool IsPersisted { get { return _dbObject.IsPersisted;  } }

        public bool IsNullable { get { return _dbObject.Nullable; } }

        public string TableName { get; set; }

        public string TableProgramatlyName { get; set; }


        public Type DotNetType { get; set; }

        public string DotNetTypeString { get { return DotNetType.ToString(); } }

        public string DotNetTypeAlias { get; set; }

        public SqlDbType SqlDbType { get; set; }
        public string SqlDbTypeString { get { return SqlDbType.ToString(); } }

        public SqlDataType SqlDataType { get { return _dbObject.DataType.SqlDataType; } }

        public System.ComponentModel.DataAnnotations.DataType DataTypeAttr { get; set; }

        public int MaxLength { get { return _dbObject.DataType.MaximumLength; } }

        public bool IsOutoutIninsert { get { return (InPrimaryKey && IsIdentity); } }

        public bool NotPassedToSql { 
            get { 
                return (IsComputed);
            } 
        }

        public bool IsFile { get; set; }
        public bool IsPhoto { get;  set; }
        public bool IsIdentifier { get;  set; }
        public string DefaultValue { get;  set; }

        public CustomTable ParentTable { get; set; }
        public CustomColumn(Column c,CustomTable parentTable, string tableName, string tableProgramatlyName)
        {
            _dbObject = c;
            ParentTable = parentTable;
            TableName = tableName;
            TableProgramatlyName = tableProgramatlyName;
            ProgramatlyName = Globals.GetProgramatlyName(NameInDatabase);
            FriendlyName = Globals.GetFriendlyName(ProgramatlyName);
            LowerProgramatlyName = ProgramatlyName.ToLower();
            InputType = SetInputType();
            if (c.DefaultConstraint!=null)
            {
                HasDefaultValue = true;
                DefaultValue = GetDefaultValue();
            }
            //set data type and complete column analysis
            SetDataTypes();
        }

        public string GetDefaultValue() {

            string defaultValue = null;
            string defaultText = this._dbObject.DefaultConstraint.Text;
            if (!string.IsNullOrEmpty(defaultText))
            {
                defaultValue = defaultText.Replace("(", "").Replace(")", "");
                if (defaultValue == "''")
                {
                    defaultValue = "";
                }


            }
            return defaultValue;

        }
        public void SetDataTypes()
        {
            switch (SmoDataType.SqlDataType)
            {
                case SqlDataType.BigInt:
                    SqlDbType = SqlDbType.BigInt;
                    DotNetType = typeof(Int64);
                    DotNetTypeAlias = "long";
                    break;

                case SqlDataType.Binary:
                    SqlDbType = SqlDbType.Binary;
                    DotNetType = typeof(Byte[]);
                    DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.Bit:
                    SqlDbType = SqlDbType.Bit;
                    DotNetType = typeof(Boolean);
                    DotNetTypeAlias = "bool";
                    break;
                case SqlDataType.Char:
                    SqlDbType = SqlDbType.Char;
                    DotNetType = typeof(String);
                    DotNetTypeAlias = "string";
                    SetStringDataType();
                    break;
                case SqlDataType.Date:
                    SqlDbType = SqlDbType.Date;
                    DotNetType = typeof(DateTime);
                    DotNetTypeAlias = "DateTime";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Date;
                    break;
                case SqlDataType.DateTime:
                    SqlDbType = SqlDbType.DateTime;
                    DotNetType = typeof(DateTime);
                    DotNetTypeAlias = "DateTime";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.DateTime;

                    break;
                case SqlDataType.DateTime2:
                    SqlDbType = SqlDbType.DateTime2;
                    DotNetType = typeof(DateTime);
                    DotNetTypeAlias = "DateTime";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.DateTime;
                    break;
                case SqlDataType.DateTimeOffset:
                    SqlDbType = SqlDbType.DateTimeOffset;
                    DotNetType = typeof(DateTimeOffset);
                    DotNetTypeAlias = "DateTimeOffset";
                    break;
                case SqlDataType.Decimal:
                    SqlDbType = SqlDbType.Decimal;
                    DotNetType = typeof(Decimal);
                    DotNetTypeAlias = "decimal";

                    break;
                case SqlDataType.Float:
                    SqlDbType = SqlDbType.Float;
                    DotNetType = typeof(Double);
                    DotNetTypeAlias = "double";
                    break;
                case SqlDataType.Image:
                    SqlDbType = SqlDbType.Image;
                    DotNetType = typeof(Byte[]);
                    DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.Int:
                    SqlDbType = SqlDbType.Int;
                    DotNetType = typeof(Int32);
                    DotNetTypeAlias = "int";
                    break;
                case SqlDataType.Money:
                    SqlDbType = SqlDbType.Money;
                    DotNetType = typeof(Decimal);
                    DotNetTypeAlias = "decimal";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Currency;

                    break;
                case SqlDataType.NChar:
                    SqlDbType = SqlDbType.NChar;
                    DotNetType = typeof(String);
                    DotNetTypeAlias = "string";
                    SetStringDataType();

                    break;
                case SqlDataType.NText:
                    SqlDbType = SqlDbType.NText;
                    DotNetType = typeof(String);
                    DotNetTypeAlias = "string";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;

                    break;
                case SqlDataType.NVarChar:
                    SqlDbType = SqlDbType.NVarChar;
                    DotNetType = typeof(String);
                    DotNetTypeAlias = "string";
                    SetStringDataType();

                    break;
                case SqlDataType.Real:
                    SqlDbType = SqlDbType.Real;
                    DotNetType = typeof(Single);
                    DotNetTypeAlias = "Single";
                    break;
                case SqlDataType.SmallDateTime:
                    SqlDbType = SqlDbType.SmallDateTime;
                    DotNetType = typeof(DateTime);
                    DotNetTypeAlias = "DateTime";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.DateTime;
                    break;
                case SqlDataType.SmallInt:
                    SqlDbType = SqlDbType.SmallInt;
                    DotNetType = typeof(Int16);
                    DotNetTypeAlias = "short";
                    break;
                case SqlDataType.SmallMoney:
                    SqlDbType = SqlDbType.SmallMoney;
                    DotNetType = typeof(Decimal);
                    DotNetTypeAlias = "decimal";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Currency;
                    break;

                case SqlDataType.Text:
                    SqlDbType = SqlDbType.Text;
                    DotNetType = typeof(String);
                    DotNetTypeAlias = "string";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;

                    break;
                case SqlDataType.Time:
                    SqlDbType = SqlDbType.Time;
                    DotNetType = typeof(TimeSpan);
                    DotNetTypeAlias = "TimeSpan";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Time;
                    break;
                case SqlDataType.Timestamp:
                    SqlDbType = SqlDbType.Timestamp;
                    DotNetType = typeof(Byte[]);
                    DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.TinyInt:
                    SqlDbType = SqlDbType.TinyInt;
                    DotNetType = typeof(Byte);
                    DotNetTypeAlias = "byte";
                    break;
                case SqlDataType.UserDefinedDataType:
                    SqlDbType = SqlDbType.UniqueIdentifier;
                    DotNetType = typeof(Object);
                    DotNetTypeAlias = "object";
                    break;
                case SqlDataType.UniqueIdentifier:
                    DotNetType = typeof(Guid);
                    DotNetTypeAlias = "Guid";
                    break;
                case SqlDataType.VarBinary:
                    SqlDbType = SqlDbType.VarBinary;
                    DotNetType = typeof(Byte[]);
                    DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.VarChar:
                    SqlDbType = SqlDbType.VarChar;
                    DotNetType = typeof(String);
                    DotNetTypeAlias = "string";
                    SetStringDataType();

                    break;
                case SqlDataType.Variant:
                    SqlDbType = SqlDbType.Variant;
                    DotNetType = typeof(Object);
                    DotNetTypeAlias = "object";
                    break;
                case SqlDataType.Xml:
                    SqlDbType = SqlDbType.Xml;
                    DotNetType = typeof(System.Xml.XmlDocument);
                    DotNetTypeAlias = "XmlDocument";
                    break;
                case SqlDataType.Geography:
                    //    SqlDbType = SqlDbType.Geography;
                    DotNetType = typeof(Object);
                    DotNetTypeAlias = "object";
                    break;
                case SqlDataType.Geometry:
                    //    SqlDbType = SqlDbType.Geometry;
                    DotNetType = typeof(Object);
                    DotNetTypeAlias = "object";
                    break;
                case SqlDataType.HierarchyId:
                    //    SqlDbType = SqlDbType.HierarchyId;
                    DotNetType = typeof(Object);
                    DotNetTypeAlias = "object";
                    break;
                case SqlDataType.None:
                    //    SqlDbType = SqlDbType.None;
                    DotNetType = typeof(Object);
                    DotNetTypeAlias = "object";
                    break;
                case SqlDataType.Numeric:
                    //    SqlDbType = SqlDbType.Numeric;
                    DotNetType = typeof(Decimal);
                    DotNetTypeAlias = "decimal";
                    break;

                case SqlDataType.NVarCharMax:
                    SqlDbType = SqlDbType.NVarChar;
                    DotNetType = typeof(String);
                    DotNetTypeAlias = "string";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;

                    break;
                case SqlDataType.SysName:
                    //    SqlDbType = SqlDbType.SysName;
                    DotNetType = typeof(Object);
                    DotNetTypeAlias = "object";
                    break;
                case SqlDataType.UserDefinedTableType:
                    SqlDbType = SqlDbType.Udt;
                    DotNetType = typeof(Object);
                    DotNetTypeAlias = "object";
                    break;
                case SqlDataType.UserDefinedType:
                    SqlDbType = SqlDbType.Udt;
                    DotNetType = typeof(Object);
                    DotNetTypeAlias = "object";
                    break;
                case SqlDataType.VarBinaryMax:
                    SqlDbType = SqlDbType.VarBinary;
                    DotNetType = typeof(Byte[]);
                    DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.VarCharMax:
                    SqlDbType = SqlDbType.VarChar;
                    DotNetType = typeof(String);
                    DotNetTypeAlias = "string";
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;

                    break;
                default:
                    DotNetType = typeof(String);
                    DotNetTypeAlias = "string";
                    break;
            }
        }

        public void SetStringDataType()
        {
            if (DotNetType == typeof(String))
            {
                if (Globals.IsMatchIdentifier(Globals.Identifier_Url, this))
                {
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Url;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Email, this))
                {
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.EmailAddress;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_File, this) || Globals.IsMatchIdentifier(Globals.Identifier_Image, this) || Globals.IsMatchIdentifier(Globals.Identifier_Photo, this))
                {
                    if (Globals.IsMatchIdentifier(Globals.Identifier_Image, this) || Globals.IsMatchIdentifier(Globals.Identifier_Photo, this))
                    {
                        this.IsPhoto = true;
                        ParentTable.HasPhoto = true;
                        ParentTable.PhotosColumns.Add(this);
                    }
                    else
                    {
                        this.IsFile = true;
                        ParentTable.HasFile = true;
                        ParentTable.FilesColumns.Add(this);
                    }
                    // add in AllFilesColumns
                    ParentTable.AllFilesColumns.Add(this);
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Upload;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Password, this))
                {
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Password;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Phone, this))
                {
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.PhoneNumber;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_PostalCode, this))
                {
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.PostalCode;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_CreditCard, this))
                {
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.CreditCard;
                }
                else if (MaxLength > 250)
                {
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;
                }
                else
                {
                    DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Text;
                }
                if (Globals.IsMatchIdentifier(Globals.Identifier_Identifier, this))
                {
                    IsIdentifier = true;
                }
            }

        }

        public EnumInputType SetInputType()
        {
            if (IsForeignKey)
            {
                return EnumInputType.select;
            }
            if (IsIdentity)
            {
                return EnumInputType.hidden;
            }
            switch (SmoDataType.SqlDataType)
            {
                case SqlDataType.Char:
                case SqlDataType.NChar:
                case SqlDataType.NVarChar:
                case SqlDataType.VarChar:
                    return SetStringSetInputType();
                case SqlDataType.BigInt:
                    return EnumInputType.number;
                case SqlDataType.Float:
                    return EnumInputType.number;
                case SqlDataType.Int:
                    return EnumInputType.number;
                case SqlDataType.Real:
                    return EnumInputType.number;
                case SqlDataType.SmallInt:
                    return EnumInputType.number;
                case SqlDataType.TinyInt:
                    return EnumInputType.number;
                case SqlDataType.Numeric:
                    return EnumInputType.number;
                case SqlDataType.Binary:
                    return EnumInputType.file;
                case SqlDataType.Bit:
                    return EnumInputType.checkbox;
                case SqlDataType.Date:
                    return EnumInputType.date;
                case SqlDataType.DateTime:
                case SqlDataType.DateTime2:
                    return EnumInputType.datetime;
                case SqlDataType.DateTimeOffset:
                    return EnumInputType.datetime;
                case SqlDataType.Decimal:
                    return EnumInputType.text;
                case SqlDataType.Image:
                    return EnumInputType.file;
                case SqlDataType.Money:
                    return EnumInputType.text;
                case SqlDataType.NText:
                    return EnumInputType.textarea;
                case SqlDataType.SmallDateTime:
                    return EnumInputType.datetime;
                case SqlDataType.SmallMoney:
                    return EnumInputType.text;
                case SqlDataType.Text:
                    return EnumInputType.textarea;
                case SqlDataType.Time:
                    return EnumInputType.time;
                case SqlDataType.Timestamp:
                    return EnumInputType.file;
                case SqlDataType.UniqueIdentifier:
                    return EnumInputType.hidden;
                case SqlDataType.VarBinary:
                    return EnumInputType.file;
                case SqlDataType.UserDefinedDataType:
                case SqlDataType.Variant:
                case SqlDataType.Xml:
                case SqlDataType.Geography:
                case SqlDataType.Geometry:
                case SqlDataType.HierarchyId:
                case SqlDataType.None:
                case SqlDataType.SysName:
                case SqlDataType.UserDefinedTableType:
                case SqlDataType.UserDefinedType:
                case SqlDataType.VarBinaryMax:
                    return EnumInputType.undefined;
                case SqlDataType.NVarCharMax:
                    return EnumInputType.textarea;
                case SqlDataType.VarCharMax:
                    return EnumInputType.textarea;
                default:
                    return EnumInputType.text;
            }
        }
        //-------------------------------------------------------------------
        public EnumInputType SetStringSetInputType()
        {
                if (Globals.IsMatchIdentifier(Globals.Identifier_Url, this))
                {
                    return EnumInputType.url;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Email, this))
                {
                    return EnumInputType.email;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_File, this) || Globals.IsMatchIdentifier(Globals.Identifier_Image, this) || Globals.IsMatchIdentifier(Globals.Identifier_Photo, this))
                {
                    if (Globals.IsMatchIdentifier(Globals.Identifier_Image, this) || Globals.IsMatchIdentifier(Globals.Identifier_Photo, this))
                    {
                        //file upload that just same name in database and file on server
                        return EnumInputType.UploadedServerPhotoWithThumbnailAndJustNameInDB;
                    }
                    else
                    {
                        return EnumInputType.UploadedServerfileWithJustNameInDB;
                    }
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Password, this))
                {
                    return EnumInputType.password;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Phone, this))
                {
                    return EnumInputType.tel;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_PostalCode, this))
                {
                    return EnumInputType.postalCode;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_CreditCard, this))
                {
                    return EnumInputType.creditCard;
                }
                else if (MaxLength > 250)
                {
                    return EnumInputType.textarea;
                }
                else
                {
                    return EnumInputType.text;
                }
            
        }

        public static bool IsMatchIdentifier(string Identifier, CustomColumn c)
        {
            int identifierIndex = c.NameInDatabase.IndexOf(Identifier);
            int indexIfMatches = (c.NameInDatabase.Length - Identifier.Length);
            if (identifierIndex > -1 && identifierIndex == indexIfMatches)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
