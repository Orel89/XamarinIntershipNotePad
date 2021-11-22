using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapNotepad.Controls
{
    public class CustomLikeButton : ImageButton
    {
        public static readonly BindableProperty IsVaforiteProperty = BindableProperty.Create(
            propertyName: nameof(IsFavorite),
            returnType: typeof(bool),
            declaringType: typeof(CustomLikeButton),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay);
        public bool IsFavorite
        {
            set => SetValue(IsVaforiteProperty, value);
            get => (bool)GetValue(IsVaforiteProperty);
        }
    }
}
