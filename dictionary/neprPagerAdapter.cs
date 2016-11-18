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

namespace dictionary
{
    public class neprPagerAdapter : Android.Support.V13.App.FragmentStatePagerAdapter
    {
        public neprPagerAdapter(FragmentManager fm) : base(fm)
        {

        }
        public override Fragment GetItem(int position)
        {
            return new neprPagerFragment(position);
        }
        public override int Count
        {
            get
            {
                return NGActivity.CountCards;
            }
        }
    }
}