﻿using MapNotepad.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MapNotepad.Behaviours
{
    class CustomEntryValidationBehaviour : Behavior<CustomEntry>
    {
        
        private CustomEntry _entry;

        public static readonly BindableProperty RegexProperty = BindableProperty.Create(
            propertyName: nameof(Regex),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryValidationBehaviour),
            defaultBindingMode: BindingMode.TwoWay);

        public string Regex
        {
            get => (string)GetValue(RegexProperty);
            set => SetValue(RegexProperty, value);
        }


        protected override void OnAttachedTo(CustomEntry entry)
        {
            base.OnAttachedTo(entry);
            
            _entry = entry;
            _entry.PropertyChanged += EntryPropertyChanged;
        }


        protected override void OnDetachingFrom(CustomEntry entry)
        {
            base.OnDetachingFrom(entry);

            _entry.PropertyChanged -= EntryPropertyChanged;
            _entry = null;
        }

        private void EntryPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CustomEntry.TextProperty.PropertyName && _entry.Text != null && Regex != null)
            {
                (_entry.BorderColor, _entry.IsMessageVisible) = IsTextValid(_entry.Text) ? (Color.FromHex("#D7DDE8"), false) : (Color.FromHex("#ff0000"), true);
            }
        }

        private bool IsTextValid(string text) => System.Text.RegularExpressions.Regex.IsMatch(text, Regex);
    }
}