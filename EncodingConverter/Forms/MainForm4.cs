using EncodingConverter.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace EncodingConverter.Forms
{
    public partial class MainForm4 : Form
    {
        ToolStripEncodingViewer _TSInputEncodingViewer;
        ToolStripEncodingViewer _TSOutputEncodingViewer;

        EncodingsViewer evInputEncoding;
        EncodingsViewer evOutputEncoding;

        EncodingInfo[] encodingInfos;
        Lazy<SaveFileDialog> _SFD;
        Lazy<OpenFileDialog> _OFD;

        OutputPathFormatter _OPF;

        bool _AllowOverwriteOutputFile;

        OneWayUpdater<string> _InputTextLink;

        int _Panel2Width;
        #region ...ctor...

        public MainForm4()
        {
            InitializeComponent();
            _TSInputEncodingViewer = new ToolStripEncodingViewer();

            _TSInputEncodingViewer = new ToolStripEncodingViewer();
            _TSOutputEncodingViewer = new ToolStripEncodingViewer();

            tsddInputEncoding.DropDownItems.Add(_TSInputEncodingViewer);
            tsddOutputEncoding.DropDownItems.Add(_TSOutputEncodingViewer);

            evInputEncoding = _TSInputEncodingViewer.EncodingsViewer;
            evOutputEncoding = _TSOutputEncodingViewer.EncodingsViewer;

            _OPF = new OutputPathFormatter(Program.ECC);

            this.Icon = Properties.Resources.Icon_Encoding_Converter_32x32;


            encodingInfos = Encoding.GetEncodings();
            evInputEncoding.EncodingInfos = encodingInfos;
            evOutputEncoding.EncodingInfos = encodingInfos;

            EncodingInfo[] favs = Properties.Settings.Default.FavoriteEncodings?.Select(cp => encodingInfos.FirstOrDefault(enc => enc.CodePage == cp)).Where(x => x != null).ToArray();
            evInputEncoding.FavoriteEncodingInfos = favs;
            evOutputEncoding.FavoriteEncodingInfos = favs;

            _OFD = new Lazy<OpenFileDialog>();
            _SFD = new Lazy<SaveFileDialog>();

            AddEventHandlers();
        }
        void AddEventHandlers()
        {
            EncodingConverterCore ECC = Program.ECC;
            #region Bindings

            //Binding to Encoding converter core:
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
            txtOutputPath.BindText(new PropertyLink<string>(() => ECC.OutputFilePath, (x) => { ECC.OutputFilePath = x; })
                , new EventLink(ECC, nameof(ECC.OutputFilePathChanged)))
                .UpdateObj2To1();
            //Bind 'ECC.InputText' to 'tbInputText':
            _InputTextLink = tbInputText.BindTextAsDestination(
                () => (!splitContainer1.Panel2Collapsed && File.Exists(ECC.InputFilePath)) ? ECC.InputText : null
                , new EventLink(ECC, nameof(ECC.InputTextChanged)));
            _InputTextLink.Update();

            //ECC.InputTextChanged += ECC_InputTextChanged; //One way update.
            //if (File.Exists(ECC.InputFilePath))
            //{
            //    tbInputText.Text = ECC.InputText;
            //}

            var text = File.Exists(ECC.InputFilePath) ? ECC.InputText : null;

            var inputEncodingEventLink = new EventLink(ECC, nameof(ECC.InputEncodingChanged));
            //Bind 'ECC.InputEncoding' to 'evInputEncoding.SelectedEncodingInfo':
            WinFormsHelpers.Bind(new PropertyLink<EncodingInfo>(() => evInputEncoding.SelectedEncodingInfo, x => evInputEncoding.SelectedEncodingInfo = x)
                , new EventLink(evInputEncoding, nameof(evInputEncoding.SelectedEncodingInfoChanged))
                , new PropertyLink<EncodingInfo>(() => encodingInfos?.FirstOrDefault(x => x.CodePage == ECC.InputEncoding?.CodePage), x => ECC.InputEncoding = x?.GetEncoding())
                , inputEncodingEventLink)
                .UpdateObj2To1();
            //Bind 'ECC.InputEncoding' to 'lblInputEncoding':
            tsddInputEncoding.BindTextAsDestination(() => ECC.InputEncoding?.EncodingName, inputEncodingEventLink).Update();

            //Bind 'ECC.OutputEncoding' to 'evOutputEncoding.SelectedEncodingInfo' and 'Settings.LastOutputEncoding':
            Properties.Settings defSet = Properties.Settings.Default;
            //1- Setup PropertyLink to 'ECC.OutputEncoding':
            var ECCOutputEncodingPropertyLink = new PropertyLink<Encoding>
                        (() => ECC.OutputEncoding//getter
                        , x => ECC.OutputEncoding = x);//setter

            //2- Setup EventLink of 'OutputEncodingChanged':
            var ECCOutputEncodingEventLink = new EventLink(ECC, nameof(ECC.OutputEncodingChanged));
            //3- Now make the Bindings:
            //  Bind 'ECC.OutputEncoding' to 'Settings.LastOutputEncoding':
            WinFormsHelpers.Bind(new PropertyLink<Encoding>
                        (() => encodingInfos?.FirstOrDefault(x => x.CodePage == defSet.LastOutputEncoding)?.GetEncoding()
                        , x => defSet.LastOutputEncoding = x.CodePage)
                , new EventLink(evOutputEncoding, nameof(evOutputEncoding.SelectedEncodingInfoChanged))
                , ECCOutputEncodingPropertyLink
                , ECCOutputEncodingEventLink)
                .UpdateObj1To2();//Update Settings to ECC.

            //  Bind 'ECC.OutputEncoding' to 'evOutputEncoding.SelectedEncodingInfo':
            //WinFormsHelpers.Bind(new PropertyLink<EncodingInfo>(() => evOutputEncoding.SelectedEncodingInfo, x => evOutputEncoding.SelectedEncodingInfo = x)
            //    , new EventLink(evOutputEncoding, nameof(evOutputEncoding.SelectedEncodingInfoChanged))
            //    , new PropertyLink<EncodingInfo>(() => encodingInfos?.FirstOrDefault(x => x.CodePage == ECC.OutputEncoding.CodePage), x => ECC.OutputEncoding = x?.GetEncoding())
            //    , ECCOutputEncodingEventLink)
            //    .UpdateObj2To1();
            WinFormsHelpers.Bind(new PropertyLink<Encoding>(
                        () => evOutputEncoding.SelectedEncodingInfo.GetEncoding()
                        , x => evOutputEncoding.SelectedEncodingInfo = encodingInfos?.FirstOrDefault(ei => x?.CodePage == ei.CodePage))
                , new EventLink(evOutputEncoding, nameof(evOutputEncoding.SelectedEncodingInfoChanged))
                , ECCOutputEncodingPropertyLink
                , ECCOutputEncodingEventLink)
                .UpdateObj2To1();

            //  Bind 'ECC.OutputEncoding' to 'lblOutputEncoding':
            tsddOutputEncoding.BindTextAsDestination(() => ECC.OutputEncoding?.EncodingName, ECCOutputEncodingEventLink).Update();


            txtOutputPathFormat.BindText(new PropertyLink<string>(() => _OPF.FormatString, x => _OPF.FormatString = x)
                , new EventLink(_OPF, nameof(_OPF.FormatStringChanged))).UpdateObj2To1();
            txtCompanionFileSearchPattern.BindText(new PropertyLink<string>(() => _OPF.CompanionFileSearchPattern, x => _OPF.CompanionFileSearchPattern = x)
                , new EventLink(_OPF, nameof(_OPF.CompanionFileSearchPatternChanged))).UpdateObj2To1();

            txtCompanionFile.BindText(new PropertyLink<string>(() => _OPF.CompanionFile, x => _OPF.CompanionFile = x)
                , new EventLink(_OPF, nameof(_OPF.CompanionFileChanged))).UpdateObj2To1();


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
            #endregion//Bindings

            ECC.OutputFilePathChanged += ECC_OutputFilePathChanged;

            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.DragEnter += InputControl_DragEnter;
            this.splitContainer1.DragDrop += InputControl_DragDrop;

            this.linkAbout.LinkClicked += this.linkAbout_LinkClicked;
            this.btnChangeOutputFile.Click += this.btnChangeOutputFile_Click;
            this.linkLabel1.LinkClicked += this.linkLabel1_LinkClicked;
            this.btnSave.Click += this.btnSave_Click;
            this.btnOpen.Click += BtnOpen_Click;
            this.btnApplyOutputFormatting.Click += BtnApplyOutputFormatting_Click;

            this.Load += MainForm_Load;
            this.FormClosed += FormMain_FormClosed;
        }


        #endregion

        private void MainForm_Load(object sender, EventArgs e)
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

        public event Action DefSetSplitterDistanceChanged;

        #region ...Event handlers...
        private void ECC_OutputFilePathChanged(object sender, EventArgs e) { _AllowOverwriteOutputFile = false; }

        private void ECC_InputTextChanged(object sender, EventArgs e)
        {
            if (!splitContainer1.Panel2Collapsed && File.Exists(Program.ECC.InputFilePath)) tbInputText.Text = Program.ECC.InputText;
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            EncodingsCollection favs;
            if (evOutputEncoding.FavoriteEncodingInfos == null)
            {
                favs = null;
            }
            else
            {
                favs = new EncodingsCollection(evOutputEncoding.FavoriteEncodingInfos.Length);
                favs.AddRange(evOutputEncoding.FavoriteEncodingInfos.Select(x => x.CodePage));
            }
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
                Trace.TraceInformation("Can not convert. Input encoding is not specified.");
                return;
            }
            if (evOutputEncoding.SelectedEncodingInfo == null)
            {
                Trace.TraceInformation("Can not convert. Output encoding is not specified.");
                return;
            }
            if (_AllowOverwriteOutputFile)
                Trace.TraceInformation("Overwrite permission already granted.");
            else
            {
                Trace.TraceInformation("Overwrite permission is not already granted.");
                if (File.Exists(txtOutputPath.Text))
                {
                    Trace.TraceInformation("Output file already exists. Ask the user whether to overwrite or not.");
                    if (MessageBox.Show(Properties.Resources.Message_Q_OutputFileAlreadyExists_Overwrite
                        , "Convert"
                        , MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        Trace.TraceInformation("User didn't chose OK, Do not overwrite.");
                        return;
                    }
                    else
                    {
                        Trace.TraceInformation("User chose 'OK'.");
                    }
                }
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
            _SFD.Value.OverwritePrompt = true;

            var outputFile = new FileInfo(txtOutputPath.Text);
            _SFD.Value.FileName = outputFile.Name; //txtOutputPath.Text;
            _SFD.Value.InitialDirectory = outputFile.DirectoryName;

            if (_SFD.Value.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {

                txtOutputPath.Text = _SFD.Value.FileName;
                //The SaveAsDialog checks for the FileAlreadyExists condition.
                //If we got here the either the user chose to overwrite OR the file does not exists.
                //In either way we don't need to check if it exists:
                //IMPORTANT: The _AllowOverwriteOutputFile MUST come after updating the txtOutputPath
                //with new value, because the Binding between txtOutputPath and other component will
                //set the _AllowOverwriteOutputFile flag back to 'false' for any change that happen.
                //Only a change that happens through SaveAsFileDialog qualifies to change the flag to
                //'true' because the dialog box will check and ask for this case.
                _AllowOverwriteOutputFile = true;
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
        }
        private void InputControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        private void BtnApplyOutputFormatting_Click(object sender, EventArgs e)
        {
            this.txtOutputPath.Text = _OPF.FormatOutputpath();
        }

        #endregion //Event handlers

        void ChangeInputFile(string file)
        {
            if (file?.Trim().ToLower() == Program.ECC.InputFilePath?.Trim().ToLower())
            {
                Program.ECC.RefreshInputFielPath();
            }
            else
            {
                Program.ECC.InputFilePath = file;
            }
        }

    }
}
