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
using SQLite;
using dictionary.mCode;
using Android.Content.PM;

namespace dictionary
{
    [Activity(Label = "dicListActivity", Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait)]

    public class dicListActivity : Activity
    {
        private static int Position_;
        private List<string> mItems, mItemsSearch;
        private ListView mListView;
        private FragmentManager fragmentManager, newCardFragm, pereimFragmentManager, searchFragmentManager;
        private addNewCard NewCardAdd;
        private ShowFragment showFragment;
        private pereimenovatCategFragmShow showFragmentPereimenovat;
        private SearchFragmentShow showFragmentSearch;
        
        //WE NEED THIS CODE HERE TO KNOW HOW MANY POSITIONS FOR CARDS MUST THE PROGRAM CREATE
        public static int countCards;
        //WE NEED TO KNOW THE NAME OF CATEGORY WHICH CAN BE ADDED TO ARCHIVE
        public static string nameOfCategoryForArchiveGlob;

        public static int ID_of_catGlob, ID_of_catGlobForPeremeshat;
        public static string CategoryNameGlob;
        public static bool MixIndicator = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.dicList);
            mListView = FindViewById<ListView>(Resource.Id.myListView);
            //calling the method that adds categories from the DB to the list
            mItems = new List<string>();
            mItemsSearch = new List<string>();

            //archive_words_Activity.categExistsIndicator = false;

            new ORM.DBCards().CreateTableCategory1Cards();

            GetAllCategs();
            if (SearchFragmentShow.textToSearch == null)
            {
                dicListAdapter adapter = new dicListAdapter(this, mItems);
                mListView.Adapter = adapter;
            }
            else
            {
                foreach(string item in mItems)
                {
                    if (item.ToUpper().Contains(SearchFragmentShow.textToSearch))
                    {
                        mItemsSearch.Add(item);
                    }
                }
                dicListAdapter adapter = new dicListAdapter(this, mItemsSearch);
                mListView.Adapter = adapter;
            }
            
            mListView.ItemClick += MListView_ItemClick;

            FindViewById<ImageButton>(Resource.Id.addFolderButton).Click += DicListActivity_Click;

            fragmentManager = this.FragmentManager;
            showFragment = new ShowFragment();

            //Initialize fragment to add new card
            newCardFragm = this.FragmentManager;
            NewCardAdd = new addNewCard();
            //INITIALIZE Fragment for search
            searchFragmentManager = this.FragmentManager;
            showFragmentSearch = new SearchFragmentShow();

            //this code is to launch ViewPagerActivity when we still have cards inside it. But if it is empty, we do nothing
            if (PagerFragment.DelCardCat1Global == true)
            {
                GetAllRecordsCategoryCards1();

                if (countCards == 0)
                {

                }
                else
                {
                    this.StartActivity(typeof(ViewPagerActivity));
                }
            }

            FindViewById<ImageButton>(Resource.Id.menuButton).Click += DicListActivity_Click1;

            //Code below is to make the alert dialog for user to know how to add new folder
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<ORM.ToDoTasks>();
            int count = 0;
            foreach (var item in table)
            {
                count++;
            }
            if (count == 0)
            {
                Android.App.AlertDialog.Builder builder1 = new Android.App.AlertDialog.Builder(this);
                builder1.SetMessage("Для добавления новой папки нажмите \"+\"");
                builder1.SetCancelable(true);
                builder1.SetNegativeButton("ОК", (object sender, DialogClickEventArgs e) =>
                { });
                Android.App.AlertDialog dialog1 = builder1.Create();
                dialog1.Show();
            }
            //Code for making the alert dialog for user to know how to add new folder ENDS HERE

            pereimFragmentManager = this.FragmentManager;
            showFragmentPereimenovat = new pereimenovatCategFragmShow();

