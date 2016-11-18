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
using Android.Support.V7.Widget;
using System.IO;
using SQLite;
using System.Collections;
using Android.Content.PM;

namespace dictionary
{
    [Activity(Label = "archivActivity", Theme = "@android:style/Theme.Black.NoTitleBar", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class archivActivity : Activity
    {
        ORM.DBCards CardsDB = new ORM.DBCards();
        public List<string> mItemsmArchCategs;
        public ListView mListViewArchCategs;

        public static string CategoryNameARCHIVE;

        //we must know where we are now
        public static string archive_indicator_glob, currentNameOfCategInArchive;
        
        //for restarting archive_words_Activity when we delete cards
        //public static bool deleting_indicator, deleting_indicator1;

        public static ArrayList ArchiveEngArrList = new ArrayList();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.arhiv);

            //we must know where we are now
            archive_indicator_glob = "archive";
            //Code For DB for archive categories
            
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var CattDB = new SQLiteConnection(dbPath);
            var table = CattDB.Table<ORM.cardsInArchive>();

            //CREATING TABLE
            CardsDB.CreateTablecardsInArchive();
            CardsDB.GetAllRecordsCategoriesFromArchive();
            //Code For DB for archive categories ENDS HERE
            
            //CODE FOR THE LISTVIEW
            mListViewArchCategs = FindViewById<ListView>(Resource.Id.myListView);

            mItemsmArchCategs = new List<string>();
            
            foreach (var item in table)
            {
                mItemsmArchCategs.Add(item.CategoryName);
            }
            
            //WE MUST HAVE UNIQUE CATEGORIES. SO WE USE DISTINCT
            IEnumerable<string> mItemsDistinct = mItemsmArchCategs.Distinct();
            mItemsmArchCategs = mItemsDistinct.ToList();
            //WE MUST HAVE UNIQUE CATEGORIES. SO WE USE DISTINCT. ENDED
            
            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1,mItems);
            MyListViewAdapter adapter = new MyListViewAdapter(this, mItemsmArchCategs);
            
            mListViewArchCategs.Adapter = adapter;
            mListViewArchCategs.ItemClick += MListView_ItemClick;
            //CODE FOR THE LISTVIEW ENDS HERE
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //the body of this function I made below
            EngCardsInArcive(sender, e);
            //Toast.MakeText(this, mItemsmArchCategs[e.Position], ToastLength.Short).Show();
            currentNameOfCategInArchive = mItemsmArchCategs[e.Position];

            foreach (string item in ArchiveEngArrList)
            {
                archive_indicator_glob = "words";
                StartActivity(new Intent(this, typeof(archive_words_Activity)));
            }
            //!!!IMPORTANT!!! THIS CODE BELOW MAKES MESSAGE WITH THE NAME OF CATEGORY YOU PRESSED:
            //Toast.MakeText(this, mItems[e.Position], ToastLength.Short).Show();
        }

        public void EngCardsInArcive(object sender, AdapterView.ItemClickEventArgs e)
        {
            new ORM.DBCards().GetAllRecordsCategoriesFromArchive();
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var CattDB = new SQLiteConnection(dbPath);
            var table = CattDB.Table<ORM.cardsInArchive>();

            ArchiveEngArrList.Clear();
            //!!!IMPORTANT  .AsEnumerable().Reverse() is for THE CARD CATEGORY WE ADDDED TO ARCHIVE IS PLACED FROM ABOVE THE LIST
            foreach (var item in table.AsEnumerable().Reverse())
            {
                if (item.CategoryName == mItemsmArchCategs[e.Position])
                {
                    CategoryNameARCHIVE = mItemsmArchCategs[e.Position];
                    ArchiveEngArrList.Add(item.EngCardArchive);
                }
            }
        }

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(ARCHIVEActivity));
            StartActivity(intent);
        }
    }
}
 