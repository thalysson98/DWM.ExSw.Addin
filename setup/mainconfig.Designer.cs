namespace DWM.ExSw.Addin.Config
{
    partial class mainconfig
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
            this.Caminho_txt = new System.Windows.Forms.TextBox();
            this.SaveConfig_bt = new System.Windows.Forms.Button();
            this.projetista_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Caminho_txt
            // 
            this.Caminho_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Caminho_txt.Location = new System.Drawing.Point(12, 38);
            this.Caminho_txt.Name = "Caminho_txt";
            this.Caminho_txt.Size = new System.Drawing.Size(330, 20);
            this.Caminho_txt.TabIndex = 3;
            // 
            // SaveConfig_bt
            // 
            this.SaveConfig_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveConfig_bt.Location = new System.Drawing.Point(267, 120);
            this.SaveConfig_bt.Name = "SaveConfig_bt";
            this.SaveConfig_bt.Size = new System.Drawing.Size(75, 23);
            this.SaveConfig_bt.TabIndex = 2;
            this.SaveConfig_bt.Text = "Salvar";
            this.SaveConfig_bt.UseVisualStyleBackColor = true;
            this.SaveConfig_bt.Click += new System.EventHandler(this.SaveConfig_bt_Click);
            // 
            // projetista_txt
            // 
            this.projetista_txt.Location = new System.Drawing.Point(12, 88);
            this.projetista_txt.Name = "projetista_txt";
            this.projetista_txt.Size = new System.Drawing.Size(107, 20);
            this.projetista_txt.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Biblioteca de material";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Projetista";
            // 
            // mainconfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 161);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.projetista_txt);
            this.Controls.Add(this.Caminho_txt);
            this.Controls.Add(this.SaveConfig_bt);
            this.MaximumSize = new System.Drawing.Size(500, 200);
            this.MinimumSize = new System.Drawing.Size(370, 200);
            this.Name = "mainconfig";
            this.ShowIcon = false;
            this.Text = "Configurações";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.mainconfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Caminho_txt;
        private System.Windows.Forms.Button SaveConfig_bt;
        private System.Windows.Forms.TextBox projetista_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}