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

            Context context;
            List<TableData> fullTableNameList = this.GetTableNameList(out context);

            if (!context.IsInputValid)
            {
                MessageBox.Show("Input error. See comments beside inputs.", "Input error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(
                "Output file names are based on schema and table names.\r\n\r\nThis will overwrite existing files with same output names in the output folder!\r\n\r\nContinue?",
                "Run Generator", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }

            this.Enabled = false;

            try
            {
                this.GenerateEntityOutput(fullTableNameList, context.Output);
                context.Output.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(CodeGenerator.GetExceptionMessage(ex), "Exception occurred", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                this.progressBar.Value = 0;
            }

            this.Enabled = true;
        }

        private static string GetExceptionMessage(Exception ex)
        {
            string result = ex.Message;

            if (ex.InnerException != null)
            {
                result += " -- " + CodeGenerator.GetExceptionMessage(ex.InnerException);
            }

            return result;
        }

        public class TableData
        {
            public string TableName { get; set; }
            public string Schema { get; set; }
            public string Namespace { get; set; }

            public override string ToString()
            {
                string result = $"TableName: {this.TableName}, Schema: {this.Schema}, Namespace: {this.Namespace}";
                return result;
            }
        }

        public class Context
        {
            public Output Output { get; set; }
            public bool IsInputValid { get; set; }
        }

        private List<TableData> GetTableNameList(out Context context)
        {
            bool isInputValid = true;

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

            if (this.programmaticAttributesRadioButton.Checked &&
                string.IsNullOrWhiteSpace(this.programmaticAttributeDefinitionClassNameTextBox.Text))
            {
                this.classNameErrorLabel.Text = "required";
                this.outputFolderErrorLabel.ForeColor = Color.DarkBlue;

                isInputValid = false;
            }
            else
            {
                this.outputFolderErrorLabel.Text = string.Empty;
            }

            if (this.programmaticAttributesRadioButton.Checked && string.IsNullOrWhiteSpace(this.nameSpaceTextBox.Text))
            {
                this.nameSpaceErrorLabel.Text = "required";
                this.outputFolderErrorLabel.ForeColor = Color.DarkBlue;

                isInputValid = false;
            }
            else
            {
                this.nameSpaceErrorLabel.Text = string.Empty;
            }

            var fullTableNameList = new List<TableData>();

            for (int i = 0; i < this.tableNameGridView.Rows.Count - 1; i++)
            {
                string schema = ((string)this.tableNameGridView["schemaColumn", i].Value)?.Trim();
                string tableName = ((string)this.tableNameGridView["tableNameColumn", i].Value)?.Trim();
                string nameSpace = ((string)this.tableNameGridView["namespaceColumn", i].Value)?.Trim();

                var errorCell = (DataGridViewTextBoxCell)this.tableNameGridView["errorTextColumn", i];
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

            context = new Context {IsInputValid = isInputValid};

            if (!isInputValid) return fullTableNameList;

            if (this.declarativeAttributesRadioButton.Checked)
            {
                context.Output = new EntityOutput(this.outputFolderTextBox.Text, Properties.Resources.GetEntityClass);
            }
            else
            {
                context.Output = new PocoOutput(this.outputFolderTextBox.Text.Trim(),
                    Properties.Resources.GetPocoEntityClass, this.nameSpaceTextBox.Text.Trim(),
                    this.programmaticAttributeDefinitionClassNameTextBox.Text.Trim());
            }

            return fullTableNameList;
        }

        private void GenerateEntityOutput(IReadOnlyCollection<TableData> fullTableNameList, Output output)
        {
            this.progressBar.Maximum = fullTableNameList.Count;
            this.progressBar.Minimum = 0;

            using (var connection = new SqlConnection(this.connectionStringTextBox.Text))
            {
                connection.Open();

                foreach (TableData tableData in fullTableNameList)
                {
                    try
                    {
                        output.WriteOutput(connection, tableData);
                        this.progressBar.Value++;

                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException($"Currently on: {{{tableData}}}", ex);
                    }
                }
            }
        }

        private void programmaticAttributesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.programmaticAttributesRadioButtonLabel.Visible = 
                this.programmaticAttributesRadioButton.Checked;

            this.programmaticAttributeDefinitionClassNameTextBox.Visible =
                this.programmaticAttributesRadioButton.Checked;
        }
    }
}
