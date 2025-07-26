namespace QuizUC00606
{
    partial class Inicial
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
            this.lbl_quiz = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbb_categoria = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_iniciar = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.instruçToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarNovoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usarPrédefinidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.exemploXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_quiz
            // 
            this.lbl_quiz.AutoSize = true;
            this.lbl_quiz.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_quiz.Location = new System.Drawing.Point(313, 67);
            this.lbl_quiz.Name = "lbl_quiz";
            this.lbl_quiz.Size = new System.Drawing.Size(267, 50);
            this.lbl_quiz.TabIndex = 0;
            this.lbl_quiz.Text = "Bem Vindo ao";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 52.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(442, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 94);
            this.label1.TabIndex = 1;
            this.label1.Text = "QuiZ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 52.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(251, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 94);
            this.label2.TabIndex = 2;
            this.label2.Text = "ATEC";
            // 
            // cbb_categoria
            // 
            this.cbb_categoria.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbb_categoria.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_categoria.FormattingEnabled = true;
            this.cbb_categoria.Items.AddRange(new object[] {
            "Misto"});
            this.cbb_categoria.Location = new System.Drawing.Point(357, 300);
            this.cbb_categoria.Name = "cbb_categoria";
            this.cbb_categoria.Size = new System.Drawing.Size(423, 45);
            this.cbb_categoria.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20.75F);
            this.label3.Location = new System.Drawing.Point(69, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(307, 38);
            this.label3.TabIndex = 4;
            this.label3.Text = "Escolha uma categoria: ";
            // 
            // btn_iniciar
            // 
            this.btn_iniciar.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_iniciar.Location = new System.Drawing.Point(300, 400);
            this.btn_iniciar.Name = "btn_iniciar";
            this.btn_iniciar.Size = new System.Drawing.Size(300, 80);
            this.btn_iniciar.TabIndex = 5;
            this.btn_iniciar.Text = "Iniciar Quiz!";
            this.btn_iniciar.UseVisualStyleBackColor = true;
            this.btn_iniciar.Click += new System.EventHandler(this.btn_iniciar_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairToolStripMenuItem,
            this.instruçToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carregarXMLToolStripMenuItem,
            this.sairToolStripMenuItem1});
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.sairToolStripMenuItem.Text = "Opções";
            // 
            // sairToolStripMenuItem1
            // 
            this.sairToolStripMenuItem1.Name = "sairToolStripMenuItem1";
            this.sairToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.sairToolStripMenuItem1.Text = "Sair";
            this.sairToolStripMenuItem1.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // instruçToolStripMenuItem
            // 
            this.instruçToolStripMenuItem.Name = "instruçToolStripMenuItem";
            this.instruçToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.instruçToolStripMenuItem.Text = "Instruções";
            this.instruçToolStripMenuItem.Click += new System.EventHandler(this.instrucoesToolStripMenuItem_Click);
            // 
            // carregarXMLToolStripMenuItem
            // 
            this.carregarXMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carregarNovoToolStripMenuItem,
            this.exemploXMLToolStripMenuItem,
            this.usarPrédefinidoToolStripMenuItem});
            this.carregarXMLToolStripMenuItem.Name = "carregarXMLToolStripMenuItem";
            this.carregarXMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.carregarXMLToolStripMenuItem.Text = "Ficheiro XML";
            // 
            // carregarNovoToolStripMenuItem
            // 
            this.carregarNovoToolStripMenuItem.Name = "carregarNovoToolStripMenuItem";
            this.carregarNovoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.carregarNovoToolStripMenuItem.Text = "Carregar novo...";
            this.carregarNovoToolStripMenuItem.Click += new System.EventHandler(this.carregarNovoToolStripMenuItem_Click);
            // 
            // usarPrédefinidoToolStripMenuItem
            // 
            this.usarPrédefinidoToolStripMenuItem.Name = "usarPrédefinidoToolStripMenuItem";
            this.usarPrédefinidoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.usarPrédefinidoToolStripMenuItem.Text = "Usar pré-definido";
            this.usarPrédefinidoToolStripMenuItem.Click += new System.EventHandler(this.usarPrédefinidoToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // exemploXMLToolStripMenuItem
            // 
            this.exemploXMLToolStripMenuItem.Name = "exemploXMLToolStripMenuItem";
            this.exemploXMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exemploXMLToolStripMenuItem.Text = "Exemplo XML";
            this.exemploXMLToolStripMenuItem.Click += new System.EventHandler(this.exemploXMLToolStripMenuItem_Click);
            // 
            // Inicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.ControlBox = false;
            this.Controls.Add(this.btn_iniciar);
            this.Controls.Add(this.cbb_categoria);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_quiz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Inicial";
            this.Text = "ATECQuiZ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_quiz;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbb_categoria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_iniciar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem instruçToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarNovoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usarPrédefinidoToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem exemploXMLToolStripMenuItem;
    }
}

