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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.dgvRows = new System.Windows.Forms.DataGridView();
            this.dgvCells = new System.Windows.Forms.DataGridView();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExplore = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.ofdRegulation = new System.Windows.Forms.OpenFileDialog();
            this.dgvParamsParamCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvCellsTypeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCellsNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCellsValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRowsIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRowsNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCells)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.dgvParams.Location = new System.Drawing.Point(12, 12);
            this.dgvParams.MultiSelect = false;
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowHeadersVisible = false;
            this.dgvParams.Size = new System.Drawing.Size(240, 493);
            this.dgvParams.TabIndex = 0;
            this.dgvParams.SelectionChanged += new System.EventHandler(this.dgvParams_SelectionChanged);
            // 
            // dgvRows
            // 
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
            this.dgvRows.Size = new System.Drawing.Size(240, 493);
            this.dgvRows.TabIndex = 1;
            this.dgvRows.SelectionChanged += new System.EventHandler(this.dgvRows_SelectionChanged);
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
            this.dgvCells.Size = new System.Drawing.Size(240, 493);
            this.dgvCells.TabIndex = 2;
            this.dgvCells.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCells_CellEndEdit);
            this.dgvCells.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCells_CellFormatting);
            this.dgvCells.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvCells_CellValidating);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.Location = new System.Drawing.Point(673, 511);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 3;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(592, 511);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExplore
            // 
            this.btnExplore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplore.Location = new System.Drawing.Point(511, 511);
            this.btnExplore.Name = "btnExplore";
            this.btnExplore.Size = new System.Drawing.Size(75, 23);
            this.btnExplore.TabIndex = 5;
            this.btnExplore.Text = "Explore";
            this.btnExplore.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(430, 511);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // ofdRegulation
            // 
            this.ofdRegulation.FileName = "Data0.bdt";
            this.ofdRegulation.Filter = "DS3 Regulation|Data0.bdt";
            // 
            // dgvParamsParamCol
            // 
            this.dgvParamsParamCol.DataPropertyName = "Name";
            this.dgvParamsParamCol.HeaderText = "Params";
            this.dgvParamsParamCol.Name = "dgvParamsParamCol";
            this.dgvParamsParamCol.ReadOnly = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(258, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvRows);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvCells);
            this.splitContainer1.Size = new System.Drawing.Size(490, 493);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.TabIndex = 7;
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
            // dgvRowsIDCol
            // 
            this.dgvRowsIDCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvRowsIDCol.DataPropertyName = "ID";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvRowsIDCol.DefaultCellStyle = dataGridViewCellStyle1;
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 546);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnExplore);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.dgvParams);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.DataGridView dgvRows;
        private System.Windows.Forms.DataGridView dgvCells;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExplore;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog ofdRegulation;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvParamsParamCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCellsTypeCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCellsNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCellsValueCol;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRowsIDCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRowsNameCol;
    }
}

