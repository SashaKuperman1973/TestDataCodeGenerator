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

namespace TestDataCodeGenerator
{
    partial class CodeGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.connectionStringTextBox = new System.Windows.Forms.TextBox();
            this.outputFolderTextBox = new System.Windows.Forms.TextBox();
            this.tableNameGridView = new System.Windows.Forms.DataGridView();
            this.schemaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.classNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namespaceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runButton = new System.Windows.Forms.Button();
            this.connectionStringErrorLabel = new System.Windows.Forms.Label();
            this.outputFolderErrorLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.nameSpaceTextBox = new System.Windows.Forms.TextBox();
            this.declarativeAttributesRadioButton = new System.Windows.Forms.RadioButton();
            this.programmaticAttributesRadioButton = new System.Windows.Forms.RadioButton();
            this.programmaticAttributesRadioButtonLabel = new System.Windows.Forms.Label();
            this.programmaticAttributeDefinitionClassNameTextBox = new System.Windows.Forms.TextBox();
            this.classNameErrorLabel = new System.Windows.Forms.Label();
            this.nameSpaceErrorLabel = new System.Windows.Forms.Label();
            this.browseOutputFolderButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClearForm = new System.Windows.Forms.Button();
            this.btnLoadProfile = new System.Windows.Forms.Button();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnLoadTables = new System.Windows.Forms.Button();
            this.chkIncludeDbName = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.tableNameGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection string with default catalogue";
            // 
            // connectionStringTextBox
            // 
            this.connectionStringTextBox.Location = new System.Drawing.Point(13, 30);
            this.connectionStringTextBox.Name = "connectionStringTextBox";
            this.connectionStringTextBox.Size = new System.Drawing.Size(1132, 20);
            this.connectionStringTextBox.TabIndex = 1;
            // 
            // outputFolderTextBox
            // 
            this.outputFolderTextBox.Location = new System.Drawing.Point(131, 68);
            this.outputFolderTextBox.Name = "outputFolderTextBox";
            this.outputFolderTextBox.Size = new System.Drawing.Size(1014, 20);
            this.outputFolderTextBox.TabIndex = 2;
            // 
            // tableNameGridView
            // 
            this.tableNameGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableNameGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.schemaColumn,
            this.tableNameColumn,
            this.classNameColumn,
            this.namespaceColumn,
            this.errorTextColumn});
            this.tableNameGridView.Location = new System.Drawing.Point(12, 240);
            this.tableNameGridView.Name = "tableNameGridView";
            this.tableNameGridView.Size = new System.Drawing.Size(1326, 408);
            this.tableNameGridView.TabIndex = 7;
            // 
            // schemaColumn
            // 
            this.schemaColumn.HeaderText = "Schema (optional)";
            this.schemaColumn.Name = "schemaColumn";
            this.schemaColumn.Width = 230;
            // 
            // tableNameColumn
            // 
            this.tableNameColumn.HeaderText = "Table Name";
            this.tableNameColumn.Name = "tableNameColumn";
            this.tableNameColumn.Width = 300;
            // 
            // classNameColumn
            // 
            this.classNameColumn.HeaderText = "Class Name (optional)";
            this.classNameColumn.Name = "classNameColumn";
            this.classNameColumn.Width = 250;
            // 
            // namespaceColumn
            // 
            this.namespaceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.namespaceColumn.HeaderText = "Namespace";
            this.namespaceColumn.Name = "namespaceColumn";
            // 
            // errorTextColumn
            // 
            this.errorTextColumn.HeaderText = "Errors";
            this.errorTextColumn.Name = "errorTextColumn";
            this.errorTextColumn.ReadOnly = true;
            this.errorTextColumn.Width = 260;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(12, 677);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 9;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // connectionStringErrorLabel
            // 
            this.connectionStringErrorLabel.AutoSize = true;
            this.connectionStringErrorLabel.Location = new System.Drawing.Point(1152, 30);
            this.connectionStringErrorLabel.Name = "connectionStringErrorLabel";
            this.connectionStringErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.connectionStringErrorLabel.TabIndex = 10;
            // 
            // outputFolderErrorLabel
            // 
            this.outputFolderErrorLabel.AutoSize = true;
            this.outputFolderErrorLabel.Location = new System.Drawing.Point(1152, 68);
            this.outputFolderErrorLabel.Name = "outputFolderErrorLabel";
            this.outputFolderErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.outputFolderErrorLabel.TabIndex = 11;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 721);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1325, 23);
            this.progressBar.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Namespace";
            // 
            // nameSpaceTextBox
            // 
            this.nameSpaceTextBox.Location = new System.Drawing.Point(82, 192);
            this.nameSpaceTextBox.Name = "nameSpaceTextBox";
            this.nameSpaceTextBox.Size = new System.Drawing.Size(560, 20);
            this.nameSpaceTextBox.TabIndex = 6;
            // 
            // declarativeAttributesRadioButton
            // 
            this.declarativeAttributesRadioButton.AutoSize = true;
            this.declarativeAttributesRadioButton.Checked = true;
            this.declarativeAttributesRadioButton.Location = new System.Drawing.Point(12, 104);
            this.declarativeAttributesRadioButton.Name = "declarativeAttributesRadioButton";
            this.declarativeAttributesRadioButton.Size = new System.Drawing.Size(176, 17);
            this.declarativeAttributesRadioButton.TabIndex = 3;
            this.declarativeAttributesRadioButton.TabStop = true;
            this.declarativeAttributesRadioButton.Text = "Class with Declarative Attributes";
            this.declarativeAttributesRadioButton.UseVisualStyleBackColor = true;
            // 
            // programmaticAttributesRadioButton
            // 
            this.programmaticAttributesRadioButton.AutoSize = true;
            this.programmaticAttributesRadioButton.Location = new System.Drawing.Point(12, 127);
            this.programmaticAttributesRadioButton.Name = "programmaticAttributesRadioButton";
            this.programmaticAttributesRadioButton.Size = new System.Drawing.Size(290, 17);
            this.programmaticAttributesRadioButton.TabIndex = 4;
            this.programmaticAttributesRadioButton.TabStop = true;
            this.programmaticAttributesRadioButton.Text = "POCO Class with generated programmatic attribute code";
            this.programmaticAttributesRadioButton.UseVisualStyleBackColor = true;
            this.programmaticAttributesRadioButton.CheckedChanged += new System.EventHandler(this.programmaticAttributesRadioButton_CheckedChanged);
            // 
            // programmaticAttributesRadioButtonLabel
            // 
            this.programmaticAttributesRadioButtonLabel.AutoSize = true;
            this.programmaticAttributesRadioButtonLabel.Location = new System.Drawing.Point(9, 158);
            this.programmaticAttributesRadioButtonLabel.Name = "programmaticAttributesRadioButtonLabel";
            this.programmaticAttributesRadioButtonLabel.Size = new System.Drawing.Size(214, 13);
            this.programmaticAttributesRadioButtonLabel.TabIndex = 0;
            this.programmaticAttributesRadioButtonLabel.Text = "Programmatic Attribute definition class name";
            this.programmaticAttributesRadioButtonLabel.Visible = false;
            // 
            // programmaticAttributeDefinitionClassNameTextBox
            // 
            this.programmaticAttributeDefinitionClassNameTextBox.Location = new System.Drawing.Point(230, 158);
            this.programmaticAttributeDefinitionClassNameTextBox.Name = "programmaticAttributeDefinitionClassNameTextBox";
            this.programmaticAttributeDefinitionClassNameTextBox.Size = new System.Drawing.Size(248, 20);
            this.programmaticAttributeDefinitionClassNameTextBox.TabIndex = 5;
            this.programmaticAttributeDefinitionClassNameTextBox.Visible = false;
            // 
            // classNameErrorLabel
            // 
            this.classNameErrorLabel.AutoSize = true;
            this.classNameErrorLabel.Location = new System.Drawing.Point(484, 161);
            this.classNameErrorLabel.Name = "classNameErrorLabel";
            this.classNameErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.classNameErrorLabel.TabIndex = 19;
            // 
            // nameSpaceErrorLabel
            // 
            this.nameSpaceErrorLabel.AutoSize = true;
            this.nameSpaceErrorLabel.Location = new System.Drawing.Point(649, 198);
            this.nameSpaceErrorLabel.Name = "nameSpaceErrorLabel";
            this.nameSpaceErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.nameSpaceErrorLabel.TabIndex = 20;
            // 
            // browseOutputFolderButton
            // 
            this.browseOutputFolderButton.Location = new System.Drawing.Point(12, 66);
            this.browseOutputFolderButton.Name = "browseOutputFolderButton";
            this.browseOutputFolderButton.Size = new System.Drawing.Size(112, 23);
            this.browseOutputFolderButton.TabIndex = 0;
            this.browseOutputFolderButton.TabStop = false;
            this.browseOutputFolderButton.Text = "Select output folder";
            this.browseOutputFolderButton.UseVisualStyleBackColor = true;
            this.browseOutputFolderButton.Click += new System.EventHandler(this.browseOutputFolderButton_Click);
            // 
            // btnClearForm
            // 
            this.btnClearForm.Location = new System.Drawing.Point(779, 104);
            this.btnClearForm.Name = "btnClearForm";
            this.btnClearForm.Size = new System.Drawing.Size(120, 23);
            this.btnClearForm.TabIndex = 21;
            this.btnClearForm.Text = "Clear Form";
            this.btnClearForm.UseVisualStyleBackColor = true;
            this.btnClearForm.Click += new System.EventHandler(this.btnClearForm_Click);
            // 
            // btnLoadProfile
            // 
            this.btnLoadProfile.Location = new System.Drawing.Point(905, 104);
            this.btnLoadProfile.Name = "btnLoadProfile";
            this.btnLoadProfile.Size = new System.Drawing.Size(110, 23);
            this.btnLoadProfile.TabIndex = 22;
            this.btnLoadProfile.Text = "Load a Profile";
            this.btnLoadProfile.UseVisualStyleBackColor = true;
            this.btnLoadProfile.Click += new System.EventHandler(this.btnLoadProfile_Click);
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Location = new System.Drawing.Point(1021, 104);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(120, 23);
            this.btnSaveProfile.TabIndex = 23;
            this.btnSaveProfile.Text = "Save Current Profile";
            this.btnSaveProfile.UseVisualStyleBackColor = true;
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);
            // 
            // btnLoadTables
            // 
            this.btnLoadTables.Location = new System.Drawing.Point(779, 154);
            this.btnLoadTables.Name = "btnLoadTables";
            this.btnLoadTables.Size = new System.Drawing.Size(120, 23);
            this.btnLoadTables.TabIndex = 24;
            this.btnLoadTables.Text = "Load Tables";
            this.btnLoadTables.UseVisualStyleBackColor = true;
            this.btnLoadTables.Click += new System.EventHandler(this.btnLoadTables_Click);
            // 
            // chkIncludeDbName
            // 
            this.chkIncludeDbName.AutoSize = true;
            this.chkIncludeDbName.Location = new System.Drawing.Point(779, 197);
            this.chkIncludeDbName.Name = "chkIncludeDbName";
            this.chkIncludeDbName.Size = new System.Drawing.Size(141, 17);
            this.chkIncludeDbName.TabIndex = 25;
            this.chkIncludeDbName.Text = "Include Database Name";
            this.chkIncludeDbName.UseVisualStyleBackColor = true;
            // 
            // CodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 755);
            this.Controls.Add(this.chkIncludeDbName);
            this.Controls.Add(this.btnLoadTables);
            this.Controls.Add(this.btnSaveProfile);
            this.Controls.Add(this.btnLoadProfile);
            this.Controls.Add(this.btnClearForm);
            this.Controls.Add(this.nameSpaceErrorLabel);
            this.Controls.Add(this.classNameErrorLabel);
            this.Controls.Add(this.programmaticAttributeDefinitionClassNameTextBox);
            this.Controls.Add(this.programmaticAttributesRadioButtonLabel);
            this.Controls.Add(this.programmaticAttributesRadioButton);
            this.Controls.Add(this.declarativeAttributesRadioButton);
            this.Controls.Add(this.nameSpaceTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.outputFolderErrorLabel);
            this.Controls.Add(this.connectionStringErrorLabel);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.tableNameGridView);
            this.Controls.Add(this.outputFolderTextBox);
            this.Controls.Add(this.browseOutputFolderButton);
            this.Controls.Add(this.connectionStringTextBox);
            this.Controls.Add(this.label1);
            this.Name = "CodeGenerator";
            this.Text = "Test Data Framework Code Generator";
            this.Load += new System.EventHandler(this.CodeGenerator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableNameGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox connectionStringTextBox;
        private System.Windows.Forms.TextBox outputFolderTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView tableNameGridView;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label connectionStringErrorLabel;
        private System.Windows.Forms.Label outputFolderErrorLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameSpaceTextBox;
        private System.Windows.Forms.RadioButton declarativeAttributesRadioButton;
        private System.Windows.Forms.RadioButton programmaticAttributesRadioButton;
        private System.Windows.Forms.Label programmaticAttributesRadioButtonLabel;
        private System.Windows.Forms.TextBox programmaticAttributeDefinitionClassNameTextBox;
        private System.Windows.Forms.Label classNameErrorLabel;
        private System.Windows.Forms.Label nameSpaceErrorLabel;
        private System.Windows.Forms.Button browseOutputFolderButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnClearForm;
        private System.Windows.Forms.Button btnLoadProfile;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnLoadTables;
        private System.Windows.Forms.DataGridViewTextBoxColumn schemaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn classNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namespaceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn errorTextColumn;
        private System.Windows.Forms.CheckBox chkIncludeDbName;
    }
}

