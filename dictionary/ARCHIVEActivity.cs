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
using System.IO;
using SQLite;

namespace dictionary
{
    [Activity(Label = "ARCHIVEActivity", Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ARCHIVEActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ARCHIVE);

            FindViewById<Button>(Resource.Id.words_archiveBN).Click += ARCHIVEActivity_Click;
            FindViewById<Button>(Resource.Id.IrregularVerbsBN).Click += ARCHIVEActivity_Click1;
        }

        private void ARCHIVEActivity_Click1(object sender, EventArgs e)
        {
            archivActivity.archive_indicator_glob = "irrVerbs";
            StartActivity(new Intent(this, typeof(irrVerbsArchiveActivity)));
        }

        private void ARCHIVEActivity_Click(object sender, EventArgs e)
        {
            archivActivity.archive_indicator_glob = "words";
            StartActivity(new Intent(this, typeof(archivActivity)));
        }

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}