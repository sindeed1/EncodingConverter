/* This file is part of AHD Subtitles Maker Professional
   A program can create and edit subtitles

   Copyright © Ala Ibrahim Hadid 2009 - 2015

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace AEC
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.linkHelp = new System.Windows.Forms.LinkLabel();
            this.linkAbout = new System.Windows.Forms.LinkLabel();
            this.linkLanguage = new System.Windows.Forms.LinkLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtPreferredInputEncoding = new System.Windows.Forms.TextBox();
            this.txtOutputPathFormat = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbInput = new System.Windows.Forms.GroupBox();
            this.splitContainerInput = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.chkAutoDetect = new System.Windows.Forms.CheckBox();
            this.encodingsTool_input = new AHD.SM.Controls.EncodingsTool();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_in = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtInputPath = new System.Windows.Forms.TextBox();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.btnApplyOutputFormatting = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainerOutput = new System.Windows.Forms.SplitContainer();
            this.encodingsTool_output = new AHD.SM.Controls.EncodingsTool();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox_out = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnChangeOutputFile = new System.Windows.Forms.Button();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
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
            // linkHelp
            // 
            resources.ApplyResources(this.linkHelp, "linkHelp");
            this.linkHelp.Name = "linkHelp";
            this.linkHelp.TabStop = true;
            // 
            // linkAbout
            // 
            resources.ApplyResources(this.linkAbout, "linkAbout");
            this.linkAbout.Name = "linkAbout";
            this.linkAbout.TabStop = true;
            // 
            // linkLanguage
            // 
            resources.ApplyResources(this.linkLanguage, "linkLanguage");
            this.linkLanguage.Name = "linkLanguage";
            this.linkLanguage.TabStop = true;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 20000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 40;
            this.toolTip1.ShowAlways = true;
            // 
            // txtPreferredInputEncoding
            // 
            resources.ApplyResources(this.txtPreferredInputEncoding, "txtPreferredInputEncoding");
            this.txtPreferredInputEncoding.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EncodingConverter.Properties.Settings.Default, "PreferredInputEncoding", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPreferredInputEncoding.Name = "txtPreferredInputEncoding";
            this.txtPreferredInputEncoding.Text = global::EncodingConverter.Properties.Settings.Default.PreferredInputEncoding;
            this.toolTip1.SetToolTip(this.txtPreferredInputEncoding, resources.GetString("txtPreferredInputEncoding.ToolTip"));
            // 
            // txtOutputPathFormat
            // 
            resources.ApplyResources(this.txtOutputPathFormat, "txtOutputPathFormat");
            this.txtOutputPathFormat.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EncodingConverter.Properties.Settings.Default, "OutputFilePathFormatString", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtOutputPathFormat.Name = "txtOutputPathFormat";
            this.txtOutputPathFormat.Text = global::EncodingConverter.Properties.Settings.Default.OutputFilePathFormatString;
            this.toolTip1.SetToolTip(this.txtOutputPathFormat, resources.GetString("txtOutputPathFormat.ToolTip"));
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbInput);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbOutput);
            // 
            // gbInput
            // 
            this.gbInput.Controls.Add(this.splitContainerInput);
            this.gbInput.Controls.Add(this.btnOpen);
            this.gbInput.Controls.Add(this.txtInputPath);
            resources.ApplyResources(this.gbInput, "gbInput");
            this.gbInput.Name = "gbInput";
            this.gbInput.TabStop = false;
            // 
            // splitContainerInput
            // 
            this.splitContainerInput.AllowDrop = true;
            resources.ApplyResources(this.splitContainerInput, "splitContainerInput");
            this.splitContainerInput.Name = "splitContainerInput";
            // 
            // splitContainerInput.Panel1
            // 
            this.splitContainerInput.Panel1.Controls.Add(this.label7);
            this.splitContainerInput.Panel1.Controls.Add(this.txtPreferredInputEncoding);
            this.splitContainerInput.Panel1.Controls.Add(this.chkAutoDetect);
            this.splitContainerInput.Panel1.Controls.Add(this.encodingsTool_input);
            this.splitContainerInput.Panel1.Controls.Add(this.linkLabel1);
            this.splitContainerInput.Panel1.Controls.Add(this.label1);
            // 
            // splitContainerInput.Panel2
            // 
            this.splitContainerInput.Panel2.Controls.Add(this.richTextBox_in);
            this.splitContainerInput.Panel2.Controls.Add(this.label2);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // chkAutoDetect
            // 
            resources.ApplyResources(this.chkAutoDetect, "chkAutoDetect");
            this.chkAutoDetect.Checked = global::EncodingConverter.Properties.Settings.Default.AutoDetectInputEncoding;
            this.chkAutoDetect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoDetect.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::EncodingConverter.Properties.Settings.Default, "AutoDetectInputEncoding", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkAutoDetect.Name = "chkAutoDetect";
            this.chkAutoDetect.UseVisualStyleBackColor = true;
            // 
            // encodingsTool_input
            // 
            resources.ApplyResources(this.encodingsTool_input, "encodingsTool_input");
            this.encodingsTool_input.Name = "encodingsTool_input";
            this.encodingsTool_input.SelectedEncoding = ((System.Text.Encoding)(resources.GetObject("encodingsTool_input.SelectedEncoding")));
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // richTextBox_in
            // 
            resources.ApplyResources(this.richTextBox_in, "richTextBox_in");
            this.richTextBox_in.Name = "richTextBox_in";
            this.richTextBox_in.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // txtInputPath
            // 
            resources.ApplyResources(this.txtInputPath, "txtInputPath");
            this.txtInputPath.Name = "txtInputPath";
            this.txtInputPath.ReadOnly = true;
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
            resources.ApplyResources(this.gbOutput, "gbOutput");
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.TabStop = false;
            // 
            // btnApplyOutputFormatting
            // 
            resources.ApplyResources(this.btnApplyOutputFormatting, "btnApplyOutputFormatting");
            this.btnApplyOutputFormatting.Name = "btnApplyOutputFormatting";
            this.btnApplyOutputFormatting.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // splitContainerOutput
            // 
            resources.ApplyResources(this.splitContainerOutput, "splitContainerOutput");
            this.splitContainerOutput.Name = "splitContainerOutput";
            // 
            // splitContainerOutput.Panel1
            // 
            this.splitContainerOutput.Panel1.Controls.Add(this.encodingsTool_output);
            this.splitContainerOutput.Panel1.Controls.Add(this.label4);
            // 
            // splitContainerOutput.Panel2
            // 
            this.splitContainerOutput.Panel2.Controls.Add(this.richTextBox_out);
            this.splitContainerOutput.Panel2.Controls.Add(this.label3);
            // 
            // encodingsTool_output
            // 
            resources.ApplyResources(this.encodingsTool_output, "encodingsTool_output");
            this.encodingsTool_output.Name = "encodingsTool_output";
            this.encodingsTool_output.SelectedEncoding = ((System.Text.Encoding)(resources.GetObject("encodingsTool_output.SelectedEncoding")));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // richTextBox_out
            // 
            resources.ApplyResources(this.richTextBox_out, "richTextBox_out");
            this.richTextBox_out.Name = "richTextBox_out";
            this.richTextBox_out.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btnChangeOutputFile
            // 
            resources.ApplyResources(this.btnChangeOutputFile, "btnChangeOutputFile");
            this.btnChangeOutputFile.Name = "btnChangeOutputFile";
            this.btnChangeOutputFile.UseVisualStyleBackColor = true;
            // 
            // txtOutputPath
            // 
            resources.ApplyResources(this.txtOutputPath, "txtOutputPath");
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.ReadOnly = true;
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.linkLanguage);
            this.Controls.Add(this.linkHelp);
            this.Controls.Add(this.linkAbout);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::EncodingConverter.Properties.Settings.Default, "MainFormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::EncodingConverter.Properties.Settings.Default.MainFormLocation;
            this.Name = "FormMain";
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
        private System.Windows.Forms.LinkLabel linkHelp;
        private System.Windows.Forms.LinkLabel linkAbout;
        private System.Windows.Forms.LinkLabel linkLanguage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbInput;
        private System.Windows.Forms.SplitContainer splitContainerInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPreferredInputEncoding;
        private System.Windows.Forms.CheckBox chkAutoDetect;
        private AHD.SM.Controls.EncodingsTool encodingsTool_input;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_in;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtInputPath;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.Button btnApplyOutputFormatting;
        private System.Windows.Forms.TextBox txtOutputPathFormat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer splitContainerOutput;
        private AHD.SM.Controls.EncodingsTool encodingsTool_output;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox_out;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnChangeOutputFile;
        private System.Windows.Forms.TextBox txtOutputPath;
    }
}

