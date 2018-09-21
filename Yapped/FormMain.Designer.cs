namespace Yapped
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.dgvParamsParamCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRows = new System.Windows.Forms.DataGridView();
            this.dgvRowsIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRowsNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCells = new System.Windows.Forms.DataGridView();
            this.dgvCellsTypeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCellsNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCellsValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ofdRegulation = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCells)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvParams
            // 
            this.dgvParams.AllowUserToAddRows = false;
            this.dgvParams.AllowUserToDeleteRows = false;
            this.dgvParams.AllowUserToResizeColumns = false;
            this.dgvParams.AllowUserToResizeRows = false;
            this.dgvParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvParams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvParamsParamCol});
            this.dgvParams.Location = new System.Drawing.Point(12, 27);
            this.dgvParams.MultiSelect = false;
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowHeadersVisible = false;
            this.dgvParams.Size = new System.Drawing.Size(240, 507);
            this.dgvParams.TabIndex = 0;
            this.dgvParams.SelectionChanged += new System.EventHandler(this.dgvParams_SelectionChanged);
            // 
            // dgvParamsParamCol
            // 
            this.dgvParamsParamCol.DataPropertyName = "Name";
            this.dgvParamsParamCol.HeaderText = "Params";
            this.dgvParamsParamCol.Name = "dgvParamsParamCol";
            this.dgvParamsParamCol.ReadOnly = true;
            // 
            // dgvRows
            // 
            this.dgvRows.AllowUserToAddRows = false;
            this.dgvRows.AllowUserToDeleteRows = false;
            this.dgvRows.AllowUserToResizeColumns = false;
            this.dgvRows.AllowUserToResizeRows = false;
            this.dgvRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvRowsIDCol,
            this.dgvRowsNameCol});
            this.dgvRows.Location = new System.Drawing.Point(0, 0);
            this.dgvRows.MultiSelect = false;
            this.dgvRows.Name = "dgvRows";
            this.dgvRows.RowHeadersVisible = false;
            this.dgvRows.Size = new System.Drawing.Size(240, 507);
            this.dgvRows.TabIndex = 1;
            this.dgvRows.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvRows_CellValidating);
            this.dgvRows.SelectionChanged += new System.EventHandler(this.dgvRows_SelectionChanged);
            // 
            // dgvRowsIDCol
            // 
            this.dgvRowsIDCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvRowsIDCol.DataPropertyName = "ID";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvRowsIDCol.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRowsIDCol.HeaderText = "ID";
            this.dgvRowsIDCol.Name = "dgvRowsIDCol";
            this.dgvRowsIDCol.Width = 43;
            // 
            // dgvRowsNameCol
            // 
            this.dgvRowsNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvRowsNameCol.DataPropertyName = "Name";
            this.dgvRowsNameCol.HeaderText = "Name";
            this.dgvRowsNameCol.Name = "dgvRowsNameCol";
            // 
            // dgvCells
            // 
            this.dgvCells.AllowUserToAddRows = false;
            this.dgvCells.AllowUserToDeleteRows = false;
            this.dgvCells.AllowUserToResizeColumns = false;
            this.dgvCells.AllowUserToResizeRows = false;
            this.dgvCells.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCells.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCells.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCellsTypeCol,
            this.dgvCellsNameCol,
            this.dgvCellsValueCol});
            this.dgvCells.Location = new System.Drawing.Point(3, 0);
            this.dgvCells.Name = "dgvCells";
            this.dgvCells.RowHeadersVisible = false;
            this.dgvCells.Size = new System.Drawing.Size(240, 507);
            this.dgvCells.TabIndex = 2;
            this.dgvCells.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCells_CellEndEdit);
            this.dgvCells.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCells_CellFormatting);
            this.dgvCells.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvCells_CellValidating);
            // 
            // dgvCellsTypeCol
            // 
            this.dgvCellsTypeCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvCellsTypeCol.DataPropertyName = "Type";
            this.dgvCellsTypeCol.HeaderText = "Type";
            this.dgvCellsTypeCol.Name = "dgvCellsTypeCol";
            this.dgvCellsTypeCol.ReadOnly = true;
            this.dgvCellsTypeCol.Width = 56;
            // 
            // dgvCellsNameCol
            // 
            this.dgvCellsNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvCellsNameCol.DataPropertyName = "Name";
            this.dgvCellsNameCol.HeaderText = "Name";
            this.dgvCellsNameCol.Name = "dgvCellsNameCol";
            this.dgvCellsNameCol.ReadOnly = true;
            this.dgvCellsNameCol.Width = 60;
            // 
            // dgvCellsValueCol
            // 
            this.dgvCellsValueCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvCellsValueCol.DataPropertyName = "Value";
            this.dgvCellsValueCol.HeaderText = "Value";
            this.dgvCellsValueCol.Name = "dgvCellsValueCol";
            // 
            // ofdRegulation
            // 
            this.ofdRegulation.FileName = "Data0.bdt";
            this.ofdRegulation.Filter = "DS3 Regulation|Data0.bdt";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(258, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvRows);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvCells);
            this.splitContainer1.Size = new System.Drawing.Size(490, 507);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.updateToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(760, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.restoreToolStripMenuItem,
            this.exploreToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.ToolTipText = "Browse for a regulation file to edit.";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.ToolTipText = "Save changes to the regulation file.";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.ToolTipText = "Restore the regulation file backup.";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // exploreToolStripMenuItem
            // 
            this.exploreToolStripMenuItem.Name = "exploreToolStripMenuItem";
            this.exploreToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exploreToolStripMenuItem.Text = "Explore";
            this.exploreToolStripMenuItem.ToolTipText = "Open the regulation file directory in Explorer.";
            this.exploreToolStripMenuItem.Click += new System.EventHandler(this.exploreToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRowToolStripMenuItem,
            this.deleteRowToolStripMenuItem,
            this.importNamesToolStripMenuItem,
            this.exportNamesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // addRowToolStripMenuItem
            // 
            this.addRowToolStripMenuItem.Name = "addRowToolStripMenuItem";
            this.addRowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addRowToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addRowToolStripMenuItem.Text = "Add Row";
            this.addRowToolStripMenuItem.ToolTipText = "Add a new row to the selected param.";
            this.addRowToolStripMenuItem.Click += new System.EventHandler(this.addRowToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            this.deleteRowToolStripMenuItem.ToolTipText = "Delete the currently selected row from the param.";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // importNamesToolStripMenuItem
            // 
            this.importNamesToolStripMenuItem.Name = "importNamesToolStripMenuItem";
            this.importNamesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.importNamesToolStripMenuItem.Text = "Import Names";
            this.importNamesToolStripMenuItem.ToolTipText = "Insert row names from the Network Test to their corresponding IDs.";
            this.importNamesToolStripMenuItem.Click += new System.EventHandler(this.importNamesToolStripMenuItem_Click);
            // 
            // exportNamesToolStripMenuItem
            // 
            this.exportNamesToolStripMenuItem.Name = "exportNamesToolStripMenuItem";
            this.exportNamesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.exportNamesToolStripMenuItem.Text = "Export Names";
            this.exportNamesToolStripMenuItem.ToolTipText = "Export row names to text files.";
            this.exportNamesToolStripMenuItem.Click += new System.EventHandler(this.exportNamesToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.updateToolStripMenuItem.BackColor = System.Drawing.Color.Yellow;
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.ToolTipText = "An update is available! Click to download it.";
            this.updateToolStripMenuItem.Visible = false;
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 546);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dgvParams);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Yapped <version>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCells)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.DataGridView dgvRows;
        private System.Windows.Forms.DataGridView dgvCells;
        private System.Windows.Forms.OpenFileDialog ofdRegulation;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvParamsParamCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCellsTypeCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCellsNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCellsValueCol;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exploreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importNamesToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRowsIDCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRowsNameCol;
        private System.Windows.Forms.ToolStripMenuItem exportNamesToolStripMenuItem;
    }
}

