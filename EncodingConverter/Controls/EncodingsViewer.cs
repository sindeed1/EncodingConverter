using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncodingConverter.Controls
{
    public partial class EncodingsViewer : UserControl
    {
        bool _UpdatingSelectedEncoding;

        private EncodingInfo _SelectedEncodingInfo;

        #region ...Events...
        public event EventHandler SelectedEncodingInfoChanged;
        #endregion
        #region ...ctor...

        public EncodingsViewer()
        {
            InitializeComponent();
            AddEventHandlers();

        }
        void AddEventHandlers()
        {
            lvAllEncodings.SelectedEncodingChanged += LvAllEncodings_SelectedEncodingChanged;
            lvFavoriteEncodings.SelectedEncodingChanged += LvFavoriteEncodings_SelectedEncodingChanged;
            tstbSearchEncodings.TextChanged += TstbSearchEncodings_TextChanged;
        }



        #endregion

        #region ...Event handlers...
        private void LvAllEncodings_SelectedEncodingChanged(object sender, EventArgs e)
        {
            if (_UpdatingSelectedEncoding)
                return;
            _UpdatingSelectedEncoding = true;

            lblSelectedEncoding.Text = lvAllEncodings.SelectedEncoding.DisplayName;
            this.SelectedEncodingInfo = lvAllEncodings.SelectedEncoding;
            lvFavoriteEncodings.SelectedEncoding = null;

            _UpdatingSelectedEncoding = false;
        }
        private void LvFavoriteEncodings_SelectedEncodingChanged(object sender, EventArgs e)
        {
            if (_UpdatingSelectedEncoding)
                return;

            _UpdatingSelectedEncoding = true;

            lblSelectedEncodingFavorites.Text = lvFavoriteEncodings.SelectedEncoding.DisplayName;
            this.SelectedEncodingInfo = lvFavoriteEncodings.SelectedEncoding;
            lvAllEncodings.SelectedEncoding = null;

            _UpdatingSelectedEncoding = false;
        }
        private void TstbSearchEncodings_TextChanged(object sender, EventArgs e)
        {
            this.lvAllEncodings.SearchText = this.tstbSearchEncodings.Text.ToLower();
            SetSelectedEncodingInfo(this.SelectedEncodingInfo);//To re-select the encoding after search.
        }

        #endregion


        public EncodingInfo SelectedEncodingInfo
        {
            get { return _SelectedEncodingInfo; }
            set
            {
                if (_SelectedEncodingInfo == value)
                    return;


                SetSelectedEncodingInfo(value);

                OnSelectedEncodingInfoChanged();
            }
        }

        void SetSelectedEncodingInfo(EncodingInfo encodingInfo)
        {
            _SelectedEncodingInfo = encodingInfo;
            this.lvAllEncodings.SelectedEncoding = _SelectedEncodingInfo;
        }
        [DefaultValue(null)]
        public EncodingInfo[] EncodingInfos { get { return lvAllEncodings.SourceEncodings; } set { lvAllEncodings.SourceEncodings = value; } }
        [DefaultValue(null)]
        public EncodingInfo[] FavoriteEncodingInfos { get { return lvFavoriteEncodings.SourceEncodings; } set { lvFavoriteEncodings.SourceEncodings = value; } }


        protected virtual void OnSelectedEncodingInfoChanged() { SelectedEncodingInfoChanged?.Invoke(this, EventArgs.Empty); }

    }
    //public class LVIEncoding : ListViewItem
    //{
    //    EncodingInfo encoding;
    //    #region ...ctor...
    //    public LVIEncoding() { }
    //    public LVIEncoding(EncodingInfo encoding) { this.Encoding = encoding; }
    //    public LVIEncoding(EncodingInfo encoding, bool isChecked) { this.Encoding = encoding; this.Checked = isChecked; }
    //    #endregion
    //    public EncodingInfo Encoding { get { return encoding; } set { encoding = value; RefreshText(); } }
    //    public void RefreshText()
    //    {
    //        SubItems.Clear();
    //        this.Text = (encoding.Name);
    //        SubItems.Add(encoding.DisplayName);
    //        SubItems.Add(encoding.CodePage.ToString());

    //        //this.Text = (encoding.EncodingName);
    //        //SubItems.Add(encoding.BodyName);
    //        //SubItems.Add(encoding.CodePage.ToString());


    //    }
    //}

}
