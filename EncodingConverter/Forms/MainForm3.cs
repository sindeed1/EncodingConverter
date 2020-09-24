using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EncodingConverter.Forms
{
    public partial class MainForm3 : Form
    {
        EncodingInfo[] encodingInfos;

        #region ...ctor...
        public MainForm3()
        {
            InitializeComponent();
            AddEventHandlers();

            encodingInfos = Encoding.GetEncodings();
            evInputEncoding.EncodingInfos = encodingInfos;
            evOutputEncoding.EncodingInfos = encodingInfos;
        }
        void AddEventHandlers()
        {
            //Binding to Settings:
            //Bind(this, nameof(this.ClientSize), Properties.Settings.Default, nameof(Properties.Settings.Default.MainFormSize));
            //Bind(splitContainer1, nameof(splitContainer1.SplitterDistance), Properties.Settings.Default, nameof(Properties.Settings.Default.MainForm_SpliContainer_SplitterDistance));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::EncodingConverter.Properties.Settings.Default, "MainFormSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.splitContainer1.DataBindings.Add(new System.Windows.Forms.Binding("SplitterDistance", global::EncodingConverter.Properties.Settings.Default, "MainForm_SpliContainer_SplitterDistance", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));


            //Binding to Encoding converter core:

            EncodingConverterCore ECC = Program.ECC;
            WinFormsHelpers.Bind(new PropertyLink<bool>(() => chkAutoDetect.Checked, x => chkAutoDetect.Checked = x)
                , x => chkAutoDetect.CheckedChanged += x
                , new PropertyLink<bool>(() => ECC.AutoDetectInputEncoding, x => ECC.AutoDetectInputEncoding = x)
                , x => ECC.AutoDetectInputEncodingChanged += x)
                .UpdateObj2To1();
            txtPreferredInputEncoding.BindText(new PropertyLink<string>(() => ECC.PreferredInputEncoding, x => ECC.PreferredInputEncoding = x)
                , x => ECC.PreferredInputEncodingChanged += x)
                .UpdateObj2To1();
            txtInputPath.BindText(new PropertyLink<string>(() => ECC.InputFilePath, x => ECC.InputFilePath = x)
                , x => ECC.InputFilePathChanged += x)
                .UpdateObj2To1();
            txtOutputPath.BindText(new PropertyLink<string>(() => ECC.OutputFilePath, x => ECC.OutputFilePath = x)
                , x => ECC.OutputFilePathChanged += x)
                .UpdateObj2To1();
            ECC.InputTextChanged += ECC_InputTextChanged; //One way  update.
            if (File.Exists(ECC.InputFilePath))
            {
                richTextBox_in.Text = ECC.InputText;
            }
            WinFormsHelpers.Bind(new PropertyLink<EncodingInfo>(() => evInputEncoding.SelectedEncodingInfo, x => evInputEncoding.SelectedEncodingInfo = x)
                , x => evInputEncoding.SelectedEncodingInfoChanged += x
                , new PropertyLink<EncodingInfo>(() => encodingInfos?.FirstOrDefault(x => x.CodePage == ECC.InputEncoding.CodePage), x => ECC.InputEncoding = x.GetEncoding())
                , x => ECC.InputEncodingChanged += x)
                .UpdateObj2To1();
            WinFormsHelpers.Bind(new PropertyLink<EncodingInfo>(() => evOutputEncoding.SelectedEncodingInfo, x => evOutputEncoding.SelectedEncodingInfo = x)
                , x => evOutputEncoding.SelectedEncodingInfoChanged += x
                , new PropertyLink<EncodingInfo>(() => encodingInfos?.FirstOrDefault(x => x.CodePage == ECC.OutputEncoding.CodePage), x => ECC.OutputEncoding = x.GetEncoding())
                , x => ECC.OutputEncodingChanged += x)
                .UpdateObj2To1();

            this.splitContainerInput.DragEnter += InputControl_DragEnter;
            this.splitContainerInput.DragDrop += InputControl_DragDrop;

            this.linkLanguage.LinkClicked += this.linkLanguage_LinkClicked;
            this.linkAbout.LinkClicked += this.linkAbout_LinkClicked;
            this.linkHelp.LinkClicked += this.linkHelp_LinkClicked;
            this.btnChangeOutputFile.Click += this.btnChangeOutputFile_Click;
            this.linkLabel1.LinkClicked += this.linkLabel1_LinkClicked;
            this.btnSave.Click += this.btnSave_Click;
            //this.encodingsTool_input.SelectedEncodingChanged += this.encodingsTool_input_SelectedEncodingChanged;
            //this.encodingsTool_output.SelectedEncodingChanged += this.encodingsTool_output_SelectedEncodingChanged;

            this.FormClosed += FormMain_FormClosed;
        }
        #endregion

        #region ...Event handlers...
        private void ECC_InputTextChanged(object sender, EventArgs e) { if (File.Exists(Program.ECC.InputFilePath)) richTextBox_in.Text = Program.ECC.InputText; }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.Save();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtInputPath.Text))
            {
                MessageBox.Show(Properties.Resources.Message_PleaseBrowseForInputFileFirst
                    , "AHD Encoding Converter");
                return;
            }
            if (txtOutputPath.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.Message_PleaseBrowseWhereToSaveTheFileFirst
                    , "AHD Encoding Converter");
                return;
            }
            if (evInputEncoding.SelectedEncodingInfo == null)
            {
                return;
            }
            if (evOutputEncoding.SelectedEncodingInfo == null)
            {
                return;
            }

            try
            {
                Program.ECC.Convert();
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show("The program doesn't have the permission to perform the conversion." + ex.ToText());
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't complete the conversion because of the following error: " + ex.ToText());
                ex.WriteToTrace();
                return;
            }

            // Done !!
            DialogResult res = MessageBox.Show(this, Properties.Resources.Message_Done,
                "AHD Encoding Converter", MessageBoxButtons.OK);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //AutoDetectInputEncdoing();
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
        private void InputControl_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files == null || files.Length <= 0)
                return;
            Program.ECC.InputFilePath = files[0];
        }
        private void InputControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        private void linkLanguage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //FrmLanguage frm = new FrmLanguage();
            //frm.ShowDialog();
        }

        #endregion
    }
}
