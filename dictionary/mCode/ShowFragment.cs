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

            //�������� ���������:
            this.Dialog.SetTitle("������� �������� ���������");
            //������ �������� � ������� ��������
            dobavitCateg = rootView.FindViewById<Button>(Resource.Id.dobavitCateg);
            zakritFragment = rootView.FindViewById<Button>(Resource.Id.zakritFragment);
            //���������
            editText = rootView.FindViewById<EditText>(Resource.Id.editText);
            //������ �������� � ������� ��������
            zakritFragment.Click += Zakrit;
            dobavitCateg.Click += DobavitCateg;

            return rootView;
        }

        //������� ��������. ������ ������
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
                Toast.MakeText(this.Activity, "����� ���������", ToastLength.Short).Show();

                //��������� dicActivity:
                this.Activity.StartActivity(new Intent(this.Activity, typeof(dicListActivity)));
                //������� ��������:
                this.Dismiss();
            }
            else
            {
                Toast.MakeText(this.Activity, "������� �������� ����� ��� �������� ����", ToastLength.Long).Show();
            }
        }
    }
}