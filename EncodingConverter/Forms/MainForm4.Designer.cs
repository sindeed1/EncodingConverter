namespace EncodingConverter.Forms
{
    partial class MainForm4
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm4));
            this.txtOutputPathFormat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnChangeOutputFile = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCompanionFile = new System.Windows.Forms.TextBox();
            this.txtCompanionFileSearchPattern = new System.Windows.Forms.TextBox();
            this.tsOutput = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsddOutputEncoding = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsInput = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsddInputEncoding = new System.Windows.Forms.ToolStripDropDownButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPreferredInputEncoding = new System.Windows.Forms.TextBox();
            this.chkAutoDetect = new System.Windows.Forms.CheckBox();
            this.linkLabelDetectInputEncoding = new System.Windows.Forms.LinkLabel();
            this.tbInputText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.linkAbout = new System.Windows.Forms.LinkLabel();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.tooltipNormal = new System.Windows.Forms.ToolTip(this.components);
            this.btnOpen = new System.Windows.Forms.Button();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.btnApplyOutputFormatting = new System.Windows.Forms.Button();
            this.ttLongRead = new System.Windows.Forms.ToolTip(this.components);
            this.gbInput = new System.Windows.Forms.GroupBox();
            this.txtInputPath = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tsOutput.SuspendLayout();
            this.tsInput.SuspendLayout();
            this.gbOutput.SuspendLayout();
            this.gbInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutputPathFormat
            // 
            this.txtOutputPathFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputPathFormat.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EncodingConverter.Properties.Settings.Default, "OutputFilePathFormatString", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtOutputPathFormat.Location = new System.Drawing.Point(110, 21);
            this.txtOutputPathFormat.Name = "txtOutputPathFormat";
            this.txtOutputPathFormat.Size = new System.Drawing.Size(129, 20);
            this.txtOutputPathFormat.TabIndex = 0;
            this.txtOutputPathFormat.Text = global::EncodingConverter.Properties.Settings.Default.OutputFilePathFormatString;
            this.ttLongRead.SetToolTip(this.txtOutputPathFormat, resources.GetString("txtOutputPathFormat.ToolTip"));
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Output path format:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(7, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Save to:";
            // 
            // btnChangeOutputFile
            // 
            this.btnChangeOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeOutputFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChangeOutputFile.Location = new System.Drawing.Point(245, 45);
            this.btnChangeOutputFile.Name = "btnChangeOutputFile";
            this.btnChangeOutputFile.Size = new System.Drawing.Size(78, 23);
            this.btnChangeOutputFile.TabIndex = 3;
            this.btnChangeOutputFile.Text = "&Change";
            this.btnChangeOutputFile.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(6, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Companion file:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(6, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Companion file search pattern:";
            // 
            // txtCompanionFile
            // 
            this.txtCompanionFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompanionFile.Location = new System.Drawing.Point(87, 73);
            this.txtCompanionFile.Name = "txtCompanionFile";
            this.txtCompanionFile.Size = new System.Drawing.Size(236, 20);
            this.txtCompanionFile.TabIndex = 3;
            this.tooltipNormal.SetToolTip(this.txtCompanionFile, "The companion file that could be used to format the output path.");
            // 
            // txtCompanionFileSearchPattern
            // 
            this.txtCompanionFileSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompanionFileSearchPattern.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EncodingConverter.Properties.Settings.Default, "CompanionFileSearchPattern", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCompanionFileSearchPattern.Location = new System.Drawing.Point(162, 47);
            this.txtCompanionFileSearchPattern.Name = "txtCompanionFileSearchPattern";
            this.txtCompanionFileSearchPattern.Size = new System.Drawing.Size(161, 20);
            this.txtCompanionFileSearchPattern.TabIndex = 2;
            this.txtCompanionFileSearchPattern.Text = global::EncodingConverter.Properties.Settings.Default.CompanionFileSearchPattern;
            this.ttLongRead.SetToolTip(this.txtCompanionFileSearchPattern, resources.GetString("txtCompanionFileSearchPattern.ToolTip"));
            // 
            // tsOutput
            // 
            this.tsOutput.Dock = System.Windows.Forms.DockStyle.None;
            this.tsOutput.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsOutput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tsddOutputEncoding});
            this.tsOutput.Location = new System.Drawing.Point(0, 71);
            this.tsOutput.Name = "tsOutput";
            this.tsOutput.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsOutput.Size = new System.Drawing.Size(256, 25);
            this.tsOutput.TabIndex = 4;
            this.tsOutput.TabStop = true;
            this.tsOutput.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(101, 22);
            this.toolStripLabel2.Text = "Output Encoding:";
            // 
            // tsddOutputEncoding
            // 
            this.tsddOutputEncoding.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddOutputEncoding.Image = ((System.Drawing.Image)(resources.GetObject("tsddOutputEncoding.Image")));
            this.tsddOutputEncoding.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddOutputEncoding.Name = "tsddOutputEncoding";
            this.tsddOutputEncoding.Size = new System.Drawing.Size(152, 22);
            this.tsddOutputEncoding.Text = "Default Output Encoding";
            // 
            // tsInput
            // 
            this.tsInput.Dock = System.Windows.Forms.DockStyle.None;
            this.tsInput.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsddInputEncoding});
            this.tsInput.Location = new System.Drawing.Point(0, 120);
            this.tsInput.Name = "tsInput";
            this.tsInput.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsInput.Size = new System.Drawing.Size(208, 25);
            this.tsInput.TabIndex = 7;
            this.tsInput.TabStop = true;
            this.tsInput.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(91, 22);
            this.toolStripLabel1.Text = "Input Encoding:";
            // 
            // tsddInputEncoding
            // 
            this.tsddInputEncoding.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddInputEncoding.Image = ((System.Drawing.Image)(resources.GetObject("tsddInputEncoding.Image")));
            this.tsddInputEncoding.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddInputEncoding.Name = "tsddInputEncoding";
            this.tsddInputEncoding.Size = new System.Drawing.Size(114, 22);
            this.tsddInputEncoding.Text = "Arabic (Windows)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(159, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Preferred encoding:";
            // 
            // txtPreferredInputEncoding
            // 
            this.txtPreferredInputEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPreferredInputEncoding.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EncodingConverter.Properties.Settings.Default, "PreferredInputEncoding", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPreferredInputEncoding.Location = new System.Drawing.Point(262, 102);
            this.txtPreferredInputEncoding.Name = "txtPreferredInputEncoding";
            this.txtPreferredInputEncoding.Size = new System.Drawing.Size(61, 20);
            this.txtPreferredInputEncoding.TabIndex = 6;
            this.txtPreferredInputEncoding.Text = global::EncodingConverter.Properties.Settings.Default.PreferredInputEncoding;
            this.tooltipNormal.SetToolTip(this.txtPreferredInputEncoding, "When detecting the encoding, the program will try to find the encoding that match" +
        "es the given text");
            // 
            // chkAutoDetect
            // 
            this.chkAutoDetect.AutoSize = true;
            this.chkAutoDetect.Checked = global::EncodingConverter.Properties.Settings.Default.AutoDetectInputEncoding;
            this.chkAutoDetect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoDetect.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::EncodingConverter.Properties.Settings.Default, "AutoDetectInputEncoding", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkAutoDetect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkAutoDetect.Location = new System.Drawing.Point(13, 104);
            this.chkAutoDetect.Name = "chkAutoDetect";
            this.chkAutoDetect.Size = new System.Drawing.Size(83, 17);
            this.chkAutoDetect.TabIndex = 4;
            this.chkAutoDetect.Text = "&Auto detect";
            this.chkAutoDetect.UseVisualStyleBackColor = true;
            // 
            // linkLabelDetectInputEncoding
            // 
            this.linkLabelDetectInputEncoding.AutoSize = true;
            this.linkLabelDetectInputEncoding.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkLabelDetectInputEncoding.Location = new System.Drawing.Point(100, 105);
            this.linkLabelDetectInputEncoding.Name = "linkLabelDetectInputEncoding";
            this.linkLabelDetectInputEncoding.Size = new System.Drawing.Size(39, 13);
            this.linkLabelDetectInputEncoding.TabIndex = 5;
            this.linkLabelDetectInputEncoding.TabStop = true;
            this.linkLabelDetectInputEncoding.Text = "Detect";
            this.tooltipNormal.SetToolTip(this.linkLabelDetectInputEncoding, "Detect input encoding");
            // 
            // tbInputText
            // 
            this.tbInputText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputText.Location = new System.Drawing.Point(2, 16);
            this.tbInputText.Multiline = true;
            this.tbInputText.Name = "tbInputText";
            this.tbInputText.ReadOnly = true;
            this.tbInputText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInputText.Size = new System.Drawing.Size(210, 241);
            this.tbInputText.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Lines read using selected encoding:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(6, 263);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(214, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Convert and &Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // linkAbout
            // 
            this.linkAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkAbout.AutoSize = true;
            this.linkAbout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAbout.Location = new System.Drawing.Point(502, 268);
            this.linkAbout.Name = "linkAbout";
            this.linkAbout.Size = new System.Drawing.Size(36, 13);
            this.linkAbout.TabIndex = 1;
            this.linkAbout.TabStop = true;
            this.linkAbout.Text = "About";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputPath.Location = new System.Drawing.Point(60, 47);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.ReadOnly = true;
            this.txtOutputPath.Size = new System.Drawing.Size(179, 20);
            this.txtOutputPath.TabIndex = 2;
            // 
            // btnOpen
            // 
            this.btnOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpen.Location = new System.Drawing.Point(6, 19);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "&Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // gbOutput
            // 
            this.gbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOutput.Controls.Add(this.tsOutput);
            this.gbOutput.Controls.Add(this.btnApplyOutputFormatting);
            this.gbOutput.Controls.Add(this.txtOutputPathFormat);
            this.gbOutput.Controls.Add(this.label6);
            this.gbOutput.Controls.Add(this.label5);
            this.gbOutput.Controls.Add(this.btnChangeOutputFile);
            this.gbOutput.Controls.Add(this.txtOutputPath);
            this.gbOutput.Location = new System.Drawing.Point(0, 156);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(329, 99);
            this.gbOutput.TabIndex = 5;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Output";
            // 
            // btnApplyOutputFormatting
            // 
            this.btnApplyOutputFormatting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyOutputFormatting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnApplyOutputFormatting.Location = new System.Drawing.Point(245, 19);
            this.btnApplyOutputFormatting.Name = "btnApplyOutputFormatting";
            this.btnApplyOutputFormatting.Size = new System.Drawing.Size(78, 23);
            this.btnApplyOutputFormatting.TabIndex = 1;
            this.btnApplyOutputFormatting.Text = "Apply";
            this.btnApplyOutputFormatting.UseVisualStyleBackColor = true;
            // 
            // ttLongRead
            // 
            this.ttLongRead.AutomaticDelay = 50;
            this.ttLongRead.AutoPopDelay = 20000;
            this.ttLongRead.InitialDelay = 50;
            this.ttLongRead.ReshowDelay = 10;
            // 
            // gbInput
            // 
            this.gbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInput.Controls.Add(this.label7);
            this.gbInput.Controls.Add(this.txtPreferredInputEncoding);
            this.gbInput.Controls.Add(this.label9);
            this.gbInput.Controls.Add(this.chkAutoDetect);
            this.gbInput.Controls.Add(this.label8);
            this.gbInput.Controls.Add(this.linkLabelDetectInputEncoding);
            this.gbInput.Controls.Add(this.txtCompanionFile);
            this.gbInput.Controls.Add(this.txtCompanionFileSearchPattern);
            this.gbInput.Controls.Add(this.btnOpen);
            this.gbInput.Controls.Add(this.txtInputPath);
            this.gbInput.Controls.Add(this.tsInput);
            this.gbInput.Location = new System.Drawing.Point(0, 0);
            this.gbInput.Name = "gbInput";
            this.gbInput.Size = new System.Drawing.Size(329, 150);
            this.gbInput.TabIndex = 0;
            this.gbInput.TabStop = false;
            this.gbInput.Text = "Input";
            // 
            // txtInputPath
            // 
            this.txtInputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputPath.Location = new System.Drawing.Point(87, 21);
            this.txtInputPath.Name = "txtInputPath";
            this.txtInputPath.ReadOnly = true;
            this.txtInputPath.Size = new System.Drawing.Size(236, 20);
            this.txtInputPath.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbInput);
            this.splitContainer1.Panel1.Controls.Add(this.gbOutput);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbInputText);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(549, 257);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.TabIndex = 0;
            // 
            // MainForm4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 291);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.linkAbout);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500000, 330);
            this.MinimumSize = new System.Drawing.Size(16, 39);
            this.Name = "MainForm4";
            this.Text = "Encoding Converter";
            this.tsOutput.ResumeLayout(false);
            this.tsOutput.PerformLayout();
            this.tsInput.ResumeLayout(false);
            this.tsInput.PerformLayout();
            this.gbOutput.ResumeLayout(false);
            this.gbOutput.PerformLayout();
            this.gbInput.ResumeLayout(false);
            this.gbInput.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutputPathFormat;
        private System.Windows.Forms.ToolTip ttLongRead;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnChangeOutputFile;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCompanionFile;
        private System.Windows.Forms.ToolTip tooltipNormal;
        private System.Windows.Forms.TextBox txtCompanionFileSearchPattern;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPreferredInputEncoding;
        private System.Windows.Forms.CheckBox chkAutoDetect;
        private System.Windows.Forms.LinkLabel linkLabelDetectInputEncoding;
        private System.Windows.Forms.TextBox tbInputText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.LinkLabel linkAbout;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.Button btnApplyOutputFormatting;
        private System.Windows.Forms.GroupBox gbInput;
        private System.Windows.Forms.TextBox txtInputPath;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip tsInput;
        private System.Windows.Forms.ToolStripDropDownButton tsddInputEncoding;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStrip tsOutput;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripDropDownButton tsddOutputEncoding;
    }
}