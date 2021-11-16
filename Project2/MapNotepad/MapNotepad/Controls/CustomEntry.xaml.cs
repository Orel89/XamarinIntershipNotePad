using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public static readonly BindableProperty LabelProperty =
        BindableProperty.Create("Label", typeof(string), typeof(CustomEntry), string.Empty);
        public string Label
        {
            set
            {
                SetValue(LabelProperty, value);
            }
            get
            {
                return (string)GetValue(LabelProperty);
            }
        }

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

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
          propertyName: nameof(Keyboard),
          returnType: typeof(Keyboard),
          declaringType: typeof(CustomEntry),
          defaultValue: Keyboard.Default,
          defaultBindingMode: BindingMode.TwoWay);

        public Keyboard Keyboard
        {
            set => SetValue(KeyboardProperty, value);
            get => (Keyboard)GetValue(KeyboardProperty);
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
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsVisibleButton
        {
            set => SetValue(IsVisibleButtonProperty, value);
            get => (bool)GetValue(IsVisibleButtonProperty);
        }

        public static readonly BindableProperty IsMessageVisibleProperty = BindableProperty.Create(
            propertyName: nameof(IsMessageVisible),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsMessageVisible
        {
            set => SetValue(IsMessageVisibleProperty, value);
            get => (bool)GetValue(IsMessageVisibleProperty);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            propertyName: nameof(IsPassword),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsPassword
        {
            set => SetValue(IsPasswordProperty, value);
            get => (bool)GetValue(IsPasswordProperty);
        }

        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
            propertyName: nameof(Message),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultBindingMode: BindingMode.TwoWay);

        public string Message
        {
            set => SetValue(MessageProperty, value);
            get => (string)GetValue(MessageProperty);
        }

        #endregion

        public CustomEntry()
        {
            InitializeComponent();
        }
    }
}