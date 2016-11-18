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
    [Activity(Label = "neprGlagoliActivityRusSystem", Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class neprGlagoliActivityRusSystem : Activity
    {
        private neprPagerAdapterRusSystem neprPagerAdapterRusSystem;
        //THIS IS THE STARTUP POSITION:
        public static int startPosition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.neprGlagoliRusSystem);

            neprPagerAdapterRusSystem = new neprPagerAdapterRusSystem(this.FragmentManager);
            var pager = FindViewById<ViewPager>(Resource.Id.pagerRus);
            pager.Adapter = neprPagerAdapterRusSystem;

            //DON`T DELETE THIS!!!!!!!!!!
            pager.SetCurrentItem(startPosition, true);
        }

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(NGActivity));
            StartActivity(intent);
        }
    }
}