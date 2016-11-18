using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.ObjectModel;

namespace dictionary
{
    class MyListViewAdapterCards : BaseAdapter<string>
    {
        public ObservableCollection<string> mItems;
        private Context mContext;

        public MyListViewAdapterCards(Context context, ObservableCollection<string> items)
        {
            mItems = items;
            mContext = context;
        }
        public override int Count
        {
            get { return mItems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override string this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                if (archivActivity.archive_indicator_glob == "archive")
                {
                    row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listview_row, null, false);
                }
                if (archivActivity.archive_indicator_glob == "words")
                {
                    row = LayoutInflater.From(mContext).Inflate(Resource.Layout.archive_words, null, false);
                }
                if (archivActivity.archive_indicator_glob == "irrVerbs")
                {
                    row = LayoutInflater.From(mContext).Inflate(Resource.Layout.irr_verbsArchive_row, null, false);
                }
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = mItems[position];

            return row;
        }
    }
}
