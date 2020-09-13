/* This file is part of AHD Subtitles Maker Professional
   A program can create and edit subtitles

   Copyright © Ala Ibrahim Hadid 2009 - 2015

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AHD.SM.Controls
{
    public partial class EncodingsTool1 : UserControl
    {
        bool ClearCheck = false;
        EncodingInfo[] allEncodingInfos;
        EncodingInfo[] encodingInfos;

        List<EncodingInfo> favoriteEncodingInfos;


        private LVIEncoding[] myCache; //array to cache items for the virtual list
        private int firstItem; //stores the index of the first item in the cache

        ListViewItem _CheckedLVItem;

        private Encoding _SelectedEncoding;

        public event EventHandler SelectedEncodingChanged;

        /// <summary>
        /// Get or set the encoding
        /// </summary>
        public Encoding SelectedEncoding
        {
            get
            {
                return _SelectedEncoding;
                //ListView lv;
                //if (tabControl1.SelectedIndex == 0)
                //{
                //    lv = listView1;
                //}
                //else
                //{
                //    lv = listView2;
                //}
                ////look up for the checked item
                //for (int i = 0; i < lv.Items.Count; i++)
                //{
                //    if (lv.Items[i].Checked)
                //        return ((LVIEncoding)(lv.Items[i])).Encoding.GetEncoding();

                //}
                //return null;
            }
            set
            {
                if (_SelectedEncoding == value)
                {
                    return;
                }

                _SelectedEncoding = value;

                ListView lv = listView1;

                for (int i = 0; i < encodingInfos.Length; i++)
                {
                    EncodingInfo item = encodingInfos[i];
                    if (item.CodePage == value.CodePage)
                    {
                        lv.SelectedIndices.Clear();
                        lv.SelectedIndices.Add(i);
                    }
                }

                //ListView lv = listView1;

                //var item = lv.Items.Cast<LVIEncoding>().FirstOrDefault(x => x.Encoding.CodePage == value.CodePage);
                //if (item != null)
                //{
                //    if (_CheckedLVItem != null)
                //        _CheckedLVItem.Checked = false;

                //    item.Checked = true;
                //    _CheckedLVItem = item;
                //    item.EnsureVisible();
                //}
            }
        }

        #region ...ctor...
        public EncodingsTool1()
        {
            InitializeComponent();
            ClearCheck = true;

            allEncodingInfos = Encoding.GetEncodings();
            encodingInfos = allEncodingInfos;


            this.SuspendLayout();
            AddEventsHanlders();

            this.SourceMain = allEncodingInfos;

            ListView lv;
            ////fill up encodings
            //lv = listView1;
            //lv.BeginUpdate();
            ////EncodingInfo[] encodingInfos = Encoding.GetEncodings();
            //for (int i = 0; i < encodingInfos.Length; i++)
            //{
            //    ListViewItem_Encoding item = new ListViewItem_Encoding();
            //    item.Encoding = encodingInfos[i].GetEncoding();
            //    lv.Items.Add(item);
            //}
            //lv.EndUpdate();

            //fill up favorites
            if (ControlsBase.Settings.FavoriteEncodings == null)
                ControlsBase.Settings.FavoriteEncodings = new EncodingsCollection();


            lv = listView2;
            EncodingsCollection ec = ControlsBase.Settings.FavoriteEncodings;
            lv.BeginUpdate();
            for (int i = 0; i < ec.Count; i++)
            {
                LVIEncoding item = new LVIEncoding();
                item.Encoding = encodingInfos.First(x => x.CodePage == ec[i]);// new EncodingInfo(); .GetEncoding(ec[i]);
                //item.Encoding = new EncodingInfo(); .GetEncoding(ec[i]);
                lv.Items.Add(item);
            }
            lv.EndUpdate();

            this.ResumeLayout();
            ClearCheck = false;
        }
        ~EncodingsTool1()
        { SaveSettings(); }
        void AddEventsHanlders()
        {
            listView1.ItemChecked += listView1_ItemChecked;
            listView1.CacheVirtualItems += SyncFileViewer1_CacheVirtualItems;
            listView1.RetrieveVirtualItem += SyncFileViewer1_RetrieveVirtualItem;
            //listView1.SearchForVirtualItem += SyncFileViewer1_SearchForVirtualItem;
            //listView1.SelectedIndexChanged += SyncFileViewer1_SelectedIndexChanged;
        }

        #endregion

        public EncodingInfo[] SourceMain
        {
            get { return encodingInfos; }
            set
            {
                encodingInfos = value;

                listView1.VirtualListSize = encodingInfos.Length;
            }
        }


        private void SyncFileViewer1_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
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
        LVIEncoding[] GetLVIENcodings(EncodingInfo[] source, int startIndex, int length)
        {
            EncodingInfo[] efs = new EncodingInfo[length];
            Array.Copy(source, startIndex, efs, 0, length);
            return efs.Select(x => new LVIEncoding(x, x.CodePage == _SelectedEncoding?.CodePage)).ToArray();
        }
        private void SyncFileViewer1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
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
                EncodingInfo ei = encodingInfos[e.ItemIndex];
                LVIEncoding lviItem = new LVIEncoding(ei, ei.CodePage == _SelectedEncoding?.CodePage);
                e.Item = lviItem;
            }
        }

        public void SaveSettings()
        {
            ControlsBase.Settings.Save();
        }
        //Add to favorite
        private void tsbAddToFavorites_Click(object sender, EventArgs e)
        {
            if (_CheckedLVItem == null)
                return;

            LVIEncoding item = (LVIEncoding)_CheckedLVItem;
            if (favoriteEncodingInfos.FirstOrDefault(x => x.CodePage == item.Encoding.CodePage) == null)
            {
                //the item already exists in the favorites
                return;
            }

            ControlsBase.Settings.FavoriteEncodings.Add(item.Encoding.CodePage);
            favoriteEncodingInfos.Add(item.Encoding);
            //Add it to the list
            LVIEncoding newItem = new LVIEncoding();
            newItem.Encoding = item.Encoding;
            listView2.Items.Add(newItem);

            if (listView2.Items.Count > 0)
                listView2.Items[listView2.Items.Count - 1].Checked = true;
            ControlsBase.Settings.Save();
        }
        //Remove from favorite
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int i = -1;
            foreach (ListViewItem Fitem in listView2.Items)
            {
                if (Fitem.Checked)
                {
                    ControlsBase.Settings.FavoriteEncodings.RemoveAt(Fitem.Index);
                    i = Fitem.Index;
                    break;
                }
            }
            if (i != -1)
                listView2.Items.RemoveAt(i);
            ControlsBase.Settings.Save();
        }
        //search encodings
        private void tstbSearchEncodings_TextChanged(object sender, EventArgs e)
        {
            ClearCheck = true;
            listView1.Items.Clear();
            string searchText = tstbSearchEncodings.Text.Trim();
            if (searchText.Length == 0)
            {
                encodingInfos = allEncodingInfos;
                return;
            }

            var result = encodingInfos.Where(x => x.Name.Length >= searchText.Length && x.Name.Contains(tstbSearchEncodings.Text));
            this.SourceMain = result.ToArray();
            if (listView1.Items.Count > 0)
                listView1.Items[0].Checked = true;
            ClearCheck = false;
        }
        //search favorites
        private void tstbSearchFavorites_TextChanged(object sender, EventArgs e)
        {
            //ClearCheck = true;
            //listView2.Items.Clear();
            //if (tstbSearchFavorites.Text.Length == 0)
            //{
            //    //fill up encodings
            //    EncodingsCollection ec = ControlsBase.Settings.FavoriteEncodings;
            //    ListView lv = listView2;
            //    lv.BeginUpdate();
            //    for (int i = 0; i < ec.Count; i++)
            //    {
            //        LVIEncoding item = new LVIEncoding();
            //        item.Encoding = encodingInfos.First(x => x.CodePage == ec[i]);// new EncodingInfo(); .GetEncoding(ec[i]);
            //                                                                      //item.Encoding = new EncodingInfo(); .GetEncoding(ec[i]);
            //        lv.Items.Add(item);
            //    }
            //    lv.EndUpdate();
            //    return;
            //}
            //for (int i = 0; i < ControlsBase.Settings.FavoriteEncodings.Count; i++)
            //{
            //    Encoding en = Encoding.GetEncoding(ControlsBase.Settings.FavoriteEncodings[i]);
            //    if (en.EncodingName.Length >= tstbSearchFavorites.Text.Length)
            //    {
            //        for (int SearchWordIndex = 0; SearchWordIndex <
            //                  (en.EncodingName.Length - tstbSearchFavorites.Text.Length) + 1; SearchWordIndex++)
            //        {
            //            string Ser = en.EncodingName.Substring(SearchWordIndex, tstbSearchFavorites.Text.Length);
            //            if (Ser.ToLower() == tstbSearchFavorites.Text.ToLower())
            //            {
            //                LVIEncoding item = new LVIEncoding();
            //                item.Encoding = en;
            //                listView2.Items.Add(item);
            //            }
            //        }
            //    }
            //}
            //if (listView2.Items.Count > 0)
            //    listView2.Items[0].Checked = true;

            //ClearCheck = false;
        }
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (ClearCheck)
                return;

            ClearCheck = true;
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = false;
            }
            e.Item.Checked = true;
            ClearCheck = false;
            toolStripLabel1_selectedEncoding.Text = e.Item.Text;

            if (SelectedEncodingChanged != null)
                SelectedEncodingChanged(this, new EventArgs());
        }
        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (ClearCheck)
                return;

            ClearCheck = true;

            foreach (ListViewItem item in listView2.Items)
            {
                item.Checked = false;
            }
            e.Item.Checked = true;
            ClearCheck = false;
            toolStripLabel1_selectedEncoding.Text = e.Item.Text;

            if (SelectedEncodingChanged != null)
                SelectedEncodingChanged(this, new EventArgs());
        }
        private void EncodingsTool_VisibleChanged(object sender, EventArgs e)
        {
            //if (this.Visible)
            //{
            //    foreach (LVIEncoding item in listView1.Items)
            //    {
            //        if (item.Checked)
            //        {
            //            item.EnsureVisible();
            //            break;
            //        }
            //    }
            //}
        }

    }
    public class EncodingsCollection1 : List<int>//encoding codepages, for save
    { }
    public class LVIEncoding : ListViewItem
    {
        EncodingInfo encoding;
        #region ...ctor...
        public LVIEncoding() { }
        public LVIEncoding(EncodingInfo encoding) { this.Encoding = encoding; }
        public LVIEncoding(EncodingInfo encoding, bool isChecked) { this.Encoding = encoding; this.Checked = isChecked; }
        #endregion
        public EncodingInfo Encoding { get { return encoding; } set { encoding = value; RefreshText(); } }
        public void RefreshText()
        {
            SubItems.Clear();
            this.Text = (encoding.Name);
            SubItems.Add(encoding.DisplayName);
            SubItems.Add(encoding.CodePage.ToString());
            
            //this.Text = (encoding.EncodingName);
            //SubItems.Add(encoding.BodyName);
            //SubItems.Add(encoding.CodePage.ToString());


        }
    }
}
