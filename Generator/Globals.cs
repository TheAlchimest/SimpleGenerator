using System;
using System.Xml;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace SMOSampleInConsol
{
    class Globals
    {
        public static string Identifier_Url = "Url";
        public static string Identifier_File = "File";
        public static string Identifier_Image = "Image";
        public static string Identifier_Photo = "Photo";
        public static string Identifier_Email = "Email";
        public static string Identifier_Phone = "Phone";
        public static string Identifier_Password = "Password";
        public static string Identifier_PostalCode = "PostalCode"; 
        public static string Identifier_CreditCard = "CreditCard";
        public static string Identifier_Identifier = "Identifier";
        //------------------------------------------------------------
        public static string GetProgramatlyName(string name)
        {
           return  name.Replace(" ", "_");
        }
        public static string GetProgramatlyCamelCase(string name)
        {
            string programatlyName = GetProgramatlyName(name);
            string camelCase = Char.ToLowerInvariant(programatlyName[0]) + programatlyName.Substring(1);
            return camelCase;
        }
        //------------------------------------------------------------
        public static string GetDataTypeString(SqlDataType type)
        {
            string dataType = "";
            switch (type)
            {
                case SqlDataType.BigInt:
                    dataType = "Int64";
                    break;
                case SqlDataType.Binary:
                    dataType = "Byte[]";
                    break;
                case SqlDataType.Bit:
                    dataType = "Boolean";
                    break;
                case SqlDataType.Char:
                    dataType = "String";
                    break;
                case SqlDataType.Date:
                    dataType = "DateTime";
                    break;
                case SqlDataType.DateTime:
                    dataType = "DateTime";
                    break;
                case SqlDataType.DateTime2:
                    dataType = "DateTime";
                    break;
                case SqlDataType.DateTimeOffset:
                    dataType = "DateTimeOffset";
                    break;
                case SqlDataType.Decimal:
                    dataType = "Decimal";

                    break;
                case SqlDataType.Float:
                    dataType = "Double";
                    break;
                case SqlDataType.Image:
                    dataType = "Byte[]";
                    break;
                case SqlDataType.Int:
                    dataType = "Int32";
                    break;
                case SqlDataType.Money:
                    dataType = "Decimal";
                    break;
                case SqlDataType.NChar:
                    dataType = "String";
                    break;
                case SqlDataType.NText:
                    dataType = "String";
                    break;
                case SqlDataType.NVarChar:
                    dataType = "String";
                    break;
                case SqlDataType.Real:
                    dataType = "Single";
                    break;
                case SqlDataType.SmallDateTime:
                    dataType = "DateTime";
                    break;
                case SqlDataType.SmallInt:
                    dataType = "Int16";
                    break;
                case SqlDataType.SmallMoney:
                    dataType = "Decimal";
                    break;

                case SqlDataType.Text:
                    dataType = "String";
                    break;
                case SqlDataType.Time:
                    dataType = "TimeSpan";
                    break;
                case SqlDataType.Timestamp:
                    dataType = "Byte[]";
                    break;
                case SqlDataType.TinyInt:
                    dataType = "Byte";
                    break;
                case SqlDataType.UserDefinedDataType:
                    dataType = "Object";
                    break;
                case SqlDataType.UniqueIdentifier:
                    dataType = "Guid";
                    break;
                case SqlDataType.VarBinary:
                    dataType = "Byte[]";
                    break;
                case SqlDataType.VarChar:
                    dataType = "String";
                    break;
                case SqlDataType.Variant:
                    dataType = "Object";
                    break;
                case SqlDataType.Xml:
                    dataType = "XmlDocument";
                    break;
                case SqlDataType.Geography: 
                    dataType = "Object";
                    break;
                case SqlDataType.Geometry: 
                    dataType = "Object";
                    break;
                case SqlDataType.HierarchyId:
                    dataType = "Object";
                     break;
                case SqlDataType.None: 
                    dataType = "";
                    break;
                case SqlDataType.Numeric: 
                    dataType = "Decimal";
                    break;
                    
                case SqlDataType.NVarCharMax : 
                    dataType = "String";
                    break;
                case SqlDataType.SysName: 
                    dataType = "Object";
                    break;
                case SqlDataType.UserDefinedTableType: 
                    dataType = "Object";
                    break;
                case SqlDataType.UserDefinedType:
                    dataType = "Object";
                    break;
                case SqlDataType.VarBinaryMax:
                    dataType = "Byte[]";
                    break;
                case SqlDataType.VarCharMax: 
                    dataType = "String";
                    break;
                default:
                    dataType = "";
                    break;
            }
            return dataType;

        }
        //-------------------------------------------------------------------
        public static string GetDataTypeAlias(SqlDataType type)
        {
            string dataType = "";
            switch (type)
            {
                 case SqlDataType.BigInt:
                    dataType = "long";
                    break;
                case SqlDataType.Binary:
                    dataType = "byte[]";
                    break;
                case SqlDataType.Bit:
                    dataType = "bool";
                    break;
                case SqlDataType.Char:
                    dataType = "string";
                    break;
                case SqlDataType.Date:
                    dataType = "DateTime";
                    break;
                case SqlDataType.DateTime:
                    dataType = "DateTime";
                    break;
                case SqlDataType.DateTime2:
                    dataType = "DateTime";
                    break;
                case SqlDataType.DateTimeOffset:
                    dataType = "DateTimeOffset";
                    break;
                case SqlDataType.Decimal:
                    dataType = "decimal";
                    break;
                case SqlDataType.Float:
                    dataType = "double";
                    break;
                case SqlDataType.Image:
                    dataType = "byte[]";
                    break;
                case SqlDataType.Int:
                    dataType = "int";
                    break;
                case SqlDataType.Money:
                    dataType = "decimal";
                    break;
                case SqlDataType.NChar:
                    dataType = "string";
                    break;
                case SqlDataType.NText:
                    dataType = "string";
                    break;
                case SqlDataType.NVarChar:
                    dataType = "string";
                    break;
                case SqlDataType.Real:
                    dataType = "Single";
                    break;
                case SqlDataType.SmallDateTime:
                    dataType = "DateTime";
                    break;
                case SqlDataType.SmallInt:
                    dataType = "short";
                    break;
                case SqlDataType.SmallMoney:
                    dataType = "decimal";
                    break;
                case SqlDataType.Text:
                    dataType = "string";
                    break;
                case SqlDataType.Time:
                    dataType = "TimeSpan";
                    break;
                case SqlDataType.Timestamp:
                    dataType = "byte[]";
                    break;
                case SqlDataType.TinyInt:
                    dataType = "byte";
                    break;
                case SqlDataType.UniqueIdentifier:
                    dataType = "Guid";
                    break;
                case SqlDataType.VarBinary:
                    dataType = "byte[]";
                    break;
                case SqlDataType.VarChar:
                    dataType = "string";
                    break;
                case SqlDataType.Variant:
                    dataType = "object";
                    break;
                case SqlDataType.Xml:
                    dataType = "XmlDocument";
                    break;
                case SqlDataType.Geography:
                    dataType = "object";
                    break;
                case SqlDataType.Geometry:
                    dataType = "object";
                    break;
                case SqlDataType.HierarchyId:
                    dataType = "object";
                    break;
                case SqlDataType.None:
                    dataType = "";
                    break;
                case SqlDataType.Numeric:
                    dataType = "decimal";
                    break;

                case SqlDataType.NVarCharMax:
                    dataType = "string";
                    break;
                case SqlDataType.SysName:
                    dataType = "object";
                    break;
                case SqlDataType.UserDefinedTableType:
                    dataType = "object";
                    break;
                case SqlDataType.UserDefinedType:
                    dataType = "object";
                    break;
                case SqlDataType.VarBinaryMax:
                    dataType = "byte[]";
                    break;
                case SqlDataType.VarCharMax:
                    dataType = "string";
                    break;
                default:
                    dataType = "";
                    break;
            }
            return dataType;
        }
        //-------------------------------------------------------------------
        public static SqlDbType GetSqlDbType(SqlDataType type)
        {
            SqlDbType dataType = SqlDbType.NVarChar;
            switch (type)
            {
                 case SqlDataType.BigInt:
                    dataType = SqlDbType.BigInt;
                    break;
                case SqlDataType.Binary:
                    dataType = SqlDbType.Binary;
                    break;
                case SqlDataType.Bit:
                    dataType = SqlDbType.Bit;
                    break;
                case SqlDataType.Char:
                    dataType = SqlDbType.Char;
                    break;
                case SqlDataType.Date:
                    dataType = SqlDbType.Date;
                    break;
                case SqlDataType.DateTime:
                    dataType = SqlDbType.DateTime;
                    break;
                case SqlDataType.DateTime2:
                    dataType = SqlDbType.DateTime2;
                    break;
                case SqlDataType.DateTimeOffset:
                    dataType = SqlDbType.DateTimeOffset;
                    break;
                case SqlDataType.Decimal:
                    dataType = SqlDbType.Decimal;
                    break;
                case SqlDataType.Float:
                    dataType = SqlDbType.Float;
                    break;
                case SqlDataType.Image:
                    dataType = SqlDbType.Image;
                    break;
                case SqlDataType.Int:
                    dataType = SqlDbType.Int;
                    break;
                case SqlDataType.Money:
                    dataType = SqlDbType.Money;
                    break;
                case SqlDataType.NChar:
                    dataType = SqlDbType.NChar;
                    break;
                case SqlDataType.NText:
                    dataType = SqlDbType.NText;
                    break;
                case SqlDataType.NVarChar:
                    dataType = SqlDbType.NVarChar;
                    break;
                case SqlDataType.Real:
                    dataType = SqlDbType.Real;
                    break;
                case SqlDataType.SmallDateTime:
                    dataType = SqlDbType.SmallDateTime;
                    break;
                case SqlDataType.SmallInt:
                    dataType = SqlDbType.SmallInt;
                    break;
                case SqlDataType.SmallMoney:
                    dataType = SqlDbType.SmallMoney;
                    break;
                
                case SqlDataType.Text:
                    dataType = SqlDbType.Text;
                    break;
                case SqlDataType.Time:
                    dataType = SqlDbType.Time;
                    break;
                case SqlDataType.Timestamp:
                    dataType = SqlDbType.Timestamp;
                    break;
                case SqlDataType.TinyInt:
                    dataType = SqlDbType.TinyInt;
                    break;
                
                case SqlDataType.UniqueIdentifier:
                    dataType = SqlDbType.UniqueIdentifier;
                    break;
                case SqlDataType.VarBinary:
                    dataType = SqlDbType.VarBinary;
                    break;
                case SqlDataType.VarChar:
                    dataType = SqlDbType.VarChar;
                    break;
                case SqlDataType.Variant:
                    dataType = SqlDbType.Variant;
                    break;
                case SqlDataType.Xml:
                    dataType = SqlDbType.Xml;
                    break;
                //case SqlDataType.Geography:
                //    dataType = SqlDbType.Geography;
                //    break;
                //case SqlDataType.Geometry:
                //    dataType = SqlDbType.Geometry;
                //    break;
                //case SqlDataType.HierarchyId:
                //    dataType = SqlDbType.HierarchyId;
                //    break;
                //case SqlDataType.None:
                //    dataType = SqlDbType.None;
                //    break;
                //case SqlDataType.Numeric:
                //    dataType = SqlDbType.Numeric;
                //    break;

                case SqlDataType.NVarCharMax:
                    dataType = SqlDbType.NVarChar;
                    break;
                //case SqlDataType.SysName:
                //    dataType = SqlDbType.SysName;
                //    break;
                case SqlDataType.UserDefinedTableType:
                    dataType = SqlDbType.Udt;
                    break;
                case SqlDataType.UserDefinedType:
                    dataType = SqlDbType.Udt;
                    break;
                case SqlDataType.VarBinaryMax:
                    dataType = SqlDbType.VarBinary;
                    break;
                case SqlDataType.VarCharMax:
                    dataType = SqlDbType.VarChar;
                    break;
                //default:
                //    dataType = null;
                //    break;
            }
            return dataType;
        }
        //-------------------------------------------------------------------
        public static Type GetDataType(SqlDataType type)
        {
            Type dataType = null;
            switch (type)
            {
                case SqlDataType.BigInt:
                    dataType = typeof(long);
                    break;
                case SqlDataType.Binary:
                    dataType = typeof(byte[]);
                    break;
                case SqlDataType.Bit:
                    dataType = typeof(bool);
                    break;
                case SqlDataType.Char:
                    dataType = typeof(string);
                    break;
                case SqlDataType.Date:
                    dataType = typeof(DateTime);
                    break;
                case SqlDataType.DateTime:
                    dataType = typeof(DateTime);
                    break;
                case SqlDataType.DateTime2:
                    dataType = typeof(DateTime);
                    break;
                case SqlDataType.DateTimeOffset:
                    dataType = typeof(DateTimeOffset);
                    break;
                case SqlDataType.Decimal:
                    dataType = typeof(decimal);
                    break;
                case SqlDataType.Float:
                    dataType = typeof(double);
                    break;
                case SqlDataType.Image:
                    dataType = typeof(byte[]);
                    break;
                case SqlDataType.Int:
                    dataType = typeof(int);
                    break;
                case SqlDataType.Money:
                    dataType = typeof(decimal);
                    break;
                case SqlDataType.NChar:
                    dataType = typeof(string);
                    break;
                case SqlDataType.NText:
                    dataType = typeof(string);
                    break;
                case SqlDataType.NVarChar:
                    dataType = typeof(string);
                    break;
                case SqlDataType.Real:
                    dataType = typeof(Single);
                    break;
                case SqlDataType.SmallDateTime:
                    dataType = typeof(DateTime);
                    break;
                case SqlDataType.SmallInt:
                    dataType = typeof(short);
                    break;
                case SqlDataType.SmallMoney:
                    dataType = typeof(decimal);
                    break;

                case SqlDataType.Text:
                    dataType = typeof(string);
                    break;
                case SqlDataType.Time:
                    dataType = typeof(TimeSpan);
                    break;
                case SqlDataType.Timestamp:
                    dataType = typeof(byte[]);
                    break;
                case SqlDataType.TinyInt:
                    dataType = typeof(byte);
                    break;

                case SqlDataType.UniqueIdentifier:
                    dataType = typeof(Guid);
                    break;
                case SqlDataType.VarBinary:
                    dataType = typeof(byte[]);
                    break;
                case SqlDataType.VarChar:
                    dataType = typeof(string);
                    break;
                case SqlDataType.Variant:
                    dataType = typeof(object);
                    break;
                case SqlDataType.Xml:
                    dataType = typeof(XmlDocument);
                    break;
                case SqlDataType.Geography:
                    dataType = typeof(object);
                    break;
                case SqlDataType.Geometry:
                    dataType = typeof(object);
                    break;
                case SqlDataType.HierarchyId:
                    dataType = typeof(object);
                    break;
                case SqlDataType.None:
                    dataType = null;
                    break;
                case SqlDataType.Numeric:
                    dataType = typeof(decimal);
                    break;

                case SqlDataType.NVarCharMax:
                    dataType = typeof(string);
                    break;
                case SqlDataType.SysName:
                    dataType = typeof(object);
                    break;
                case SqlDataType.UserDefinedTableType:
                    dataType = typeof(object);
                    break;
                case SqlDataType.UserDefinedType:
                    dataType = typeof(object);
                    break;
                case SqlDataType.VarBinaryMax:
                    dataType = typeof(byte[]);
                    break;
                case SqlDataType.VarCharMax:
                    dataType = typeof(string);
                    break;
                default:
                    dataType = null;
                    break;
            }
            return dataType;
        }

        public static bool IsMatchIdentifier(string Identifier,CustomColumn c)
        { 
            int identifierIndex=c.NameInDatabase.IndexOf(Identifier);
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

       
        public static string AddSpacesToSentence(string text)
        {
            return AddSpacesToSentence(text, true);
        }
        public static string AddSpacesToSentence(string text, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        public static string GetFriendlyName(string text)
        {
            return GetFriendlyName(text, true);
        }
        public static string GetFriendlyName(string text, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    {
                        newText.Append(' ');
                        newText.Append(text[i].ToString().ToLower());
                    }
                    else {
                        newText.Append(text[i]);
                    }
            }
            return newText.ToString();
        }
        public static bool IsNumericType(Type t)
        {
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsDecimalType(Type t)
        {
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static EnumInputType GetElementInputType(CustomColumn column)
        {
            if (column.InPrimaryKey)
            {
                return EnumInputType.hidden;
            }
            else if (column.IsForeignKey)
            {
                return EnumInputType.select;
            }
            else
            if (column.SqlDataType == SqlDataType.Text|| column.SqlDataType == SqlDataType.NText)
            {
                return EnumInputType.textarea;
            }
            else if (column.DotNetType == typeof(System.Enum))
            {
                return EnumInputType.select;
            }
            else if (column.DotNetType == typeof(System.DateTime))
            {
                return EnumInputType.date;
            }
            else if (column.DotNetType == typeof(System.String))
            {
                return GetStringElementType(column);
            }
            else if (Globals.IsNumericType(column.DotNetType))
            {
                return EnumInputType.number;
            }
            else if (column.DotNetType == typeof(System.Boolean))
            {
                return EnumInputType.checkbox;
            }
            else
            {
                return EnumInputType.text;
            }
        }

        public static EnumInputType GetStringElementType(CustomColumn column)
        {

            if (column.ProgramatlyName.ToLower().EndsWith("url"))
            {
                return EnumInputType.url;
            }
            else if (column.ProgramatlyName.ToLower().EndsWith("mobile"))
            {
                return EnumInputType.tel;
            }
            else if (column.ProgramatlyName.ToLower().EndsWith("email"))
            {
                return EnumInputType.email;
            }
            else if (column.ProgramatlyName.ToLower().EndsWith("date"))
            {
                return EnumInputType.date;
            }
            else if (column.ProgramatlyName.ToLower().IndexOf("date") > -1)
            {
                return EnumInputType.date;
            }
            else if (column.ProgramatlyName.ToLower().EndsWith("password"))
            {
                return EnumInputType.password;
            }
            else if (column.MaxLength > 0 && column.MaxLength > 150)
            {
                return EnumInputType.textarea;
            }
            else
            {
                return EnumInputType.text;
            }
        }




    }
}
