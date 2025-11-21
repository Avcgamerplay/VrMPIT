namespace Hakatonv2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.base_panel = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.main_panel = new System.Windows.Forms.Panel();
            this.quest_status_test = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.patient_name_label = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel_control = new System.Windows.Forms.Panel();
            this.stop_button = new System.Windows.Forms.Button();
            this.start_emdr_button = new System.Windows.Forms.Button();
            this.start_bp_button = new System.Windows.Forms.Button();
            this.pause_button = new System.Windows.Forms.Button();
            this.button_open_choose_scene = new System.Windows.Forms.Button();
            this.already_scene_name = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.stream_button = new System.Windows.Forms.Button();
            this.base_panel.SuspendLayout();
            this.main_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // base_panel
            // 
            this.base_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.base_panel.Controls.Add(this.lblStatus);
            this.base_panel.Location = new System.Drawing.Point(2, 1);
            this.base_panel.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.base_panel.Name = "base_panel";
            this.base_panel.Size = new System.Drawing.Size(796, 456);
            this.base_panel.TabIndex = 1;
            this.base_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblStatus.Location = new System.Drawing.Point(0, 410);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblStatus.Size = new System.Drawing.Size(795, 38);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Ожидание подключения устройства";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Click += new System.EventHandler(this.label2_Click);
            // 
            // main_panel
            // 
            this.main_panel.Controls.Add(this.quest_status_test);
            this.main_panel.Controls.Add(this.tableLayoutPanel1);
            this.main_panel.Controls.Add(this.patient_name_label);
            this.main_panel.Controls.Add(this.chart1);
            this.main_panel.Controls.Add(this.panel_control);
            this.main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel.Location = new System.Drawing.Point(0, 0);
            this.main_panel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(799, 456);
            this.main_panel.TabIndex = 2;
            this.main_panel.Visible = false;
            this.main_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.main_panel_Paint);
            // 
            // quest_status_test
            // 
            this.quest_status_test.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.quest_status_test.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.quest_status_test.Location = new System.Drawing.Point(251, 12);
            this.quest_status_test.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.quest_status_test.Name = "quest_status_test";
            this.quest_status_test.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.quest_status_test.Size = new System.Drawing.Size(229, 25);
            this.quest_status_test.TabIndex = 4;
            this.quest_status_test.Text = "Устройство активно";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.15789F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.8421F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 224);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.81818F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.18182F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 220);
            this.tableLayoutPanel1.TabIndex = 3;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // patient_name_label
            // 
            this.patient_name_label.AutoSize = true;
            this.patient_name_label.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.patient_name_label.Location = new System.Drawing.Point(6, 12);
            this.patient_name_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.patient_name_label.Name = "patient_name_label";
            this.patient_name_label.Size = new System.Drawing.Size(117, 19);
            this.patient_name_label.TabIndex = 2;
            this.patient_name_label.Text = "Пациент 1111";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(10, 38);
            this.chart1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(468, 181);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // panel_control
            // 
            this.panel_control.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_control.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_control.Controls.Add(this.stream_button);
            this.panel_control.Controls.Add(this.stop_button);
            this.panel_control.Controls.Add(this.start_emdr_button);
            this.panel_control.Controls.Add(this.start_bp_button);
            this.panel_control.Controls.Add(this.pause_button);
            this.panel_control.Controls.Add(this.button_open_choose_scene);
            this.panel_control.Controls.Add(this.already_scene_name);
            this.panel_control.Controls.Add(this.label1);
            this.panel_control.Location = new System.Drawing.Point(487, 12);
            this.panel_control.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(302, 432);
            this.panel_control.TabIndex = 0;
            this.panel_control.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_control_Paint);
            // 
            // stop_button
            // 
            this.stop_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stop_button.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stop_button.Location = new System.Drawing.Point(19, 381);
            this.stop_button.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(262, 43);
            this.stop_button.TabIndex = 6;
            this.stop_button.Text = "Завершить сеанс";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // start_emdr_button
            // 
            this.start_emdr_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.start_emdr_button.Enabled = false;
            this.start_emdr_button.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.start_emdr_button.Location = new System.Drawing.Point(19, 331);
            this.start_emdr_button.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.start_emdr_button.Name = "start_emdr_button";
            this.start_emdr_button.Size = new System.Drawing.Size(262, 43);
            this.start_emdr_button.TabIndex = 5;
            this.start_emdr_button.Text = "Запустить терапевтический модуль";
            this.start_emdr_button.UseVisualStyleBackColor = true;
            this.start_emdr_button.Click += new System.EventHandler(this.start_emdr_button_Click);
            // 
            // start_bp_button
            // 
            this.start_bp_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.start_bp_button.Enabled = false;
            this.start_bp_button.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.start_bp_button.Location = new System.Drawing.Point(19, 283);
            this.start_bp_button.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.start_bp_button.Name = "start_bp_button";
            this.start_bp_button.Size = new System.Drawing.Size(262, 43);
            this.start_bp_button.TabIndex = 4;
            this.start_bp_button.Text = "Безопасное место";
            this.start_bp_button.UseVisualStyleBackColor = true;
            this.start_bp_button.Click += new System.EventHandler(this.start_bp_button_Click);
            // 
            // pause_button
            // 
            this.pause_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pause_button.Enabled = false;
            this.pause_button.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pause_button.Location = new System.Drawing.Point(19, 234);
            this.pause_button.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(262, 43);
            this.pause_button.TabIndex = 3;
            this.pause_button.Text = "Пауза";
            this.pause_button.UseVisualStyleBackColor = true;
            this.pause_button.Click += new System.EventHandler(this.pause_button_Click);
            // 
            // button_open_choose_scene
            // 
            this.button_open_choose_scene.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_open_choose_scene.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_open_choose_scene.Location = new System.Drawing.Point(19, 51);
            this.button_open_choose_scene.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_open_choose_scene.Name = "button_open_choose_scene";
            this.button_open_choose_scene.Size = new System.Drawing.Size(262, 43);
            this.button_open_choose_scene.TabIndex = 1;
            this.button_open_choose_scene.Text = "Выбрать сцену";
            this.button_open_choose_scene.UseVisualStyleBackColor = true;
            this.button_open_choose_scene.Click += new System.EventHandler(this.button_open_choose_scene_Click);
            // 
            // already_scene_name
            // 
            this.already_scene_name.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.already_scene_name.ForeColor = System.Drawing.Color.Black;
            this.already_scene_name.Location = new System.Drawing.Point(58, 95);
            this.already_scene_name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.already_scene_name.Name = "already_scene_name";
            this.already_scene_name.Size = new System.Drawing.Size(192, 25);
            this.already_scene_name.TabIndex = 2;
            this.already_scene_name.Text = "сцена не выбрана";
            this.already_scene_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Панель управления сценой";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // stream_button
            // 
            this.stream_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stream_button.Enabled = false;
            this.stream_button.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stream_button.Location = new System.Drawing.Point(19, 185);
            this.stream_button.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.stream_button.Name = "stream_button";
            this.stream_button.Size = new System.Drawing.Size(262, 43);
            this.stream_button.TabIndex = 7;
            this.stream_button.Text = "Запуск стрима";
            this.stream_button.UseVisualStyleBackColor = true;
            this.stream_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(799, 456);
            this.Controls.Add(this.main_panel);
            this.Controls.Add(this.base_panel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Панель врача";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.base_panel.ResumeLayout(false);
            this.main_panel.ResumeLayout(false);
            this.main_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel_control.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel base_panel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel main_panel;
        private System.Windows.Forms.Panel panel_control;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_open_choose_scene;
        private System.Windows.Forms.Label already_scene_name;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label patient_name_label;
        private System.Windows.Forms.Button start_emdr_button;
        private System.Windows.Forms.Button start_bp_button;
        private System.Windows.Forms.Button pause_button;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label quest_status_test;
        private System.Windows.Forms.Button stream_button;
    }
}

