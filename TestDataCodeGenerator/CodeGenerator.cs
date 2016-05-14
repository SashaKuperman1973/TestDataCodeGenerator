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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TestDataCodeGenerator
{
    public partial class CodeGenerator : Form
    {
        public CodeGenerator()
        {
            this.InitializeComponent();
        }

        #region Public Properties

        public string ProfileName
        {
            get
            {
                return this.Text.Length < this.baseTitleLength + 3
                    ? string.Empty
                    : this.Text.Substring(this.baseTitleLength + 3);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.Text = this.Text.Substring(0, this.baseTitleLength);
                    return;
                }

                this.Text = this.Text.Substring(0, this.baseTitleLength) + " - " + value;
            }
        }

        public string ConnectionString => this.connectionStringTextBox.Text;
        public string DefaultNameSpace => this.nameSpaceTextBox.Text;
        public string GeneratedClassName => this.programmaticAttributeDefinitionClassNameTextBox.Text;
        public string OutputFolder => this.outputFolderTextBox.Text;
        public GenerationOption GenerationOption
            => this.programmaticAttributesRadioButton.Checked ? GenerationOption.Poco : GenerationOption.Declarative;

        public List<TableRow> TableList
        {
            get
            {
                var result = new List<TableRow>();
                for (int i = 0; i < this.tableNameGridView.Rows.Count - 1; i++)
                {
                    result.Add(new TableRow
                    {
                        NamespaceOverride = (string) this.tableNameGridView["namespaceColumn", i].Value,
                        Schema = (string) this.tableNameGridView["schemaColumn", i].Value,
                        TableName = (string) this.tableNameGridView["tableNameColumn", i].Value,
                    });
                }

                return result;
            }
        }

        #endregion Public Properties

        private int baseTitleLength;

        private void CodeGenerator_Load(object sender, EventArgs e)
        {
            this.baseTitleLength = this.Text.Length;

            this.Closing += this.CodeGenerator_FormClosing;
            this.Shown += this.CodeGenerator_Shown;

            ProfileCollection profileCollection = ProfileStorage.Deserialize();

            if (profileCollection?.LastEnteredProfile == null)
            {
                return;
            }

            this.PopulateScreen(profileCollection.LastEnteredProfile);
        }

        private void CodeGenerator_Shown(object sender, EventArgs e)
        {
            this.Shown -= this.CodeGenerator_Shown;
            this.tableNameGridView.Focus();
        }

        private void CodeGenerator_FormClosing(object sender, CancelEventArgs e)
        {
            this.Closing -= this.CodeGenerator_FormClosing;

            var lastProfile = new Profile
            {
                ProfileName = this.ProfileName,
                ConnectionString = this.connectionStringTextBox.Text,
                OutputFolder = this.outputFolderTextBox.Text,
                GenerationOption = this.declarativeAttributesRadioButton.Checked
                    ? GenerationOption.Declarative
                    : GenerationOption.Poco,
                GeneratedClassName = this.programmaticAttributeDefinitionClassNameTextBox.Text,
                DefaultNameSpace = this.nameSpaceTextBox.Text,
                TableList = this.TableList
            };

            ProfileCollection profileCollection = ProfileStorage.Deserialize() ?? new ProfileCollection();
            profileCollection.LastEnteredProfile = lastProfile;

            ProfileStorage.Serialize(profileCollection);
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

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Clear form?",
                "Clear Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }

            this.connectionStringTextBox.Text = string.Empty;
            this.outputFolderTextBox.Text = string.Empty;

            this.programmaticAttributeDefinitionClassNameTextBox.Text = string.Empty;
            this.nameSpaceTextBox.Text = string.Empty;

            this.tableNameGridView.Rows.Clear();
        }

        private void PopulateScreen(Profile profile)
        {
            this.ProfileName = profile.ProfileName;

            this.connectionStringTextBox.Text = profile.ConnectionString;
            this.outputFolderTextBox.Text = profile.OutputFolder;

            this.declarativeAttributesRadioButton.Checked = profile.GenerationOption == GenerationOption.Declarative;
            this.programmaticAttributesRadioButton.Checked = profile.GenerationOption == GenerationOption.Poco;

            this.programmaticAttributeDefinitionClassNameTextBox.Text = profile.GeneratedClassName;
            this.nameSpaceTextBox.Text = profile.DefaultNameSpace;
            
            this.tableNameGridView.Rows.Clear();

            profile.TableList.ForEach(
                table => this.tableNameGridView.Rows.Add(table.Schema, table.TableName, table.NamespaceOverride, string.Empty)
                );
        }

        private void btnLoadProfile_Click(object sender, EventArgs e)
        {
            using (var loadDialog = new LoadDialog())
            {
                if (loadDialog.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }

                this.PopulateScreen(loadDialog.ProfileToLoad);
                this.ProfileName = loadDialog.ProfileToLoad.ProfileName;
            }
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveDialog())
            {
                saveDialog.ShowDialog(this);
            }
        }

        public static void Delete(string profileName, ProfileCollection profileColllection, ListBox profileListBox)
        {
            if (profileName.Trim() == string.Empty)
            {
                return;
            }

            if (MessageBox.Show(
                $"Delete profile {profileName.Trim()}?",
                "Delete Profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }

            List<Profile> profileList = profileColllection.ProfileList;

            int position =
                profileList.FindIndex(
                    profile =>
                        profile.ProfileName.Equals(profileName.Trim(), StringComparison.InvariantCultureIgnoreCase));

            profileList.RemoveAt(position);

            position = profileListBox.Items.IndexOf(profileName.Trim());
            profileListBox.Items.RemoveAt(position);

            ProfileStorage.Serialize(profileColllection);
        }
    }
}
