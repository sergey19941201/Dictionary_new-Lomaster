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
    class addNewIrrVerb : DialogFragment
    {
        ORM.DBCards CardsDB = new ORM.DBCards();
        private EditText eng1_ET, eng2_ET, eng3_ET;
        private EditText rusEdText;
        private Button dobavitBn, zakritFragmentbn;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.AddNewIrrVerb, container, false);

            NGActivity.mixIndicatorUSER = false;

            //Заглавие фрагмента:
            //this.Dialog.SetTitle("Добавление карты");
            //Кнопки переименовать и Закрыть фрагмент
            dobavitBn = rootView.FindViewById<Button>(Resource.Id.dobavitBn);
            zakritFragmentbn = rootView.FindViewById<Button>(Resource.Id.zakritFragmentBn);
            //ЕдитТекст
            eng1_ET = rootView.FindViewById<EditText>(Resource.Id.eng1_ET);
            eng2_ET = rootView.FindViewById<EditText>(Resource.Id.eng2_ET);
            eng3_ET = rootView.FindViewById<EditText>(Resource.Id.eng3_ET);
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
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<ORM.IrrVerbsUsers>();

            if (String.IsNullOrEmpty(eng1_ET.Text) || String.IsNullOrEmpty(eng2_ET.Text) || String.IsNullOrEmpty(eng3_ET.Text) || String.IsNullOrEmpty(rusEdText.Text))
            {
                Toast.MakeText(this.Activity, "Заполните все поля", ToastLength.Short).Show();
            }
            else
            {
                CardsDB.CreateTableIrrVerbsUSERS();
                CardsDB.InsertIrregVerb(eng1_ET.Text, eng2_ET.Text, eng3_ET.Text, rusEdText.Text);
                Toast.MakeText(this.Activity, "Карта добавлена", ToastLength.Short).Show();

                //Clearing EditTexts:
                eng1_ET.Text = null;
                eng2_ET.Text = null;
                eng3_ET.Text = null;
                rusEdText.Text = null;

                Dismiss();
            }
        }
    }
}