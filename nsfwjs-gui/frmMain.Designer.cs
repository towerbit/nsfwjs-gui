
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboHost = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudProbability = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkipLimit)).BeginInit();
            this.gbxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProbability)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Output Directory";
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Location = new System.Drawing.Point(121, 58);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(226, 23);
            this.txtOutputDirectory.TabIndex = 2;
            this.txtOutputDirectory.Text = "d:\\temp\\nsfwjs_output";
            // 
            // btnOutputDirectory
            // 
            this.btnOutputDirectory.Location = new System.Drawing.Point(353, 58);
            this.btnOutputDirectory.Name = "btnOutputDirectory";
            this.btnOutputDirectory.Size = new System.Drawing.Size(38, 23);
            this.btnOutputDirectory.TabIndex = 3;
            this.btnOutputDirectory.Text = "...";
            this.btnOutputDirectory.UseVisualStyleBackColor = true;
            this.btnOutputDirectory.Click += new System.EventHandler(this.btnOutputDirectory_Click);
            // 
            // nudSkipLimit
            // 
            this.nudSkipLimit.Location = new System.Drawing.Point(121, 116);
            this.nudSkipLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSkipLimit.Name = "nudSkipLimit";
            this.nudSkipLimit.Size = new System.Drawing.Size(65, 23);
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
            this.lvwSource.Location = new System.Drawing.Point(18, 234);
            this.lvwSource.Name = "lvwSource";
            this.lvwSource.Size = new System.Drawing.Size(393, 483);
            this.lvwSource.TabIndex = 6;
            this.lvwSource.UseCompatibleStateImageBehavior = false;
            this.lvwSource.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwSource_MouseDoubleClick);
            this.lvwSource.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwSource_MouseUp);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRemove.Location = new System.Drawing.Point(99, 205);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(38, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "r";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(18, 205);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add ...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(255, 205);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(336, 205);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
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
            this.lstLog.ItemHeight = 17;
            this.lstLog.Location = new System.Drawing.Point(437, 48);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(889, 667);
            this.lstLog.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(437, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Log";
            // 
            // groupBox1
            // 
            this.gbxSettings.Controls.Add(this.cboHost);
            this.gbxSettings.Controls.Add(this.txtOutputDirectory);
            this.gbxSettings.Controls.Add(this.label1);
            this.gbxSettings.Controls.Add(this.label7);
            this.gbxSettings.Controls.Add(this.label5);
            this.gbxSettings.Controls.Add(this.label6);
            this.gbxSettings.Controls.Add(this.label2);
            this.gbxSettings.Controls.Add(this.label3);
            this.gbxSettings.Controls.Add(this.btnOutputDirectory);
            this.gbxSettings.Controls.Add(this.nudProbability);
            this.gbxSettings.Controls.Add(this.nudSkipLimit);
            this.gbxSettings.Location = new System.Drawing.Point(12, 21);
            this.gbxSettings.Name = "groupBox1";
            this.gbxSettings.Size = new System.Drawing.Size(398, 160);
            this.gbxSettings.TabIndex = 12;
            this.gbxSettings.TabStop = false;
            this.gbxSettings.Text = "Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "WebAPI Host";
            // 
            // cboHost
            // 
            this.cboHost.FormattingEnabled = true;
            this.cboHost.Items.AddRange(new object[] {
            "localhost",
            "frp.evenstandard.top"});
            this.cboHost.Location = new System.Drawing.Point(121, 27);
            this.cboHost.Name = "cboHost";
            this.cboHost.Size = new System.Drawing.Size(270, 25);
            this.cboHost.TabIndex = 6;
            this.cboHost.Text = "frp.evenstandard.top";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Min Probability";
            // 
            // nudProbability
            // 
            this.nudProbability.Location = new System.Drawing.Point(121, 87);
            this.nudProbability.Name = "nudProbability";
            this.nudProbability.Size = new System.Drawing.Size(65, 23);
            this.nudProbability.TabIndex = 5;
            this.nudProbability.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Skip if matched";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(192, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "times";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.gbxSettings);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lvwSource);
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
    }
}