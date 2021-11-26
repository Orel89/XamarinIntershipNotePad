using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MapNotepad.Controls;
using MapNotepad.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NoBorderEntry), typeof(NoBorderEntryRendererAndroid))]
namespace MapNotepad.Droid.Renderers
{
    public class NoBorderEntryRendererAndroid : EntryRenderer 
    {
        public NoBorderEntryRendererAndroid(Context context) : base (context){ }
        
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            Control.Background = null;
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}