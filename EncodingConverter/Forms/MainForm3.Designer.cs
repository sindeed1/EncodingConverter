namespace EncodingConverter.Forms
{
    partial class MainForm3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm3));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbInput = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCompanionFile = new System.Windows.Forms.TextBox();
            this.txtCompanionFileSearchPattern = new System.Windows.Forms.TextBox();
            this.splitContainerInput = new System.Windows.Forms.SplitContainer();
            this.lblInputEncoding = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPreferredInputEncoding = new System.Windows.Forms.TextBox();
            this.chkAutoDetect = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtInputPath = new System.Windows.Forms.TextBox();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.btnApplyOutputFormatting = new System.Windows.Forms.Button();
            this.txtOutputPathFormat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainerOutput = new System.Windows.Forms.SplitContainer();
            this.lblOutputEncoding = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox_out = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnChangeOutputFile = new System.Windows.Forms.Button();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.linkAbout = new System.Windows.Forms.LinkLabel();
            this.ttLongRead = new System.Windows.Forms.ToolTip(this.components);
            this.tooltipNormal = new System.Windows.Forms.ToolTip(this.components);
            this.tbInputText = new System.Windows.Forms.TextBox();
            this.evInputEncoding = new EncodingConverter.Controls.EncodingsViewer();
            this.evOutputEncoding = new EncodingConverter.Controls.EncodingsViewer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInput)).BeginInit();
            this.splitContainerInput.Panel1.SuspendLayout();
            this.splitContainerInput.Panel2.SuspendLayout();
            this.splitContainerInput.SuspendLayout();
            this.gbOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOutput)).BeginInit();
            this.splitContainerOutput.Panel1.SuspendLayout();
            this.splitContainerOutput.Panel2.SuspendLayout();
            this.splitContainerOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbInput);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbOutput);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(648, 427);
            this.splitContainer1.SplitterDistance = 325;
            this.splitContainer1.TabIndex = 16;
            // 
            // gbInput
            // 
            this.gbInput.Controls.Add(this.label9);
            this.gbInput.Controls.Add(this.label8);
            this.gbInput.Controls.Add(this.txtCompanionFile);
            this.gbInput.Controls.Add(this.txtCompanionFileSearchPattern);
            this.gbInput.Controls.Add(this.splitContainerInput);
            this.gbInput.Controls.Add(this.btnOpen);
            this.gbInput.Controls.Add(this.txtInputPath);
            this.gbInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbInput.Location = new System.Drawing.Point(0, 0);
            this.gbInput.Name = "gbInput";
            this.gbInput.Size = new System.Drawing.Size(325, 427);
            this.gbInput.TabIndex = 4;
            this.gbInput.TabStop = false;
            this.gbInput.Text = "Input";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(6, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Companion file:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(6, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Companion file search pattern:";
            // 
            // txtCompanionFile
            // 
            this.txtCompanionFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompanionFile.Location = new System.Drawing.Point(87, 73);
            this.txtCompanionFile.Name = "txtCompanionFile";
            this.txtCompanionFile.Size = new System.Drawing.Size(232, 20);
            this.txtCompanionFile.TabIndex = 14;
            this.tooltipNormal.SetToolTip(this.txtCompanionFile, "The companion file that could be used to format the output path.");
            // 
            // txtCompanionFileSearchPattern
            // 
            this.txtCompanionFileSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompanionFileSearchPattern.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EncodingConverter.Properties.Settings.Default, "CompanionFileSearchPattern", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCompanionFileSearchPattern.Location = new System.Drawing.Point(162, 47);
            this.txtCompanionFileSearchPattern.Name = "txtCompanionFileSearchPattern";
            this.txtCompanionFileSearchPattern.Size = new System.Drawing.Size(157, 20);
            this.txtCompanionFileSearchPattern.TabIndex = 13;
            this.txtCompanionFileSearchPattern.Text = global::EncodingConverter.Properties.Settings.Default.CompanionFileSearchPattern;
            this.ttLongRead.SetToolTip(this.txtCompanionFileSearchPattern, resources.GetString("txtCompanionFileSearchPattern.ToolTip"));
            // 
            // splitContainerInput
            // 
            this.splitContainerInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerInput.Location = new System.Drawing.Point(6, 99);
            this.splitContainerInput.Name = "splitContainerInput";
            this.splitContainerInput.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerInput.Panel1
            // 
            this.splitContainerInput.Panel1.Controls.Add(this.lblInputEncoding);
            this.splitContainerInput.Panel1.Controls.Add(this.label7);
            this.splitContainerInput.Panel1.Controls.Add(this.txtPreferredInputEncoding);
            this.splitContainerInput.Panel1.Controls.Add(this.chkAutoDetect);
            this.splitContainerInput.Panel1.Controls.Add(this.evInputEncoding);
            this.splitContainerInput.Panel1.Controls.Add(this.linkLabel1);
            this.splitContainerInput.Panel1.Controls.Add(this.label1);
            // 
            // splitContainerInput.Panel2
            // 
            this.splitContainerInput.Panel2.Controls.Add(this.tbInputText);
            this.splitContainerInput.Panel2.Controls.Add(this.label2);
            this.splitContainerInput.Size = new System.Drawing.Size(313, 322);
            this.splitContainerInput.SplitterDistance = 184;
            this.splitContainerInput.TabIndex = 7;
            // 
            // lblInputEncoding
            // 
            this.lblInputEncoding.AutoSize = true;
            this.lblInputEncoding.Location = new System.Drawing.Point(120, 0);
            this.lblInputEncoding.Name = "lblInputEncoding";
            this.lblInputEncoding.Size = new System.Drawing.Size(78, 13);
            this.lblInputEncoding.TabIndex = 4;
            this.lblInputEncoding.Text = "Input encoding";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(149, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Preferred encoding:";
            // 
            // txtPreferredInputEncoding
            // 
            this.txtPreferredInputEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPreferredInputEncoding.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EncodingConverter.Properties.Settings.Default, "PreferredInputEncoding", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPreferredInputEncoding.Location = new System.Drawing.Point(252, 21);
            this.txtPreferredInputEncoding.Name = "txtPreferredInputEncoding";
            this.txtPreferredInputEncoding.Size = new System.Drawing.Size(58, 20);
            this.txtPreferredInputEncoding.TabIndex = 8;
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
            this.chkAutoDetect.Location = new System.Drawing.Point(3, 23);
            this.chkAutoDetect.Name = "chkAutoDetect";
            this.chkAutoDetect.Size = new System.Drawing.Size(81, 17);
            this.chkAutoDetect.TabIndex = 7;
            this.chkAutoDetect.Text = "&Auto detect";
            this.chkAutoDetect.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkLabel1.Location = new System.Drawing.Point(90, 24);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(39, 13);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Detect";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select read encoding:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Lines read using selected encoding:";
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
            // txtInputPath
            // 
            this.txtInputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputPath.Location = new System.Drawing.Point(87, 21);
            this.txtInputPath.Name = "txtInputPath";
            this.txtInputPath.ReadOnly = true;
            this.txtInputPath.Size = new System.Drawing.Size(232, 20);
            this.txtInputPath.TabIndex = 1;
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.btnApplyOutputFormatting);
            this.gbOutput.Controls.Add(this.txtOutputPathFormat);
            this.gbOutput.Controls.Add(this.label6);
            this.gbOutput.Controls.Add(this.splitContainerOutput);
            this.gbOutput.Controls.Add(this.label5);
            this.gbOutput.Controls.Add(this.btnChangeOutputFile);
            this.gbOutput.Controls.Add(this.txtOutputPath);
            this.gbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOutput.Location = new System.Drawing.Point(0, 0);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(319, 427);
            this.gbOutput.TabIndex = 5;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Output";
            // 
            // btnApplyOutputFormatting
            // 
            this.btnApplyOutputFormatting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyOutputFormatting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnApplyOutputFormatting.Location = new System.Drawing.Point(235, 19);
            this.btnApplyOutputFormatting.Name = "btnApplyOutputFormatting";
            this.btnApplyOutputFormatting.Size = new System.Drawing.Size(78, 23);
            this.btnApplyOutputFormatting.TabIndex = 10;
            this.btnApplyOutputFormatting.Text = "Apply";
            this.btnApplyOutputFormatting.UseVisualStyleBackColor = true;
            // 
            // txtOutputPathFormat
            // 
            this.txtOutputPathFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputPathFormat.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EncodingConverter.Properties.Settings.Default, "OutputFilePathFormatString", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtOutputPathFormat.Location = new System.Drawing.Point(110, 21);
            this.txtOutputPathFormat.Name = "txtOutputPathFormat";
            this.txtOutputPathFormat.Size = new System.Drawing.Size(119, 20);
            this.txtOutputPathFormat.TabIndex = 9;
            this.txtOutputPathFormat.Text = global::EncodingConverter.Properties.Settings.Default.OutputFilePathFormatString;
            this.ttLongRead.SetToolTip(this.txtOutputPathFormat, resources.GetString("txtOutputPathFormat.ToolTip"));
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Output path format:";
            // 
            // splitContainerOutput
            // 
            this.splitContainerOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerOutput.Location = new System.Drawing.Point(6, 73);
            this.splitContainerOutput.Name = "splitContainerOutput";
            this.splitContainerOutput.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerOutput.Panel1
            // 
            this.splitContainerOutput.Panel1.Controls.Add(this.lblOutputEncoding);
            this.splitContainerOutput.Panel1.Controls.Add(this.evOutputEncoding);
            this.splitContainerOutput.Panel1.Controls.Add(this.label4);
            // 
            // splitContainerOutput.Panel2
            // 
            this.splitContainerOutput.Panel2.Controls.Add(this.richTextBox_out);
            this.splitContainerOutput.Panel2.Controls.Add(this.label3);
            this.splitContainerOutput.Size = new System.Drawing.Size(307, 348);
            this.splitContainerOutput.SplitterDistance = 191;
            this.splitContainerOutput.TabIndex = 7;
            // 
            // lblOutputEncoding
            // 
            this.lblOutputEncoding.AutoSize = true;
            this.lblOutputEncoding.Location = new System.Drawing.Point(129, 0);
            this.lblOutputEncoding.Name = "lblOutputEncoding";
            this.lblOutputEncoding.Size = new System.Drawing.Size(84, 13);
            this.lblOutputEncoding.TabIndex = 4;
            this.lblOutputEncoding.Text = "OutputEncoding";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Select writing encoding:";
            // 
            // richTextBox_out
            // 
            this.richTextBox_out.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_out.Location = new System.Drawing.Point(0, 21);
            this.richTextBox_out.Name = "richTextBox_out";
            this.richTextBox_out.ReadOnly = true;
            this.richTextBox_out.Size = new System.Drawing.Size(307, 129);
            this.richTextBox_out.TabIndex = 4;
            this.richTextBox_out.Text = "";
            this.richTextBox_out.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Lines will be written using selected encoding:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(7, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Save to:";
            // 
            // btnChangeOutputFile
            // 
            this.btnChangeOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeOutputFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChangeOutputFile.Location = new System.Drawing.Point(235, 45);
            this.btnChangeOutputFile.Name = "btnChangeOutputFile";
            this.btnChangeOutputFile.Size = new System.Drawing.Size(78, 23);
            this.btnChangeOutputFile.TabIndex = 0;
            this.btnChangeOutputFile.Text = "&Change";
            this.btnChangeOutputFile.UseVisualStyleBackColor = true;
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputPath.Location = new System.Drawing.Point(60, 47);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.ReadOnly = true;
            this.txtOutputPath.Size = new System.Drawing.Size(169, 20);
            this.txtOutputPath.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(429, 435);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(214, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Convert and &Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // linkAbout
            // 
            this.linkAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkAbout.AutoSize = true;
            this.linkAbout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAbout.Location = new System.Drawing.Point(14, 440);
            this.linkAbout.Name = "linkAbout";
            this.linkAbout.Size = new System.Drawing.Size(35, 13);
            this.linkAbout.TabIndex = 12;
            this.linkAbout.TabStop = true;
            this.linkAbout.Text = "About";
            // 
            // ttLongRead
            // 
            this.ttLongRead.AutomaticDelay = 50;
            this.ttLongRead.AutoPopDelay = 20000;
            this.ttLongRead.InitialDelay = 50;
            this.ttLongRead.ReshowDelay = 10;
            // 
            // tbInputText
            // 
            this.tbInputText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputText.Location = new System.Drawing.Point(0, 23);
            this.tbInputText.Multiline = true;
            this.tbInputText.Name = "tbInputText";
            this.tbInputText.ReadOnly = true;
            this.tbInputText.Size = new System.Drawing.Size(313, 111);
            this.tbInputText.TabIndex = 6;
            // 
            // evInputEncoding
            // 
            this.evInputEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.evInputEncoding.Location = new System.Drawing.Point(3, 46);
            this.evInputEncoding.Name = "evInputEncoding";
            this.evInputEncoding.SelectedEncodingInfo = null;
            this.evInputEncoding.Size = new System.Drawing.Size(310, 139);
            this.evInputEncoding.TabIndex = 2;
            // 
            // evOutputEncoding
            // 
            this.evOutputEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.evOutputEncoding.Location = new System.Drawing.Point(0, 26);
            this.evOutputEncoding.Name = "evOutputEncoding";
            this.evOutputEncoding.SelectedEncodingInfo = null;
            this.evOutputEncoding.Size = new System.Drawing.Size(307, 162);
            this.evOutputEncoding.TabIndex = 2;
            // 
            // MainForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 465);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.linkAbout);
            this.Name = "MainForm3";
            this.Text = "Encoding Converter";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbInput.ResumeLayout(false);
            this.gbInput.PerformLayout();
            this.splitContainerInput.Panel1.ResumeLayout(false);
            this.splitContainerInput.Panel1.PerformLayout();
            this.splitContainerInput.Panel2.ResumeLayout(false);
            this.splitContainerInput.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInput)).EndInit();
            this.splitContainerInput.ResumeLayout(false);
            this.gbOutput.ResumeLayout(false);
            this.gbOutput.PerformLayout();
            this.splitContainerOutput.Panel1.ResumeLayout(false);
            this.splitContainerOutput.Panel1.PerformLayout();
            this.splitContainerOutput.Panel2.ResumeLayout(false);
            this.splitContainerOutput.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOutput)).EndInit();
            this.splitContainerOutput.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbInput;
        private System.Windows.Forms.SplitContainer splitContainerInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPreferredInputEncoding;
        private System.Windows.Forms.CheckBox chkAutoDetect;
        private Controls.EncodingsViewer evInputEncoding;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtInputPath;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.Button btnApplyOutputFormatting;
        private System.Windows.Forms.TextBox txtOutputPathFormat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer splitContainerOutput;
        private Controls.EncodingsViewer evOutputEncoding;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox_out;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnChangeOutputFile;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.LinkLabel linkAbout;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCompanionFile;
        private System.Windows.Forms.TextBox txtCompanionFileSearchPattern;
        private System.Windows.Forms.ToolTip ttLongRead;
        private System.Windows.Forms.Label lblInputEncoding;
        private System.Windows.Forms.Label lblOutputEncoding;
        private System.Windows.Forms.ToolTip tooltipNormal;
        private System.Windows.Forms.TextBox tbInputText;
    }
}

