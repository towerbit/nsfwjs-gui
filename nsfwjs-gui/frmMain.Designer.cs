
namespace nsfwjs_gui
{
    partial class frmMain
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.btnOutputDirectory = new System.Windows.Forms.Button();
            this.nudSkipLimit = new System.Windows.Forms.NumericUpDown();
            this.lvwSource = new System.Windows.Forms.ListView();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxSettings = new System.Windows.Forms.GroupBox();
            this.cboHWDeviceType = new System.Windows.Forms.ComboBox();
            this.cboHost = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudProbability = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkipLimit)).BeginInit();
            this.gbxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProbability)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Output Directory";
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Location = new System.Drawing.Point(190, 82);
            this.txtOutputDirectory.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(353, 30);
            this.txtOutputDirectory.TabIndex = 2;
            this.txtOutputDirectory.Text = "d:\\temp\\nsfwjs_output";
            // 
            // btnOutputDirectory
            // 
            this.btnOutputDirectory.Location = new System.Drawing.Point(555, 82);
            this.btnOutputDirectory.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnOutputDirectory.Name = "btnOutputDirectory";
            this.btnOutputDirectory.Size = new System.Drawing.Size(60, 32);
            this.btnOutputDirectory.TabIndex = 3;
            this.btnOutputDirectory.Text = "...";
            this.btnOutputDirectory.UseVisualStyleBackColor = true;
            this.btnOutputDirectory.Click += new System.EventHandler(this.btnOutputDirectory_Click);
            // 
            // nudSkipLimit
            // 
            this.nudSkipLimit.Location = new System.Drawing.Point(190, 161);
            this.nudSkipLimit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudSkipLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSkipLimit.Name = "nudSkipLimit";
            this.nudSkipLimit.Size = new System.Drawing.Size(102, 30);
            this.nudSkipLimit.TabIndex = 5;
            this.nudSkipLimit.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // lvwSource
            // 
            this.lvwSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvwSource.HideSelection = false;
            this.lvwSource.Location = new System.Drawing.Point(19, 387);
            this.lvwSource.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lvwSource.Name = "lvwSource";
            this.lvwSource.Size = new System.Drawing.Size(624, 623);
            this.lvwSource.TabIndex = 6;
            this.lvwSource.UseCompatibleStateImageBehavior = false;
            this.lvwSource.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwSource_MouseDoubleClick);
            this.lvwSource.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwSource_MouseUp);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRemove.Location = new System.Drawing.Point(157, 347);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(60, 32);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "r";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(29, 347);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(118, 32);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add ...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(402, 347);
            this.btnStart.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(118, 32);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(529, 347);
            this.btnStop.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(118, 32);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.BackColor = System.Drawing.Color.Black;
            this.lstLog.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 24;
            this.lstLog.Location = new System.Drawing.Point(687, 68);
            this.lstLog.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(1395, 940);
            this.lstLog.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(687, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "Log";
            // 
            // gbxSettings
            // 
            this.gbxSettings.Controls.Add(this.cboHWDeviceType);
            this.gbxSettings.Controls.Add(this.cboHost);
            this.gbxSettings.Controls.Add(this.txtOutputDirectory);
            this.gbxSettings.Controls.Add(this.label1);
            this.gbxSettings.Controls.Add(this.label7);
            this.gbxSettings.Controls.Add(this.label5);
            this.gbxSettings.Controls.Add(this.label8);
            this.gbxSettings.Controls.Add(this.label6);
            this.gbxSettings.Controls.Add(this.label2);
            this.gbxSettings.Controls.Add(this.label3);
            this.gbxSettings.Controls.Add(this.btnOutputDirectory);
            this.gbxSettings.Controls.Add(this.nudProbability);
            this.gbxSettings.Controls.Add(this.nudSkipLimit);
            this.gbxSettings.Location = new System.Drawing.Point(19, 30);
            this.gbxSettings.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gbxSettings.Name = "gbxSettings";
            this.gbxSettings.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gbxSettings.Size = new System.Drawing.Size(625, 309);
            this.gbxSettings.TabIndex = 12;
            this.gbxSettings.TabStop = false;
            this.gbxSettings.Text = "Settings";
            // 
            // cboHWDeviceType
            // 
            this.cboHWDeviceType.FormattingEnabled = true;
            this.cboHWDeviceType.Location = new System.Drawing.Point(190, 198);
            this.cboHWDeviceType.Name = "cboHWDeviceType";
            this.cboHWDeviceType.Size = new System.Drawing.Size(422, 32);
            this.cboHWDeviceType.TabIndex = 7;
            // 
            // cboHost
            // 
            this.cboHost.FormattingEnabled = true;
            this.cboHost.Items.AddRange(new object[] {
            "localhost",
            "frp.evenstandard.top"});
            this.cboHost.Location = new System.Drawing.Point(190, 38);
            this.cboHost.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cboHost.Name = "cboHost";
            this.cboHost.Size = new System.Drawing.Size(422, 32);
            this.cboHost.TabIndex = 6;
            this.cboHost.Text = "frp.evenstandard.top";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "WebAPI Host";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(302, 164);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 24);
            this.label7.TabIndex = 1;
            this.label7.Text = "times";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(302, 126);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 201);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 24);
            this.label8.TabIndex = 1;
            this.label8.Text = "HW Device Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 164);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 24);
            this.label6.TabIndex = 1;
            this.label6.Text = "Skip if matched";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 126);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Min Probability";
            // 
            // nudProbability
            // 
            this.nudProbability.Location = new System.Drawing.Point(190, 123);
            this.nudProbability.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudProbability.Name = "nudProbability";
            this.nudProbability.Size = new System.Drawing.Size(102, 30);
            this.nudProbability.TabIndex = 5;
            this.nudProbability.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2121, 1029);
            this.Controls.Add(this.gbxSettings);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lvwSource);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmMain";
            this.Text = "nsfwjs-gui";
            ((System.ComponentModel.ISupportInitialize)(this.nudSkipLimit)).EndInit();
            this.gbxSettings.ResumeLayout(false);
            this.gbxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProbability)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Button btnOutputDirectory;
        private System.Windows.Forms.NumericUpDown nudSkipLimit;
        private System.Windows.Forms.ListView lvwSource;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbxSettings;
        private System.Windows.Forms.ComboBox cboHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudProbability;
        private System.Windows.Forms.ComboBox cboHWDeviceType;
        private System.Windows.Forms.Label label8;
    }
}