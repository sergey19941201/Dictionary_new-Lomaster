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
    [Activity(Label = "ViewPagerActivity", Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ViewPagerActivity : Activity
    {
        //for PagerAdapter
        private PagerAdapter pagerAdapter;
        //THIS IS THE STARTUP POSITION:
        public static int startPosition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FragmentLayout);

            //for pagerAdapter
            pagerAdapter = new PagerAdapter(this.FragmentManager);
            var pager = FindViewById<ViewPager>(Resource.Id.pager);
            pager.Adapter = pagerAdapter;

            //DON`T DELETE THIS!!!!!!!!!!
            pager.SetCurrentItem(startPosition, true);
        }

        //THE BACK BUTTON CODE MUST BE PLACED IN THIS ACTIVITY
        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(dicListActivity));
            StartActivity(intent);
        }
    }
}