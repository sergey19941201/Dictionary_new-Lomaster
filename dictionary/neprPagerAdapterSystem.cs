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
    public class neprPagerAdapterSystem : Android.Support.V13.App.FragmentStatePagerAdapter
    {
        public neprPagerAdapterSystem(FragmentManager fm) : base(fm)
        {

        }
        public override Fragment GetItem(int position)
        {
            return new neprPagerFragmentSystem(position);
        }
        public override int Count
        {
            get
            {
                return 74;
            }
        }
    }
}