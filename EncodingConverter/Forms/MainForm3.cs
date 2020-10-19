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
using System.Diagnostics;

namespace EncodingConverter.Forms
{
    public partial class MainForm3 : Form
    {
        EncodingInfo[] encodingInfos;
        Lazy<SaveFileDialog> _SFD;
        Lazy<OpenFileDialog> _OFD;

        OutputPathFormatter _OFF;
        #region ...ctor...
        public MainForm3()
        {
            InitializeComponent();
            _OFF = new OutputPathFormatter(Program.ECC);
            AddEventHandlers();

            this.gbInput.AllowDrop = true;

            encodingInfos = Encoding.GetEncodings();
            evInputEncoding.EncodingInfos = encodingInfos;
            evOutputEncoding.EncodingInfos = encodingInfos;

            EncodingInfo[] favs = Properties.Settings.Default.FavoriteEncodings?.Select(cp => encodingInfos.FirstOrDefault(enc => enc.CodePage == cp)).ToArray();
            evInputEncoding.FavoriteEncodingInfos = favs;
            evOutputEncoding.FavoriteEncodingInfos = favs;

            _OFD = new Lazy<OpenFileDialog>();
            _SFD = new Lazy<SaveFileDialog>();
        }
        void AddEventHandlers()
        {
            //Binding to Settings:
            //Bind(this, nameof(this.ClientSize), Properties.Settings.Default, nameof(Properties.Settings.Default.MainFormSize));
            //Bind(splitContainer1, nameof(splitContainer1.SplitterDistance), Properties.Settings.Default, nameof(Properties.Settings.Default.MainForm_SpliContainer_SplitterDistance));
            //this.DataBindings.Add(new Binding("ClientSize", Properties.Settings.Default,  "MainFormSize", true, DataSourceUpdateMode.OnPropertyChanged));
            //this.splitContainer1.DataBindings.Add(new Binding("SplitterDistance", Properties.Settings.Default, "MainForm_SpliContainer_SplitterDistance", true, DataSourceUpdateMode.OnPropertyChanged));


            //Binding to Encoding converter core:
            EncodingConverterCore ECC = Program.ECC;
            //Bind 'ECC.AutoDetectInputEncoding' to 'chkAutoDetect.Checked':
            WinFormsHelpers.Bind(new PropertyLink<bool>(() => chkAutoDetect.Checked, x => chkAutoDetect.Checked = x)
                , new EventLink(chkAutoDetect, nameof(CheckBox.CheckedChanged))
                , new PropertyLink<bool>(() => ECC.AutoDetectInputEncoding, x => ECC.AutoDetectInputEncoding = x)
                , new EventLink(ECC, nameof(ECC.AutoDetectInputEncodingChanged)))
                .UpdateObj2To1();
            //Bind 'ECC.PreferredInputEncoding' to 'txtPreferredInputEncoding':
            txtPreferredInputEncoding.BindText(new PropertyLink<string>(() => ECC.PreferredInputEncoding, x => ECC.PreferredInputEncoding = x)
                , new EventLink(ECC, nameof(ECC.PreferredInputEncodingChanged)))
                .UpdateObj2To1();
            //Bind 'ECC.InputFilePath' to 'txtInputPath':
            txtInputPath.BindText(new PropertyLink<string>(() => ECC.InputFilePath, x => ECC.InputFilePath = x)
                , new EventLink(ECC, nameof(ECC.InputFilePathChanged)))
                .UpdateObj2To1();
            //Bind 'ECC.OutputFilePath' to 'txtOutputPath':
            txtOutputPath.BindText(new PropertyLink<string>(() => ECC.OutputFilePath, x => ECC.OutputFilePath = x)
                , new EventLink(ECC, nameof(ECC.OutputFilePathChanged)))
                .UpdateObj2To1();
            ECC.InputTextChanged += ECC_InputTextChanged; //One way update.
            if (File.Exists(ECC.InputFilePath))
            {
                richTextBox_in.Text = ECC.InputText;
            }

            var inputEncodingEventLink = new EventLink(ECC, nameof(ECC.InputEncodingChanged));
            //Bind 'ECC.InputEncoding' to 'evInputEncoding.SelectedEncodingInfo':
            WinFormsHelpers.Bind(new PropertyLink<EncodingInfo>(() => evInputEncoding.SelectedEncodingInfo, x => evInputEncoding.SelectedEncodingInfo = x)
                , new EventLink(evInputEncoding, nameof(evInputEncoding.SelectedEncodingInfoChanged))
                , new PropertyLink<EncodingInfo>(() => encodingInfos?.FirstOrDefault(x => x.CodePage == ECC.InputEncoding.CodePage), x => ECC.InputEncoding = x?.GetEncoding())
                , inputEncodingEventLink)
                .UpdateObj2To1();
            //Bind 'ECC.InputEncoding' to 'lblInputEncoding':
            lblInputEncoding.BindTextAsDestination(() => ECC.InputEncoding?.EncodingName, inputEncodingEventLink).Update();

            //Bind 'ECC.OutputEncoding' to 'evOutputEncoding.SelectedEncodingInfo':
            var outputEncodingEventLink = new EventLink(ECC, nameof(ECC.OutputEncodingChanged));
            WinFormsHelpers.Bind(new PropertyLink<EncodingInfo>(() => evOutputEncoding.SelectedEncodingInfo, x => evOutputEncoding.SelectedEncodingInfo = x)
                , new EventLink(evOutputEncoding, nameof(evOutputEncoding.SelectedEncodingInfoChanged))
                , new PropertyLink<EncodingInfo>(() => encodingInfos?.FirstOrDefault(x => x.CodePage == ECC.OutputEncoding.CodePage), x => ECC.OutputEncoding = x?.GetEncoding())
                , outputEncodingEventLink)
                .UpdateObj2To1();
            //Bind 'ECC.OutputEncoding' to 'lblOutputEncoding':
            lblOutputEncoding.BindTextAsDestination(() => ECC.OutputEncoding?.EncodingName, outputEncodingEventLink).Update();


            txtOutputPathFormat.BindText(new PropertyLink<string>(() => _OFF.FormatString, x => _OFF.FormatString = x)
                , new EventLink(_OFF, nameof(_OFF.FormatStringChanged))).UpdateObj2To1();
            txtCompanionFileSearchPattern.BindText(new PropertyLink<string>(() => _OFF.CompanionFileSearchPattern, x => _OFF.CompanionFileSearchPattern = x)
                , new EventLink(_OFF, nameof(_OFF.CompanionFileSearchPatternChanged))).UpdateObj2To1();

            txtCompanionFile.BindText(new PropertyLink<string>(() => _OFF.CompanionFile, x => _OFF.CompanionFile = x)
                , new EventLink(_OFF, nameof(_OFF.CompanionFileChanged))).UpdateObj2To1();


            //Bind favorite encoding of OutputEncodingsViewer to favorite encoding of InputEncodingsViewer
            var OutputEncodingFavsPropLink = new PropertyLink<EncodingInfo[]>(() => evOutputEncoding.FavoriteEncodingInfos, x => evOutputEncoding.FavoriteEncodingInfos = x);
            var evLink = new EventLink(evOutputEncoding, nameof(evOutputEncoding.FavoriteEncodingInfosChanged));

            WinFormsHelpers.Bind(OutputEncodingFavsPropLink
                , evLink
                , new PropertyLink<EncodingInfo[]>(() => evInputEncoding.FavoriteEncodingInfos, x => evInputEncoding.FavoriteEncodingInfos = x)
                , new EventLink(evInputEncoding, nameof(evInputEncoding.FavoriteEncodingInfosChanged)))
                ;

            //WinFormsHelpers.Bind(OutputEncodingFavsPropLink
            //    , evLink
            //    , new PropertyLink<EncodingsCollection>(() => evInputEncoding.FavoriteEncodingInfos, x => evInputEncoding.FavoriteEncodingInfos = x)
            //    , new EventLink(evInputEncoding, nameof(evInputEncoding.FavoriteEncodingInfosChanged)))
            //    ;

            this.gbInput.DragEnter += InputControl_DragEnter;
            this.gbInput.DragDrop += InputControl_DragDrop;

            this.linkAbout.LinkClicked += this.linkAbout_LinkClicked;
            this.btnChangeOutputFile.Click += this.btnChangeOutputFile_Click;
            this.linkLabel1.LinkClicked += this.linkLabel1_LinkClicked;
            this.btnSave.Click += this.btnSave_Click;
            this.btnOpen.Click += BtnOpen_Click;
            this.btnApplyOutputFormatting.Click += BtnApplyOutputFormatting_Click;


            this.Load += MainForm3_Load;
            this.FormClosed += FormMain_FormClosed;
        }

