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

namespace dictionary
{
    class neprPagerFragmentSystem : Fragment
    {
        int position;
        public neprPagerFragmentSystem(int position)
        {
            this.position = position;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.neprFragmentSystem, container, false);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = string.Format("Position {0}", position);

            ////////////////////////////////////
            //“”“ —¬Œ… Ã» —»Õƒ» ¿“Œ– —ƒ≈À¿“‹. œ≈–≈ƒ≈À¿“‹.
            dicListActivity.MixIndicator = false;
            if (dicListActivity.MixIndicator == false)
            {
                foreach (string i in MainActivity.form1AL)
                {
                    if (this.position == MainActivity.form1AL.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.form1Text).Text = i;
                    }
                }

                foreach (string i in MainActivity.form2AL)
                {
                    if (this.position == MainActivity.form2AL.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.form2Text).Text = i;
                    }
                }

                foreach (string i in MainActivity.form3AL)
                {
                    if (this.position == MainActivity.form3AL.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.form3Text).Text = i;
                    }
                }
            }
            else
            {
                ///“”“ —Œ–“»–Œ¬ ¿ ¡”ƒ≈“
            }
            /////////////////////////////////////

            view.FindViewById<ImageButton>(Resource.Id.perevernBn).Click += NeprPagerFragmentSystem_Click;

            return view;
        }

        private void NeprPagerFragmentSystem_Click(object sender, EventArgs e)
        {
            neprGlagoliActivityRusSystem.startPosition = this.position;
            StartActivity(new Intent(this.Activity, typeof(neprGlagoliActivityRusSystem)));
        }
    }
}