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

namespace dictionary.mCode
{
    class ShowFragment : DialogFragment
    {
        //private EditText;
        private EditText editText;
        //private Button dobavitCateg;
        private Button dobavitCateg, zakritFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.FragLayout, container, false);

            //Заглавие фрагмента:
            this.Dialog.SetTitle("Введите название категории");
            //Кнопки Добавить и Закрыть фрагмент
            dobavitCateg = rootView.FindViewById<Button>(Resource.Id.dobavitCateg);
            zakritFragment = rootView.FindViewById<Button>(Resource.Id.zakritFragment);
            //ЕдитТекст
            editText = rootView.FindViewById<EditText>(Resource.Id.editText);
            //Кнопки Добавить и Закрыть фрагмент
            zakritFragment.Click += Zakrit;
            dobavitCateg.Click += DobavitCateg;

            return rootView;
        }

        //Закрыть фрагмент. Кнопка Отмена
        void Zakrit(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        void DobavitCateg(object sender, EventArgs e)
        {
            if (editText.Text != "")
            {
                ORM.DBRepository dbr = new ORM.DBRepository();
                string result = dbr.InsertRecord((editText.Text).ToLower());
                Toast.MakeText(this.Activity, "Папка добавлена", ToastLength.Short).Show();

                //Запустить dicActivity:
                this.Activity.StartActivity(new Intent(this.Activity, typeof(dicListActivity)));
                //Закрыть фрагмент:
                this.Dismiss();
            }
            else
            {
                Toast.MakeText(this.Activity, "Введите название папки или отмените ввод", ToastLength.Long).Show();
            }
        }
    }
}