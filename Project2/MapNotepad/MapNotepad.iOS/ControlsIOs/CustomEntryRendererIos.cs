using Foundation;
using MapNotepad.Controls;
using MapNotepad.iOS.ControlsIOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRendererIos))]
namespace MapNotepad.iOS.ControlsIOs
{
    public class CustomEntryRendererIos : EntryRenderer 
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}