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

using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Content.PM;

namespace dictionary
{
    [Activity(Label = "ViewPagerActivityRus", Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ViewPagerActivityRus : Activity
    {
        //for PagerAdapter
        private PagerAdapterRus pagerAdapterRus;
        //THIS IS THE STARTUP POSITION:
        public static int startPosition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FragmentLayoutRus);

            //for pagerAdapter
            pagerAdapterRus = new PagerAdapterRus(this.FragmentManager);
            var pagerRus = FindViewById<ViewPager>(Resource.Id.pagerRus);
            pagerRus.Adapter = pagerAdapterRus;

            //DON`T DELETE THIS!!!!!!!!!!
            pagerRus.SetCurrentItem(startPosition, true);
        }

        //THE BACK BUTTON CODE MUST BE PLACED IN THIS ACTIVITY
        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(dicListActivity));
            StartActivity(intent);
        }
    }
}