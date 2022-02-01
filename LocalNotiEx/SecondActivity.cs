using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalNotiEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    class SecondActivity: Activity
    {
        private TextView mytextView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.secondActivityLayout);
            mytextView = FindViewById<TextView>(Resource.Id.textView1);
            if (Intent.Extras != null)
            {
                string message = Intent.Extras.GetString(key: "key", string.Empty);
                if (!string.IsNullOrEmpty(message))
                {
                    mytextView.Text = message;
                }
            }
        }
    }
}