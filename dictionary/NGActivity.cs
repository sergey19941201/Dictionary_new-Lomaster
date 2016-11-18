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
using dictionary.mCode;
using System.IO;
using SQLite;

namespace dictionary
{
    [Activity(Label = "NGActivity", Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class NGActivity : Activity
    {
        private FragmentManager fragmentManager;
        private addNewIrrVerb irrV;

        public static int CountCards;
        public static bool mixIndicator, mixIndicatorUSER;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NG);

            //Creating table IrregularVerbsUser
            new ORM.DBCards().CreateTableIrrVerbsUSERS();

            fragmentManager = this.FragmentManager;
            irrV = new addNewIrrVerb();

            FindViewById<Button>(Resource.Id.polzovNeprGl_BN).Click += NGActivity_Click;
            FindViewById<Button>(Resource.Id.systemnieNeprGl_BN).Click += NGActivity_Click1;
        }

        private void NGActivity_Click1(object sender, EventArgs e)
        {
            PopupMenu menumuz = new PopupMenu(this, FindViewById<Button>(Resource.Id.polzovNeprGl_BN));

            menumuz.Inflate(Resource.Layout.popupMenuForNGlagoli);

            menumuz.MenuItemClick += (s1, arg1) =>
            {
                if (arg1.Item.TitleFormatted.ToString() == "Учить")
                {
                    StartActivity(new Intent(this, typeof(neprGlagoliActivitySystem)));
                }
                if (arg1.Item.TitleFormatted.ToString() == "Перемешать")
                {
                    neprGlagoliActivitySystem.startPosition = 0;
                    neprGlagoliActivityRusSystem.startPosition = 0;
                    if (mixIndicator == false)
                    {
                        //mixIndicator = true;
                        //clearing sorted random array lists
                        MainActivity.form1AL.Clear();
                        MainActivity.form2AL.Clear();
                        MainActivity.form3AL.Clear();
                        MainActivity.translationAL.Clear();

                        ///////////////////////////////////////
                        //RANDOMIZING
                        var rnd = new Random();
                        var randomlyOrdered = MainActivity.AllDataListIrrVerbsSystem.OrderBy(i => rnd.Next());
                        foreach (var i in randomlyOrdered)
                        {
                            Console.WriteLine("form1: " + i.sysFORM1 + ". form2: " + i.sysFORM2 + ". form3: " + i.sysFORM3 + ". transl: " + i.sysTRANSL);
                            MainActivity.form1AL.Add(i.sysFORM1);
                            MainActivity.form2AL.Add(i.sysFORM2);
                            MainActivity.form3AL.Add(i.sysFORM3);
                            MainActivity.translationAL.Add(i.sysTRANSL);
                        }
                        //RANDOMIZING.ENDED
                        ///////////////////////////////////////
                    }
                    else
                    {
                        //mixIndicator = false;
                        //MainActivity.fillingArrayLists();
                    }

                }
            };
            try
            {
                menumuz.Show();
            }
            catch
            {

            }
        }

        private void NGActivity_Click(object sender, EventArgs e)
        {
            PopupMenu menumuz = new PopupMenu(this, FindViewById<Button>(Resource.Id.polzovNeprGl_BN));

            menumuz.Inflate(Resource.Layout.popupMenuForNGlagoli);

            menumuz.MenuItemClick += (s1, arg1) =>
            {
                if (arg1.Item.TitleFormatted.ToString() == "Учить")
                {
                    if (cnt() != 0)
                    {
                        //WE NEED FUNCTION WITH RETURNED VALUE HERE BECAUSE IF IT IS VOID IT DOESN`T WORK
                        cnt();
                        StartActivity(new Intent(this, typeof(neprGlagoliActivity)));
                    }
                    else
                    {
                        Toast.MakeText(this, "Пустая папка. Добавьте карту.", ToastLength.Short).Show();
                    }
                }
                if (arg1.Item.TitleFormatted.ToString() == "Добавить карту")
                {
                    //Toast.MakeText(this, new ORM.DBCards().GetAllRecordsIrregularVerbs(), ToastLength.Long).Show();
                    irrV.Show(fragmentManager, "fragmentManager");
                }
                if (arg1.Item.TitleFormatted.ToString() == "Перемешать")
                {
                    neprGlagoliActivity.startPosition = 0;
                    neprGlagoliActivityRus.startPosition = 0;
                    if (mixIndicatorUSER == false)
                    {
                        mixIndicatorUSER = true;
                        sortingIrrVerbsUSER.FILL_Big_list();
                    }
                    else
                    {
                        sortingIrrVerbsUSER.FILL_Big_list();
                        //mixIndicatorUSER = false;
                    }
                }
                //
            };
            try
            {
                menumuz.Show();
            }
            catch
            {

            }
        }

        public static int cnt()
        {
            //we need to know how many positions the program must create
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<ORM.IrrVerbsUsers>();
            CountCards = 0;
            //!!!IMPORTANT  .AsEnumerable().Reverse() is for sorting cards at the end of the list
            foreach (var item in table.AsEnumerable().Reverse())
            {
                CountCards++;
            }
            return CountCards;
            //we need to know how many positions the program must create.ENDED
        }

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}