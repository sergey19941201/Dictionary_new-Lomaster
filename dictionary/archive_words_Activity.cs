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
using SQLite;
using System.IO;
using System.Collections;
using SQLite;
using Android.Content.PM;

namespace dictionary
{
    [Activity(Label = "archive_words_Activity", Theme = "@android:style/Theme.Black.NoTitleBar", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class archive_words_Activity : Activity
    {
        public List<string> mItemsList;
        private ListView mListView;

        //we need this bool to know if the category of the cards that we want to recover exists in main listview
        public static bool categExistsIndicator;
        private bool cardExistsIndicator;
        private string cardENG_to_Delete;
        private static int RecoverIdOfCateg, id_of_the_NEW_added_category;
        private string RecoverItemInArchiveEng, RecoverItemInArchiveRu, RecoverItemInArchiveNameOfCateg;
        ObservableCollection<string> mItems = new ObservableCollection<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.archive_words_bg);

            mListView = FindViewById<ListView>(Resource.Id.myListView);

            ObservableCollection<string> mItems = new ObservableCollection<string>();
            mItemsList = new List<string>();

            ArrayList ArchiveEngArrList = new ArrayList();

            new ORM.DBCards().GetAllRecordsCategoriesFromArchive();
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var CattDB = new SQLiteConnection(dbPath);
            var table = CattDB.Table<ORM.cardsInArchive>();

            ArchiveEngArrList.Clear();

            foreach (var item in table)
            {
                if (item.CategoryName == archivActivity.CategoryNameARCHIVE)
                {
                    ArchiveEngArrList.Add(item.EngCardArchive);
                }
            }
            foreach (string item in ArchiveEngArrList)
            {
                //Toast.MakeText(this,"ENG: "+ item, ToastLength.Short).Show();
                mItems.Add(item);
                mItemsList.Add(item);
            }
            //This code is to start archivActivity when the ArchiveEngArrList is empty 
            if (ArchiveEngArrList.Count == 0)
            {
                StartActivity(new Intent(this, typeof(archivActivity)));
            }
            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);

            mListView.Adapter = new MyListViewAdapterCards(this, mItems);

            mListView.ItemClick += MListView_ItemClick;
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var menuArchive = new PopupMenu(this, mListView.GetChildAt(0));

            menuArchive.Inflate(Resource.Layout.PopupMenuArchive);

            menuArchive.MenuItemClick += (s1, arg1) =>
            {
                switch (arg1.Item.ItemId)
                {
                    case Resource.Id.udalit:

                        new ORM.DBCards().RemoveArchiveCategory(Convert.ToInt32(new ORM.DBCards().cardsInArchive_Id1(mItemsList[e.Position])));
                        Toast.MakeText(this, "Удалена карта: " + (mItemsList[e.Position]), ToastLength.Short).Show();
                        StartActivity(new Intent(this, typeof(archive_words_Activity)));

                        break;

                    case Resource.Id.vosstanovit:

                        //put a mark that in the category table the desired category is not found by default. If it is found, the variable is overwritten to true
                        categExistsIndicator = false;
                        //put a mark that recoverable card does not exist in the main table with cards.If it is found, the variable is overwritten to true
                        cardExistsIndicator = false;
                        //DB archive cards
                        string dbPathCArch = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
                        var CattDBCArch = new SQLiteConnection(dbPathCArch);
                        var tableCArch = CattDBCArch.Table<ORM.cardsInArchive>();
                        //excavation on the cards from the archive
                        foreach (var itemCArch in tableCArch)
                        {
                            if (itemCArch.EngCardArchive == mItemsList[e.Position])
                            {
                                //We assign all data on the card that we want to restore the variables
                                RecoverIdOfCateg = itemCArch.CategoryId;
                                RecoverItemInArchiveNameOfCateg = itemCArch.CategoryName;
                                RecoverItemInArchiveEng = itemCArch.EngCardArchive;
                                RecoverItemInArchiveRu = itemCArch.RusCardArchive;

                                //now we need to check if whether the category with the ID in the category table
                                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ormdemo15.db3");
                                var CattDB = new SQLiteConnection(dbPath);
                                var table = CattDB.Table<ORM.ToDoTasks>();
                                //excavation on the table of the categories
                                foreach (var item in table)
                                {
                                    //if we find the category that is equal to a category that is written in the record of our card
                                    if (item.Id == RecoverIdOfCateg)
                                    {
                                        //put a mark in the category table the required category is found
                                        categExistsIndicator = true;
                                        break;
                                    }
                                    else
                                    {
                                        //ставим отметку, что в ТАБЛИЦЕ КАТЕГОРИЙ искомая категория НЕ найдена
                                        categExistsIndicator = false;
                                    }
                                }
                                //Insert the card from an archive, if this card is not present and there is such a category
                                if (categExistsIndicator == true)
                                {
                                    IFcatExistsRecover();
                                }
                                else
                                {
                                    IFcatNotExistsRecover();
                                }
                            }
                        }
                        break;
                }
            };

            menuArchive.Show();
        }

