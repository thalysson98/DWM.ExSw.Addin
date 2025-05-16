namespace DWM.ExSw.Addin.setup
{
    partial class materiaisData
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
            this.materiais_list = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filtro_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // materiais_list
            // 
            this.materiais_list.AllowColumnReorder = true;
            this.materiais_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materiais_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.materiais_list.FullRowSelect = true;
            this.materiais_list.GridLines = true;
            this.materiais_list.HideSelection = false;
            this.materiais_list.Location = new System.Drawing.Point(12, 77);
            this.materiais_list.Name = "materiais_list";
            this.materiais_list.Size = new System.Drawing.Size(613, 326);
            this.materiais_list.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.materiais_list.TabIndex = 0;
            this.materiais_list.UseCompatibleStateImageBehavior = false;
            this.materiais_list.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Material";
            this.columnHeader1.Width = 247;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Un.";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 44;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Massa Específica";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 116;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "UN1";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "UN2";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 58;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Peso Tabela";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 82;
            // 
            // filtro_txt
            // 
            this.filtro_txt.Location = new System.Drawing.Point(13, 40);
            this.filtro_txt.Name = "filtro_txt";
            this.filtro_txt.Size = new System.Drawing.Size(300, 20);
            this.filtro_txt.TabIndex = 1;
            this.filtro_txt.TextChanged += new System.EventHandler(this.filtro_txt_TextChanged);
            // 
            // materiaisData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 415);
            this.Controls.Add(this.filtro_txt);
            this.Controls.Add(this.materiais_list);
            this.MaximizeBox = false;
            this.Name = "materiaisData";
            this.Text = "materiaisData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView materiais_list;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TextBox filtro_txt;
    }
}