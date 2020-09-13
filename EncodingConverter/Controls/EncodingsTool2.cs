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
        int _SelectedEncodingIndex;

        EncodingInfo[] _Encodings;
        EncodingInfo[] _FavoriteEncodings;

        #region ...ctor...

        public EncodingsViewer()
        {
            InitializeComponent();
            AddEventHandlers();

        }
        void AddEventHandlers()
        {

        }
        #endregion

        void InitAllEncodinngsTab()
        {
            if (_Encodings == null)
                _Encodings = new EncodingInfo[0];

            this.lvAllEncodings.Items.AddRange(_Encodings.Select(x => new LVIEncoding(x)).ToArray());
            if (lvAllEncodings.Items.Count <= 0)
            {
                return;
            }

            if (_SelectedEncodingIndex >= lvAllEncodings.Items.Count)
            {

            }
        }
        void InitFavoritesTab()
        {
            if (_FavoriteEncodings == null)
                _FavoriteEncodings = new EncodingInfo[0];

            this.lvFavoriteEncodings.Items.AddRange(_FavoriteEncodings.Select(x => new LVIEncoding(x)).ToArray());
        }

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
