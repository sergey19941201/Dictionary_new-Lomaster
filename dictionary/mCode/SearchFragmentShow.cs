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
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using Android.Preferences;
using System.Globalization;

namespace dictionary.mCode
{
    class SearchFragmentShow : DialogFragment
    {
        private EditText SearchEditText;
        private Button SearchBtn, otmenaBtn;
        public static bool searchIndicator;
        public static string textToSearch;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.SearchFragment, container, false);

            //Заглавие фрагмента:
            //this.Dialog.SetTitle("Поиск папок");

            otmenaBtn = rootView.FindViewById<Button>(Resource.Id.otmenaBtn);
            otmenaBtn.Click += delegate
              {
                  Dismiss();
              };

            SearchBtn = rootView.FindViewById<Button>(Resource.Id.SearchBtn);
            SearchBtn.Click += SearchBtn_Click;

            SearchEditText = rootView.FindViewById<EditText>(Resource.Id.SearchEditText);

            return rootView;
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            ORM.DBRepository dbr = new ORM.DBRepository();

            if (String.IsNullOrEmpty(SearchEditText.Text) || SearchEditText.Text == " " || SearchEditText.Text == "  " || SearchEditText.Text == "   ")
            {
                Toast.MakeText(this.Activity, "Ничего не введено.", ToastLength.Short).Show();
            }
            else
            {
                searchIndicator = true;
                textToSearch = SearchEditText.Text.ToUpper();
                this.Activity.StartActivity(new Intent(this.Activity, typeof(dicListActivity)));
            }
        }
    }
}