using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntry : Frame
    {

        #region ---public properties---

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
          propertyName: nameof(Placeholder),
          returnType: typeof(string),
          declaringType: typeof(CustomEntry),
          defaultValue: string.Empty);

        public string Placeholder
        {
            set => SetValue(PlaceholderProperty, value);
            get => (string)GetValue(PlaceholderProperty);
        }
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            propertyName: nameof(PlaceholderColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry),
            defaultValue: Color.Silver);
        public Color PlaceholderColor
        {
            set => SetValue(PlaceholderColorProperty, value);
            get => (Color)GetValue(PlaceholderColorProperty);
        }
        public static readonly BindableProperty SurroundColorProperty = BindableProperty.Create(
            propertyName: nameof(SurroundColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry),
            defaultValue: Color.White);
        public Color SurroundColor
        {
            set => SetValue(SurroundColorProperty, value);
            get => (Color)GetValue(SurroundColorProperty);
        }
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            propertyName: nameof(ImageSource),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty);
        public string ImageSource
        {
            set => SetValue(ImageSourceProperty, value);
            get => (string)GetValue(ImageSourceProperty);
        }
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
          propertyName: nameof(Text),
          returnType: typeof(string),
          declaringType: typeof(CustomEntry),
          defaultValue: string.Empty);
        public string Text
        {
            set => SetValue(TextProperty, value);
            get => (string)GetValue(TextProperty);
        }
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
           propertyName: nameof(TextColor),
           returnType: typeof(Color),
           declaringType: typeof(CustomToolBar),
           defaultValue: Color.Silver);
        public Color TextColor
        {
            set => SetValue(TextColorProperty, value);
            get => (Color)GetValue(TextColorProperty);
        }
        #endregion
        public CustomEntry()
        {
            InitializeComponent();
        }
    }
}