        private void MainForm3_Load(object sender, EventArgs e)
        {
            Properties.Settings defSet = Properties.Settings.Default;
            this.DataBindings.Add(new Binding("ClientSize"
                                            , Properties.Settings.Default
                                            , nameof(Properties.Settings.Default.MainFormSize)
                                            , true
                                            , DataSourceUpdateMode.OnPropertyChanged));
            //this.splitContainer1.DataBindings.Add(new Binding("SplitterDistance"
            //                                                , Properties.Settings.Default
            //                                                , nameof(Properties.Settings.Default.MainForm_SpliContainer_SplitterDistance)
            //                                                , true
            //                                                , DataSourceUpdateMode.OnPropertyChanged));
            //splitContainer1.SplitterMoved += SplitContainer1_SplitterMoved;
            //Trying to bind 'splitContainer1.SplitterDistance' to 'Properties.Settings.Default.MainForm_SpliContainer_SplitterDistance' with 'splitContainer1.SplitterMoved' as activator
            defSet.PropertyChanged += DefSet_PropertyChanged;
            WinFormsHelpers.Bind(new PropertyLink<int>(() => splitContainer1.SplitterDistance, x => { if (x != splitContainer1.SplitterDistance) splitContainer1.SplitterDistance = x; })
                , new EventLink(splitContainer1, nameof(splitContainer1.SplitterMoved))// x => splitContainer1.SplitterMoved += (s, es) => x(s, EventArgs.Empty)
                , new PropertyLink<int>(() => defSet.MainForm_SpliContainer_SplitterDistance, x => defSet.MainForm_SpliContainer_SplitterDistance = x)
                , new EventLink(this, nameof(this.DefSetSplitterDistanceChanged)))// x => defSet.PropertyChanged += (s, es) => { if (es.PropertyName == nameof(defSet.MainForm_SpliContainer_SplitterDistance)) x(s, EventArgs.Empty); })
                .UpdateObj2To1();

            //WinFormsHelpers.Bind(new PropertyLink<int>(() => splitContainer1.SplitterDistance, x => { if (x != splitContainer1.SplitterDistance) splitContainer1.SplitterDistance = x; })
            //    , x => splitContainer1.SplitterMoved += (s, es) => x(s, EventArgs.Empty)
            //    , new PropertyLink<int>(() => defSet.MainForm_SpliContainer_SplitterDistance, x => defSet.MainForm_SpliContainer_SplitterDistance = x)
            //    , x => defSet.PropertyChanged += (s, es) => { if (es.PropertyName == nameof(defSet.MainForm_SpliContainer_SplitterDistance)) x(s, EventArgs.Empty); })
            //    .UpdateObj2To1();
        }

