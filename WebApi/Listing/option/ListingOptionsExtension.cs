using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi
{

    public static partial class OptionsExtensions
    {
        public static ListingOptions UseAeEngine(this ListingOptions option)
        {
            option.RegisterExtension(new AeListingOptionsExtension(s =>
            {
            }));

            return option;
        }

        public static ListingOptions UseEbayEngine(this ListingOptions option)
        {
            option.RegisterExtension(new EbayListingOptionsExtension(s =>
            {
            }));

            return option;
        }
    }
}
