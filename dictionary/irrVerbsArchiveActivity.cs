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
using Android.Content.PM;
using System.Collections.ObjectModel;
using System.IO;
using SQLite;

namespace dictionary
{
    [Activity(Label = "irrVerbsArchiveActivity", Icon = "@drawable/icon", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class irrVerbsArchiveActivity : Activity
    {
        private ListView mListView;
        ObservableCollection<string> mItems = new ObservableCollection<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.irrVerbsArchive);

            mListView = FindViewById<ListView>(Resource.Id.myListView);

            string dbPathCArch = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "IrrVerbsArchive.db3");
            var irrVerbArchive = new SQLiteConnection(dbPathCArch);
            var table = irrVerbArchive.Table<ORM.IrrVerbsArchive>();
            //excavation on the cards from the archive
            foreach (var itemIVU in table)
            {
                mItems.Add(itemIVU.form1);
            }


            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1,mItems);
            MyListViewAdapterCards adapter = new MyListViewAdapterCards(this, mItems);
            mListView.Adapter = adapter;

            mListView.ItemClick += MListView_ItemClick;
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Toast.MakeText(this, mItems[e.Position], ToastLength.Short).Show();
            var menuArchive = new PopupMenu(this, mListView.GetChildAt(0));

            menuArchive.Inflate(Resource.Layout.PopupMenuArchive);

            string form1temp = mItems[e.Position];

            menuArchive.MenuItemClick += (s1, arg1) =>
            {
                switch (arg1.Item.ItemId)
                {
                    case Resource.Id.udalit:

                        int id = Convert.ToInt32(new ORM.DBCards().FindIdOfIrrVerbInArchive(form1temp));
                        new ORM.DBCards().IrrVerbRemoveCardArchive(id);
                        Toast.MakeText(this, "Удалена карта: " + (form1temp), ToastLength.Short).Show();
                        StartActivity(new Intent(this, typeof(irrVerbsArchiveActivity)));

                        break;

                    case Resource.Id.vosstanovit:

                        string dbPathCArch = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "IrrVerbsArchive.db3");
                        var CattDBCArch = new SQLiteConnection(dbPathCArch);
                        var tableCArch = CattDBCArch.Table<ORM.IrrVerbsArchive>();
                        //excavation on the cards from the archive
                        foreach (var itemCArch in tableCArch)
                        {
                            if (itemCArch.form1 == form1temp)
                            {
                                new ORM.DBCards().InsertIrregVerb(itemCArch.form1, itemCArch.form2, itemCArch.form3, itemCArch.translation);
                                int id1 = Convert.ToInt32(new ORM.DBCards().FindIdOfIrrVerbInArchive(form1temp));
                                new ORM.DBCards().IrrVerbRemoveCardArchive(id1);
                                Toast.MakeText(this, "Восстановлена карта: " + (form1temp), ToastLength.Short).Show();
                                StartActivity(new Intent(this, typeof(irrVerbsArchiveActivity)));
                            }

                        }
                        break;
                }
            };
            try
            {
                menuArchive.Show();
            }
            catch
            {

            }
        }

        public override void OnBackPressed()
        {
            StartActivity(new Intent(this, typeof(ARCHIVEActivity)));
        }
    }
}
