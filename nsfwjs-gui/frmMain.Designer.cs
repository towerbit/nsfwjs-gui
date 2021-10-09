
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
            this.rbtLocal = new System.Windows.Forms.RadioButton();
            this.rbtRemote = new System.Windows.Forms.RadioButton();
            this.chkOutputLimit = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.btnOutputDirectory = new System.Windows.Forms.Button();
            this.nudOutputLimit = new System.Windows.Forms.NumericUpDown();
            this.lvwSource = new System.Windows.Forms.ListView();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputLimit)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtLocal
            // 
            this.rbtLocal.AutoSize = true;
            this.rbtLocal.Location = new System.Drawing.Point(10, 29);
            this.rbtLocal.Name = "rbtLocal";
            this.rbtLocal.Size = new System.Drawing.Size(150, 21);
            this.rbtLocal.TabIndex = 0;
            this.rbtLocal.TabStop = true;
            this.rbtLocal.Text = "Start local rest server";
            this.rbtLocal.UseVisualStyleBackColor = true;
            // 
            // rbtRemote
            // 
            this.rbtRemote.AutoSize = true;
            this.rbtRemote.Location = new System.Drawing.Point(10, 56);
            this.rbtRemote.Name = "rbtRemote";
            this.rbtRemote.Size = new System.Drawing.Size(160, 21);
            this.rbtRemote.TabIndex = 0;
            this.rbtRemote.TabStop = true;
            this.rbtRemote.Text = "Use remote rest server";
            this.rbtRemote.UseVisualStyleBackColor = true;
            // 
            // chkOutputLimit
            // 
            this.chkOutputLimit.AutoSize = true;
            this.chkOutputLimit.Location = new System.Drawing.Point(10, 116);
            this.chkOutputLimit.Name = "chkOutputLimit";
            this.chkOutputLimit.Size = new System.Drawing.Size(178, 21);
            this.chkOutputLimit.TabIndex = 4;
            this.chkOutputLimit.Text = "Max output count limit to ";
            this.chkOutputLimit.UseVisualStyleBackColor = true;
            this.chkOutputLimit.CheckedChanged += new System.EventHandler(this.chkOutputLimit_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Output Directory";
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Location = new System.Drawing.Point(121, 86);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(226, 23);
            this.txtOutputDirectory.TabIndex = 2;
            this.txtOutputDirectory.Text = "d:\\temp\\nsfwjs_output";
            // 
            // btnOutputDirectory
            // 
            this.btnOutputDirectory.Location = new System.Drawing.Point(353, 86);
            this.btnOutputDirectory.Name = "btnOutputDirectory";
            this.btnOutputDirectory.Size = new System.Drawing.Size(38, 23);
            this.btnOutputDirectory.TabIndex = 3;
            this.btnOutputDirectory.Text = "...";
            this.btnOutputDirectory.UseVisualStyleBackColor = true;
            this.btnOutputDirectory.Click += new System.EventHandler(this.btnOutputDirectory_Click);
            // 
            // nudOutputLimit
            // 
            this.nudOutputLimit.Location = new System.Drawing.Point(194, 115);
            this.nudOutputLimit.Name = "nudOutputLimit";
            this.nudOutputLimit.Size = new System.Drawing.Size(65, 23);
            this.nudOutputLimit.TabIndex = 5;
            this.nudOutputLimit.Value = new decimal(new int[] {
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
            this.groupBox1.Controls.Add(this.txtOutputDirectory);
            this.groupBox1.Controls.Add(this.rbtRemote);
            this.groupBox1.Controls.Add(this.rbtLocal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkOutputLimit);
            this.groupBox1.Controls.Add(this.btnOutputDirectory);
            this.groupBox1.Controls.Add(this.nudOutputLimit);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 160);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lvwSource);
            this.Name = "frmMain";
            this.Text = "nsfwjs-gui";
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputLimit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtLocal;
        private System.Windows.Forms.RadioButton rbtRemote;
        private System.Windows.Forms.CheckBox chkOutputLimit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Button btnOutputDirectory;
        private System.Windows.Forms.NumericUpDown nudOutputLimit;
        private System.Windows.Forms.ListView lvwSource;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}