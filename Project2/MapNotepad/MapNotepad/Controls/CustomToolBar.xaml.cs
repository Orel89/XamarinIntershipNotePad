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
    public partial class CustomToolBar : ContentView
    {
        public static readonly BindableProperty BackButtonTapCommandProperty =
           BindableProperty.Create(nameof(BackButtonTapCommand), typeof(ICommand), typeof(CustomToolBar));
        public ICommand BackButtonTapCommand
        {
            set
            {
                SetValue(BackButtonTapCommandProperty, value);
            }
            get
            {
                return (ICommand)GetValue(BackButtonTapCommandProperty);
            }
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(CustomToolBar), string.Empty);
        public string Title
        {
            set
            {
                SetValue(TitleProperty, value);
            }
            get
            {
                return (string)GetValue(TitleProperty);
            }
        }

        public static readonly BindableProperty TitleColorProperty =
            BindableProperty.Create("TitleColor", typeof(Color), typeof(CustomToolBar), Color.Black);
        public Color TitleColor
        {
            set
            {
                SetValue(TitleColorProperty, value);
            }
            get
            {
                return (Color)GetValue(TitleColorProperty);
            }
        }

        public static readonly BindableProperty ArrowLeftImageProperty =
          BindableProperty.Create("ArrowLeftImage", typeof(string), typeof(CustomToolBar), string.Empty);
        public string ArrowLeftImage
        {
            set
            {
                SetValue(ArrowLeftImageProperty, value);
            }
            get
            {
                return (string)GetValue(ArrowLeftImageProperty);
            }
        }

        public static readonly BindableProperty RightImageProperty = BindableProperty.Create(
         propertyName: nameof(RightImage),
         returnType: typeof(string),
         declaringType: typeof(CustomToolBar),
         defaultBindingMode: BindingMode.TwoWay);
        public string RightImage
        {
            set => SetValue(RightImageProperty, value);
            get => (string)GetValue(RightImageProperty);
        }

        public static readonly BindableProperty RightButtonCommandProperty = BindableProperty.Create(
          propertyName: nameof(RightButtonCommand),
          returnType: typeof(ICommand),
          declaringType: typeof(CustomToolBar),
          defaultBindingMode: BindingMode.TwoWay);
        public ICommand RightButtonCommand
        {
            set => SetValue(RightButtonCommandProperty, value);
            get => (ICommand)GetValue(RightButtonCommandProperty);
        }

        public static readonly BindableProperty IsEnableRightButtonProperty = BindableProperty.Create(
         propertyName: nameof(IsEnableRightButton),
         returnType: typeof(bool),
         declaringType: typeof(CustomToolBar),
         defaultValue: true,
         defaultBindingMode: BindingMode.TwoWay);
        public bool IsEnableRightButton
        {
            set => SetValue(IsEnableRightButtonProperty, value);
            get => (bool)GetValue(IsEnableRightButtonProperty);
        }

        public CustomToolBar()
        {
            InitializeComponent();
        }
    }
}