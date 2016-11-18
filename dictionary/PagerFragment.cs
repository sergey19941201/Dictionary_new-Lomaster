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
using System.Collections;
using SQLite;
using System.IO;

namespace dictionary
{
    public class FillingListWords
    {
        public string engWORD { get; set; }
        public string rusWORD { get; set; }
        public int catID { get; set; }
        public string catNAME { get; set; }
    }

    public class PagerFragment : Fragment
    {
        //ArrayList for the english cards. WE NEED ARRAYLIST TO COLLECT ELEMENTS, THAT ARE NOT NULL
        public static ArrayList EngArrList = new ArrayList();

        public static List<FillingListWords> AllDataListWords = new List<FillingListWords>();

        //indicator for deleting cards from category1
        public static bool DelCardCat1Global = false;

        int position;
        public PagerFragment(int position)
        {
            this.position = position;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment, container, false);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = string.Format("Position {0}", position);

            //indicator for deleting cards from category1. NEED TO BE FALSE HERE
            DelCardCat1Global = false;

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var db = new SQLiteConnection(dbPath);
            ORM.DBCards CardsDB = new ORM.DBCards();
            //CREATING TABLE
            CardsDB.CreateTablecardsInArchive();

            //Объявляем предупреждение
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this.Activity);
            builder.SetTitle("Предупреждение!");
            builder.SetMessage("Удалить карту?");
            //Если поставить false, то это окно НЕ будет закрываться если кликнуть вне его
            builder.SetCancelable(true);
            builder.SetNegativeButton("Нет", (object sender, DialogClickEventArgs e) => { });

            //Объявляем предупреждение for archive
            Android.App.AlertDialog.Builder builderArchive = new Android.App.AlertDialog.Builder(this.Activity);
            builderArchive.SetTitle("Предупреждение!");
            builderArchive.SetMessage("Переместить карту в архив?");
            //Если поставить false, то это окно НЕ будет закрываться если кликнуть вне его
            builderArchive.SetCancelable(true);
            builderArchive.SetNegativeButton("Нет", (object sender, DialogClickEventArgs e) => { });

            if (dicListActivity.MixIndicator == false)
            {
                AllDataListWords.Clear();
                GetAllRecordsCategoryCardsENG1();
            }
            //if (dicListActivity.MixIndicator == false)
            {
                foreach (string i in EngArrList)
                {
                    if (this.position == EngArrList.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.TextView).Text = i;
                    }
                }
            }
            /* else
             {
                 foreach (string i in sortingCards.EngArrListIsSortedRandom)
                 {
                     if (this.position == sortingCards.EngArrListIsSortedRandom.IndexOf(i))
                     {
                         view.FindViewById<TextView>(Resource.Id.TextView).Text = i;
                     }
                 }
             }*/

            //ImageButton to turn cards
            view.FindViewById<ImageButton>(Resource.Id.perevernBn).Click += delegate
            {
                PagerFragmentRus.POS_Global = this.position;
                ViewPagerActivityRus.startPosition = this.position;
                var intent = new Intent(this.Activity, typeof(ViewPagerActivityRus));
                StartActivity(intent);
            };

            //deleteBn
            view.FindViewById<ImageButton>(Resource.Id.deleteBn).Click += delegate
            {
                builder.SetPositiveButton("Удалить", (object sender, DialogClickEventArgs e) =>
                {
                    GetAllRecordsCategoryCardsENG1();
                    CardsDB.RemoveCard1(Convert.ToInt32(CardsDB.FindENG_Id1(view.FindViewById<TextView>(Resource.Id.TextView).Text)));
                    Toast.MakeText(this.Activity, "Удалена карта: " + view.FindViewById<TextView>(Resource.Id.TextView).Text, ToastLength.Short).Show();
                    DelCardCat1Global = true;
                    dicListActivity.MixIndicator = false;
                    //Запускаем ViewPagerActivity
                    var intent = new Intent(this.Activity, typeof(dicListActivity));
                    StartActivity(intent);
                });
                //Создаём фрагмент
                Android.App.AlertDialog dialog = builder.Create();
                dialog.Show();
            };

            //dobavitVArchivBn
            view.FindViewById<ImageButton>(Resource.Id.dobavitVArchivBn).Click += delegate
            {
                builderArchive.SetPositiveButton("Переместить", (object sender, DialogClickEventArgs e) =>
                {
                    CardsDB.InsertRecordcardsInArchive(dicListActivity.CategoryNameGlob, dicListActivity.ID_of_catGlob, view.FindViewById<TextView>(Resource.Id.TextView).Text, CardsDB.FindRUS_name1(Convert.ToInt32(CardsDB.FindENG_Id1(view.FindViewById<TextView>(Resource.Id.TextView).Text))));
                    //GetAllRecordsCategoryCardsENG1();
                    CardsDB.RemoveCard1(Convert.ToInt32(CardsDB.FindENG_Id1(view.FindViewById<TextView>(Resource.Id.TextView).Text)));
                    Toast.MakeText(this.Activity, "Перемещено в архив: " + view.FindViewById<TextView>(Resource.Id.TextView).Text, ToastLength.Short).Show();
                    DelCardCat1Global = true;
                    dicListActivity.MixIndicator = false;

                    //Запускаем ViewPagerActivity
                    StartActivity(new Intent(this.Activity, typeof(dicListActivity)));
                });
                //Создаём фрагмент
                Android.App.AlertDialog dialog = builderArchive.Create();
                dialog.Show();
            };
            return view;
        }

        private object getBaseContext()
        {
            throw new NotImplementedException();
        }

        //Сode to retrieve all the ENG records
        public string GetAllRecordsCategoryCardsENG1()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retrieving the data using ORM...";
            var table = db.Table<ORM.Category1Cards>();

            EngArrList.Clear();
            //!!!IMPORTANT  .AsEnumerable().Reverse() is for sorting cards at the end of the list
            foreach (var item in table.AsEnumerable().Reverse())
            {
                if (item.CategoryId == dicListActivity.ID_of_catGlob)
                {
                    output += "\n" + item.Id + " --- Eng: " + item.Eng1c + ",   Rus: " + item.Rus1c;
                    if (item.Eng1c != null)
                    {
                        AllDataListWords.Add(new FillingListWords { engWORD = item.Eng1c, rusWORD = item.Rus1c, catID = item.CategoryId, catNAME = item.CategoryName });
                    }
                }
            }

            foreach (var i in AllDataListWords)
            {
                EngArrList.Add(i.engWORD);
            }

            return output;
        }
    }
}