using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MapNotepad.Controls;
using MapNotepad.Droid.ControlsAndroid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRendererAndroid))]
namespace MapNotepad.Droid.ControlsAndroid
{
    public class CustomEntryRendererAndroid : EntryRenderer 
    {
        public CustomEntryRendererAndroid(Context context): base(context)
        {
          
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                
            }
        }
    }
}