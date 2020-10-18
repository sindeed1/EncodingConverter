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

        public event EventHandler FavoriteEncodingInfosChanged;

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
            tsbAddToFavorites.Click += TsbAddToFavorites_Click;
            tsbRemoveFavoriteEncoding.Click += TsbRemoveFavoriteEncoding_Click;
        }

        private void TsbRemoveFavoriteEncoding_Click(object sender, EventArgs e)
        {
            EncodingInfo[] newInfos;
            if (_SelectedEncodingInfo == null)
                return;
            if (lvFavoriteEncodings.SourceEncodings == null)
                return;

            if (lvFavoriteEncodings.SourceEncodings.FirstOrDefault(x => x == _SelectedEncodingInfo) == null)
                return;

            newInfos = lvFavoriteEncodings.SourceEncodings.Where(x => x.CodePage != _SelectedEncodingInfo.CodePage).ToArray();// new EncodingInfo[lvFavoriteEncodings.SourceEncodings.Length - 1];
            this.FavoriteEncodingInfos = newInfos;
        }

        private void TsbAddToFavorites_Click(object sender, EventArgs e)
        {
            EncodingInfo[] newInfos;
            if (lvFavoriteEncodings.SourceEncodings == null)
            {
                newInfos = new EncodingInfo[1];
                newInfos[0] = _SelectedEncodingInfo;
                this.FavoriteEncodingInfos = newInfos;
            }
            else
            {
                var res = lvFavoriteEncodings.SourceEncodings.FirstOrDefault(x => x == _SelectedEncodingInfo);
                if (res == null)
                {
                    newInfos = new EncodingInfo[lvFavoriteEncodings.SourceEncodings.Length + 1];
                    Array.Copy(lvFavoriteEncodings.SourceEncodings, newInfos, lvFavoriteEncodings.SourceEncodings.Length);
                    newInfos[newInfos.Length - 1] = _SelectedEncodingInfo;
                    this.FavoriteEncodingInfos = newInfos;
                }
            }
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

        [SettingsBindable(true)]
        [DefaultValue(null)]
        public EncodingInfo[] FavoriteEncodingInfos
        {
            get { return lvFavoriteEncodings.SourceEncodings; }
            set
            {
                if (lvFavoriteEncodings.SourceEncodings == value)
                    return;
                lvFavoriteEncodings.SourceEncodings = value;
                FavoriteEncodingInfosChanged?.Invoke(this, EventArgs.Empty);
            }
        }


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
