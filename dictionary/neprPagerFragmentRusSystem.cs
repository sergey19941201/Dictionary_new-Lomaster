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
    class neprPagerFragmentRusSystem : Fragment
    {
        int position;
        public neprPagerFragmentRusSystem(int position)
        {
            this.position = position;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.neprFragmentRusSystem, container, false);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = string.Format("Position {0}", position);

            ////////////////////////////////////
            //“”“ —¬Œ… Ã» —»Õƒ» ¿“Œ– —ƒ≈À¿“‹. œ≈–≈ƒ≈À¿“‹.
            dicListActivity.MixIndicator = false;
            if (dicListActivity.MixIndicator == false)
            {
                foreach (string i in MainActivity.translationAL)
                {
                    if (this.position == MainActivity.translationAL.IndexOf(i))
                    {
                        view.FindViewById<TextView>(Resource.Id.translTV).Text = i;
                    }
                }
            }
            else
            {
                ///“”“ —Œ–“»–Œ¬ ¿ ¡”ƒ≈“
            }
            /////////////////////////////////////

            view.FindViewById<ImageButton>(Resource.Id.perevernBn).Click += NeprPagerFragmentRusSystem_Click;

            return view;
        }

        private void NeprPagerFragmentRusSystem_Click(object sender, EventArgs e)
        {
            neprGlagoliActivitySystem.startPosition = this.position;
            StartActivity(new Intent(this.Activity, typeof(neprGlagoliActivitySystem)));
        }
    }
}