namespace AutoAmong
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
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

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.syncField = new System.Windows.Forms.TextBox();
            this.syncBtn = new System.Windows.Forms.Button();
            this.syncLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Digite m!sync no chat de partida e insira o Token abaixo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "AutoAmong";
            // 
            // syncField
            // 
            this.syncField.Location = new System.Drawing.Point(25, 65);
            this.syncField.Name = "syncField";
            this.syncField.Size = new System.Drawing.Size(358, 20);
            this.syncField.TabIndex = 2;
            this.syncField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.syncField_KeyDown);
            // 
            // syncBtn
            // 
            this.syncBtn.Location = new System.Drawing.Point(308, 100);
            this.syncBtn.Name = "syncBtn";
            this.syncBtn.Size = new System.Drawing.Size(75, 23);
            this.syncBtn.TabIndex = 3;
            this.syncBtn.Text = "Sincronizar";
            this.syncBtn.UseVisualStyleBackColor = true;
            this.syncBtn.Click += new System.EventHandler(this.syncBtn_Click);
            // 
            // syncLabel
            // 
            this.syncLabel.AutoSize = true;
            this.syncLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.syncLabel.Location = new System.Drawing.Point(25, 100);
            this.syncLabel.Name = "syncLabel";
            this.syncLabel.Size = new System.Drawing.Size(104, 13);
            this.syncLabel.TabIndex = 4;
            this.syncLabel.Text = "Falha ao sincronizar.";
            this.syncLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 142);
            this.Controls.Add(this.syncLabel);
            this.Controls.Add(this.syncBtn);
            this.Controls.Add(this.syncField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(424, 181);
            this.MinimumSize = new System.Drawing.Size(424, 181);
            this.Name = "Form1";
            this.Text = "AutoAmong - @amongooc";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox syncField;
        private System.Windows.Forms.Button syncBtn;
        private System.Windows.Forms.Label syncLabel;
    }
}

