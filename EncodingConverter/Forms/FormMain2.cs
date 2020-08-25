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
using System.Reflection;
using System.Runtime.Remoting.Channels;

namespace EncodingConverter.Forms
{
    public partial class FormMain2 : Form
    {
        #region ...ctor...

        public FormMain2()
        {
            InitializeComponent();
            AddEventHandlers();
        }
        void AddEventHandlers()
        {
            //Binding to Settings:
            //Bind(this, nameof(this.ClientSize), Properties.Settings.Default, nameof(Properties.Settings.Default.MainFormSize));
            //Bind(splitContainer1, nameof(splitContainer1.SplitterDistance), Properties.Settings.Default, nameof(Properties.Settings.Default.MainForm_SpliContainer_SplitterDistance));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::EncodingConverter.Properties.Settings.Default, "MainFormSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.splitContainer1.DataBindings.Add(new System.Windows.Forms.Binding("SplitterDistance", global::EncodingConverter.Properties.Settings.Default, "MainForm_SpliContainer_SplitterDistance", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));


            //Binding to Encoding converter core:

            PropertyLink<bool> pl = new PropertyLink<bool>(() => chkAutoDetect.Checked, x => chkAutoDetect.Checked = x);
            //PropertyLink<bool> pl = new PropertyLink<bool>();
            pl.Set = x => chkAutoDetect.Checked = x;
            pl.Get = () => chkAutoDetect.Checked;

            EncodingConverterCore ECC = Program.ECC;
            Bind(new PropertyLink<bool>(() => chkAutoDetect.Checked, x => chkAutoDetect.Checked = x)
                , x => chkAutoDetect.CheckedChanged += x
                , new PropertyLink<bool>(() => ECC.AutoDetectInputEncoding, x => ECC.AutoDetectInputEncoding = x)
                , x => ECC.AutoDetectInputEncodingChanged += x)
                .UpdateObj2To1();
            BindText(txtPreferredInputEncoding
                , new PropertyLink<string>(() => ECC.PreferredInputEncoding, x => ECC.PreferredInputEncoding = x)
                , x => ECC.PreferredInputEncodingChanged += x)
                .UpdateObj2To1();
            BindText(txtInputPath
                , new PropertyLink<string>(() => ECC.InputFilePath, x => ECC.InputFilePath = x)
                , x => ECC.InputFilePathChanged += x)
                .UpdateObj2To1();
            BindText(txtOutputPath
                , new PropertyLink<string>(() => ECC.OutputFilePath, x => ECC.OutputFilePath = x)
                , x => ECC.OutputFilePathChanged += x)
                .UpdateObj2To1();
            ECC.InputTextChanged += ECC_InputTextChanged; //One way  update.
            if (File.Exists(ECC.InputFilePath))
            {
                richTextBox_in.Text = ECC.InputText;
            }
            Bind(new PropertyLink<Encoding>(() => encodingsTool_input.SelectedEncoding, x => encodingsTool_input.SelectedEncoding = x)
                , x => encodingsTool_input.SelectedEncodingChanged += x
                , new PropertyLink<Encoding>(() => ECC.InputEncoding, x => ECC.InputEncoding = x)
                , x => ECC.InputEncodingChanged += x)
                .UpdateObj2To1();
            Bind(new PropertyLink<Encoding>(() => encodingsTool_output.SelectedEncoding, x => encodingsTool_output.SelectedEncoding = x)
                , x => encodingsTool_output.SelectedEncodingChanged += x
                , new PropertyLink<Encoding>(() => ECC.OutputEncoding, x => ECC.OutputEncoding = x)
                , x => ECC.OutputEncodingChanged += x)
                .UpdateObj2To1();

            //Bind(chkAutoDetect, nameof(chkAutoDetect.Checked), Program.ECC, nameof(Program.ECC.AutoDetectInputEncoding));
            //Bind(txtPreferredInputEncoding, nameof(txtPreferredInputEncoding.Text), Program.ECC, nameof(Program.ECC.PreferredInputEncoding));
            //Bind(txtInputPath, nameof(txtInputPath.Text), Program.ECC, nameof(Program.ECC.InputFilePath));
            //Bind(txtOutputPath, nameof(txtOutputPath.Text), Program.ECC, nameof(Program.ECC.OutputFilePath));
            //Bind(richTextBox_in, nameof(richTextBox_in.Text), Program.ECC, nameof(Program.ECC.InputText));
            //Bind(encodingsTool_input, nameof(encodingsTool_input.SelectedEncoding), Program.ECC, nameof(Program.ECC.InputEncoding));
            //Bind(encodingsTool_output, nameof(encodingsTool_output.SelectedEncoding), Program.ECC, nameof(Program.ECC.OutputEncoding));


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

        private void ECC_InputTextChanged(object sender, EventArgs e) { if (File.Exists(Program.ECC.InputFilePath)) richTextBox_in.Text = Program.ECC.InputText; }
        #endregion
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.Save();
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
                Program.ECC.InputFilePath = op.FileName;
            }
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
            if (encodingsTool_input.SelectedEncoding == null)
            {
                return;
            }
            if (encodingsTool_output.SelectedEncoding == null)
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



