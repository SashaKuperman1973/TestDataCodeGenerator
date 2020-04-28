# TestDataCodeGenerator

A somewhat rushed tool for generating classes from MS Sql Server database tables.

Designed to be used with TestDataFramework, which is where the executable is bundled. 
TestDataFramework is available on GitHub and the Nuget Gallery.

Not a lot of error handling. If an exception does occur, it will probably have the name of the table it's trying to access from the database.
You may be able to figure out what's going on from there.

So:

- Make sure your table names are spelled correctly in the target table grid list, with no leading or trailing spaces.

- Make sure your connection string is right.


Allows several tables to be specified at a time, will generate one file per table/class.

It will save the last used form when you close the app and load it on start up. You can also save the form in a named slot at any time (unlimited slots).

Two main options, related to TestDataFramework:

1. Class with Declarative Attributes
- Generates target classes from tables decorated with attributes that TestDataFramework uses.

2. POCO class with generated programmatic attribute code
- Generates plain POCO classes, plus one class that has code to programmatically associate TestDataFramework attributes to the POCO classes.

- Ignore this decorator class and just use the generated POCO classes if you're not interested in working with TestDataFramework and just want a generator.		
		- Alternately roll your own generator from this project.

		
Input fields:

Connection string with default catalogue
	- A valid MS SQL Server connection string
		
Programmmatic Attribute definition class name field	(visible when POCO generation is selected)
	- Name of the static class that encapsulates the attribute assignment code.
	
NameSpace
	- The default namespace for all generated classes.
	
Namespace text column in grid adjacent to Table Name text column
	- Overrides default namespace for that table/class. Leave blank if you want to use default.
