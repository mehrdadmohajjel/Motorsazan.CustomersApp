using Motorsazan.CustomersApp.Extensions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Views.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialUIEntry : ContentView
    {
        public MaterialUIEntry()
        {
            InitializeComponent();
            EntryElement.PlaceholderColor = PlaceholderColor;
        }


        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialUIEntry), null);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }


        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialUIEntry), null);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }


        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialUIEntry), Color.Gray);

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }


        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialUIEntry), Color.Gray);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }


        public static readonly BindableProperty FocusedBorderColorProperty =
            BindableProperty.Create(nameof(FocusedBorderColor), typeof(Color), typeof(MaterialUIEntry), BorderColorProperty.DefaultValue);

        public Color FocusedBorderColor
        {
            get => !IsSet(FocusedBorderColorProperty)
                    ? (Color)GetValue(BorderColorProperty)
                    : (Color)GetValue(FocusedBorderColorProperty);

            set => SetValue(FocusedBorderColorProperty, value);
        }


        public static readonly BindableProperty AnimationSpeedProperty =
            BindableProperty.Create(nameof(AnimationDuration), typeof(uint), typeof(MaterialUIEntry), (uint)100);

        public uint AnimationDuration
        {
            get => (uint)GetValue(AnimationSpeedProperty);
            set => SetValue(AnimationSpeedProperty, value);
        }


        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(MaterialUIEntry), null);

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set
            {
                SetValue(ImageProperty, value);
                ImageElement.Source = value;

                var rightMargin = value != null ? 50d : 10d;

                PlaceHolderLabelElement.Margin = new Thickness
                {
                    Left = PlaceHolderLabelElement.Margin.Left,
                    Top = PlaceHolderLabelElement.Margin.Top,
                    Right = rightMargin,
                    Bottom = PlaceHolderLabelElement.Margin.Bottom
                };

                EntryElement.Margin = new Thickness
                {
                    Left = EntryElement.Margin.Left,
                    Top = EntryElement.Margin.Top,
                    Right = rightMargin,
                    Bottom = EntryElement.Margin.Bottom
                };
            }
        }

        public static readonly BindableProperty IsPlaceHolderIntractiveProperty =
            BindableProperty.Create(nameof(IsPlaceHolderIntractive), typeof(bool), typeof(MaterialUIEntry), true);

        public bool IsPlaceHolderIntractive
        {
            get => (bool)GetValue(IsPlaceHolderIntractiveProperty);
            set => SetValue(IsPlaceHolderIntractiveProperty, value);
        }


        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialUIEntry), Keyboard.Default);

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }


        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialUIEntry), false);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        async void EntryElement_Focused(object sender, FocusEventArgs e)
        {
            if (IsPlaceHolderIntractive && !string.IsNullOrWhiteSpace(Placeholder))
            {
                PlaceHolderLabelElement.IsVisible = true;
                await TranslateLabelToTitle();
            }
            else
            {
                PlaceHolderLabelElement.IsVisible = false;
            }

            FrameElement.BorderColor = FocusedBorderColor;
        }

        async void EntryElement_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                PlaceHolderLabelElement.IsVisible = true;
            }

            if (IsPlaceHolderIntractive && !string.IsNullOrWhiteSpace(Placeholder))
            {
                await TranslateLabelToPlaceHolder();
            }

            FrameElement.BorderColor = BorderColor;
        }

        async Task TranslateLabelToTitle()
        {
            if (string.IsNullOrEmpty(Text))
            {
                var placeHolder = PlaceHolderLabelElement;

                if (Device.RuntimePlatform == Device.iOS)
                {
                    await placeHolder.TranslateTo(0, -placeHolder.Height, AnimationDuration);
                    return;
                }

                await placeHolder.TranslateTo(0, -(placeHolder.Height + 5), AnimationDuration);
            }
        }

        async Task TranslateLabelToPlaceHolder()
        {
            if (string.IsNullOrEmpty(Text))
            {
                await PlaceHolderLabelElement.TranslateTo(0, 0, AnimationDuration);
            }
        }

        void ImageElement_Tapped(object sender, EventArgs e)
        {
            EntryElement.Focus();
        }

        void PasswordShowHideImageElement_Tapped(object sender, EventArgs e)
        {
            EntryElement.Focus();

            if (IsPassword)
            {
                IsPassword = false;
                PasswordShowHideImageElement.Source = "Motorsazan.CustomersApp.Resources.Images.ClosedEye.png".ToImageSource();
                PasswordShowHideImageElement.IsVisible = true;
                return;
            }

            IsPassword = true;
            PasswordShowHideImageElement.Source = "Motorsazan.CustomersApp.Resources.Images.Eye.png".ToImageSource();
        }
    }
}