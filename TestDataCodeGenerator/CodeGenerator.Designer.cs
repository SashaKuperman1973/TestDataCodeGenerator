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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.browseOutputFolderButton = new System.Windows.Forms.Button();
            this.outputFolderTextBox = new System.Windows.Forms.TextBox();
            this.tableNameGridView = new System.Windows.Forms.DataGridView();
            this.runButton = new System.Windows.Forms.Button();
            this.connectionStringErrorLabel = new System.Windows.Forms.Label();
            this.outputFolderErrorLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.nameSpaceTextBox = new System.Windows.Forms.TextBox();
            this.schemaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Namespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorText = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // browseOutputFolderButton
            // 
            this.browseOutputFolderButton.Location = new System.Drawing.Point(12, 66);
            this.browseOutputFolderButton.Name = "browseOutputFolderButton";
            this.browseOutputFolderButton.Size = new System.Drawing.Size(112, 23);
            this.browseOutputFolderButton.TabIndex = 6;
            this.browseOutputFolderButton.Text = "Select output folder";
            this.browseOutputFolderButton.UseVisualStyleBackColor = true;
            this.browseOutputFolderButton.Click += new System.EventHandler(this.browseOutputFolderButton_Click);
            // 
            // outputFolderTextBox
            // 
            this.outputFolderTextBox.Location = new System.Drawing.Point(131, 68);
            this.outputFolderTextBox.Name = "outputFolderTextBox";
            this.outputFolderTextBox.Size = new System.Drawing.Size(1014, 20);
            this.outputFolderTextBox.TabIndex = 7;
            // 
            // tableNameGridView
            // 
            this.tableNameGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableNameGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.schemaColumn,
            this.tableNameColumn,
            this.Namespace,
            this.ErrorText});
            this.tableNameGridView.Location = new System.Drawing.Point(12, 161);
            this.tableNameGridView.Name = "tableNameGridView";
            this.tableNameGridView.Size = new System.Drawing.Size(1133, 408);
            this.tableNameGridView.TabIndex = 8;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(12, 598);
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
            this.progressBar.Location = new System.Drawing.Point(13, 642);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(629, 23);
            this.progressBar.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Namespace";
            // 
            // nameSpaceTextBox
            // 
            this.nameSpaceTextBox.Location = new System.Drawing.Point(131, 113);
            this.nameSpaceTextBox.Name = "nameSpaceTextBox";
            this.nameSpaceTextBox.Size = new System.Drawing.Size(511, 20);
            this.nameSpaceTextBox.TabIndex = 14;
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
            this.tableNameColumn.Width = 200;
            // 
            // Namespace
            // 
            this.Namespace.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Namespace.HeaderText = "Namespace";
            this.Namespace.Name = "Namespace";
            // 
            // ErrorText
            // 
            this.ErrorText.HeaderText = "";
            this.ErrorText.Name = "ErrorText";
            this.ErrorText.ReadOnly = true;
            this.ErrorText.Width = 170;
            // 
            // CodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 672);
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
            ((System.ComponentModel.ISupportInitialize)(this.tableNameGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox connectionStringTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button browseOutputFolderButton;
        private System.Windows.Forms.TextBox outputFolderTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView tableNameGridView;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label connectionStringErrorLabel;
        private System.Windows.Forms.Label outputFolderErrorLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Namespace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameSpaceTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorText;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn schemaColumn;
    }
}

