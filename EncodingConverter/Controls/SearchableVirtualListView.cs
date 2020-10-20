using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncodingConverter.Controls
{
    class SearchableVirtualListView : ListView
    {
        ListViewItem _OldCheckedItem;

        private EncodingInfo[] _MainSource;
        EncodingInfo[] encodingInfos;

        private string _SearchText;

        private LVIEncoding[] myCache; //array to cache items for the virtual list
        private int firstItem; //stores the index of the first item in the cache

        private EncodingInfo _SelectedEncoding;

        public EncodingInfo SelectedEncoding
        {
            get { return _SelectedEncoding; }
            set { _SelectedEncoding = value; }
        }


        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                _SearchText = value;

                this.Items.Clear();
                string searchText = _SearchText.Trim();
                if (searchText.Length == 0)
                {
                    encodingInfos = _MainSource;
                    return;
                }

                var result = encodingInfos.Where(x => x.Name.Length >= searchText.Length && x.Name.Contains(searchText));
                encodingInfos = result.ToArray();
                if (this.Items.Count > 0)
                    this.Items[0].Checked = true;
            }
        }


        public EncodingInfo[] Source
        {
            get { return _MainSource; }
            set { _MainSource = value; }
        }


        protected override void OnCacheVirtualItems(CacheVirtualItemsEventArgs e)
        {
            base.OnCacheVirtualItems(e);
            //We've gotten a request to refresh the cache.
            //First check if it's really necessary.
            if (myCache != null && e.StartIndex >= firstItem && e.EndIndex <= firstItem + myCache.Length)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //Now we need to rebuild the cache.
            firstItem = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1; //indexes are inclusive
            myCache = GetLVIENcodings(encodingInfos, firstItem, length);
        }
        protected override void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
        {
            base.OnRetrieveVirtualItem(e);
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.

            //check to see if the requested item is currently in the cache
            if (myCache != null && e.ItemIndex >= firstItem && e.ItemIndex < firstItem + myCache.Length)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                e.Item = myCache[e.ItemIndex - firstItem];
            }
            else
            {
                //A cache miss, so create a new ListViewItem and pass it back.
                e.Item = new LVIEncoding(encodingInfos[e.ItemIndex]);// new Controls.SyncFileFramesViewerItem(_WorkData.SyncFile.SynchedFrames[e.ItemIndex], _WorkData.SyncFile);
            }
        }

        protected override void OnItemChecked(ItemCheckedEventArgs e)
        {
            base.OnItemChecked(e);
            if (_OldCheckedItem != null)
                _OldCheckedItem.Checked = false;

            _OldCheckedItem = e.Item;
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            _OldCheckedItem.EnsureVisible();
        }


        LVIEncoding[] GetLVIENcodings(EncodingInfo[] source, int startIndex, int length)
        {
            EncodingInfo[] efs = new EncodingInfo[length];
            Array.Copy(source, startIndex, efs, 0, length);
            return efs.Select(x => new LVIEncoding(x)).ToArray();
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
            this.Text = (encoding.Name);
            //SubItems.Add(encoding.Name);
            SubItems.Add(encoding.DisplayName);
            SubItems.Add(encoding.CodePage.ToString());

            //this.Text = (encoding.EncodingName);
            //SubItems.Add(encoding.BodyName);
            //SubItems.Add(encoding.CodePage.ToString());


        }
    }

}
