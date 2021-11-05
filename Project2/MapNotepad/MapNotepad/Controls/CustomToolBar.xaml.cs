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
        public static readonly BindableProperty ButtonProperty =
           BindableProperty.Create("Button", typeof(ICommand), typeof(CustomToolBar));
        public ICommand Button
        {
            set
            {
                SetValue(ButtonProperty, value);
            }
            get
            {
                return (ICommand)GetValue(ButtonProperty);
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
    
        public CustomToolBar()
        {
            InitializeComponent();
        }
    }
}