SELECT [sch].[name], [tbl].[name]

FROM sys.tables tbl with (nolock) 
join sys.schemas sch with (nolock) ON
	tbl.schema_id = sch.schema_id

WHERE [tbl].[name] != 'sysdiagrams'

ORDER BY [sch].[name], [tbl].[name]