        private void IFcatNotExistsRecover()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var CattDB = new SQLiteConnection(dbPath);
            var table = CattDB.Table<ORM.ToDoTasks>();
            //Since the category does not exist, create it with the same name
            new ORM.DBRepository().InsertRecord(RecoverItemInArchiveNameOfCateg);
            //Now you need to find ID of the inserted category
            //excavation on the table of the categories
            foreach (var item in table)
            {
                if (item.Task == RecoverItemInArchiveNameOfCateg)
                {
                    id_of_the_NEW_added_category = item.Id;
                    break;
                }
            }

            //Now you need to go through all the cards archive, find the ones that have been with the old category ID and update this ID
            //Database of all the cards
            string dbPathAllCdsARCHIVE = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var CattDBAllCdsARCHIVE = new SQLiteConnection(dbPathAllCdsARCHIVE);
            var tableAllCdsARCHIVE = CattDBAllCdsARCHIVE.Table<ORM.cardsInArchive>();
            foreach (var cardARCHIVE in tableAllCdsARCHIVE.AsEnumerable().Reverse())
            {
                if (cardARCHIVE.CategoryId == RecoverIdOfCateg)
                {
                    //We do update categoryID and categoryNames for these cards
                    new ORM.DBCards().updateArchiveCard(cardARCHIVE.Id, id_of_the_NEW_added_category, RecoverItemInArchiveNameOfCateg);

                    //DB of all the cards
                    string dbPathAllCds = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Card1111sDB.db3");
                    var CattDBAllCds = new SQLiteConnection(dbPathAllCds);
                    var tableAllCds = CattDBAllCds.Table<ORM.Category1Cards>();
                    //The penetration of all cards.Looking for compliance with our
                    foreach (var card in tableAllCds)
                    {
                        if (card.Eng1c == RecoverItemInArchiveEng || card.Rus1c == RecoverItemInArchiveRu)
                        {
                            cardExistsIndicator = true;

                            break;
                        }
                    }
                    //Insert the card from an archive, if there is no such a card
                    if (cardExistsIndicator == false)
                    {
                        //Inserting card:
                        new ORM.DBCards().InsertRecordCategory1Cards(RecoverItemInArchiveEng, RecoverItemInArchiveRu, id_of_the_NEW_added_category, RecoverItemInArchiveNameOfCateg);

                        //deleting card from the archive:
                        cardENG_to_Delete = RecoverItemInArchiveEng;
                        try
                        {
                            //deleting card from the archive HERE:
                            new ORM.DBCards().RemoveArchiveCategory(Convert.ToInt32(new ORM.DBCards().cardsInArchive_Id1(RecoverItemInArchiveEng)));
                        }
                        catch
                        {
                            Toast.MakeText(this, "CRASH", ToastLength.Short).Show();
                        }
                        cardExistsIndicator = true;
                        //We put a mark that in the category table the desired category not found
                        categExistsIndicator = true;
                        StartActivity(new Intent(this, typeof(archive_words_Activity)));
                    }
                }
            }
        }

        private void IFcatExistsRecover()
        {
            //DB of all the cards
            string dbPathAllCds = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var CattDBAllCds = new SQLiteConnection(dbPathAllCds);
            var tableAllCds = CattDBAllCds.Table<ORM.Category1Cards>();

            //The penetration of all cards.Looking for compliance with our
            foreach (var card in tableAllCds)
            {
                if (card.Eng1c == RecoverItemInArchiveEng || card.Rus1c == RecoverItemInArchiveRu)
                {
                    cardExistsIndicator = true;
                    Toast.MakeText(this, "Восстановить невозможно." + "\n" + "Карта существует.", ToastLength.Short).Show();
                    break;
                }
            }
            if (cardExistsIndicator == false)
            {
                new ORM.DBCards().InsertRecordCategory1Cards(RecoverItemInArchiveEng, RecoverItemInArchiveRu, RecoverIdOfCateg, RecoverItemInArchiveNameOfCateg);
                //deleting card from the archive:
                new ORM.DBCards().RemoveArchiveCategory(Convert.ToInt32(new ORM.DBCards().cardsInArchive_Id1(RecoverItemInArchiveEng)));
                Toast.MakeText(this, "Восстановлена карта: " + (RecoverItemInArchiveEng), ToastLength.Short).Show();
                StartActivity(new Intent(this, typeof(archive_words_Activity)));
            }
        }

        public override void OnBackPressed()
        {
            StartActivity(new Intent(this, typeof(archivActivity)));
        }
    }
}