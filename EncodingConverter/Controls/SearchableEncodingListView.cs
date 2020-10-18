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
        private System.Windows.Forms.ColumnHeader columnHeader3_name;
        private System.Windows.Forms.ColumnHeader columnHeader4_body;
        private System.Windows.Forms.ColumnHeader columnHeader5_codepage;

        bool _UpdatingSelectedEncoding;

        private EncodingInfo _SelectedEncoding;

        #region ...Events...
        public event EventHandler SelectedEncodingChanged;
        #endregion

        public SearchableEncodingListView()
        {
            this.columnHeader3_name = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4_body = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5_codepage = new System.Windows.Forms.ColumnHeader();

            this.columnHeader3_name.Text = "Name";
            this.columnHeader3_name.Width = 120;

            this.columnHeader4_body.Text = "Body Name";
            this.columnHeader4_body.Width = 225;

            this.columnHeader5_codepage.Text = "Code Page";
            this.columnHeader5_codepage.Width = 70;


            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3_name,
            this.columnHeader4_body,
            this.columnHeader5_codepage});

            this.CheckBoxes = true;
            this.GridLines = true;
            this.MultiSelect = false;
            this.ShowItemToolTips = true;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;

            //this.ObjectToListViewItemConverter = x => new LVIEncoding((EncodingInfo)x);
            //this.ObjectToSearchableTextConverter = x => ((LVIEncoding)x).Encoding.DisplayName;
            this.ObjectToListViewItemConverter = x => new LVIEncoding(x);
            this.ObjectToSearchableTextConverter = x => x.DisplayName.ToLower();
        }

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
                if (this.CheckedIndices.Count == 0)
                {
                    return null;
                }
                return ((LVIEncoding)this.Items[this.CheckedIndices[0]]).Encoding;
            }
            set
            {
                if (_UpdatingSelectedEncoding)
                    return;
                if (_SelectedEncoding == value)
                    return;

                _UpdatingSelectedEncoding = true;
                if (value == null)
                {
                    var checkedItems = this.CheckedItems.Cast<LVIEncoding>();//Get all checked item.
                    checkedItems.Foreach(x => x.Checked = false);//Uncheck the items

                    _SelectedEncoding = value;//Means _SelectedEncoding = null
                    return;
                }

                //Get first item to match the encoding page:
                var selectedItem = this.Items.Cast<LVIEncoding>().FirstOrDefault(x => x.Encoding.CodePage == value.CodePage);
                if (selectedItem == null)
                {
                    //There is no such an encoding at the currently displayed items.
                    //Current policy is to ignore the request:
                    return;
                }

                if (this.CheckedIndices.Count > 0)
                {
                    var checkedItems = this.CheckedItems.Cast<LVIEncoding>();
                    checkedItems.Foreach(x => x.Checked = false);
                }
                selectedItem.Checked = true;

                _SelectedEncoding = value;
                
                _UpdatingSelectedEncoding = false;

                OnSelectedEncodingChanged();
            }
        }

        protected virtual void OnSelectedEncodingChanged() { SelectedEncodingChanged?.Invoke(this, EventArgs.Empty); }


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
}
