namespace DWM.ExSw.Addin
{
    partial class Form1
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
            BrightIdeasSoftware.CellStyle cellStyle1 = new BrightIdeasSoftware.CellStyle();
            BrightIdeasSoftware.CellStyle cellStyle2 = new BrightIdeasSoftware.CellStyle();
            BrightIdeasSoftware.CellStyle cellStyle3 = new BrightIdeasSoftware.CellStyle();
            this.treeListViewTasks = new BrightIdeasSoftware.TreeListView();
            this.dataTreeListView1 = new BrightIdeasSoftware.DataTreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.checkStateRenderer1 = new BrightIdeasSoftware.CheckStateRenderer();
            this.describedTaskRenderer1 = new BrightIdeasSoftware.DescribedTaskRenderer();
            this.hyperlinkStyle1 = new BrightIdeasSoftware.HyperlinkStyle();
            this.multiImageRenderer1 = new BrightIdeasSoftware.MultiImageRenderer();
            this.virtualObjectListView1 = new BrightIdeasSoftware.VirtualObjectListView();
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.treeListViewTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTreeListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.virtualObjectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListViewTasks
            // 
            this.treeListViewTasks.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListViewTasks.FullRowSelect = true;
            this.treeListViewTasks.HideSelection = false;
            this.treeListViewTasks.Location = new System.Drawing.Point(63, 55);
            this.treeListViewTasks.Name = "treeListViewTasks";
            this.treeListViewTasks.ShowGroups = false;
            this.treeListViewTasks.Size = new System.Drawing.Size(725, 170);
            this.treeListViewTasks.TabIndex = 0;
            this.treeListViewTasks.UseCompatibleStateImageBehavior = false;
            this.treeListViewTasks.View = System.Windows.Forms.View.Details;
            this.treeListViewTasks.VirtualMode = true;
            // 
            // dataTreeListView1
            // 
            this.dataTreeListView1.AllColumns.Add(this.olvColumn1);
            this.dataTreeListView1.AllColumns.Add(this.olvColumn2);
            this.dataTreeListView1.AllColumns.Add(this.olvColumn3);
            this.dataTreeListView1.AllColumns.Add(this.olvColumn4);
            this.dataTreeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.dataTreeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataTreeListView1.DataSource = null;
            this.dataTreeListView1.HideSelection = false;
            this.dataTreeListView1.Location = new System.Drawing.Point(63, 250);
            this.dataTreeListView1.Name = "dataTreeListView1";
            this.dataTreeListView1.RootKeyValueString = "";
            this.dataTreeListView1.ShowGroups = false;
            this.dataTreeListView1.Size = new System.Drawing.Size(493, 175);
            this.dataTreeListView1.TabIndex = 1;
            this.dataTreeListView1.UseCompatibleStateImageBehavior = false;
            this.dataTreeListView1.View = System.Windows.Forms.View.Details;
            this.dataTreeListView1.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.IsButton = true;
            // 
            // hyperlinkStyle1
            // 
            cellStyle1.Font = null;
            cellStyle1.ForeColor = System.Drawing.Color.Blue;
            this.hyperlinkStyle1.Normal = cellStyle1;
            cellStyle2.Font = null;
            cellStyle2.FontStyle = System.Drawing.FontStyle.Underline;
            this.hyperlinkStyle1.Over = cellStyle2;
            this.hyperlinkStyle1.OverCursor = System.Windows.Forms.Cursors.Hand;
            cellStyle3.Font = null;
            cellStyle3.ForeColor = System.Drawing.Color.Purple;
            this.hyperlinkStyle1.Visited = cellStyle3;
            // 
            // virtualObjectListView1
            // 
            this.virtualObjectListView1.AllColumns.Add(this.olvColumn5);
            this.virtualObjectListView1.AllColumns.Add(this.olvColumn6);
            this.virtualObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn5,
            this.olvColumn6});
            this.virtualObjectListView1.HideSelection = false;
            this.virtualObjectListView1.Location = new System.Drawing.Point(589, 271);
            this.virtualObjectListView1.Name = "virtualObjectListView1";
            this.virtualObjectListView1.ShowGroups = false;
            this.virtualObjectListView1.Size = new System.Drawing.Size(121, 97);
            this.virtualObjectListView1.TabIndex = 2;
            this.virtualObjectListView1.UseCompatibleStateImageBehavior = false;
            this.virtualObjectListView1.View = System.Windows.Forms.View.Details;
            this.virtualObjectListView1.VirtualMode = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.virtualObjectListView1);
            this.Controls.Add(this.dataTreeListView1);
            this.Controls.Add(this.treeListViewTasks);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.treeListViewTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTreeListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.virtualObjectListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.TreeListView treeListViewTasks;
        private BrightIdeasSoftware.DataTreeListView dataTreeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.CheckStateRenderer checkStateRenderer1;
        private BrightIdeasSoftware.DescribedTaskRenderer describedTaskRenderer1;
        private BrightIdeasSoftware.HyperlinkStyle hyperlinkStyle1;
        private BrightIdeasSoftware.MultiImageRenderer multiImageRenderer1;
        private BrightIdeasSoftware.VirtualObjectListView virtualObjectListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
    }
}