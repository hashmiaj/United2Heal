using System;
using United2Heal.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(United2Heal.iOS.Implementations.FontAwesome))]
namespace United2Heal.iOS.Implementations
{
    /// <summary>
    /// FontAwesome Implementation Class iOS
    /// </summary>
    public class FontAwesome : IFontAwesome
    {
        /// <summary>
        /// Gets the solid font family.
        /// </summary>
        /// <returns>The solid font family.</returns>
        public string GetSolidFontFamily()
        {
            return "Font Awesome 5 Solid";
        }

        public string GetRegularFontFamily()
        {
            return "Font Awesome 5 Regular";
        }
    }
}
