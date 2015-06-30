namespace ResXCreator
{
    internal partial class Edit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.exitButton = new System.Windows.Forms.Button();
            this.editDataGridView = new System.Windows.Forms.DataGridView();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResourceNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.editDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Location = new System.Drawing.Point(560, 350);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(87, 25);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "Выход";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // editDataGridView
            // 
            this.editDataGridView.AllowUserToAddRows = false;
            this.editDataGridView.AllowUserToDeleteRows = false;
            this.editDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.editDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.editDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.ResourceNameColumn,
            this.Value,
            this.CommentColumn});
            this.editDataGridView.Location = new System.Drawing.Point(14, 13);
            this.editDataGridView.Name = "editDataGridView";
            this.editDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.editDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.editDataGridView.Size = new System.Drawing.Size(633, 331);
            this.editDataGridView.TabIndex = 2;
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveChangesButton.Image = global::ResXCreator.Properties.Resources.saveHS;
            this.saveChangesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveChangesButton.Location = new System.Drawing.Point(400, 350);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(154, 25);
            this.saveChangesButton.TabIndex = 0;
            this.saveChangesButton.Text = "Сохранить";
            this.saveChangesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saveChangesButton.UseVisualStyleBackColor = true;
            this.saveChangesButton.Click += new System.EventHandler(this.saveChangesButton_Click);
            // 
            // IDColumn
            // 
            this.IDColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IDColumn.DataPropertyName = "ID";
            this.IDColumn.HeaderText = "№";
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            this.IDColumn.Width = 46;
            // 
            // ResourceNameColumn
            // 
            this.ResourceNameColumn.DataPropertyName = "ResourceName";
            this.ResourceNameColumn.HeaderText = "Имя";
            this.ResourceNameColumn.Name = "ResourceNameColumn";
            this.ResourceNameColumn.Visible = false;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Текст";
            this.Value.Name = "Value";
            // 
            // CommentColumn
            // 
            this.CommentColumn.DataPropertyName = "Comment";
            this.CommentColumn.HeaderText = "Примечание";
            this.CommentColumn.Name = "CommentColumn";
            // 
            // Edit
            // 
            this.AcceptButton = this.saveChangesButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitButton;
            this.ClientSize = new System.Drawing.Size(661, 387);
            this.Controls.Add(this.editDataGridView);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.saveChangesButton);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Edit";
            this.ShowIcon = false;
            this.Text = "Редактирование файла";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.editDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.DataGridView editDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResourceNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommentColumn;
    }
}