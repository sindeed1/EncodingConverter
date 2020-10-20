using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncodingConverter.Controls
{
    class SearchableEncodingListView : SearchableListView<EncodingInfo>
    {
        private System.Windows.Forms.ColumnHeader columnHeader1_name;
        private System.Windows.Forms.ColumnHeader columnHeader2_body;
        private System.Windows.Forms.ColumnHeader columnHeader3_codepage;

        bool _UpdatingSelectedEncoding;

        private EncodingInfo _SelectedEncoding;
        EncodingInfo _TempEnc;//Used to preserve the selected encoding between changed in the current 'base.CurrentSource'

        #region ...Events...
        public event EventHandler SelectedEncodingChanged;
        #endregion

        #region ...ctor...
        public SearchableEncodingListView()
        {
            this.columnHeader1_name = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2_body = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3_codepage = new System.Windows.Forms.ColumnHeader();

            this.columnHeader1_name.Text = "Name";
            this.columnHeader1_name.Width = 225;

            this.columnHeader2_body.Text = "Body Name";
            this.columnHeader2_body.Width = 120;

            this.columnHeader3_codepage.Text = "Code Page";
            this.columnHeader3_codepage.Width = 70;


            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1_name,
            this.columnHeader2_body,
            this.columnHeader3_codepage});

            this.CheckBoxes = true;
            this.GridLines = true;
            this.MultiSelect = false;
            this.ShowItemToolTips = true;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;

            this.ObjectToListViewItemConverter = x => new LVIEncoding(x);//, x == _SelectedEncoding);
            this.ObjectToSearchableTextConverter = x => x.DisplayName.ToLower();
        }
        #endregion //ctor

        [DefaultValue(null)]
        public EncodingInfo[] SourceEncodings
        {
            get { return this.SourceList; }
            set { this.SourceList = value; }
        }

        /// <summary>
        /// Returns the selected encoding. Null if no encoding is selected.
        /// </summary>
        /// <remarks>The selected encoding is the encoding of the only checked item.</remarks>
        [DefaultValue(null)]
        public EncodingInfo SelectedEncoding
        {
            get
            {
                return _SelectedEncoding;
            }
            set
            {
                if (_UpdatingSelectedEncoding)
                    return;
                if (_SelectedEncoding == value)
                    return;

                _UpdatingSelectedEncoding = true;
                this.BeginUpdate();
                if (value == null)
                {
                    //Uncheck all:
                    var checkedItems = this.CheckedItems.Cast<LVIEncoding>();//Get all checked item.
                    checkedItems.Foreach(x => x.Checked = false);//Uncheck all items

                    //_SelectedEncoding = value;//Means _SelectedEncoding = null
                }
                else
                {
                    //Get first item to match the encoding page:
                    var selectedItem = this.Items.Cast<LVIEncoding>().FirstOrDefault(x => x.Encoding.CodePage == value.CodePage);
                    if (selectedItem == null)
                    {
                        //There is no such an encoding at the currently displayed items.
                        //Current policy is to ignore the request:
                        _UpdatingSelectedEncoding = false;
                        return;
                    }

                    if (this.CheckedIndices.Count > 0)
                    {
                        var checkedItems = this.CheckedItems.Cast<LVIEncoding>();
                        checkedItems.Foreach(x => x.Checked = false);
                    }
                    selectedItem.Checked = true;
                    selectedItem.EnsureVisible();
                }

                _SelectedEncoding = value;
                
                this.EndUpdate();
                _UpdatingSelectedEncoding = false;

                OnSelectedEncodingChanged();
            }
        }

        void SetSelectedEncoding(EncodingInfo value)
        {
            if (_UpdatingSelectedEncoding)
                return;

            _UpdatingSelectedEncoding = true;
            this.BeginUpdate();
            if (value == null)
            {
                //Uncheck all:
                var checkedItems = this.CheckedItems.Cast<LVIEncoding>();//Get all checked item.
                checkedItems.Foreach(x => x.Checked = false);//Uncheck all items
                //_SelectedEncoding = value;//Means _SelectedEncoding = null
            }
            else
            {
                //Get first item to match the encoding page:
                var selectedItem = this.Items.Cast<LVIEncoding>().FirstOrDefault(x => x.Encoding.CodePage == value.CodePage);
                if (selectedItem == null)
                {
                    //There is no such an encoding at the currently displayed items.
                    //Current policy is to ignore the request:
                    _UpdatingSelectedEncoding = false;
                    return;
                }

                if (this.CheckedIndices.Count > 0)
                {
                    var checkedItems = this.CheckedItems.Cast<LVIEncoding>();
                    checkedItems.Foreach(x => x.Checked = false);
                }
                selectedItem.Checked = true;
                selectedItem.EnsureVisible();
            }

            _SelectedEncoding = value;

            this.EndUpdate();
            _UpdatingSelectedEncoding = false;
        }

        protected virtual void OnSelectedEncodingChanged() { SelectedEncodingChanged?.Invoke(this, EventArgs.Empty); }

        protected override void OnCurrentSourceChanged()
        {
            base.OnCurrentSourceChanged();
            if (this.CurrentSource == null || this.CurrentSource.Length == 0)
            {
                this.SelectedEncoding = null;
            }
            else
            {
                this.SelectedEncoding = _TempEnc;
            }
        }

        protected override void OnCurrentSourceChanging(EncodingInfo[] newCurrentSource)
        {
            base.OnCurrentSourceChanging(newCurrentSource);
            _TempEnc = _SelectedEncoding;
        }
        protected override void OnItemChecked(ItemCheckedEventArgs e)
        {
            base.OnItemChecked(e);

            if (e.Item.Checked)
            {
                this.SelectedEncoding = ((LVIEncoding)e.Item).Encoding;
            }
            else
            {
                this.SelectedEncoding = null;
            }
        }
    }

    public class LVIEncoding : ListViewItem
    {
        EncodingInfo encoding;
        #region ...ctor...
        //public LVIEncoding() { }
        public LVIEncoding(EncodingInfo encoding) { this.Encoding = encoding; }
        //public LVIEncoding(EncodingInfo encoding, bool @checked)
        //{
        //    this.Encoding = encoding; 
        //    this.Checked = @checked;
        //}
        #endregion
        public EncodingInfo Encoding { get { return encoding; } set { encoding = value; RefreshText(); } }
        public void RefreshText()
        {
            SubItems.Clear();
            this.Text = (encoding.DisplayName);
            SubItems.Add(encoding.Name);
            SubItems.Add(encoding.CodePage.ToString());
        }
    }
}
