/*
    Copyright 2016 Alexander Kuperman

    This file is part of TestDataCodeGenerator.

    TestDataCodeGenerator is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    TestDataCodeGenerator is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with TestDataFramework.  If not, see <http://www.gnu.org/licenses/>.
*/

-- =============================================
-- Author: Tapan Kumar, Alexander Kuperman
-- Create date: 13/05/2016
-- Description: Generates a custom Entity Class 
--	for TestDataFramework from the input table
-- =============================================

-- String tokens that start with @@@ get replaced in the tool

DECLARE @TableName AS SYSNAME = '@@@TableName';
DECLARE @PrintableTableName VARCHAR(MAX) = Object_Name(Object_ID(@TableName));

DECLARE @Result VARCHAR(MAX) = 'using System;
using TestDataFramework;

namespace @@@NameSpace
{
  [Table("'+DB_NAME()+'", "'+OBJECT_SCHEMA_NAME(OBJECT_ID(@TableName))+'", "'+OBJECT_NAME(OBJECT_ID(@TableName))+'")]
  public class ' + @PrintableTableName + '
  {'

SELECT @Result = @Result +

	CASE WHEN Attribute IS NOT NULL THEN 
	'
	' + Attribute ELSE '' END + 
	
	CASE WHEN OriginalColumnName != ColumnName THEN 
	'
	[Column("'+OriginalColumnName+'")]' ELSE '' END +
	
	CASE WHEN ForeignKeyAttribute IS NOT NULL THEN 
	'
	' + ForeignKeyAttribute ELSE '' END +

	CASE WHEN PrimaryKeyAttribute IS NOT NULL THEN 
	'
	' + PrimaryKeyAttribute ELSE '' END +

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

SET @Result = @Result  + '
  }
}'

Select @Result
