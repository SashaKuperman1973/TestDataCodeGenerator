using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDataCodeGenerator
{
    public partial class CodeGenerator : Form
    {
        public CodeGenerator()
        {
            this.InitializeComponent();
        }

        private void browseOutputFolderButton_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.outputFolderTextBox.Text = this.folderBrowserDialog.SelectedPath;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            this.progressBar.Value = 0;

            bool isInputValid;
            // Tuple is tableName, schema
            List<TableData> fullTableNameList = this.GetTableNameList(out isInputValid);

            if (!isInputValid)
            {
                MessageBox.Show("Input error. See comments beside inputs.", "Input error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(
                "Output file names are based on schema and table names. This will overwrite existing files in the output folder! Continue?",
                "Run Generator", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            this.Enabled = false;

            try
            {
                this.GenerateOutput(fullTableNameList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(CodeGenerator.GetExcpetionMessage(ex), "Exception occurred", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            this.Enabled = true;
        }

        private static string GetExcpetionMessage(Exception ex)
        {
            string result = ex.Message;

            if (ex.InnerException != null)
            {
                result += " -- " + CodeGenerator.GetExcpetionMessage(ex.InnerException);
            }

            return result;
        }

        public class TableData
        {
            public string TableName { get; set; }
            public string Schema { get; set; }
            public string Namespace { get; set; }
        }

        // Tuple is tableName, schema
        private List<TableData> GetTableNameList(out bool isInputValid)
        {
            isInputValid = true;

            if (string.IsNullOrWhiteSpace(this.connectionStringTextBox.Text))
            {
                this.connectionStringErrorLabel.Text = "required";
                this.connectionStringErrorLabel.ForeColor = Color.DarkBlue;

                isInputValid = false;
            }
            else
            {
                this.connectionStringErrorLabel.Text = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(this.outputFolderTextBox.Text))
            {
                this.outputFolderErrorLabel.Text = "required";
                this.outputFolderErrorLabel.ForeColor = Color.DarkBlue;

                isInputValid = false;
            }
            else
            {
                this.outputFolderErrorLabel.Text = string.Empty;
            }

            var fullTableNameList = new List<TableData>();

            for (int i = 0; i < this.tableNameGridView.Rows.Count - 1; i++)
            {
                string schema = ((string)this.tableNameGridView[0, i].Value)?.Trim();
                string tableName = ((string)this.tableNameGridView[1, i].Value)?.Trim();
                string nameSpace = ((string)this.tableNameGridView[2, i].Value)?.Trim();

                var errorCell = (DataGridViewTextBoxCell)this.tableNameGridView[2, i];
                errorCell.Style = new DataGridViewCellStyle { ForeColor = Color.DarkBlue };

                if (string.IsNullOrWhiteSpace(tableName))
                {
                    errorCell.Value = "Table name required";

                    isInputValid = false;
                    continue;
                }

                bool error = false;

                error |= schema?.Contains("[") ?? false;
                error |= schema?.Contains("]") ?? false;
                error |= tableName.Contains("[");
                error |= tableName.Contains("]");
                error |= schema?.Contains(".") ?? false;
                error |= tableName.Contains(".");

                if (error)
                {
                    errorCell.Value = "No brackets or periods allowed";

                    isInputValid = false;
                    continue;
                }

                string actualNameSpace = string.IsNullOrWhiteSpace(nameSpace)
                    ? this.nameSpaceTextBox.Text?.Trim()
                    : nameSpace;

                if (string.IsNullOrWhiteSpace(actualNameSpace))
                {
                    errorCell.Value = "namespace required";

                    isInputValid = false;
                    continue;
                }

                errorCell.Value = null;

                fullTableNameList.Add(new TableData { TableName = tableName, Schema = schema, Namespace = actualNameSpace});
            }

            return fullTableNameList;
        }

        private void GenerateOutput(IReadOnlyCollection<TableData> fullTableNameList)
        {
            this.progressBar.Maximum = fullTableNameList.Count;
            this.progressBar.Minimum = 0;

            using (var connection = new SqlConnection(this.connectionStringTextBox.Text))
            {
                connection.Open();

                foreach (TableData tableData in fullTableNameList)
                {
                    string fullTableName = $"[{tableData.TableName}]";

                    if (!string.IsNullOrWhiteSpace(tableData.Schema))
                    {
                        fullTableName = $"[{tableData.Schema}]." + fullTableName;
                    }

                    var sql = new StringBuilder(Properties.Resources.GetEntityClass);

                    sql.Replace("@@@TableName", fullTableName);
                    sql.Replace("@@@NameSpace", tableData.Namespace);

                    string code;
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql.ToString();

                        code = (string)command.ExecuteScalar();
                    }

                    string fileName = tableData.TableName.Replace(" ", "_");
                    if (!string.IsNullOrWhiteSpace(tableData.Schema))
                    {
                        fileName = tableData.Schema.Replace(" ", "_") + "." + fileName;
                    }

                    fileName += ".cs";

                    File.WriteAllText(this.outputFolderTextBox.Text + @"\" + fileName, code);

                    this.progressBar.Value++;
                }
            }
        }
    }
}
