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
    class pereimenovatCategFragmShow : DialogFragment
    {
        //private database
        private ORM.DBRepository dbr = new ORM.DBRepository();
        //private EditText;
        private EditText pereimenovatEditText;
        //private Button dobavitCateg;
        private Button pereimenovatCateg, zakritFragment;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.pereimenCategFragment, container, false);

            //this.Dialog.SetTitle("Введите новое название папки");
            
            pereimenovatCateg = rootView.FindViewById<Button>(Resource.Id.pereimenovatCateg);
            zakritFragment = rootView.FindViewById<Button>(Resource.Id.zakritFragment);
           
            pereimenovatEditText = rootView.FindViewById<EditText>(Resource.Id.pereimenovatEditText);
            //put the name of category to pereimenovatEditText:
            pereimenovatEditText.Text = dbr.GetTaskById(dicListActivity.ID_of_catGlob);

            zakritFragment.Click += Zakrit;
            pereimenovatCateg.Click += PereimenovatCateg;
            
            return rootView;
        }

        void Zakrit(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        void PereimenovatCateg(object sender, EventArgs e)
        {
            if (pereimenovatEditText.Text == String.Empty || pereimenovatEditText.Text == " " || pereimenovatEditText.Text == "  " || pereimenovatEditText.Text == "   ")
            {
                Toast.MakeText(this.Activity, "Введите название или отмените ввод", ToastLength.Short).Show();
            }
            else
            {
                dbr.updateRecord(dicListActivity.ID_of_catGlob, pereimenovatEditText.Text);
                //Start dicListActivity:
                this.Activity.StartActivity(typeof(dicListActivity));
            }
        }
    }
}