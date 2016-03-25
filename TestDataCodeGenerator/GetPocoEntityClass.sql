-- =============================================
-- Author: Tapan Kumar, Alexander Kuperman
-- Create date: 06/02/2015
-- Description: Generates a custom Entity Class 
--	for TestDataFramework from the input table
-- =============================================
DECLARE @TableName AS SYSNAME = '@@@TableName'	-- This is a token that gets replaced in the tool
DECLARE @PrintableTableName VARCHAR(MAX) = Object_Name(Object_ID(@TableName));

DECLARE @TypeResult VARCHAR(MAX) = 'using System;
using TestDataFramework;

namespace @@@NameSpace
{
  public class ' + @PrintableTableName + '
  {'

SELECT @TypeResult = @TypeResult +
	'
    public ' + ColumnType + NullableSign + ' ' + ColumnName + ' { get; set; }
	'
FROM
(
    SELECT 
		col.name OriginalColumnName,
        
        REPLACE(col.name, ' ', '_') ColumnName,
        
        col.column_id ColumnId,
        
        CASE typ.name 
            WHEN 'bigint' THEN 'long'
            WHEN 'binary' THEN 'byte[]'
            WHEN 'bit' THEN 'bool'
            WHEN 'char' THEN 'string'
            WHEN 'date' THEN 'DateTime'
            WHEN 'datetime' THEN 'DateTime'
            WHEN 'datetime2' THEN 'DateTime'
            WHEN 'datetimeoffset' THEN 'DateTimeOffset'
            WHEN 'decimal' THEN 'decimal'
            WHEN 'float' THEN 'float'
            WHEN 'image' THEN 'byte[]'
            WHEN 'int' THEN 'int'
            WHEN 'money' THEN 'decimal'
            WHEN 'nchar' THEN 'char'
            WHEN 'ntext' THEN 'string'
            WHEN 'numeric' THEN 'decimal'
            WHEN 'nvarchar' THEN 'string'
            WHEN 'real' THEN 'double'
            WHEN 'smalldatetime' THEN 'DateTime'
            WHEN 'smallint' THEN 'short'
            WHEN 'smallmoney' THEN 'decimal'
            WHEN 'text' THEN 'string'
            WHEN 'time' THEN 'TimeSpan'
            WHEN 'timestamp' THEN 'DateTime'
            WHEN 'tinyint' THEN 'byte'
            WHEN 'uniqueidentifier' THEN 'Guid'
            WHEN 'varbinary' THEN 'byte[]'
            WHEN 'varchar' THEN 'string'
            ELSE 'UNKNOWN_' + typ.name
        END ColumnType,
        
        CASE 
            WHEN typ.name = 'bigint' THEN NULL -- 'long'
            WHEN typ.name = 'binary' THEN NULL  -- 'byte[]'
            WHEN typ.name = 'bit' THEN NULL -- 'bool'
            WHEN typ.name = 'char' THEN NULL -- 'string'
            WHEN typ.name = 'date' THEN NULL -- 'DateTime'
            WHEN typ.name = 'datetime' THEN NULL -- 'DateTime'
            WHEN typ.name = 'datetime2' THEN NULL -- 'DateTime'
            WHEN typ.name = 'datetimeoffset' THEN NULL -- 'DateTimeOffset'
											WHEN typ.name = 'decimal' THEN '[Precision('+CAST(col.scale as nvarchar(100))+')]'
											WHEN typ.name = 'float' THEN NULL 
            WHEN typ.name = 'image' THEN NULL -- 'byte[]'
            WHEN typ.name = 'int' THEN NULL -- 'int'
											WHEN typ.name = 'money' THEN NULL 
            WHEN typ.name = 'nchar' THEN NULL -- 'char'
											WHEN typ.name = 'ntext' THEN NULL 
											WHEN typ.name = 'numeric' THEN '[Precision('+CAST(col.scale as nvarchar(100))+')]'
											WHEN typ.name = 'nvarchar' THEN '[StringLength('+CAST(CASE col.max_length WHEN -1 THEN 50 ELSE col.max_length/2 END as nvarchar(100))+')]'
            WHEN typ.name = 'real' THEN NULL -- 'double'
            WHEN typ.name = 'smalldatetime' THEN NULL -- 'DateTime'
            WHEN typ.name = 'smallint' THEN NULL -- 'short'
											WHEN typ.name = 'smallmoney' THEN '[Precision('+CAST(col.scale as nvarchar(100))+')]'
											WHEN typ.name = 'text' THEN NULL 
            WHEN typ.name = 'time' THEN NULL -- 'TimeSpan'
            WHEN typ.name = 'timestamp' THEN NULL -- 'DateTime'
            WHEN typ.name = 'tinyint' THEN NULL -- 'byte'
            WHEN typ.name = 'uniqueidentifier' THEN NULL -- 'Guid'
            WHEN typ.name = 'varbinary' THEN NULL -- 'byte[]'
											WHEN typ.name = 'varchar' THEN '[StringLength('+CAST(CASE col.max_length WHEN -1 THEN 50 ELSE col.max_length END as nvarchar(100))+')]'
            ELSE NULL 
        END Attribute,
        
        CASE 
            WHEN col.is_nullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            THEN '?' 
            ELSE '' 
        END NullableSign,
        
        CASE fkcol.parent_object_id
			WHEN NULL THEN NULL 
			ELSE '[ForeignKey("'+sch.name+'", "'+OBJECT_NAME(fkcol.referenced_object_id)+'", "'+COL_NAME(fkcol.referenced_object_id, fkcol.referenced_column_id)+'"'+')]' 
        END ForeignKeyAttribute,
        
        CASE idx.is_primary_key
			WHEN 1 THEN '[PrimaryKey('+CASE col.is_identity WHEN 1 THEN 'PrimaryKeyAttribute.KeyTypeEnum.Auto' ELSE 'PrimaryKeyAttribute.KeyTypeEnum.Manual' END+')]'
			ELSE NULL
		END PrimaryKeyAttribute
        
	from sys.columns col with (nolock)
		join sys.tables tbl with (nolock) ON
			col.object_id = tbl.object_id
		join sys.schemas sch with (nolock) ON
			tbl.schema_id = sch.schema_id
		join sys.types typ with (nolock) ON
			col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id	
		left join sys.foreign_key_columns fkcol with (nolock) ON
			col.object_id = fkcol.parent_object_id AND col.column_id = fkcol.parent_column_id
		left join sys.index_columns AS idx_col with (nolock) ON
			col.object_id = idx_col.object_id AND col.column_id = idx_col.column_id
		left join sys.indexes idx with (nolock) ON
			idx_col.object_id = idx.object_id AND idx_col.index_id = idx.index_id AND idx.is_primary_key = 1
				
	where tbl.object_id = OBJECT_ID(@TableName)
			
) t

SET @TypeResult = @TypeResult  + '
  }
}'

