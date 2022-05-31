﻿using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Styles
{
    /// <summary>
    /// Class helps to reduce repetitive markup, and allows an apps appearance to be more easily changed.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LabelStyles
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Styles" /> class.
        /// </summary>
        public LabelStyles()
        {
            this.InitializeComponent();
        }
    }
}