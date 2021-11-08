using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
          defaultValue: string.Empty,
          defaultBindingMode: BindingMode.TwoWay);
        public string Placeholder
        {
            set => SetValue(PlaceholderProperty, value);
            get => (string)GetValue(PlaceholderProperty);
        }
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            propertyName: nameof(PlaceholderColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry),
            defaultValue: Color.Silver,
            defaultBindingMode: BindingMode.TwoWay);
        public Color PlaceholderColor
        {
            set => SetValue(PlaceholderColorProperty, value);
            get => (Color)GetValue(PlaceholderColorProperty);
        }

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            propertyName: nameof(ImageSource),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);
        public string ImageSource
        {
            set => SetValue(ImageSourceProperty, value);
            get => (string)GetValue(ImageSourceProperty);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
          propertyName: nameof(Text),
          returnType: typeof(string),
          declaringType: typeof(CustomEntry),
          defaultValue: string.Empty,
          defaultBindingMode: BindingMode.TwoWay);
        public string Text
        {
            set => SetValue(TextProperty, value);
            get => (string)GetValue(TextProperty);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
           propertyName: nameof(TextColor),
           returnType: typeof(Color),
           declaringType: typeof(CustomEntry),
           defaultValue: Color.Silver,
           defaultBindingMode: BindingMode.TwoWay);
        public Color TextColor
        {
            set => SetValue(TextColorProperty, value);
            get => (Color)GetValue(TextColorProperty);
        }

        public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(
          propertyName: nameof(ButtonCommand),
          returnType: typeof(ICommand),
          declaringType: typeof(CustomEntry),
          defaultValue: null,
          defaultBindingMode: BindingMode.TwoWay);
        public ICommand ButtonCommand
        {
            set => SetValue(ButtonCommandProperty, value);
            get => (ICommand)GetValue(ButtonCommandProperty);
        }

        public static readonly BindableProperty IsVisibleButtonProperty = BindableProperty.Create(
         propertyName: nameof(IsVisibleButton),
         returnType: typeof(bool),
         declaringType: typeof(CustomEntry),
         defaultValue: true,
         defaultBindingMode: BindingMode.TwoWay);
        public bool IsVisibleButton
        {
            set => SetValue(IsVisibleButtonProperty, value);
            get => (bool)GetValue(IsVisibleButtonProperty);
        }

        #endregion

        public CustomEntry()
        {
            InitializeComponent();
        }
    }
}