        Binding Bind(Control control, string propertyNameOfControl, object dataSource, string propertyOfDataSource)
        {
            Binding binding = new Binding(propertyNameOfControl, dataSource, propertyOfDataSource, true, DataSourceUpdateMode.OnPropertyChanged);
            control.DataBindings.Add(binding);
            return binding;
        }
        UpdateLock<T> Bind<T>(PropertyLink<T> property1, Action<EventHandler> wireObj1ChangeEvent, PropertyLink<T> property2, Action<EventHandler> wireObj2ChangeEvent)
        {
            UpdateLock<T> binding = new UpdateLock<T>(property1, wireObj1ChangeEvent, property2, wireObj2ChangeEvent);
            return binding;
        }
        UpdateLock<String> BindText(Control textBox, PropertyLink<String> property2, Action<EventHandler> wireObj2ChangeEvent)
        {
            PropertyLink<String> property1 = new PropertyLink<String>(() => textBox.Text, x => textBox.Text = x);

            UpdateLock<String> binding = new UpdateLock<String>(property1, x => textBox.TextChanged += x, property2, wireObj2ChangeEvent);
            return binding;
        }

    }

    class PropertyLink<T>
    {
        public Action<T> Set;
        public Func<T> Get;
        public PropertyLink() { }
        public PropertyLink(Func<T> get, Action<T> set)
        {
            Set = set;
            Get = get;
        }
    }
    class EventLink
    {
        //Wire
        //Unwire
    }
    class OneWayUpdater<T>
    {
        public Func<T> SourceGetter;

        public Action<T> DestinationSetter;

    }
    class UpdateLock<T>
    {
        PropertyLink<T> _Obj1PropertyLink;
        Action<EventHandler> _WireObj1ChangeEvent;
        Action<EventHandler> _UnWireObj1ChangeEvent;


        PropertyLink<T> _Obj2PropertyLink;
        Action<EventHandler> _WireObj2ChangeEvent;
        Action<EventHandler> _UnWireObj2ChangeEvent;

        bool _Updating;

        public UpdateLock(PropertyLink<T> obj1PropertyLink
            , Action<EventHandler> wireObj1ChangeEvent
            //, Action<EventHandler> unWireObj1ChangeEvent
            , PropertyLink<T> obj2PropertyLink
            , Action<EventHandler> wireObj2ChangeEvent
            //, Action<EventHandler> unWireObj2ChangeEvent
            )
        {
            _Obj1PropertyLink = obj1PropertyLink;
            _WireObj1ChangeEvent = wireObj1ChangeEvent;
            //_UnWireObj1ChangeEvent = unWireObj1ChangeEvent;

            _Obj2PropertyLink = obj2PropertyLink;
            _WireObj2ChangeEvent = wireObj2ChangeEvent;
            //_UnWireObj2ChangeEvent = unWireObj2ChangeEvent;


            _WireObj1ChangeEvent?.Invoke(this.Obj1Changed);
            _WireObj2ChangeEvent?.Invoke(this.Obj2Changed);
        }


        void Obj1Changed(T newValue)
        {
            if (_Updating)
                return;
            _Updating = true;
            _Obj2PropertyLink.Set(newValue);
            //_Obj2PropertyLink(newValue);
            _Updating = false;
        }
        void Obj2Changed(T newValue)
        {
            if (_Updating)
                return;
            _Updating = true;
            _Obj1PropertyLink.Set(newValue);
            //_Obj1PropertyLink(newValue);
            _Updating = false;
        }

        void Obj1Changed(object sender, EventArgs e) { UpdateObj1To2(); }
        void Obj2Changed(object sender, EventArgs e) { UpdateObj2To1(); }


        public void UpdateObj1To2()
        {
            if (_Updating)
                return;

            _Updating = true;
            T value;
            value = _Obj1PropertyLink.Get();
            _Obj2PropertyLink.Set(value);
            _Updating = false;
        }
        public void UpdateObj2To1()
        {
            if (_Updating)
                return;

            _Updating = true;
            T value;
            value = _Obj2PropertyLink.Get();
            _Obj1PropertyLink.Set(value);
            _Updating = false;
        }

    }
}
