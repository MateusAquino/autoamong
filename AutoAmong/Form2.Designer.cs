namespace AutoAmong
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.errorMsg = new System.Windows.Forms.Label();
            this.playersTable = new System.Windows.Forms.DataGridView();
            this.ForceMute = new System.Windows.Forms.DataGridViewImageColumn();
            this.Cor = new System.Windows.Forms.DataGridViewImageColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discord = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DiscordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backBtn = new System.Windows.Forms.Button();
            this.clientState = new System.Windows.Forms.Label();
            this.matchState = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.voteBtn = new System.Windows.Forms.Button();
            this.gameBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.playersTable)).BeginInit();
            this.SuspendLayout();
            // 
            // errorMsg
            // 
            this.errorMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorMsg.AutoSize = true;
            this.errorMsg.BackColor = System.Drawing.Color.Transparent;
            this.errorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorMsg.ForeColor = System.Drawing.Color.OrangeRed;
            this.errorMsg.Location = new System.Drawing.Point(12, 439);
            this.errorMsg.Name = "errorMsg";
            this.errorMsg.Size = new System.Drawing.Size(242, 15);
            this.errorMsg.TabIndex = 4;
            this.errorMsg.Text = "Crie uma sala no Among Us para começar.";
            // 
            // playersTable
            // 
            this.playersTable.AllowUserToAddRows = false;
            this.playersTable.AllowUserToDeleteRows = false;
            this.playersTable.AllowUserToResizeRows = false;
            this.playersTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playersTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.playersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playersTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ForceMute,
            this.Cor,
            this.Nome,
            this.Discord,
            this.DiscordID});
            this.playersTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.playersTable.Location = new System.Drawing.Point(221, 12);
            this.playersTable.MultiSelect = false;
            this.playersTable.Name = "playersTable";
            this.playersTable.ShowCellErrors = false;
            this.playersTable.ShowCellToolTips = false;
            this.playersTable.ShowEditingIcon = false;
            this.playersTable.ShowRowErrors = false;
            this.playersTable.Size = new System.Drawing.Size(551, 337);
            this.playersTable.TabIndex = 6;
            this.playersTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.playersTable_CellClick);
            this.playersTable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.playersTable_MouseDown);
            // 
            // ForceMute
            // 
            this.ForceMute.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ForceMute.HeaderText = "Mute";
            this.ForceMute.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ForceMute.MinimumWidth = 40;
            this.ForceMute.Name = "ForceMute";
            this.ForceMute.ReadOnly = true;
            this.ForceMute.Width = 40;
            // 
            // Cor
            // 
            this.Cor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Cor.HeaderText = "Cor";
            this.Cor.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Cor.MinimumWidth = 40;
            this.Cor.Name = "Cor";
            this.Cor.ReadOnly = true;
            this.Cor.Width = 40;
            // 
            // Nome
            // 
            this.Nome.FillWeight = 72.14898F;
            this.Nome.HeaderText = "Nome";
            this.Nome.MinimumWidth = 130;
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            // 
            // Discord
            // 
            this.Discord.FillWeight = 126.5771F;
            this.Discord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Discord.HeaderText = "Discord";
            this.Discord.MinimumWidth = 100;
            this.Discord.Name = "Discord";
            // 
            // DiscordID
            // 
            this.DiscordID.FillWeight = 99.36306F;
            this.DiscordID.HeaderText = "Discord ID";
            this.DiscordID.MinimumWidth = 30;
            this.DiscordID.Name = "DiscordID";
            this.DiscordID.ReadOnly = true;
            // 
            // backBtn
            // 
            this.backBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backBtn.Location = new System.Drawing.Point(26, 90);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(75, 23);
            this.backBtn.TabIndex = 8;
            this.backBtn.Text = "Recomeçar";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // clientState
            // 
            this.clientState.AutoSize = true;
            this.clientState.BackColor = System.Drawing.Color.Transparent;
            this.clientState.ForeColor = System.Drawing.Color.White;
            this.clientState.Location = new System.Drawing.Point(218, 355);
            this.clientState.Name = "clientState";
            this.clientState.Size = new System.Drawing.Size(55, 13);
            this.clientState.TabIndex = 9;
            this.clientState.Text = "Estado: ...";
            // 
            // matchState
            // 
            this.matchState.AutoSize = true;
            this.matchState.BackColor = System.Drawing.Color.Transparent;
            this.matchState.ForeColor = System.Drawing.Color.White;
            this.matchState.Location = new System.Drawing.Point(218, 372);
            this.matchState.Name = "matchState";
            this.matchState.Size = new System.Drawing.Size(55, 13);
            this.matchState.TabIndex = 10;
            this.matchState.Text = "Partida: ...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(174, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "12.5";
            // 
            // voteBtn
            // 
            this.voteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.voteBtn.Location = new System.Drawing.Point(221, 399);
            this.voteBtn.Name = "voteBtn";
            this.voteBtn.Size = new System.Drawing.Size(100, 23);
            this.voteBtn.TabIndex = 12;
            this.voteBtn.Text = "Forçar Votação";
            this.voteBtn.UseVisualStyleBackColor = true;
            this.voteBtn.Click += new System.EventHandler(this.voteBtn_Click);
            // 
            // gameBtn
            // 
            this.gameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gameBtn.Location = new System.Drawing.Point(327, 399);
            this.gameBtn.Name = "gameBtn";
            this.gameBtn.Size = new System.Drawing.Size(100, 23);
            this.gameBtn.TabIndex = 13;
            this.gameBtn.Text = "Forçar Jogo";
            this.gameBtn.UseVisualStyleBackColor = true;
            this.gameBtn.Click += new System.EventHandler(this.gameBtn_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AutoAmong.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.gameBtn);
            this.Controls.Add(this.voteBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.matchState);
            this.Controls.Add(this.clientState);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.playersTable);
            this.Controls.Add(this.errorMsg);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form2";
            this.Text = "AutoAmong - @amongooc";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playersTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label errorMsg;
        private System.Windows.Forms.DataGridView playersTable;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Label clientState;
        private System.Windows.Forms.Label matchState;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewImageColumn ForceMute;
        private System.Windows.Forms.DataGridViewImageColumn Cor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewComboBoxColumn Discord;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscordID;
        private System.Windows.Forms.Button voteBtn;
        private System.Windows.Forms.Button gameBtn;
    }
}