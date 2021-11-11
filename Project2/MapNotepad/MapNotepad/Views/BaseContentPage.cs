using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MapNotepad.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            object p = On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

    }
}
