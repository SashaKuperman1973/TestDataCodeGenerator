using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TestDataCodeGenerator
{
    public abstract class Output
    {
        protected readonly string OutputFolderPath;
        private readonly string sql;

        protected Output(string outputFolderPath, string sql)
        {
            this.OutputFolderPath = outputFolderPath;
            this.sql = sql;
        }

        protected void WriteEntityToFile(CodeGenerator.TableData tableData, string code)
        {
            string fileName = tableData.TableName.Replace(" ", "_");
            if (!string.IsNullOrWhiteSpace(tableData.Schema))
            {
                fileName = tableData.Schema.Replace(" ", "_") + "." + fileName;
            }

            fileName += ".cs";

            File.WriteAllText(this.OutputFolderPath + @"\" + fileName, code);
        }

        public abstract void WriteOutput(SqlConnection connection, CodeGenerator.TableData tableData);

        protected string GetSql(CodeGenerator.TableData tableData)
        {
            string fullTableName = $"[{tableData.TableName}]";

            if (!string.IsNullOrWhiteSpace(tableData.Schema))
            {
                fullTableName = $"[{tableData.Schema}]." + fullTableName;
            }

            var sqlSb = new StringBuilder(this.sql);
            sqlSb.Replace("@@@TableName", fullTableName);
            sqlSb.Replace("@@@NameSpace", tableData.Namespace);

            return sqlSb.ToString();
        }
    }

    public class EntityOutput : Output
    {
        public EntityOutput(string outputFolderPath, string sql) : base(outputFolderPath, sql)
        {

        }

        public override void WriteOutput(SqlConnection connection, CodeGenerator.TableData tableData)
        {
            string code;
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = this.GetSql(tableData);

                code = (string)command.ExecuteScalar();
            }

            this.WriteEntityToFile(tableData, code);
        }
    }

    public class PocoOutput : Output
    {
        private readonly string fileNameSpace;
        private readonly string fileClass;

        private readonly HashSet<string> namespaceCollection = new HashSet<string>();
        private readonly StringBuilder codeStringBuilder = new StringBuilder();

        public PocoOutput(string outputFolderPath, string sql, string fileNameSpace, string fileClass) : base(outputFolderPath, sql)
        {
            this.fileNameSpace = fileNameSpace;
            this.fileClass = fileClass;

            this.AddInitialNameSpaces();
        }

        private void AddInitialNameSpaces()
        {
            this.namespaceCollection.Add("System");
            this.namespaceCollection.Add("TestDataFramework");
            this.namespaceCollection.Add("TestDataFramework.Populator.Interfaces");
        }

        private void WritePopulationCodeToFile(string code)
        {
            File.WriteAllText($@"{this.OutputFolderPath}\{this.fileClass}.cs", code);
        }

        #region WritePopulationCode

        public string WritePopulationCode()
        {
            var finalStringBuilder = new StringBuilder();

            this.WriteNameSpaces(finalStringBuilder);
            finalStringBuilder.AppendLine();

            this.WriteCodeNameSpace(finalStringBuilder);
            this.WriteCodeClass(finalStringBuilder);

            this.WriteCodeMethod(finalStringBuilder);
            this.WriteCode(finalStringBuilder);
            this.CloseCodeMethod(finalStringBuilder);

            this.CloseCodeClass(finalStringBuilder);
            this.CloseCodeNameSpace(finalStringBuilder);

            return finalStringBuilder.ToString();
        }

        private void WriteNameSpaces(StringBuilder finalStringBuilder)
        {
            this.namespaceCollection.ToList().ForEach(ns => finalStringBuilder.AppendLine($"using {ns};"));
        }

        private void WriteCodeNameSpace(StringBuilder finalStringBuilder)
        {
            finalStringBuilder.AppendLine("namespace " + this.fileNameSpace + "\r\n{");
        }

        private void CloseCodeNameSpace(StringBuilder finalStringBuilder)
        {
            finalStringBuilder.AppendLine("}");
        }

        private void WriteCodeClass(StringBuilder finalStringBuilder)
        {
            finalStringBuilder.AppendLine("\tpublic class " + this.fileClass + "\r\n\t{");
        }

        private void CloseCodeClass(StringBuilder finalStringBuilder)
        {
            finalStringBuilder.AppendLine("\t}");
        }

        private void WriteCodeMethod(StringBuilder finalStringBuilder)
        {
            finalStringBuilder.AppendLine("\t\tpublic static void Decorate(IPopulator populator)\r\n\t\t{");
        }

        private void CloseCodeMethod(StringBuilder finalStringBuilder)
        {
            finalStringBuilder.AppendLine("\t\t}");
        }

        private void WriteCode(StringBuilder finalStringBuilder)
        {
            finalStringBuilder.Append(this.codeStringBuilder);
        }

        #endregion WritePopulationCode

        public override void WriteOutput(SqlConnection connection, CodeGenerator.TableData tableData)
        {
            string typeResult, codeResult;

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = this.GetSql(tableData);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    typeResult = reader.GetString(0);
                    codeResult = reader.GetString(1);
                }
            }

            this.WriteEntityToFile(tableData, typeResult);
            this.AddCodeResult(codeResult, tableData);

            string code = this.WritePopulationCode();
            this.WritePopulationCodeToFile(code);
        }

        private void AddCodeResult(string codeResult, CodeGenerator.TableData tableData)
        {
            if (!tableData.Namespace.Equals(this.fileNameSpace))
            {
                this.namespaceCollection.Add(tableData.Namespace);
            }

            this.codeStringBuilder.AppendLine(codeResult);
        }
    }
}