DECLARE @CodeResult VARCHAR(MAX) = '';

SELECT @CodeResult = @CodeResult + 
	
	CASE WHEN 
	Attribute IS NOT NULL OR 
	OriginalColumnName != ColumnName OR
	ForeignKeyAttribute IS NOT NULL OR
	PrimaryKeyAttribute IS NOT NULL 

	THEN

		CASE WHEN Attribute IS NOT NULL THEN 
		'
				.AddAttributeToMember(m => m.' + ColumnName + ', new ' + Attribute + ')' ELSE '' END + 
	
		CASE WHEN OriginalColumnName != ColumnName THEN 
		'
				.AddAttributeToMember(m => m.' + ColumnName + ', new ColumnAttribute("' + OriginalColumnName + '")' ELSE '' END +
	
		CASE WHEN ForeignKeyAttribute IS NOT NULL THEN 
		'
				.AddAttributeToMember(m => m.' + ColumnName + ', new ' + ForeignKeyAttribute + ')' ELSE '' END + 

		CASE WHEN PrimaryKeyAttribute IS NOT NULL THEN 
		'
				.AddAttributeToMember(m => m.' + ColumnName + ', new ' + PrimaryKeyAttribute + ')' ELSE '' END

	ELSE ''

	END

FROM
(
    SELECT 
		col.name OriginalColumnName,
        
        REPLACE(col.name, ' ', '_') ColumnName,
        
        col.column_id ColumnId,
        
        CASE 
			-- keeping the unsused entries here for future just in case
            WHEN typ.name = 'bigint' THEN NULL -- 'long'
            WHEN typ.name = 'binary' THEN NULL  -- 'byte[]'
            WHEN typ.name = 'bit' THEN NULL -- 'bool'
            WHEN typ.name = 'char' THEN NULL -- 'string'
            WHEN typ.name = 'date' THEN NULL -- 'DateTime'
            WHEN typ.name = 'datetime' THEN NULL -- 'DateTime'
            WHEN typ.name = 'datetime2' THEN NULL -- 'DateTime'
            WHEN typ.name = 'datetimeoffset' THEN NULL -- 'DateTimeOffset'
											WHEN typ.name = 'decimal' THEN 'PrecisionAttribute('+CAST(col.scale as nvarchar(100))+')'
											WHEN typ.name = 'float' THEN NULL 
            WHEN typ.name = 'image' THEN NULL -- 'byte[]'
            WHEN typ.name = 'int' THEN NULL -- 'int'
											WHEN typ.name = 'money' THEN NULL 
            WHEN typ.name = 'nchar' THEN NULL -- 'char'
											WHEN typ.name = 'ntext' THEN NULL 
											WHEN typ.name = 'numeric' THEN 'PrecisionAttribute('+CAST(col.scale as nvarchar(100))+')'
											WHEN typ.name = 'nvarchar' THEN 'StringLengthAttribute('+CAST(CASE col.max_length WHEN -1 THEN 50 ELSE col.max_length/2 END as nvarchar(100))+')'
            WHEN typ.name = 'real' THEN NULL -- 'double'
            WHEN typ.name = 'smalldatetime' THEN NULL -- 'DateTime'
            WHEN typ.name = 'smallint' THEN NULL -- 'short'
											WHEN typ.name = 'smallmoney' THEN 'PrecisionAttribute('+CAST(col.scale as nvarchar(100))+')'
											WHEN typ.name = 'text' THEN NULL 
            WHEN typ.name = 'time' THEN NULL -- 'TimeSpan'
            WHEN typ.name = 'timestamp' THEN NULL -- 'DateTime'
            WHEN typ.name = 'tinyint' THEN NULL -- 'byte'
            WHEN typ.name = 'uniqueidentifier' THEN NULL -- 'Guid'
            WHEN typ.name = 'varbinary' THEN NULL -- 'byte[]'
											WHEN typ.name = 'varchar' THEN 'StringLengthAttribute('+CAST(CASE col.max_length WHEN -1 THEN 50 ELSE col.max_length END as nvarchar(100))+')'
            ELSE NULL 
        END Attribute,
        
        CASE fkcol.parent_object_id
			WHEN NULL THEN NULL 
			ELSE 'ForeignKeyAttribute("'+sch.name+'", "'+OBJECT_NAME(fkcol.referenced_object_id)+'", "'+COL_NAME(fkcol.referenced_object_id, fkcol.referenced_column_id)+'"'+')' 
        END ForeignKeyAttribute,
        
        CASE idx.is_primary_key
			WHEN 1 THEN 'PrimaryKeyAttribute('+CASE col.is_identity WHEN 1 THEN 'PrimaryKeyAttribute.KeyTypeEnum.Auto' ELSE 'PrimaryKeyAttribute.KeyTypeEnum.Manual' END+')'
			ELSE NULL
		END PrimaryKeyAttribute
        
	from sys.columns col with (nolock)
		join sys.tables tbl with (nolock) ON
			col.object_id = tbl.object_id
		join sys.schemas sch with (nolock) ON
			tbl.schema_id = sch.schema_id
		join sys.types typ with (nolock) ON
			col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id	
		left join sys.foreign_key_columns fkcol with (nolock) ON
			col.object_id = fkcol.parent_object_id AND col.column_id = fkcol.parent_column_id
		left join sys.index_columns AS idx_col with (nolock) ON
			col.object_id = idx_col.object_id AND col.column_id = idx_col.column_id
		left join sys.indexes idx with (nolock) ON
			idx_col.object_id = idx.object_id AND idx_col.index_id = idx.index_id AND idx.is_primary_key = 1
				
	where tbl.object_id = OBJECT_ID(@TableName)
			
) t

DECLARE @schema VARCHAR(MAX);
SELECT @schema = sch.name from sys.schemas sch
	join sys.tables tbl
	ON tbl.schema_id = sch.schema_id
	where tbl.object_id = OBJECT_ID(@TableName);

DECLARE @TypeCodeResult VARCHAR(MAX) = '
			populator.DecorateType<'+@PrintableTableName+'>()
			.AddAttributeToType(new TableAttribute("' + DB_NAME() + '", "' + @schema + '", "' + @PrintableTableName + '"))';

IF @CodeResult != ''
Set @TypeCodeResult = @TypeCodeResult + @CodeResult;

Set @TypeCodeResult = @TypeCodeResult + ';';

Select @TypeResult, @TypeCodeResult;
