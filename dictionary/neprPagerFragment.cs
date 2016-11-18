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
    public class neprPagerFragment : Fragment
    {
        //ArrayList for id of the cards
        public static ArrayList IdArrList = new ArrayList();
        //ArrayList for the english verbs. WE NEED ARRAYLIST TO COLLECT ELEMENTS, THAT ARE NOT NULL
        public static ArrayList form1AL = new ArrayList();
        public static ArrayList form2AL = new ArrayList();
        public static ArrayList form3AL = new ArrayList();

        int position;
        public neprPagerFragment(int position)
        {
            this.position = position;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.neprFragment, container, false);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = string.Format("Position {0}", position);

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
            var db = new SQLiteConnection(dbPath);
            ORM.DBCards CardsDB = new ORM.DBCards();

            CardsDB.CreateTableIrrVerbsUSERS();

            //Declare warning
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this.Activity);
            builder.SetTitle("Предупреждение!");
            builder.SetMessage("Удалить карту?");
            //if we make false, this window will not close if we click outside it
            builder.SetCancelable(true);
            builder.SetNegativeButton("Нет", (object sender, DialogClickEventArgs e) => { });

            //Declare warning for archive
            Android.App.AlertDialog.Builder builderArchive = new Android.App.AlertDialog.Builder(this.Activity);
            builderArchive.SetTitle("Предупреждение!");
            builderArchive.SetMessage("Переместить карту в архив?");
            //if we make false, this window will not close if we click outside it
            builderArchive.SetCancelable(true);
            builderArchive.SetNegativeButton("Нет", (object sender, DialogClickEventArgs e) => { });


            ////////////////////////////////////
            //ТУТ СВОЙ МИКСИНДИКАТОР СДЕЛАТЬ. ПЕРЕДЕЛАТЬ.
            //NGActivity.mixIndicatorUSER = false;
            if (NGActivity.mixIndicatorUSER == false)
            {
                //fumction to fill arrayLists
                fillingArrayLists();
                foreach (string i in IdArrList)
                {
                    if (this.position == IdArrList.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.item_id).Text = i;
                    }
                }
                foreach (string i in form1AL)
                {
                    if (this.position == form1AL.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.form1Text).Text = i;
                    }
                }

                foreach (string i in form2AL)
                {
                    if (this.position == form2AL.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.form2Text).Text = i;
                    }
                }

                foreach (string i in form3AL)
                {
                    if (this.position == form3AL.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.form3Text).Text = i;
                    }
                }
            }
            else
            {
                //sortingIrrVerbsUSER.getInfoCard();
                //sortingIrrVerbsUSER.FILL_Big_list();
                //fillingArrayLists();
                foreach (int i in sortingIrrVerbsUSER.idIsSortedRandom)
                {
                    if (this.position == sortingIrrVerbsUSER.idIsSortedRandom.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.item_id).Text = Convert.ToString(i);
                    }
                }

                foreach (string i in sortingIrrVerbsUSER.form1IsSortedRandom)
                {
                    if (this.position == sortingIrrVerbsUSER.form1IsSortedRandom.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.form1Text).Text = i;
                    }
                }
                foreach (string i in sortingIrrVerbsUSER.form2IsSortedRandom)
                {
                    if (this.position == sortingIrrVerbsUSER.form2IsSortedRandom.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.form2Text).Text = i;
                    }
                }

                foreach (string i in sortingIrrVerbsUSER.form3IsSortedRandom)
                {
                    if (this.position == sortingIrrVerbsUSER.form3IsSortedRandom.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.form3Text).Text = i;
                    }
                }
            }
            /////////////////////////////////////

            view.FindViewById<ImageButton>(Resource.Id.perevernBn).Click += NeprPagerFragment_Click;

            view.FindViewById<ImageButton>(Resource.Id.dobavitVArchivBn).Click += delegate
            {
                builderArchive.SetPositiveButton("Переместить", (object sender, DialogClickEventArgs e) =>
                {
                    NGActivity.mixIndicatorUSER = false;

                    string dbPathCArch = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
                    var irrVerbUsers = new SQLiteConnection(dbPathCArch);
                    var table = irrVerbUsers.Table<ORM.IrrVerbsUsers>();
                    //excavation on the cards from the archive
                    foreach (var itemIVU in table)
                    {
                        try
                        {
                            int id = Convert.ToInt32(view.FindViewById<TextView>(Resource.Id.item_id).Text);
                            //Toast.MakeText(this.Activity, Convert.ToString(id), ToastLength.Short).Show();
                            if (itemIVU.Id == id)
                            {
                                //Toast.MakeText(this.Activity, itemIVU.form1, ToastLength.Short).Show();
                                new ORM.DBCards().InsertIrregVerbArchive(itemIVU.form1, itemIVU.form2, itemIVU.form3, itemIVU.translation);

                                Toast.MakeText(this.Activity, "Перемещено в архив: " + itemIVU.form1, ToastLength.Short).Show();
                            }

                            new ORM.DBCards().IrrVerbRemoveCard(id);
                        }
                        catch
                        {

                        }
                        NGActivity.cnt();
                        if (NGActivity.cnt() != 0)
                        {
                            StartActivity(new Intent(this.Activity, typeof(neprGlagoliActivity)));
                        }
                        else
                        {
                            StartActivity(new Intent(this.Activity, typeof(NGActivity)));
                        }
                        //DelCardCat1Global = true;
                        //dicListActivity.MixIndicator = false;

                        //Запускаем ViewPagerActivity
                        ///StartActivity(new Intent(this.Activity, typeof(dicListActivity)));

                    }
                });

                //creating dialog
                Android.App.AlertDialog dialog = builderArchive.Create();
                dialog.Show();

            };

            view.FindViewById<ImageButton>(Resource.Id.deleteBn).Click += delegate
                {
                    builder.SetPositiveButton("Удалить", (object sender, DialogClickEventArgs e) =>
                    {
                        NGActivity.mixIndicatorUSER = false;

                        new ORM.DBCards().GetAllRecordsIrregularVerbs();
                    //delete by id from the textView where the Id of the current card was written before
                    int id = Convert.ToInt32(view.FindViewById<TextView>(Resource.Id.item_id).Text);
                        new ORM.DBCards().IrrVerbRemoveCard(id);
                    //count positions
                    NGActivity.cnt();
                        if (NGActivity.cnt() != 0)
                        {
                            StartActivity(new Intent(this.Activity, typeof(neprGlagoliActivity)));
                        }
                        else
                        {
                            StartActivity(new Intent(this.Activity, typeof(NGActivity)));
                        }
                    });
                //Creating dialog
                Android.App.AlertDialog dialog = builder.Create();
                    dialog.Show();
                };

            return view;
        }


        private void NeprPagerFragment_Click(object sender, EventArgs e)
        {
            neprGlagoliActivityRus.startPosition = this.position;
            StartActivity(new Intent(this.Activity, typeof(neprGlagoliActivityRus)));
        }

        //Сode to fill our arrayLists
        private void fillingArrayLists()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<ORM.IrrVerbsUsers>();

            IdArrList.Clear();
            form1AL.Clear();
            form2AL.Clear();
            form3AL.Clear();

            //!!!IMPORTANT  .AsEnumerable().Reverse() is for sorting cards at the end of the list
            foreach (var item in table.AsEnumerable().Reverse())
            {
                //adding IDs
                IdArrList.Add(Convert.ToString(item.Id));

                if (item.form1 != null)
                {
                    form1AL.Add(item.form1);
                }
                if (item.form2 != null)
                {
                    form2AL.Add(item.form2);
                }
                if (item.form3 != null)
                {
                    form3AL.Add(item.form3);
                }
            }
        }
    }
}