            FindViewById<ImageButton>(Resource.Id.searchButton).Click += DicListActivity_Click2;
        }

        private void DicListActivity_Click2(object sender, EventArgs e)
        {
            showFragmentSearch.Show(searchFragmentManager, "searchFragmentManager");
        }

        private void DicListActivity_Click1(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }

        private void DicListActivity_Click(object sender, EventArgs e)
        {
            showFragment.Show(fragmentManager, "fragmentManager");
        }
        
        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<ORM.ToDoTasks>();

            if (SearchFragmentShow.textToSearch == null)
            {
                foreach (var item in table)
                {
                    if (item.Task == mItems[e.Position])
                    {
                        CategoryNameGlob = mItems[e.Position];
                        ID_of_catGlob = item.Id;
                    }
                }
            }
            else
            {
                foreach (var item in table)
                {
                    if (item.Task == mItemsSearch[e.Position])
                    {
                        CategoryNameGlob = mItemsSearch[e.Position];
                        ID_of_catGlob = item.Id;
                    }
                }
            }
            PopupMenu menumuz = new PopupMenu(this, mListView.GetChildAt(0));

            menumuz.Inflate(Resource.Layout.popupMenuForCategories);

            menumuz.MenuItemClick += (s1, arg1) =>
            {
                Position_ = e.Position;
                if (arg1.Item.TitleFormatted.ToString() == "Учить")
                {
                    uchit();
                }

                if (arg1.Item.TitleFormatted.ToString() == "Переименовать папку")
                {
                    showFragmentPereimenovat.Show(pereimFragmentManager, "pereimFragmentManager");
                }

                if (arg1.Item.TitleFormatted.ToString() == "Перемешать")
                {
                    peremeshat();
                }

                if (arg1.Item.TitleFormatted.ToString() == "Добавить карту")
                {
                    dobavitCartu();
                }

                if (arg1.Item.TitleFormatted.ToString() == "Удалить папку")
                {
                    udalitPapku();
                }
            };
            //try
            //{
               menumuz.Show();
            /*}
            catch
            {

            }*/
            //Toast.MakeText(this, mItems[e.Position], ToastLength.Short).Show();
        }

        //Сode to retrieve all the records.WE NEED THIS CODE HERE TO KNOW HOW MANY POSITIONS FOR CARDS MUST THE PROGRAM CREATE
        public string GetAllRecordsCategoryCards1()
        {
            countCards = 0;

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retrieving the data using ORM...";
            var table = db.Table<ORM.Category1Cards>();

            foreach (var item in table)
            {
                if (item.CategoryId == ID_of_catGlob)
                {
                    output += "\n" + item.Id + " --- Eng: " + item.Eng1c + ",   Rus: " + item.Rus1c;
                    if (item.Eng1c != null)
                    {
                        countCards++;
                    }
                }
            }
            return output;
        }

        //adding categories from the DB to the list
        public string GetAllCategs()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retrieving the data using ORM...";
            var table = db.Table<ORM.ToDoTasks>();
            //!!!IMPORTANT  .AsEnumerable().Reverse() is for THE LAST CATEGORY WE ADD IS PLACED FROM ABOVE THE LIST
            foreach (var item in table.AsEnumerable().Reverse())
            {
                output += "\n" + item.Task;
                mItems.Add(item.Task);
            }

            return output;
        }
        public override void OnBackPressed()
        {
            if (SearchFragmentShow.textToSearch == null)
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
            else
            {
                SearchFragmentShow.textToSearch = null;
                StartActivity(new Intent(this, typeof(dicListActivity)));
            }
        }
        private void uchit()
        {
            ViewPagerActivity.startPosition = 0;
            string dbPath1 = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db1 = new SQLiteConnection(dbPath1);
            var table1 = db1.Table<ORM.ToDoTasks>();

            if (SearchFragmentShow.textToSearch == null)
            {
                foreach (var item in table1)
                {
                    if (item.Task == mItems[Position_])
                    {
                        CategoryNameGlob = mItems[Position_];
                        ID_of_catGlob = item.Id;
                    }
                }
            }
            else
            {
                foreach (var item in table1)
                {
                    if (item.Task == mItemsSearch[Position_])
                    {
                        CategoryNameGlob = mItemsSearch[Position_];
                        ID_of_catGlob = item.Id;
                    }
                }
            }

            if (ID_of_catGlobForPeremeshat != ID_of_catGlob)
            {
                MixIndicator = false;
            }

            GetAllRecordsCategoryCards1();
            //WE NEED THIS CODE HERE TO KNOW HOW MANY POSITIONS FOR CARDS MUST THE PROGRAM CREATE
            if (countCards == 0)
            {
                Toast.MakeText(this, "Пустая папка. Добавьте карту.", ToastLength.Long).Show();
            }
            else
            {
                this.StartActivity(typeof(ViewPagerActivity));
            }
        }
        private void peremeshat()
        {
            //DON`T DELETE
            MixIndicator = true;

            ViewPagerActivity.startPosition = 0;
            ViewPagerActivityRus.startPosition = 0;
            PagerFragment.EngArrList.Clear();
            PagerFragmentRus.RusArrList.Clear();

            ///////////////////////////////////////
            //RANDOMIZING
            var rnd = new Random();
            var randomlyOrdered = PagerFragment.AllDataListWords.OrderBy(i => rnd.Next());
            foreach (var i in randomlyOrdered)
            {
                PagerFragment.EngArrList.Add(i.engWORD);
                PagerFragmentRus.RusArrList.Add(i.rusWORD);
                ID_of_catGlobForPeremeshat = i.catID;
            }
            //RANDOMIZING.ENDED
            ///////////////////////////////////////
        }
        private void dobavitCartu()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<ORM.ToDoTasks>();

            MixIndicator = false;
            if (SearchFragmentShow.textToSearch == null)
            {
                foreach (var item in table)
                {
                    if (item.Task == mItems[Position_])
                    {
                        CategoryNameGlob = mItems[Position_];
                        ID_of_catGlob = item.Id;
                    }
                }
            }
            else
            {
                foreach (var item in table)
                {
                    if (item.Task == mItemsSearch[Position_])
                    {
                        CategoryNameGlob = mItemsSearch[Position_];
                        ID_of_catGlob = item.Id;
                    }
                }
            }
            NewCardAdd.Show(newCardFragm, "newCardFragm");
        }
        private void udalitPapku()
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Предупреждение!");
            builder.SetMessage("Удалить папку?");
            builder.SetCancelable(true);
            builder.SetNegativeButton("Нет", (object send, DialogClickEventArgs e_1) =>
            { });
            builder.SetPositiveButton("Удалить", (object send, DialogClickEventArgs e_1) =>
            {
                //deleting the category:
                string deleted_category = new ORM.DBRepository().RemoveTask(ID_of_catGlob);
                //deleting the cards:
                new ORM.DBCards().RemoveCardsFromTheCategory();
                Toast.MakeText(this, "Удалена категория: " + CategoryNameGlob, ToastLength.Short).Show();
                //Запускаем dicActivity
                this.StartActivity(typeof(dicListActivity));
            });
            //Создаём фрагмент
            Android.App.AlertDialog dialog = builder.Create();
            dialog.Show();
        }
    }
}