using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncodingConverter.Controls
{
    /// <summary>
    /// A <see cref="ListView"/> with a text search system.
    /// </summary>
    /// <typeparam name="T">Type of source items witch are used to generate <see cref="ListViewItem"/>s</typeparam>
    class SearchableListView<T> : ListView
    {
        static char[] _SplitChars = { ' ' };

        T[] _CurrentSource;

        Func<T, ListViewItem> _ObjectToListViewItemConverter;
        Func<T, string> _ObjectToSearchableTextConverter;
        //Func<object, bool> _Predicate;
        T[] _SourceList;
        private string _SearchText;


        #region ...Events...
        public event EventHandler SearchTextChanged;
        public event EventHandler CurrentSourceChanged;
        public event EventHandler CurrentSourceChanging;
        #endregion

        #region ...Properties...
        [DefaultValue(null)]
        public Func<T, ListViewItem> ObjectToListViewItemConverter
        {
            get { return _ObjectToListViewItemConverter; }
            set
            {
                if (_ObjectToListViewItemConverter == value)
                    return;

                _ObjectToListViewItemConverter = value;

                RefreshSearchText();
            }
        }

        [DefaultValue(null)]
        public Func<T, string> ObjectToSearchableTextConverter
        {
            get { return _ObjectToSearchableTextConverter; }
            set
            {
                if (_ObjectToSearchableTextConverter == value)
                    return;

                _ObjectToSearchableTextConverter = value;

                RefreshSearchText();
            }
        }

        [DefaultValue(null)]
        public T[] SourceList
        {
            get { return _SourceList; }
            set
            {
                if (_SourceList == value)
                    return;
                _SourceList = value;
                RefreshSearchText();
            }
        }

        [DefaultValue(null)]
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (_SearchText == value)
                    return;
                
                _SearchText = value;

                RefreshSearchText();
                OnSearchTextChanged();
            }
        }
        #endregion

        #region ...Event invokers...
        protected virtual void OnSearchTextChanged() { SearchTextChanged?.Invoke(this, EventArgs.Empty); }
        protected virtual void OnCurrentSourceChanged() { CurrentSourceChanged?.Invoke(this, EventArgs.Empty); }
        protected virtual void OnCurrentSourceChanging(T[] newCurrentSource) { CurrentSourceChanging?.Invoke(this, EventArgs.Empty); }
        #endregion
        void RefreshSearchText()
        {
            if (_SearchText == null || _ObjectToSearchableTextConverter == null)
            {
                this.CurrentSource = _SourceList;
                return;
            }

            string searchText = _SearchText.Trim();//.ToLower();
            if (searchText.Length == 0)
            {
                this.CurrentSource = _SourceList;
                return;
            }

            var searchStrings = searchText.Split(_SplitChars, StringSplitOptions.RemoveEmptyEntries);
            var result = _SourceList.Where(x => _ObjectToSearchableTextConverter(x).Contains(searchStrings));
            this.CurrentSource = result.ToArray();
        }

        [DefaultValue(null)]
        public T[] CurrentSource
        {
            get { return _CurrentSource; }
            set
            {
                if (value == _CurrentSource)
                {
                    return;
                }
                OnCurrentSourceChanging(value);
                this.Items.Clear();
                _CurrentSource = value;
                if (_CurrentSource != null && _CurrentSource.Length > 0
                    && _ObjectToListViewItemConverter != null)
                {
                    //this.BeginUpdate();
                    this.Items.AddRange(_CurrentSource.Select(_ObjectToListViewItemConverter).ToArray());
                    //this.EndUpdate();
                }
                OnCurrentSourceChanged();
            }
        }

        //void SetCurrentSource(object[] newCurrentSource)
        //{
        //    if (newCurrentSource == _CurrentSource)
        //    {
        //        return;
        //    }
        //    this.Items.Clear();
        //    _CurrentSource = newCurrentSource;
        //    if (_CurrentSource == null || _CurrentSource.Length == 0
        //        || _ObjectToListViewItemConverter == null)
        //    {
        //        return;
        //    }
        //    this.Items.AddRange(_CurrentSource.Select(_ObjectToListViewItemConverter).ToArray());
        //}


    }
}
