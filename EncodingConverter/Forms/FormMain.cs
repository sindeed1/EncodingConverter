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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using AHD.Forms;
using System.IO;
using href.Utils;
using EncodingConverter;
using System.Diagnostics;

namespace AEC
{
    public partial class FormMain : Form
    {
        private bool isDetectingInputEncoding = false;
        private object[] _OutputPathFormattingParameters = new object[23];

        #region ...ctor...

        public FormMain()
        {
            InitializeComponent();
            AddEventHandlers();
        }
        public FormMain(string[] args)
        {
            //if (Program.Settings.ShowLanguagesForm)
            //{
            //    FrmLanguage frm = new FrmLanguage();
            //    frm.ShowDialog();
            //}
            InitializeComponent();
            AddEventHandlers();
            if (args != null)
            {
                if (args.Length == 0) return;
                ChangeInputFile(args[0]);
                //txtOutputPath.Text = txtInputPath.Text = args[0];
                //AutoDetectInputEncdoing();
                //ShowOutput();
            }
        }

        void AddEventHandlers()
        {
            this.splitContainerInput.DragEnter += InputControl_DragEnter;
            this.splitContainerInput.DragDrop += InputControl_DragDrop;

            this.linkLanguage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLanguage_LinkClicked);
            this.linkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAbout_LinkClicked);
            this.linkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHelp_LinkClicked);
            this.btnChangeOutputFile.Click += new System.EventHandler(this.btnChangeOutputFile_Click);
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.encodingsTool_input.SelectedEncodingChanged += new System.EventHandler(this.encodingsTool_input_SelectedEncodingChanged);
            this.encodingsTool_output.SelectedEncodingChanged += new System.EventHandler(this.encodingsTool_output_SelectedEncodingChanged);

            this.txtInputPath.TextChanged += TxtInputPath_TextChanged;

        }
        #endregion

        private void TxtInputPath_TextChanged(object sender, EventArgs e)
        {
        }

        void ChangeInputFile(string inputPath)
        {
            txtInputPath.Text = inputPath;
            if (chkAutoDetect.Checked)
                AutoDetectInputEncdoing();
            else
            {
                Encoding encoding;
                encoding = encodingsTool_input.SelectedEncoding;
                richTextBox_in.Lines = File.ReadAllLines(txtInputPath.Text, encoding);
            }

            txtOutputPath.Text = FormatOutputpath(inputPath);// string.Format(formatString, directory, fileName, fileExtention, outputEncoding.EncodingName, outputEncoding.BodyName);
            ShowOutput();
        }
        string FormatOutputpath(string inputPath)
        {
            string formatString = Program.Settings.OutputFilePathFormatString;
            FileInfo file = new FileInfo(inputPath);
            string directory = file.DirectoryName;
            Trace.TraceInformation("Old current directory '" + Directory.GetCurrentDirectory() + "'");
            Directory.SetCurrentDirectory(directory);
            Trace.TraceInformation("New current directory '" + Directory.GetCurrentDirectory() + "'");

            string fileExtention = file.Extension;
            string fileName = file.Name;
            fileName = fileName.Substring(0, fileName.Length - fileExtention.Length);
            fileExtention = fileExtention.TrimStart('.');

            Encoding outputEncoding = encodingsTool_output.SelectedEncoding;
            Encoding inputEncoding = encodingsTool_input.SelectedEncoding;
            _OutputPathFormattingParameters[0] = directory;                     //{0} directory path
            _OutputPathFormattingParameters[1] = fileName;                      //{1} file name without extension
            _OutputPathFormattingParameters[2] = fileExtention;                 //{2} extension
                                                                                //{3-9} reserved and empty
            _OutputPathFormattingParameters[10] = outputEncoding.EncodingName;   //{10} Output encoding name
            _OutputPathFormattingParameters[11] = outputEncoding.BodyName;       //{11} Output encoding Body name
            _OutputPathFormattingParameters[12] = outputEncoding.CodePage;       //{12} Output encoding Code page
                                                                                //{13-19} reserved and empty
            _OutputPathFormattingParameters[20] = inputEncoding.EncodingName;    //{20} Input encoding name
            _OutputPathFormattingParameters[21] = inputEncoding.BodyName;        //{21} Input encoding Body name
            _OutputPathFormattingParameters[22] = inputEncoding.CodePage;        //{22} Input encoding Code page

            string result;
            result = string.Format(formatString, _OutputPathFormattingParameters);
                //, directory                     //{0} directory path
                //, fileName                      //{1} file name without extension
                //, fileExtention                 //{2} extension
                //, "", "", "", "", "", "", ""    //{3-9} reserved and empty
                //, outputEncoding.EncodingName   //{10} Output encoding name
                //, outputEncoding.BodyName       //{11} Output encoding Body name
                //, outputEncoding.CodePage       //{12} Output encoding Code page
                //, "", "", "", "", "", "", ""    //{13-19} reserved and empty
                //, inputEncoding.EncodingName   //{20} Input encoding name
                //, inputEncoding.BodyName       //{21} Input encoding Body name
                //, inputEncoding.CodePage       //{22} Input encoding Code page
                ////, "", "", "", "", "", "", ""    //{23-29} reserved and empty
                //);

            return result;
        }
        private void InputControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        private void InputControl_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files == null || files.Length <= 0)
                return;

            ChangeInputFile(files[0]);
        }

        private void AutoDetectInputEncdoing()
        {
            if (!File.Exists(txtInputPath.Text))
            {
                MessageBox.Show(Program.ResourceManager.GetString("Message_PleaseBrowseForInputFileFirst"),
                    "AHD Encoding Converter");
                return;
            }
            // do auto-detect
            isDetectingInputEncoding = true;
            FileStream stream = new FileStream(txtInputPath.Text, FileMode.Open, FileAccess.Read);
            byte[] buf = new byte[stream.Length];
            stream.Read(buf, 0, (int)stream.Length);
            Encoding encoding = EncodingTools.DetectInputCodepage(buf);
            stream.Close();
            encodingsTool_input.SelectedEncoding = encoding;
            richTextBox_in.Lines = File.ReadAllLines(txtInputPath.Text, encoding);
            isDetectingInputEncoding = false;
        }
        private void ShowOutput()
        {
            if (File.Exists(txtInputPath.Text))
                richTextBox_out.Lines = File.ReadAllLines(txtInputPath.Text, encodingsTool_output.SelectedEncoding);
        }
        private void linkLanguage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //FrmLanguage frm = new FrmLanguage();
            //frm.ShowDialog();
        }
        private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //string helpFile = ".\\" + Program.CultureInfo.Name + "\\Help.chm";
            //if (System.IO.File.Exists(helpFile))
            //    Help.ShowHelp(this, helpFile, "AHD Encoding Converter");
            //else
            //    Help.ShowHelp(this, ".\\en-US\\Help.chm", "AHD Encoding Converter");
        }
        private void linkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //MessageDialog.ShowMessage(Program.ResourceManager.GetString("Message_About"), "About AHD Encoding Converter " + Application.ProductVersion);
        }
        // Change input
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = Program.ResourceManager.GetString("Message_OpenTheFileYouWantToConvertEncodingFor");
            op.Filter = Program.ResourceManager.GetString("Filter_AllFiles");
            op.FileName = txtInputPath.Text;
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                ChangeInputFile(op.FileName);
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AutoDetectInputEncdoing();
        }
        private void encodingsTool_input_SelectedEncodingChanged(object sender, EventArgs e)
        {
            if (isDetectingInputEncoding)
                return;
            if (File.Exists(txtInputPath.Text))
                richTextBox_in.Lines = File.ReadAllLines(txtInputPath.Text, encodingsTool_input.SelectedEncoding);
        }
        private void encodingsTool_output_SelectedEncodingChanged(object sender, EventArgs e)
        {
            ShowOutput();
        }
        private void btnChangeOutputFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = Program.ResourceManager.GetString("Message_BrowseWhereYouWantToSaveTheFile");
            sav.Filter = Program.ResourceManager.GetString("Filter_AllFiles");
            sav.FileName = txtOutputPath.Text;
            if (sav.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtOutputPath.Text = sav.FileName;
            }
        }
        // save !
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtInputPath.Text))
            {
                MessageBox.Show(Program.ResourceManager.GetString("Message_PleaseBrowseForInputFileFirst")
                    , "AHD Encoding Converter");
                //MessageDialog.ShowErrorMessage(Program.ResourceManager.GetString("Message_PleaseBrowseForInputFileFirst")
                //    , "AHD Encoding Converter");
                return;
            }
            if (txtOutputPath.Text.Length == 0)
            {
                MessageBox.Show(Program.ResourceManager.GetString("Message_PleaseBrowseWhereToSaveTheFileFirst"),
                    "AHD Encoding Converter");
                //MessageDialog.ShowErrorMessage(Program.ResourceManager.GetString("Message_PleaseBrowseWhereToSaveTheFileFirst"),
                //    "AHD Encoding Converter");
                return;
            }
            // Read all lines from the file
            string[] lines = File.ReadAllLines(txtInputPath.Text, encodingsTool_input.SelectedEncoding);
            // Save it !
            File.WriteAllLines(txtOutputPath.Text, lines, encodingsTool_output.SelectedEncoding);
            // Done !!
            DialogResult res = MessageBox.Show(this, Program.ResourceManager.GetString("Message_Done"),
                "AHD Encoding Converter", MessageBoxButtons.OK);
            ////MessageDialogResult res = MessageDialog.ShowMessage(this, Program.ResourceManager.GetString("Message_Done"),
            ////    "AHD Encoding Converter",
            ////    MessageDialogButtons.Ok | MessageDialogButtons.Checked, MessageDialogIcon.Info, true, Program.ResourceManager.GetString("Button_OK"), "",
            ////    "", Program.ResourceManager.GetString("CheckBox_OpenSavedFile"));
            //if ((res & MessageDialogResult.Checked) == MessageDialogResult.Checked)
            //{
            //    // Open destination file
            //    try
            //    {
            //        System.Diagnostics.Process.Start(textBox1.Text);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageDialog.ShowErrorMessage(ex.Message, "AHD Encoding Converter");
            //    }
            //}
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

        }
    }
}
