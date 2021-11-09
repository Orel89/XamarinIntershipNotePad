using MapNotepad.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapNotepad.Triggers
{
    public class EmailVerificationTrigger : TriggerAction<CustomEntry>
    {
        protected override void Invoke(CustomEntry sender)
        {
            var frame = sender as Frame;
            var email = frame.BorderColor;

        }
    }
}
