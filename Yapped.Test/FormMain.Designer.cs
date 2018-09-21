namespace Yapped.Test
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.dgvParamsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRows = new System.Windows.Forms.DataGridView();
            this.dgvRowsIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRowsNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCells = new System.Windows.Forms.DataGridView();
            this.dgvCellsTypeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCellsNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCellsValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ofdRegulation = new System.Windows.Forms.OpenFileDialog();
            this.lblSizeWarning = new System.Windows.Forms.Label();
            this.dgvLayout = new System.Windows.Forms.DataGridView();
            this.dgvLayoutTypeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLayoutNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLayoutSizeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLayoutDefaultCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCells)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayout)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvParams
            // 
            this.dgvParams.AllowUserToAddRows = false;
            this.dgvParams.AllowUserToDeleteRows = false;
            this.dgvParams.AllowUserToResizeColumns = false;
            this.dgvParams.AllowUserToResizeRows = false;
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvParamsCol});
            this.dgvParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvParams.Location = new System.Drawing.Point(3, 3);
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowHeadersVisible = false;
            this.dgvParams.Size = new System.Drawing.Size(238, 405);
            this.dgvParams.TabIndex = 0;
            this.dgvParams.SelectionChanged += new System.EventHandler(this.dgvParams_SelectionChanged);
            // 
            // dgvParamsCol
            // 
            this.dgvParamsCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvParamsCol.DataPropertyName = "Name";
            this.dgvParamsCol.HeaderText = "Param";
            this.dgvParamsCol.Name = "dgvParamsCol";
            this.dgvParamsCol.ReadOnly = true;
            // 
            // dgvRows
            // 
            this.dgvRows.AllowUserToAddRows = false;
            this.dgvRows.AllowUserToDeleteRows = false;
            this.dgvRows.AllowUserToResizeColumns = false;
            this.dgvRows.AllowUserToResizeRows = false;
            this.dgvRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvRowsIDCol,
            this.dgvRowsNameCol});
            this.dgvRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRows.Location = new System.Drawing.Point(247, 3);
            this.dgvRows.Name = "dgvRows";
            this.dgvRows.RowHeadersVisible = false;
            this.dgvRows.Size = new System.Drawing.Size(238, 405);
            this.dgvRows.TabIndex = 1;
            this.dgvRows.SelectionChanged += new System.EventHandler(this.dgvRows_SelectionChanged);
            // 
            // dgvRowsIDCol
            // 
            this.dgvRowsIDCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvRowsIDCol.DataPropertyName = "ID";
            this.dgvRowsIDCol.HeaderText = "ID";
            this.dgvRowsIDCol.Name = "dgvRowsIDCol";
            this.dgvRowsIDCol.ReadOnly = true;
            this.dgvRowsIDCol.Width = 43;
            // 
            // dgvRowsNameCol
            // 
            this.dgvRowsNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvRowsNameCol.DataPropertyName = "Name";
            this.dgvRowsNameCol.HeaderText = "Name";
            this.dgvRowsNameCol.Name = "dgvRowsNameCol";
            this.dgvRowsNameCol.ReadOnly = true;
            // 
            // dgvCells
            // 
            this.dgvCells.AllowUserToAddRows = false;
            this.dgvCells.AllowUserToDeleteRows = false;
            this.dgvCells.AllowUserToResizeColumns = false;
            this.dgvCells.AllowUserToResizeRows = false;
            this.dgvCells.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCells.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCellsTypeCol,
            this.dgvCellsNameCol,
            this.dgvCellsValueCol});
            this.dgvCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCells.Location = new System.Drawing.Point(491, 3);
            this.dgvCells.Name = "dgvCells";
            this.dgvCells.RowHeadersVisible = false;
            this.dgvCells.Size = new System.Drawing.Size(238, 405);
            this.dgvCells.TabIndex = 2;
            this.dgvCells.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCells_CellFormatting);
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
            this.dgvCellsValueCol.ReadOnly = true;
            // 
            // ofdRegulation
            // 
            this.ofdRegulation.FileName = "Data0.bdt";
            this.ofdRegulation.Filter = "Regulation BDT|*.bdt";
            // 
            // lblSizeWarning
            // 
            this.lblSizeWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSizeWarning.AutoSize = true;
            this.lblSizeWarning.ForeColor = System.Drawing.Color.Firebrick;
            this.lblSizeWarning.Location = new System.Drawing.Point(12, 449);
            this.lblSizeWarning.Name = "lblSizeWarning";
            this.lblSizeWarning.Size = new System.Drawing.Size(35, 13);
            this.lblSizeWarning.TabIndex = 8;
            this.lblSizeWarning.Text = "label1";
            this.lblSizeWarning.Visible = false;
            // 
            // dgvLayout
            // 
            this.dgvLayout.AllowUserToAddRows = false;
            this.dgvLayout.AllowUserToDeleteRows = false;
            this.dgvLayout.AllowUserToResizeColumns = false;
            this.dgvLayout.AllowUserToResizeRows = false;
            this.dgvLayout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayout.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvLayoutTypeCol,
            this.dgvLayoutNameCol,
            this.dgvLayoutSizeCol,
            this.dgvLayoutDefaultCol});
            this.dgvLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLayout.Location = new System.Drawing.Point(735, 3);
            this.dgvLayout.Name = "dgvLayout";
            this.dgvLayout.RowHeadersVisible = false;
            this.dgvLayout.Size = new System.Drawing.Size(240, 405);
            this.dgvLayout.TabIndex = 9;
            this.dgvLayout.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvLayout_CellPainting);
            this.dgvLayout.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvLayout_CellValidating);
            // 
            // dgvLayoutTypeCol
            // 
            this.dgvLayoutTypeCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvLayoutTypeCol.DataPropertyName = "Type";
            this.dgvLayoutTypeCol.HeaderText = "Type";
            this.dgvLayoutTypeCol.Name = "dgvLayoutTypeCol";
            this.dgvLayoutTypeCol.Width = 56;
            // 
            // dgvLayoutNameCol
            // 
            this.dgvLayoutNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvLayoutNameCol.DataPropertyName = "Name";
            this.dgvLayoutNameCol.HeaderText = "Name";
            this.dgvLayoutNameCol.Name = "dgvLayoutNameCol";
            this.dgvLayoutNameCol.Width = 60;
            // 
            // dgvLayoutSizeCol
            // 
            this.dgvLayoutSizeCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvLayoutSizeCol.DataPropertyName = "Size";
            this.dgvLayoutSizeCol.HeaderText = "Size";
            this.dgvLayoutSizeCol.Name = "dgvLayoutSizeCol";
            this.dgvLayoutSizeCol.Width = 52;
            // 
            // dgvLayoutDefaultCol
            // 
            this.dgvLayoutDefaultCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvLayoutDefaultCol.DataPropertyName = "Default";
            this.dgvLayoutDefaultCol.HeaderText = "Default";
            this.dgvLayoutDefaultCol.Name = "dgvLayoutDefaultCol";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.dgvLayout, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvParams, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvRows, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvCells, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(978, 411);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1002, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEntryToolStripMenuItem,
            this.deleteEntriesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // addEntryToolStripMenuItem
            // 
            this.addEntryToolStripMenuItem.Name = "addEntryToolStripMenuItem";
            this.addEntryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addEntryToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addEntryToolStripMenuItem.Text = "Add Entry";
            this.addEntryToolStripMenuItem.Click += new System.EventHandler(this.addEntryToolStripMenuItem_Click);
            // 
            // deleteEntriesToolStripMenuItem
            // 
            this.deleteEntriesToolStripMenuItem.Name = "deleteEntriesToolStripMenuItem";
            this.deleteEntriesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.deleteEntriesToolStripMenuItem.Text = "Delete Entries";
            this.deleteEntriesToolStripMenuItem.Click += new System.EventHandler(this.deleteEntriesToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 479);
            this.Controls.Add(this.lblSizeWarning);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Yapped.Test <version>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCells)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayout)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblSizeWarning;
        private System.Windows.Forms.DataGridView dgvLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvParamsCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRowsIDCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRowsNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCellsTypeCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCellsNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCellsValueCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLayoutTypeCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLayoutNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLayoutSizeCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLayoutDefaultCol;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEntriesToolStripMenuItem;
    }
}