        private void DefSet_PropertyChanged(object sender, PropertyChangedEventArgs es)
        {
            if (es.PropertyName == nameof(Properties.Settings.Default.MainForm_SpliContainer_SplitterDistance))
                DefSetSplitterDistanceChanged?.Invoke();
        }

        #endregion
        public event Action DefSetSplitterDistanceChanged;

        #region ...Event handlers...
        //private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        //{
        //    Trace.WriteLine("Splitter moved.");
        //    Properties.Settings.Default.MainForm_SpliContainer_SplitterDistance = splitContainer1.SplitterDistance;
        //}
        //private void TxtCompanionFileSearchPattern_TextChanged(object sender, EventArgs e)
        //{
        //    txtCompanionFile.Text = _OFF.CompanionFile;
        //}

        private void ECC_InputTextChanged(object sender, EventArgs e)
        {
            if (File.Exists(Program.ECC.InputFilePath)) richTextBox_in.Text = Program.ECC.InputText;
            //txtCompanionFile.Text = _OFF.CompanionFile;
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            EncodingsCollection favs = new EncodingsCollection(evOutputEncoding.FavoriteEncodingInfos.Length);
            favs.AddRange(evOutputEncoding.FavoriteEncodingInfos.Select(x => x.CodePage));
            Properties.Settings.Default.FavoriteEncodings = favs;
            Program.Settings.Save();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtInputPath.Text))
            {
                MessageBox.Show(Properties.Resources.Message_PleaseBrowseForInputFileFirst
                    , Properties.Resources.ProgramTitel);
                return;
            }
            if (txtOutputPath.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.Message_PleaseBrowseWhereToSaveTheFileFirst
                    , Properties.Resources.ProgramTitel);
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
                MessageBox.Show(Properties.Resources.Message_Err_NoPermissionToPerformConversion + ex.ToText());
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.Message_Err_ConversionFailedForTheFollowingError + ex.ToText());
                ex.WriteToTrace();
                return;
            }

            // Done !!
            DialogResult res = MessageBox.Show(this, Properties.Resources.Message_Done,
                Properties.Resources.ProgramTitel, MessageBoxButtons.OK);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //AutoDetectInputEncdoing();
        }
        private void btnChangeOutputFile_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sav = new SaveFileDialog();
            _SFD.Value.Title = Properties.Resources.Message_BrowseWhereYouWantToSaveTheFile;// Program.ResourceManager.GetString("Message_BrowseWhereYouWantToSaveTheFile");
            _SFD.Value.Filter = Properties.Resources.Filter_AllFiles;// Program.ResourceManager.GetString("Filter_AllFiles");
            _SFD.Value.FileName = txtOutputPath.Text;
            if (_SFD.Value.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtOutputPath.Text = _SFD.Value.FileName;
            }
        }
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            //OpenFileDialog op = new OpenFileDialog();
            _OFD.Value.Title = Properties.Resources.Message_OpenTheFileYouWantToConvertEncodingFor;// Program.ResourceManager.GetString("Message_OpenTheFileYouWantToConvertEncodingFor");
            _OFD.Value.Filter = Properties.Resources.Filter_AllFiles;// Program.ResourceManager.GetString("Filter_AllFiles");
            _OFD.Value.FileName = txtInputPath.Text;
            if (_OFD.Value.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                ChangeInputFile(_OFD.Value.FileName);
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
            DialogResult res = MessageBox.Show(this, Properties.Resources.Message_About,
                Properties.Resources.ProgramTitel, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void InputControl_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files == null || files.Length <= 0)
                return;
            string file = files[0];
            ChangeInputFile(file);
            //if (file?.Trim().ToLower() == Program.ECC.InputFilePath?.Trim().ToLower())
            //{
            //    Program.ECC.RefreshInputFielPath();
            //}
            //else
            //{
            //    Program.ECC.InputFilePath = files[0];
            //}
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
        private void BtnApplyOutputFormatting_Click(object sender, EventArgs e)
        {
            this.txtOutputPath.Text = _OFF.FormatOutputpath();
        }

        #endregion

        void ChangeInputFile(string file)
        {
            if (file?.Trim().ToLower() == Program.ECC.InputFilePath?.Trim().ToLower())
            {
                Program.ECC.RefreshInputFielPath();
            }
            else
            {
                Program.ECC.InputFilePath = file;
                //Program.ECC.OutputFilePath = EncodingHelper.FormatOutputpath(file, Program.ECC.InputEncoding, Program.ECC.OutputEncoding);
            }
        }

    }
}
