namespace Hakatonv2
{
    partial class PatientCard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.Label lblSessionCount;
        private System.Windows.Forms.Label lblLastSession;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.lblSessionCount = new System.Windows.Forms.Label();
            this.lblLastSession = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.delete_button = new System.Windows.Forms.Button();
            this.start_seans_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFullName
            // 
            this.lblFullName.BackColor = System.Drawing.Color.LightGray;
            this.lblFullName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFullName.Location = new System.Drawing.Point(0, 0);
            this.lblFullName.Margin = new System.Windows.Forms.Padding(4);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(380, 49);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Иванов Иван Иванович";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBirthDate.Location = new System.Drawing.Point(1, 47);
            this.lblBirthDate.Margin = new System.Windows.Forms.Padding(4);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(304, 31);
            this.lblBirthDate.TabIndex = 1;
            this.lblBirthDate.Text = "Дата рождения: 01.01.1980";
            this.lblBirthDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSessionCount
            // 
            this.lblSessionCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSessionCount.Location = new System.Drawing.Point(1, 76);
            this.lblSessionCount.Margin = new System.Windows.Forms.Padding(4);
            this.lblSessionCount.Name = "lblSessionCount";
            this.lblSessionCount.Size = new System.Drawing.Size(304, 29);
            this.lblSessionCount.TabIndex = 2;
            this.lblSessionCount.Text = "Сеансов: 5";
            this.lblSessionCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastSession
            // 
            this.lblLastSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLastSession.Location = new System.Drawing.Point(0, 101);
            this.lblLastSession.Margin = new System.Windows.Forms.Padding(4);
            this.lblLastSession.Name = "lblLastSession";
            this.lblLastSession.Size = new System.Drawing.Size(304, 33);
            this.lblLastSession.TabIndex = 3;
            this.lblLastSession.Text = "Последний: 15.12.2023";
            this.lblLastSession.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblId
            // 
            this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblId.Location = new System.Drawing.Point(0, 131);
            this.lblId.Margin = new System.Windows.Forms.Padding(4);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(304, 34);
            this.lblId.TabIndex = 4;
            this.lblId.Text = "Последний: 15.12.2023";
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // delete_button
            // 
            this.delete_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_button.Location = new System.Drawing.Point(337, 131);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(40, 41);
            this.delete_button.TabIndex = 6;
            this.delete_button.Text = "🗑️";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // start_seans_button
            // 
            this.start_seans_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.start_seans_button.Location = new System.Drawing.Point(295, 133);
            this.start_seans_button.Name = "start_seans_button";
            this.start_seans_button.Size = new System.Drawing.Size(36, 37);
            this.start_seans_button.TabIndex = 7;
            this.start_seans_button.Text = "▶️";
            this.start_seans_button.UseVisualStyleBackColor = true;
            // 
            // PatientCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.start_seans_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblLastSession);
            this.Controls.Add(this.lblSessionCount);
            this.Controls.Add(this.lblBirthDate);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatientCard";
            this.Size = new System.Drawing.Size(380, 175);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button start_seans_button;
    }
}
