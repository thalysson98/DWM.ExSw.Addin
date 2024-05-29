namespace DWM.ExSw.Addin.setup
{
    partial class dimenssoes
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
            this.Apply_bt = new System.Windows.Forms.Button();
            this.dimens_list = new System.Windows.Forms.ListView();
            this.Cota = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // Apply_bt
            // 
            this.Apply_bt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Apply_bt.Location = new System.Drawing.Point(96, 343);
            this.Apply_bt.MaximumSize = new System.Drawing.Size(75, 23);
            this.Apply_bt.MinimumSize = new System.Drawing.Size(75, 23);
            this.Apply_bt.Name = "Apply_bt";
            this.Apply_bt.Size = new System.Drawing.Size(75, 23);
            this.Apply_bt.TabIndex = 1;
            this.Apply_bt.Text = "Aplicar";
            this.Apply_bt.UseVisualStyleBackColor = true;
            this.Apply_bt.Click += new System.EventHandler(this.Apply_bt_Click);
            // 
            // dimens_list
            // 
            this.dimens_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dimens_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Cota,
            this.valor});
            this.dimens_list.FullRowSelect = true;
            this.dimens_list.HideSelection = false;
            this.dimens_list.Location = new System.Drawing.Point(12, 12);
            this.dimens_list.Name = "dimens_list";
            this.dimens_list.Size = new System.Drawing.Size(243, 325);
            this.dimens_list.TabIndex = 4;
            this.dimens_list.UseCompatibleStateImageBehavior = false;
            this.dimens_list.View = System.Windows.Forms.View.Details;
            this.dimens_list.SelectedIndexChanged += new System.EventHandler(this.dimens_list_SelectedIndexChanged);
            // 
            // Cota
            // 
            this.Cota.Text = "Cota";
            this.Cota.Width = 125;
            // 
            // valor
            // 
            this.valor.Text = "Valor";
            this.valor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.valor.Width = 110;
            // 
            // dimenssoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 378);
            this.Controls.Add(this.dimens_list);
            this.Controls.Add(this.Apply_bt);
            this.MaximumSize = new System.Drawing.Size(400, 1000);
            this.Name = "dimenssoes";
            this.ShowIcon = false;
            this.Text = "Dimensões";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Apply_bt;
        private System.Windows.Forms.ListView dimens_list;
        private System.Windows.Forms.ColumnHeader Cota;
        private System.Windows.Forms.ColumnHeader valor;
    }
}