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
            this.searchPanel = new System.Windows.Forms.Panel();
            this.add_new_client_button = new System.Windows.Forms.Button();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.downloadbase_button = new System.Windows.Forms.Button();
            this.uploadbase_button = new System.Windows.Forms.Button();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(12, 91);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(20);
            this.flowLayoutPanel.Size = new System.Drawing.Size(713, 302);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // searchPanel
            // 
            this.searchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchPanel.Controls.Add(this.add_new_client_button);
            this.searchPanel.Controls.Add(this.btnClearSearch);
            this.searchPanel.Controls.Add(this.txtSearch);
            this.searchPanel.Location = new System.Drawing.Point(41, 41);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(684, 50);
            this.searchPanel.TabIndex = 1;
            // 
            // add_new_client_button
            // 
            this.add_new_client_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.add_new_client_button.Location = new System.Drawing.Point(425, 4);
            this.add_new_client_button.Name = "add_new_client_button";
            this.add_new_client_button.Size = new System.Drawing.Size(255, 45);
            this.add_new_client_button.TabIndex = 2;
            this.add_new_client_button.Text = "Добавить нового пациента";
            this.add_new_client_button.UseVisualStyleBackColor = true;
            this.add_new_client_button.Click += new System.EventHandler(this.add_new_client_button_Click);
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearSearch.Location = new System.Drawing.Point(276, 0);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(108, 45);
            this.btnClearSearch.TabIndex = 1;
            this.btnClearSearch.Text = "очистить";
            this.btnClearSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearch.Location = new System.Drawing.Point(3, 11);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(267, 22);
            this.txtSearch.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial Black", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(713, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "Панель управления пациентами";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // downloadbase_button
            // 
            this.downloadbase_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadbase_button.Location = new System.Drawing.Point(610, 399);
            this.downloadbase_button.Name = "downloadbase_button";
            this.downloadbase_button.Size = new System.Drawing.Size(115, 42);
            this.downloadbase_button.TabIndex = 3;
            this.downloadbase_button.Text = "Скачать базу пациентов";
            this.downloadbase_button.UseVisualStyleBackColor = true;
            this.downloadbase_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // uploadbase_button
            // 
            this.uploadbase_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadbase_button.Location = new System.Drawing.Point(466, 399);
            this.uploadbase_button.Name = "uploadbase_button";
            this.uploadbase_button.Size = new System.Drawing.Size(138, 42);
            this.uploadbase_button.TabIndex = 4;
            this.uploadbase_button.Text = "Загрузить базу пациентов";
            this.uploadbase_button.UseVisualStyleBackColor = true;
            this.uploadbase_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(737, 449);
            this.Controls.Add(this.uploadbase_button);
            this.Controls.Add(this.downloadbase_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.flowLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartScreen";
            this.Text = "StartScreen";
            this.Load += new System.EventHandler(this.StartScreen_Load);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Button add_new_client_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button downloadbase_button;
        private System.Windows.Forms.Button uploadbase_button;
    }
}