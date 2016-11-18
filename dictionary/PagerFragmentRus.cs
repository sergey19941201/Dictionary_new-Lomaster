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
    

    public class PagerFragmentRus : Fragment
    {
        public static int POS_Global;
        public static int thisPosRusGlobal;

        //ArrayList for the russian cards. WE NEED ARRAYLIST TO COLLECT ELEMENTS, THAT ARE NOT NULL
        public static ArrayList RusArrList = new ArrayList();

        

        int positionRus;
        public PagerFragmentRus(int positionRus)
        {
            this.positionRus = positionRus;
            thisPosRusGlobal = positionRus;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment, container, false);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = string.Format("" + positionRus);

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var db = new SQLiteConnection(dbPath);
            ORM.DBCards CardsDB = new ORM.DBCards();
            //CREATING TABLE
            CardsDB.CreateTablecardsInArchive();

            //��������� ��������������
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this.Activity);
            builder.SetTitle("��������������!");
            builder.SetMessage("������� �����?");
            //���� ��������� false, �� ��� ���� �� ����� ����������� ���� �������� ��� ���
            builder.SetCancelable(true);
            builder.SetNegativeButton("���", (object sender, DialogClickEventArgs e) => { });

            //��������� �������������� for archive
            Android.App.AlertDialog.Builder builderArchive = new Android.App.AlertDialog.Builder(this.Activity);
            builderArchive.SetTitle("��������������!");
            builderArchive.SetMessage("����������� ����� � �����?");
            //���� ��������� false, �� ��� ���� �� ����� ����������� ���� �������� ��� ���
            builderArchive.SetCancelable(true);
            builderArchive.SetNegativeButton("���", (object sender, DialogClickEventArgs e) => { });

            var intent = new Intent(this.Activity, typeof(ViewPagerActivity));

            if (dicListActivity.MixIndicator == false)
            {
                GetAllRecordsCategoryCardsRUS1();
            }

            //if (dicListActivity.MixIndicator == false)
            //{
                foreach (string i in RusArrList)
                {
                    if (this.positionRus == RusArrList.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.TextView).Text = i;
                    }
                }
            //}

            //ImageButton to turn cards
            view.FindViewById<ImageButton>(Resource.Id.perevernBn).Click += delegate
            {
                ViewPagerActivity.startPosition = this.positionRus;
                StartActivity(intent);
            };

            //deleteBn
            view.FindViewById<ImageButton>(Resource.Id.deleteBn).Click += delegate
            {
                builder.SetPositiveButton("�������", (object sender, DialogClickEventArgs e) =>
                {
                    GetAllRecordsCategoryCardsRUS1();
                    CardsDB.RemoveCard1(Convert.ToInt32(CardsDB.FindRUS_Id1(view.FindViewById<TextView>(Resource.Id.TextView).Text)));
                    Toast.MakeText(this.Activity, "������� �����: "+ view.FindViewById<TextView>(Resource.Id.TextView).Text, ToastLength.Short).Show();
                    PagerFragment.DelCardCat1Global = true;
                    dicListActivity.MixIndicator = false;
                    //��������� ViewPagerActivity
                    var intentT = new Intent(this.Activity, typeof(dicListActivity));
                    StartActivity(intentT);
                });
                //������ ��������
                Android.App.AlertDialog dialog = builder.Create();
                dialog.Show();
            };

            //dobavitVArchivBn
            view.FindViewById<ImageButton>(Resource.Id.dobavitVArchivBn).Click += delegate
            {
                builderArchive.SetPositiveButton("�����������", (object sender, DialogClickEventArgs e) =>
                {
                    new ORM.DBCards().InsertRecordcardsInArchive(dicListActivity.CategoryNameGlob, dicListActivity.ID_of_catGlob, CardsDB.FindENG_name1(Convert.ToInt32(CardsDB.FindRUS_Id1(view.FindViewById<TextView>(Resource.Id.TextView).Text))), view.FindViewById<TextView>(Resource.Id.TextView).Text);
                    //GetAllRecordsCategoryCardsRUS1();
                    CardsDB.RemoveCard1(Convert.ToInt32(CardsDB.FindRUS_Id1(view.FindViewById<TextView>(Resource.Id.TextView).Text)));
                    Toast.MakeText(this.Activity, "���������� � �����: " + view.FindViewById<TextView>(Resource.Id.TextView).Text, ToastLength.Short).Show();
                    PagerFragment.DelCardCat1Global = true;
                    dicListActivity.MixIndicator = false;

                    //��������� ViewPagerActivity
                    StartActivity(new Intent(this.Activity, typeof(dicListActivity)));
                });
                //������ ��������
                Android.App.AlertDialog dialog = builderArchive.Create();
                dialog.Show();
            };

            return view;
        }

        private object getBaseContext()
        {
            throw new NotImplementedException();
        }

        //�ode to retrieve all the RUS records
        public string GetAllRecordsCategoryCardsRUS1()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retrieving the data using ORM...";
            var table = db.Table<ORM.Category1Cards>();

            RusArrList.Clear();

            foreach (var i in PagerFragment.AllDataListWords)
            {
                RusArrList.Add(i.rusWORD);
            }
            return output;
        }
    }
}
