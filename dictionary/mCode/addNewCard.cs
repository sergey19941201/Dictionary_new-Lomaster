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
using SQLite;

namespace dictionary.mCode
{
    class addNewCard : DialogFragment
    {
        ORM.DBCards CardsDB = new ORM.DBCards();
        private EditText engEdText;
        private EditText rusEdText;
        private Button dobavitBn, zakritFragmentbn;
        private bool RusTextIsfine= true, EngTextIsfine= true;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.AddCardFragment, container, false);

            //Заглавие фрагмента:
            this.Dialog.SetTitle("Добавление карты");
            //Кнопки переименовать и Закрыть фрагмент
            dobavitBn = rootView.FindViewById<Button>(Resource.Id.dobavitBn);
            zakritFragmentbn = rootView.FindViewById<Button>(Resource.Id.zakritFragmentBn);
            //ЕдитТекст
            engEdText = rootView.FindViewById<EditText>(Resource.Id.engEditText);
            rusEdText = rootView.FindViewById<EditText>(Resource.Id.rusEditText);

            //Кнопки Добавить и Закрыть фрагмент
            dobavitBn.Click += DobavitBn_Click;
            zakritFragmentbn.Click += ZakritFragmentbn_Click;

            return rootView;
        }

        private void ZakritFragmentbn_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void DobavitBn_Click(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<ORM.Category1Cards>();
            //Сode to retrieve all the RUS records
            foreach (var item in table)
            {
                if (item.Rus1c != null)
                    {
                        if (item.Rus1c != rusEdText.Text)
                        {
                            RusTextIsfine = true;
                        }
                        if (item.Rus1c == rusEdText.Text)
                        {
                            RusTextIsfine = false;
                            break;
                        }
                    }
                
            }
            //Сode to retrieve all the ENG records
            foreach (var item in table)
            {
                    if (item.Eng1c != null)
                    {
                        if (item.Eng1c != engEdText.Text)
                        {
                            EngTextIsfine = true;
                        }
                        if (item.Eng1c == engEdText.Text)
                        {
                            EngTextIsfine = false;
                            break;
                        }
                    }
                
            }

            if (RusTextIsfine == true && EngTextIsfine == true)
            {
                if (String.IsNullOrEmpty(engEdText.Text) || String.IsNullOrEmpty(rusEdText.Text))
                {
                    Toast.MakeText(this.Activity, "Заполните все поля", ToastLength.Short).Show();
                }
                else
                {
                    CardsDB.CreateTableCategory1Cards();
                    CardsDB.InsertRecordCategory1Cards(engEdText.Text, rusEdText.Text, dicListActivity.ID_of_catGlob, dicListActivity.CategoryNameGlob);
                    Toast.MakeText(this.Activity, "Карта добавлена", ToastLength.Short).Show();

                    //Clearing EditTexts:
                    engEdText.Text = null;
                    rusEdText.Text = null;

                    Dismiss();
                } 
            }
            else
            {
                Toast.MakeText(this.Activity, "Такое имя уже есть", ToastLength.Short).Show();
            }
        }
    }
}