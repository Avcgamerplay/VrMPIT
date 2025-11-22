namespace Hakatonv2
{
    partial class StartScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.add_new_client_button = new System.Windows.Forms.Button();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.downloadbase_button = new System.Windows.Forms.Button();
            this.uploadbase_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 41);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(20);
            this.flowLayoutPanel.Size = new System.Drawing.Size(802, 329);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // add_new_client_button
            // 
            this.add_new_client_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.add_new_client_button.BackColor = System.Drawing.Color.MediumPurple;
            this.add_new_client_button.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_new_client_button.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.add_new_client_button.Location = new System.Drawing.Point(666, 376);
            this.add_new_client_button.Name = "add_new_client_button";
            this.add_new_client_button.Size = new System.Drawing.Size(139, 42);
            this.add_new_client_button.TabIndex = 2;
            this.add_new_client_button.Text = "Добавить нового пациента";
            this.add_new_client_button.UseVisualStyleBackColor = false;
            this.add_new_client_button.Click += new System.EventHandler(this.add_new_client_button_Click);
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSearch.BackColor = System.Drawing.Color.MediumPurple;
            this.btnClearSearch.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClearSearch.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnClearSearch.Location = new System.Drawing.Point(696, 13);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(97, 28);
            this.btnClearSearch.TabIndex = 1;
            this.btnClearSearch.Text = "очистить";
            this.btnClearSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.Lavender;
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearch.Location = new System.Drawing.Point(22, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(668, 23);
            this.txtSearch.TabIndex = 0;
            // 
            // downloadbase_button
            // 
            this.downloadbase_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.downloadbase_button.BackColor = System.Drawing.Color.MediumPurple;
            this.downloadbase_button.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.downloadbase_button.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.downloadbase_button.Location = new System.Drawing.Point(158, 376);
            this.downloadbase_button.Name = "downloadbase_button";
            this.downloadbase_button.Size = new System.Drawing.Size(147, 42);
            this.downloadbase_button.TabIndex = 3;
            this.downloadbase_button.Text = "Скачать базу пациентов";
            this.downloadbase_button.UseVisualStyleBackColor = false;
            this.downloadbase_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // uploadbase_button
            // 
            this.uploadbase_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uploadbase_button.BackColor = System.Drawing.Color.MediumPurple;
            this.uploadbase_button.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.uploadbase_button.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.uploadbase_button.Image = ((System.Drawing.Image)(resources.GetObject("uploadbase_button.Image")));
            this.uploadbase_button.Location = new System.Drawing.Point(3, 376);
            this.uploadbase_button.Name = "uploadbase_button";
            this.uploadbase_button.Size = new System.Drawing.Size(138, 42);
            this.uploadbase_button.TabIndex = 4;
            this.uploadbase_button.Text = "Загрузить базу пациентов";
            this.uploadbase_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.uploadbase_button.UseVisualStyleBackColor = false;
            this.uploadbase_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.add_new_client_button);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.btnClearSearch);
            this.panel1.Controls.Add(this.downloadbase_button);
            this.panel1.Controls.Add(this.uploadbase_button);
            this.panel1.Controls.Add(this.flowLayoutPanel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 423);
            this.panel1.TabIndex = 5;
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(832, 442);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartScreen";
            this.Text = "StartScreen";
            this.Load += new System.EventHandler(this.StartScreen_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Button add_new_client_button;
        private System.Windows.Forms.Button downloadbase_button;
        private System.Windows.Forms.Button uploadbase_button;
        private System.Windows.Forms.Panel panel1;
    }
}