using System;
using System.Windows.Forms;
using System.Windows.Media;

namespace DWM.TaskPaneHost
{
    partial class TaskpaneHostUI
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        /// 
        
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.abrirVortexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aplicaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bancoDeMateriaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aplicativosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planificadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geradorDeArquivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.PanePrincipal = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Estrutura_list = new System.Windows.Forms.ListView();
            this.qtd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.material = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mcorte = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.info = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.list_estruturaBD = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.revisorData_txt = new System.Windows.Forms.TextBox();
            this.desenhistaData_txt = new System.Windows.Forms.TextBox();
            this.projetistaData_txt = new System.Windows.Forms.TextBox();
            this.revisor_cb = new System.Windows.Forms.ComboBox();
            this.desenhista_cb = new System.Windows.Forms.ComboBox();
            this.projetista_cb = new System.Windows.Forms.ComboBox();
            this.ApplyAprovador_bt = new System.Windows.Forms.Button();
            this.ApplyDesenhista_bt = new System.Windows.Forms.Button();
            this.ApplyProjetista_bt = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Atualizar_bt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Comercial_check = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.das = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ListaDeCorte_check = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.calcPB_bt = new System.Windows.Forms.Button();
            this.pesobt_txt = new System.Windows.Forms.TextBox();
            this.cabecalho_pane = new System.Windows.Forms.Panel();
            this.Codigo_txt = new System.Windows.Forms.TextBox();
            this.revisao_txt = new System.Windows.Forms.TextBox();
            this.unidade_cb = new System.Windows.Forms.ComboBox();
            this.sub_cb = new System.Windows.Forms.ComboBox();
            this.tipo_cb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Denominacao_txt = new System.Windows.Forms.TextBox();
            this.material_txt = new System.Windows.Forms.TextBox();
            this.textDenominacao_txt = new System.Windows.Forms.TextBox();
            this.CRM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CRM_Control1 = new System.Windows.Forms.ToolStripMenuItem();
            this.salvar_bt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PanePrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cabecalho_pane.SuspendLayout();
            this.CRM.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirVortexToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(709, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // abrirVortexToolStripMenuItem
            // 
            this.abrirVortexToolStripMenuItem.Name = "abrirVortexToolStripMenuItem";
            this.abrirVortexToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.abrirVortexToolStripMenuItem.Text = "Abrir Vortex";
            this.abrirVortexToolStripMenuItem.Click += new System.EventHandler(this.abrirVortexToolStripMenuItem_Click);
            // 
            // aplicaçãoToolStripMenuItem
            // 
            this.aplicaçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bancoDeMateriaisToolStripMenuItem,
            this.configuraçõesToolStripMenuItem});
            this.aplicaçãoToolStripMenuItem.Name = "aplicaçãoToolStripMenuItem";
            this.aplicaçãoToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.aplicaçãoToolStripMenuItem.Text = "Menu";
            // 
            // bancoDeMateriaisToolStripMenuItem
            // 
            this.bancoDeMateriaisToolStripMenuItem.Name = "bancoDeMateriaisToolStripMenuItem";
            this.bancoDeMateriaisToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.bancoDeMateriaisToolStripMenuItem.Text = "Banco de materiais";
            this.bancoDeMateriaisToolStripMenuItem.Click += new System.EventHandler(this.bancoDeMateriaisToolStripMenuItem_Click);
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            this.configuraçõesToolStripMenuItem.Click += new System.EventHandler(this.configuraçõesToolStripMenuItem_Click);
            // 
            // aplicativosToolStripMenuItem
            // 
            this.aplicativosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planificadorToolStripMenuItem,
            this.geradorDeArquivosToolStripMenuItem});
            this.aplicativosToolStripMenuItem.Name = "aplicativosToolStripMenuItem";
            this.aplicativosToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.aplicativosToolStripMenuItem.Text = "Aplicativos";
            // 
            // planificadorToolStripMenuItem
            // 
            this.planificadorToolStripMenuItem.Name = "planificadorToolStripMenuItem";
            this.planificadorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.planificadorToolStripMenuItem.Text = "Planificador";
            // 
            // geradorDeArquivosToolStripMenuItem
            // 
            this.geradorDeArquivosToolStripMenuItem.Name = "geradorDeArquivosToolStripMenuItem";
            this.geradorDeArquivosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.geradorDeArquivosToolStripMenuItem.Text = "Gerador de arquivos";
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 1027);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 38);
            this.panel1.TabIndex = 43;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.button1.Image = global::DWM.ExSw.Addin.Properties.Resources.export_20x20;
            this.button1.Location = new System.Drawing.Point(640, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 28);
            this.button1.TabIndex = 33;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Gainsboro;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label10.Location = new System.Drawing.Point(394, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Ultima exportação:";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Gainsboro;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label11.Location = new System.Drawing.Point(494, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "thalysson.santos 03/04/23";
            // 
            // PanePrincipal
            // 
            this.PanePrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanePrincipal.Controls.Add(this.splitContainer1);
            this.PanePrincipal.Controls.Add(this.cabecalho_pane);
            this.PanePrincipal.Controls.Add(this.Denominacao_txt);
            this.PanePrincipal.Controls.Add(this.material_txt);
            this.PanePrincipal.Controls.Add(this.textDenominacao_txt);
            this.PanePrincipal.Controls.Add(this.salvar_bt);
            this.PanePrincipal.Controls.Add(this.label4);
            this.PanePrincipal.Location = new System.Drawing.Point(3, 27);
            this.PanePrincipal.Name = "PanePrincipal";
            this.PanePrincipal.Size = new System.Drawing.Size(703, 994);
            this.PanePrincipal.TabIndex = 44;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(13, 162);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Estrutura_list);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.list_estruturaBD);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.Atualizar_bt);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.ListaDeCorte_check);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.calcPB_bt);
            this.splitContainer1.Panel2.Controls.Add(this.pesobt_txt);
            this.splitContainer1.Size = new System.Drawing.Size(680, 829);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 81;
            // 
            // Estrutura_list
            // 
            this.Estrutura_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Estrutura_list.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Estrutura_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.qtd,
            this.material,
            this.mcorte,
            this.pl,
            this.pb,
            this.info});
            this.Estrutura_list.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.Estrutura_list.FullRowSelect = true;
            this.Estrutura_list.GridLines = true;
            this.Estrutura_list.HideSelection = false;
            this.Estrutura_list.Location = new System.Drawing.Point(3, 4);
            this.Estrutura_list.MultiSelect = false;
            this.Estrutura_list.Name = "Estrutura_list";
            this.Estrutura_list.Size = new System.Drawing.Size(674, 221);
            this.Estrutura_list.TabIndex = 44;
            this.Estrutura_list.UseCompatibleStateImageBehavior = false;
            this.Estrutura_list.View = System.Windows.Forms.View.Details;
            this.Estrutura_list.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.Estrutura_list_ColumnClick);
            this.Estrutura_list.SelectedIndexChanged += new System.EventHandler(this.Estrutura_list_SelectedIndexChanged);
            // 
            // qtd
            // 
            this.qtd.Text = "Qtd.";
            this.qtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.qtd.Width = 35;
            // 
            // material
            // 
            this.material.Text = "Material";
            this.material.Width = 350;
            // 
            // mcorte
            // 
            this.mcorte.Text = "Medida de Corte";
            this.mcorte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mcorte.Width = 110;
            // 
            // pl
            // 
            this.pl.Text = "PL(Un)";
            this.pl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pl.Width = 50;
            // 
            // pb
            // 
            this.pb.Text = "PB(Un)";
            this.pb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pb.Width = 50;
            // 
            // info
            // 
            this.info.Text = "Info";
            this.info.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.info.Width = 80;
            // 
            // list_estruturaBD
            // 
            this.list_estruturaBD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_estruturaBD.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.list_estruturaBD.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.list_estruturaBD.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.list_estruturaBD.FullRowSelect = true;
            this.list_estruturaBD.GridLines = true;
            this.list_estruturaBD.HideSelection = false;
            this.list_estruturaBD.Location = new System.Drawing.Point(3, 3);
            this.list_estruturaBD.MultiSelect = false;
            this.list_estruturaBD.Name = "list_estruturaBD";
            this.list_estruturaBD.Size = new System.Drawing.Size(674, 309);
            this.list_estruturaBD.TabIndex = 81;
            this.list_estruturaBD.UseCompatibleStateImageBehavior = false;
            this.list_estruturaBD.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Qtd.";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 35;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Material";
            this.columnHeader2.Width = 350;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Medida de Corte";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 110;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "PL(Un)";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "PB(Un)";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 50;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Info";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 80;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.revisorData_txt);
            this.groupBox2.Controls.Add(this.desenhistaData_txt);
            this.groupBox2.Controls.Add(this.projetistaData_txt);
            this.groupBox2.Controls.Add(this.revisor_cb);
            this.groupBox2.Controls.Add(this.desenhista_cb);
            this.groupBox2.Controls.Add(this.projetista_cb);
            this.groupBox2.Controls.Add(this.ApplyAprovador_bt);
            this.groupBox2.Controls.Add(this.ApplyDesenhista_bt);
            this.groupBox2.Controls.Add(this.ApplyProjetista_bt);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(3, 365);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 165);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            // 
            // revisorData_txt
            // 
            this.revisorData_txt.Location = new System.Drawing.Point(140, 125);
            this.revisorData_txt.Name = "revisorData_txt";
            this.revisorData_txt.Size = new System.Drawing.Size(65, 22);
            this.revisorData_txt.TabIndex = 82;
            this.revisorData_txt.Text = "00/00/0000";
            this.revisorData_txt.TextChanged += new System.EventHandler(this.revisorData_txt_TextChanged);
            // 
            // desenhistaData_txt
            // 
            this.desenhistaData_txt.Location = new System.Drawing.Point(140, 78);
            this.desenhistaData_txt.Name = "desenhistaData_txt";
            this.desenhistaData_txt.Size = new System.Drawing.Size(65, 22);
            this.desenhistaData_txt.TabIndex = 81;
            this.desenhistaData_txt.Text = "00/00/0000";
            this.desenhistaData_txt.TextChanged += new System.EventHandler(this.desenhistaData_txt_TextChanged);
            // 
            // projetistaData_txt
            // 
            this.projetistaData_txt.Location = new System.Drawing.Point(140, 35);
            this.projetistaData_txt.Name = "projetistaData_txt";
            this.projetistaData_txt.Size = new System.Drawing.Size(65, 22);
            this.projetistaData_txt.TabIndex = 80;
            this.projetistaData_txt.Text = "00/00/0000";
            this.projetistaData_txt.TextChanged += new System.EventHandler(this.projetistaData_txt_TextChanged);
            // 
            // revisor_cb
            // 
            this.revisor_cb.FormattingEnabled = true;
            this.revisor_cb.Location = new System.Drawing.Point(13, 124);
            this.revisor_cb.Name = "revisor_cb";
            this.revisor_cb.Size = new System.Drawing.Size(121, 21);
            this.revisor_cb.TabIndex = 77;
            this.revisor_cb.SelectedIndexChanged += new System.EventHandler(this.revisor_cb_SelectedIndexChanged);
            this.revisor_cb.TextChanged += new System.EventHandler(this.revisor_cb_SelectedIndexChanged);
            // 
            // desenhista_cb
            // 
            this.desenhista_cb.FormattingEnabled = true;
            this.desenhista_cb.Location = new System.Drawing.Point(13, 79);
            this.desenhista_cb.Name = "desenhista_cb";
            this.desenhista_cb.Size = new System.Drawing.Size(121, 21);
            this.desenhista_cb.TabIndex = 76;
            this.desenhista_cb.SelectedIndexChanged += new System.EventHandler(this.desenhista_cb_SelectedIndexChanged);
            this.desenhista_cb.TextChanged += new System.EventHandler(this.desenhista_cb_SelectedIndexChanged);
            // 
            // projetista_cb
            // 
            this.projetista_cb.FormattingEnabled = true;
            this.projetista_cb.Location = new System.Drawing.Point(13, 34);
            this.projetista_cb.Name = "projetista_cb";
            this.projetista_cb.Size = new System.Drawing.Size(121, 21);
            this.projetista_cb.TabIndex = 75;
            this.projetista_cb.SelectedIndexChanged += new System.EventHandler(this.projetista_cb_SelectedIndexChanged);
            this.projetista_cb.TextChanged += new System.EventHandler(this.projetista_cb_SelectedIndexChanged);
            // 
            // ApplyAprovador_bt
            // 
            this.ApplyAprovador_bt.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.ApplyAprovador_bt.Location = new System.Drawing.Point(211, 125);
            this.ApplyAprovador_bt.Name = "ApplyAprovador_bt";
            this.ApplyAprovador_bt.Size = new System.Drawing.Size(33, 22);
            this.ApplyAprovador_bt.TabIndex = 78;
            this.ApplyAprovador_bt.Text = "+";
            this.ApplyAprovador_bt.UseVisualStyleBackColor = true;
            this.ApplyAprovador_bt.Click += new System.EventHandler(this.ApplyAprovador_bt_Click);
            // 
            // ApplyDesenhista_bt
            // 
            this.ApplyDesenhista_bt.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.ApplyDesenhista_bt.Location = new System.Drawing.Point(211, 80);
            this.ApplyDesenhista_bt.Name = "ApplyDesenhista_bt";
            this.ApplyDesenhista_bt.Size = new System.Drawing.Size(33, 22);
            this.ApplyDesenhista_bt.TabIndex = 78;
            this.ApplyDesenhista_bt.Text = "+";
            this.ApplyDesenhista_bt.UseVisualStyleBackColor = true;
            this.ApplyDesenhista_bt.Click += new System.EventHandler(this.ApplyDesenhista_bt_Click);
            // 
            // ApplyProjetista_bt
            // 
            this.ApplyProjetista_bt.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.ApplyProjetista_bt.Location = new System.Drawing.Point(211, 35);
            this.ApplyProjetista_bt.Name = "ApplyProjetista_bt";
            this.ApplyProjetista_bt.Size = new System.Drawing.Size(33, 22);
            this.ApplyProjetista_bt.TabIndex = 78;
            this.ApplyProjetista_bt.Text = "+";
            this.ApplyProjetista_bt.UseVisualStyleBackColor = true;
            this.ApplyProjetista_bt.Click += new System.EventHandler(this.ApplyProjetista_bt_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label9.Location = new System.Drawing.Point(138, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 72;
            this.label9.Text = "Data";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label7.Location = new System.Drawing.Point(138, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 73;
            this.label7.Text = "Data";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label5.Location = new System.Drawing.Point(138, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Data";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label8.Location = new System.Drawing.Point(10, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 69;
            this.label8.Text = "Revisor";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label6.Location = new System.Drawing.Point(10, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 70;
            this.label6.Text = "Desenhista";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label3.Location = new System.Drawing.Point(11, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Projetista";
            // 
            // Atualizar_bt
            // 
            this.Atualizar_bt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Atualizar_bt.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.Atualizar_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Atualizar_bt.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.Atualizar_bt.Image = global::DWM.ExSw.Addin.Properties.Resources.atualizar_20x20;
            this.Atualizar_bt.Location = new System.Drawing.Point(3, 318);
            this.Atualizar_bt.Name = "Atualizar_bt";
            this.Atualizar_bt.Size = new System.Drawing.Size(36, 35);
            this.Atualizar_bt.TabIndex = 43;
            this.Atualizar_bt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Atualizar_bt.UseVisualStyleBackColor = true;
            this.Atualizar_bt.Click += new System.EventHandler(this.Atualizar_bt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Comercial_check);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.das);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(270, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 165);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            // 
            // Comercial_check
            // 
            this.Comercial_check.AutoSize = true;
            this.Comercial_check.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.Comercial_check.Location = new System.Drawing.Point(15, 20);
            this.Comercial_check.Name = "Comercial_check";
            this.Comercial_check.Size = new System.Drawing.Size(76, 17);
            this.Comercial_check.TabIndex = 79;
            this.Comercial_check.Text = "Comercial";
            this.Comercial_check.UseVisualStyleBackColor = true;
            this.Comercial_check.CheckedChanged += new System.EventHandler(this.Comercial_check_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(17, 90);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(166, 22);
            this.textBox3.TabIndex = 74;
            this.textBox3.WordWrap = false;
            // 
            // das
            // 
            this.das.AutoSize = true;
            this.das.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.das.Location = new System.Drawing.Point(15, 66);
            this.das.Name = "das";
            this.das.Size = new System.Drawing.Size(132, 17);
            this.das.TabIndex = 73;
            this.das.Text = "Usar perfil estrutural";
            this.das.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.checkBox1.Location = new System.Drawing.Point(15, 43);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(177, 17);
            this.checkBox1.TabIndex = 73;
            this.checkBox1.Text = "Excluir/Atualizar lista de corte";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ListaDeCorte_check
            // 
            this.ListaDeCorte_check.AutoSize = true;
            this.ListaDeCorte_check.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.ListaDeCorte_check.Location = new System.Drawing.Point(45, 328);
            this.ListaDeCorte_check.Name = "ListaDeCorte_check";
            this.ListaDeCorte_check.Size = new System.Drawing.Size(177, 17);
            this.ListaDeCorte_check.TabIndex = 45;
            this.ListaDeCorte_check.Text = "Excluir/Atualizar lista de corte";
            this.ListaDeCorte_check.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.Location = new System.Drawing.Point(485, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 74;
            this.label1.Text = "PB. Total (kg)";
            // 
            // calcPB_bt
            // 
            this.calcPB_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.calcPB_bt.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.calcPB_bt.Location = new System.Drawing.Point(635, 324);
            this.calcPB_bt.Name = "calcPB_bt";
            this.calcPB_bt.Size = new System.Drawing.Size(33, 22);
            this.calcPB_bt.TabIndex = 80;
            this.calcPB_bt.Text = "+";
            this.calcPB_bt.UseVisualStyleBackColor = true;
            this.calcPB_bt.Click += new System.EventHandler(this.calcPB_bt_Click);
            // 
            // pesobt_txt
            // 
            this.pesobt_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pesobt_txt.Location = new System.Drawing.Point(563, 324);
            this.pesobt_txt.Name = "pesobt_txt";
            this.pesobt_txt.Size = new System.Drawing.Size(66, 22);
            this.pesobt_txt.TabIndex = 75;
            this.pesobt_txt.TextChanged += new System.EventHandler(this.pesobt_txt_TextChanged);
            // 
            // cabecalho_pane
            // 
            this.cabecalho_pane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cabecalho_pane.Controls.Add(this.Codigo_txt);
            this.cabecalho_pane.Controls.Add(this.revisao_txt);
            this.cabecalho_pane.Controls.Add(this.unidade_cb);
            this.cabecalho_pane.Controls.Add(this.sub_cb);
            this.cabecalho_pane.Controls.Add(this.tipo_cb);
            this.cabecalho_pane.Controls.Add(this.label2);
            this.cabecalho_pane.Controls.Add(this.label15);
            this.cabecalho_pane.Controls.Add(this.label12);
            this.cabecalho_pane.Controls.Add(this.label13);
            this.cabecalho_pane.Controls.Add(this.label14);
            this.cabecalho_pane.Location = new System.Drawing.Point(0, 0);
            this.cabecalho_pane.Name = "cabecalho_pane";
            this.cabecalho_pane.Size = new System.Drawing.Size(605, 68);
            this.cabecalho_pane.TabIndex = 79;
            // 
            // Codigo_txt
            // 
            this.Codigo_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Codigo_txt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Codigo_txt.Location = new System.Drawing.Point(13, 24);
            this.Codigo_txt.Name = "Codigo_txt";
            this.Codigo_txt.Size = new System.Drawing.Size(114, 27);
            this.Codigo_txt.TabIndex = 75;
            this.Codigo_txt.Text = "000.000.0000";
            this.Codigo_txt.TextChanged += new System.EventHandler(this.Codigo_txt_TextChanged);
            // 
            // revisao_txt
            // 
            this.revisao_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.revisao_txt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.revisao_txt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.revisao_txt.Location = new System.Drawing.Point(137, 23);
            this.revisao_txt.MaxLength = 1;
            this.revisao_txt.Name = "revisao_txt";
            this.revisao_txt.Size = new System.Drawing.Size(34, 27);
            this.revisao_txt.TabIndex = 53;
            this.revisao_txt.Text = "A";
            this.revisao_txt.TextChanged += new System.EventHandler(this.revisao_txt_TextChanged);
            // 
            // unidade_cb
            // 
            this.unidade_cb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unidade_cb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.unidade_cb.FormattingEnabled = true;
            this.unidade_cb.Items.AddRange(new object[] {
            "PC",
            "CJ"});
            this.unidade_cb.Location = new System.Drawing.Point(499, 23);
            this.unidade_cb.Name = "unidade_cb";
            this.unidade_cb.Size = new System.Drawing.Size(55, 21);
            this.unidade_cb.TabIndex = 71;
            this.unidade_cb.TabStop = false;
            // 
            // sub_cb
            // 
            this.sub_cb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sub_cb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sub_cb.FormattingEnabled = true;
            this.sub_cb.Items.AddRange(new object[] {
            "003",
            "004"});
            this.sub_cb.Location = new System.Drawing.Point(424, 23);
            this.sub_cb.Name = "sub_cb";
            this.sub_cb.Size = new System.Drawing.Size(69, 21);
            this.sub_cb.TabIndex = 69;
            this.sub_cb.TabStop = false;
            // 
            // tipo_cb
            // 
            this.tipo_cb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tipo_cb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tipo_cb.FormattingEnabled = true;
            this.tipo_cb.Items.AddRange(new object[] {
            "PI",
            "PA"});
            this.tipo_cb.Location = new System.Drawing.Point(363, 23);
            this.tipo_cb.Name = "tipo_cb";
            this.tipo_cb.Size = new System.Drawing.Size(55, 21);
            this.tipo_cb.TabIndex = 67;
            this.tipo_cb.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.Location = new System.Drawing.Point(134, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Rev.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label15.Location = new System.Drawing.Point(10, 5);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 13);
            this.label15.TabIndex = 76;
            this.label15.Text = "Nº Desenho";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label12.Location = new System.Drawing.Point(360, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 68;
            this.label12.Text = "Tipo";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label13.Location = new System.Drawing.Point(418, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 70;
            this.label13.Text = "SubGrupo";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label14.Location = new System.Drawing.Point(496, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 13);
            this.label14.TabIndex = 72;
            this.label14.Text = "Unidade";
            // 
            // Denominacao_txt
            // 
            this.Denominacao_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Denominacao_txt.BackColor = System.Drawing.SystemColors.Menu;
            this.Denominacao_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Denominacao_txt.Location = new System.Drawing.Point(13, 113);
            this.Denominacao_txt.Name = "Denominacao_txt";
            this.Denominacao_txt.ReadOnly = true;
            this.Denominacao_txt.Size = new System.Drawing.Size(680, 22);
            this.Denominacao_txt.TabIndex = 51;
            this.Denominacao_txt.WordWrap = false;
            this.Denominacao_txt.TextChanged += new System.EventHandler(this.Denominacao_txt_TextChanged);
            // 
            // material_txt
            // 
            this.material_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.material_txt.BackColor = System.Drawing.SystemColors.Menu;
            this.material_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.material_txt.Location = new System.Drawing.Point(13, 134);
            this.material_txt.Name = "material_txt";
            this.material_txt.ReadOnly = true;
            this.material_txt.Size = new System.Drawing.Size(680, 22);
            this.material_txt.TabIndex = 50;
            this.material_txt.WordWrap = false;
            // 
            // textDenominacao_txt
            // 
            this.textDenominacao_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textDenominacao_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textDenominacao_txt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textDenominacao_txt.ContextMenuStrip = this.CRM;
            this.textDenominacao_txt.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDenominacao_txt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textDenominacao_txt.Location = new System.Drawing.Point(13, 89);
            this.textDenominacao_txt.Name = "textDenominacao_txt";
            this.textDenominacao_txt.Size = new System.Drawing.Size(680, 25);
            this.textDenominacao_txt.TabIndex = 49;
            this.textDenominacao_txt.TextChanged += new System.EventHandler(this.textDenominacao_txt_TextChanged);
            // 
            // CRM
            // 
            this.CRM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CRM_Control1});
            this.CRM.Name = "CRM";
            this.CRM.Size = new System.Drawing.Size(165, 26);
            // 
            // CRM_Control1
            // 
            this.CRM_Control1.Name = "CRM_Control1";
            this.CRM_Control1.Size = new System.Drawing.Size(164, 22);
            this.CRM_Control1.Text = "Obter dimensões";
            this.CRM_Control1.Click += new System.EventHandler(this.CRM_Control1_Click);
            // 
            // salvar_bt
            // 
            this.salvar_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.salvar_bt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.salvar_bt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.salvar_bt.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.salvar_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.salvar_bt.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.salvar_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.salvar_bt.Image = global::DWM.ExSw.Addin.Properties.Resources.salvar_20x20;
            this.salvar_bt.Location = new System.Drawing.Point(637, 19);
            this.salvar_bt.Name = "salvar_bt";
            this.salvar_bt.Size = new System.Drawing.Size(44, 36);
            this.salvar_bt.TabIndex = 66;
            this.salvar_bt.UseVisualStyleBackColor = false;
            this.salvar_bt.Click += new System.EventHandler(this.salvar_bt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label4.Location = new System.Drawing.Point(10, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Denominação";
            // 
            // TaskpaneHostUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(450, 0);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.PanePrincipal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.Name = "TaskpaneHostUI";
            this.Size = new System.Drawing.Size(709, 1065);
            this.Load += new System.EventHandler(this.TaskpaneHostUI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PanePrincipal.ResumeLayout(false);
            this.PanePrincipal.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cabecalho_pane.ResumeLayout(false);
            this.cabecalho_pane.PerformLayout();
            this.CRM.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aplicaçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Panel PanePrincipal;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox unidade_cb;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox sub_cb;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox tipo_cb;
        private System.Windows.Forms.Button salvar_bt;
        public System.Windows.Forms.TextBox revisao_txt;
        public System.Windows.Forms.TextBox Denominacao_txt;
        public System.Windows.Forms.TextBox material_txt;
        public System.Windows.Forms.TextBox textDenominacao_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox ListaDeCorte_check;
        public System.Windows.Forms.ListView Estrutura_list;
        private System.Windows.Forms.ColumnHeader qtd;
        private System.Windows.Forms.ColumnHeader material;
        private System.Windows.Forms.ColumnHeader mcorte;
        private System.Windows.Forms.ColumnHeader pb;
        private System.Windows.Forms.ColumnHeader pl;
        private System.Windows.Forms.ColumnHeader info;
        public System.Windows.Forms.Button Atualizar_bt;
        private System.Windows.Forms.ToolStripMenuItem aplicativosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planificadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geradorDeArquivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        public System.Windows.Forms.TextBox Codigo_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox pesobt_txt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox revisor_cb;
        private System.Windows.Forms.ComboBox desenhista_cb;
        private System.Windows.Forms.ComboBox projetista_cb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ApplyAprovador_bt;
        private System.Windows.Forms.Button ApplyDesenhista_bt;
        private System.Windows.Forms.Button ApplyProjetista_bt;
        private System.Windows.Forms.Panel cabecalho_pane;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox Comercial_check;
        private System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.CheckBox das;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox projetistaData_txt;
        private System.Windows.Forms.TextBox desenhistaData_txt;
        private System.Windows.Forms.TextBox revisorData_txt;
        private System.Windows.Forms.ContextMenuStrip CRM;
        private System.Windows.Forms.ToolStripMenuItem CRM_Control1;
        private System.Windows.Forms.ToolStripMenuItem bancoDeMateriaisToolStripMenuItem;
        private System.Windows.Forms.Button calcPB_bt;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem abrirVortexToolStripMenuItem;
        public ListView list_estruturaBD;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
    }
}
