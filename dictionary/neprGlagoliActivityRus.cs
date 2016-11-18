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
using Android.Support.V4.App;
using Android.Support.V4.View;

namespace dictionary
{
    [Activity(Label = "neprGlagoliActivityRus", Theme = "@android:style/Theme.Black.NoTitleBar", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class neprGlagoliActivityRus : Activity
    {
        private neprPagerAdapterRus neprPagerAdapterRus;
        //THIS IS THE STARTUP POSITION:
        public static int startPosition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.neprGlagoliRus);

            neprPagerAdapterRus = new neprPagerAdapterRus(this.FragmentManager);
            var pagerRus = FindViewById<ViewPager>(Resource.Id.pagerRus);
            pagerRus.Adapter = neprPagerAdapterRus;

            //DON`T DELETE THIS!!!!!!!!!!
            pagerRus.SetCurrentItem(startPosition, true);
        }

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(NGActivity));
            StartActivity(intent);
        }
    }
}