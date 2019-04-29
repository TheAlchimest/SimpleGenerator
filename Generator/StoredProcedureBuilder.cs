using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMOSampleInConsol.IO;

namespace SMOSampleInConsol
{
    class StoredProcedureBuilder
    {
        #region --------------CurrentTable--------------
        //------------------------------------------------------------------------------------------------------
        //CurrentTable
        //--------------------------------------------------------------------
        private CustomTable _CurrentTable;
        /// <summary>
        /// Gets or sets entity CurrentTable. 
        /// </summary>
        public CustomTable CurrentTable
        {
            get { return _CurrentTable; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        public static void Create(CustomTable t)
        {
            StoredProcedureBuilder sp = new StoredProcedureBuilder(t);
            string proc = sp.GenerateInsertProcedure();
            WriteStoredProcedure(proc);
            proc += sp.GenerateUpdatePrcedure();
            proc += sp.GenerateDeletePrcedure();
            proc += sp.GenerateGet();
            if (sp.CurrentTable.ID != null)
            {
            proc += sp.GenerateGetPageByPage();
            }
            FileManager.SaveFile(".Sql", sp.CurrentTable.PathOfStoredProcerduresFile, proc);
            //WriteStoredProcedure(proc);
        }

        public StoredProcedureBuilder(CustomTable t)
        { _CurrentTable = t; }

        //---------------------------------
        private string GenerateInsertProcedure()
        {
            StringBuilder procedureStatement = new StringBuilder();
            try
            {
                
                StringBuilder parametersDecleration = new StringBuilder();
                StringBuilder insertColumns = new StringBuilder();
                StringBuilder insertParameters = new StringBuilder();
                StringBuilder outPutParametersValue = new StringBuilder();
                for (int i = 0; i < CurrentTable.Columns.Count; i++)
                {
                    CustomColumn c=CurrentTable.Columns[i];
                    if (!c.NotPassedToSql)
                    {
                        parametersDecleration.AppendFormat("\n\t@{0} {1}", new string[] { c.ProgramatlyName, c.SqlDbTypeString });
                        switch (c.SqlDataType)
                        {
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.Binary:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.Char:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.NChar:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.NVarChar:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.VarBinary:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.VarChar:
                                parametersDecleration.AppendFormat("({0})", c.MaxLength);
                                break;
                        }
                        if (c.IsNullable)
                        {
                            parametersDecleration.Append(" = NULL");
                        }
                        if (c.IsOutoutIninsert)
                        {
                            parametersDecleration.AppendFormat(" out");
                            outPutParametersValue.AppendFormat("\n\tSet @{0}  = @@Identity", c.ProgramatlyName);
                        }
                        else
                        {
                            insertColumns.AppendFormat("\n\t\t[{0}]", c.NameInDatabase);
                            insertParameters.AppendFormat("\n\t\t@{0}", c.ProgramatlyName);
                            if (i < CurrentTable.Columns.Count - 1)
                            {
                                insertColumns.Append(",");
                                insertParameters.Append(",");
                            }
                        }

                        
                        if (i < CurrentTable.Columns.Count - 1)
                        {
                            parametersDecleration.Append(",");
                        }
                       
                    }
                }


                procedureStatement.AppendFormat("\n\nCREATE PROCEDURE {0}_Create", CurrentTable.ProgramatlyName);
                procedureStatement.Append(parametersDecleration.ToString());
                procedureStatement.Append("\nAS");
                procedureStatement.AppendFormat("\n\tINSERT INTO {0}.[{1}] ",AppConfig.Instance.dbOwner, CurrentTable.NameInDatabase);
                procedureStatement.Append("\n\t(");
                procedureStatement.Append(insertColumns.ToString());
                procedureStatement.Append("\n\t)");
                procedureStatement.AppendFormat("\n\tValues({0})", insertParameters.ToString());
                procedureStatement.AppendFormat("\n\n\t{0}", outPutParametersValue.ToString());
                procedureStatement.Append("\nGO");

            }
            catch (Exception ex)
            {
                //MessageBox.Show("My Generated Code Exception:" + ex.Message);
                Console.WriteLine("My Generated Code Exception:" + ex.Message);

            }
            return procedureStatement.ToString();

        }
        //---------------------------------
        private string GenerateUpdatePrcedure()
        {
            StringBuilder procedureStatement = new StringBuilder();
            try
            {
               
                StringBuilder parametersDecleration = new StringBuilder();
                StringBuilder parameters = new StringBuilder();
                StringBuilder whereParameter = new StringBuilder();
                for (int i = 0; i < CurrentTable.Columns.Count; i++)
                {
                    CustomColumn c = CurrentTable.Columns[i];
                    if (!c.NotPassedToSql)
                    {
                        parametersDecleration.AppendFormat("\n\t@{0} {1}", new string[] { c.ProgramatlyName, c.SqlDbTypeString });
                        switch (c.SqlDataType)
                        {
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.Binary:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.Char:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.NChar:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.NVarChar:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.VarBinary:
                            case Microsoft.SqlServer.Management.Smo.SqlDataType.VarChar:
                                parametersDecleration.AppendFormat("({0})", c.MaxLength);
                                break;
                        }

                        if (c.IsNullable)
                        {
                            parametersDecleration.Append(" = NULL");
                        }

                        if (c.InPrimaryKey)
                        {
                            whereParameter.AppendFormat("\n\tWhere [{0}]  = @{1}", c.NameInDatabase, c.ProgramatlyName);
                        }
                        else
                        {
                            parameters.AppendFormat("\n\t\t[{0}]=@{1}", c.NameInDatabase, c.ProgramatlyName);
                            if (i < CurrentTable.Columns.Count - 1)
                            {
                                parameters.Append(",");
                            }
                        }

                        
                        if (i < CurrentTable.Columns.Count - 1)
                        {
                            parametersDecleration.Append(",");
                        }
                    }
                }



                procedureStatement.AppendFormat("\n\nCREATE PROCEDURE {0}_Update", CurrentTable.ProgramatlyName);
                procedureStatement.Append(parametersDecleration.ToString());
                procedureStatement.Append("\nAS");
                procedureStatement.AppendFormat("\n\tUPDATE {0}.[{1}] ", AppConfig.Instance.dbOwner, CurrentTable.NameInDatabase);
                procedureStatement.AppendFormat("\n\tSET");
                procedureStatement.AppendFormat("\n{0}", parameters.ToString());
                procedureStatement.AppendFormat("\n\n\t{0}", whereParameter.ToString());
                procedureStatement.Append("\nGO");

            }
            catch (Exception ex)
            {
                //MessageBox.Show("My Generated Code Exception:" + ex.Message);
                Console.WriteLine("My Generated Code Exception:" + ex.Message);

            }
            return procedureStatement.ToString();

        }
        //---------------------------------
        private string GenerateDeletePrcedure()
        {
            StringBuilder procedureStatement = new StringBuilder();
            try
            {
                
                StringBuilder parametersDecleration = new StringBuilder();
                StringBuilder whereParameter = new StringBuilder();

                    CustomColumn c = CurrentTable.ID;
                    if (c != null)
                    {
                        parametersDecleration.AppendFormat("\n\t@{0} {1}", new string[] { c.ProgramatlyName, c.SqlDbTypeString });
                        whereParameter.AppendFormat("\n\tWhere [{0}]  = @{1}", c.NameInDatabase, c.ProgramatlyName);
                            switch (c.SqlDataType)
                            {
                                case Microsoft.SqlServer.Management.Smo.SqlDataType.Binary:
                                case Microsoft.SqlServer.Management.Smo.SqlDataType.Char:
                                case Microsoft.SqlServer.Management.Smo.SqlDataType.NChar:
                                case Microsoft.SqlServer.Management.Smo.SqlDataType.NVarChar:
                                case Microsoft.SqlServer.Management.Smo.SqlDataType.VarBinary:
                                case Microsoft.SqlServer.Management.Smo.SqlDataType.VarChar:
                                    parametersDecleration.AppendFormat("({0})", c.MaxLength);
                                    break;
                            }
                    }



                    procedureStatement.AppendFormat("\n\nCREATE PROCEDURE {0}_Delete", CurrentTable.ProgramatlyName);
                procedureStatement.Append(parametersDecleration.ToString());
                procedureStatement.Append("\nAS");
                procedureStatement.AppendFormat("\n\tDELETE FROM {0}.[{1}] ", AppConfig.Instance.dbOwner, CurrentTable.NameInDatabase);
                procedureStatement.Append(whereParameter.ToString());
                procedureStatement.Append("\nGO");

            }
            catch (Exception ex)
            {
                //MessageBox.Show("My Generated Code Exception:" + ex.Message);
                Console.WriteLine("My Generated Code Exception:" + ex.Message);

            }
            return procedureStatement.ToString();

        }
        //---------------------------------
        private string GenerateGet()
        {
            StringBuilder procedureStatement = new StringBuilder();
            try
            {
                
                StringBuilder parametersDecleration = new StringBuilder();
                StringBuilder whereParameter = new StringBuilder();

                CustomColumn c = CurrentTable.ID;
                if (c != null)
                {
                    switch (c.SqlDataType)
                    {
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.Binary:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.Char:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.NChar:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.NVarChar:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.VarBinary:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.VarChar:
                            parametersDecleration.AppendFormat("\n\t@{0} {1}({2}) = NULL", new string[] { c.ProgramatlyName, c.SqlDbTypeString, c.MaxLength.ToString() });
                            break;
                        default:
                            parametersDecleration.AppendFormat("\n\t@{0} {1} = NULL", new string[] { c.ProgramatlyName, c.SqlDbTypeString });
                            break;

                    }

                    whereParameter.AppendFormat("\n\tWhere (@{1} IS NULL OR [{0}]  = @{1})", c.NameInDatabase, c.ProgramatlyName);

                }



                procedureStatement.AppendFormat("\n\nCREATE PROCEDURE {0}_Get", CurrentTable.ProgramatlyName);
                procedureStatement.Append(parametersDecleration.ToString());
                procedureStatement.Append("\nAS");
                procedureStatement.AppendFormat("\n\tSELECT * FROM {0}.[{1}] ", AppConfig.Instance.dbOwner, CurrentTable.NameInDatabase);
                procedureStatement.Append(whereParameter.ToString());
                procedureStatement.Append("\nGO");

            }
            catch (Exception ex)
            {
                //MessageBox.Show("My Generated Code Exception:" + ex.Message);
                Console.WriteLine("My Generated Code Exception:" + ex.Message);

            }
            return procedureStatement.ToString();

        }
        //---------------------------------
        //---------------------------------
        private string GenerateGetPageByPage()
        {
            StringBuilder procedureStatement = new StringBuilder();
            try
            {

                string proc = @"
Create PROCEDURE {2}
	@PageIndex int,
	@PageSize  int,
	@SortName  nvarchar(128) =null,
	@SortOrder  nvarchar(128) =null,
	@QType  nvarchar(128) =null,
	@Query  nvarchar(128) =null,
	@TotalRecords   int out
As
BEGIN
	/***************************************************************************************************************/
													/*Conditions Regions*/
	/***************************************************************************************************************/
	declare @Conditions varchar(Max) 
	declare @OrderByStatement varchar(Max) 
	Set @Conditions =' (0=0) ';
	Set @OrderByStatement =' ';
	----------------------------------------------------------------------------------------------------------
	IF @QType is not null And @Query is not null 
		Set @Conditions = @Conditions + ' And  ( '+@QType+' Like ''%'+@Query+''' )'
	/***************************************************************************************************************/
													/*Order BY Regions*/
	/***************************************************************************************************************/
	IF @SortName is not null and @SortOrder is not null 
		Set @OrderByStatement = ' ORDER BY  '+@SortName+' '+ @SortOrder

	/***************************************************************************************************************/
													/*Sql Statement*/
	/***************************************************************************************************************/
	
	declare @SqlStatement nvarchar(Max)
	----------------------------------------------------------
	DECLARE @PageLowerBound int
	SET @PageIndex = @PageIndex-1
	DECLARE @PageUpperBound int
	SET @PageLowerBound = @PageSize * @PageIndex
	SET @PageUpperBound = @PageSize - 1 + @PageLowerBound
	----------------------------------------------------------
	-- Create a temp table TO store the SELECT results
	CREATE TABLE #PageIndexTable
	(
		IndexId int IDENTITY (0, 1) NOT NULL,
		ID {4}
	)
	----------------------------------------------------------
	Set @SqlStatement='-------------------------------------------
	-------------------------------------------
	INSERT INTO #PageIndexTable (ID)
	SELECT {3} FROM [{0}].[{1}] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [{1}].*
	FROM         #PageIndexTable inner join  [{0}].{1}
	on [{0}].{1}.{3} =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
";
                string procedureGetPageByPage = "[dbo].[{0}_GetPageByPage]";
                procedureGetPageByPage = string.Format(procedureGetPageByPage, CurrentTable.ProgramatlyName);

                CustomColumn c = CurrentTable.ID;
                if (c != null)
                {
                     switch (c.SqlDataType)
                    {
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.Binary:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.Char:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.NChar:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.NVarChar:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.VarBinary:
                        case Microsoft.SqlServer.Management.Smo.SqlDataType.VarChar:
                            
                    procedureStatement.AppendFormat(proc, new string[] { AppConfig.Instance.dbOwner, CurrentTable.NameInDatabase, procedureGetPageByPage, c.NameInDatabase,c.SqlDbTypeString+"("+c.MaxLength.ToString()+")" });
                            break;
                        default:
                            procedureStatement.AppendFormat(proc, new string[] { AppConfig.Instance.dbOwner, CurrentTable.NameInDatabase, procedureGetPageByPage, c.NameInDatabase, c.SqlDbTypeString });
                            break;

                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("My Generated Code Exception:" + ex.Message);
                Console.WriteLine("My Generated Code Exception:" + ex.Message);

            }
            return procedureStatement.ToString();

        }
        //---------------------------------
        //---------------------------------
        public static void WriteStoredProcedure(string sql)
        {
            string q = @"SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO";
            q = q + "\n\n" + sql;
            //ProjectBuilder.db.dbObject.ExecuteNonQuery(q);
        }
        //----------------------------------
         
    }
    
}
