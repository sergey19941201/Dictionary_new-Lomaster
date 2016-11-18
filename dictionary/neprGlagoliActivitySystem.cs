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
    [Activity(Label = "neprGlagoliActivitySystem", Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class neprGlagoliActivitySystem : Activity
    {
        private neprPagerAdapterSystem neprPagerAdapterSystem;
        //THIS IS THE STARTUP POSITION:
        public static int startPosition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.neprGlagoliSystem);

            neprPagerAdapterSystem = new neprPagerAdapterSystem(this.FragmentManager);
            var pager = FindViewById<ViewPager>(Resource.Id.pager);
            pager.Adapter = neprPagerAdapterSystem;

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