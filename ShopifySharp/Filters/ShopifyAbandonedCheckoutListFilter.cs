﻿using Newtonsoft.Json;

namespace ShopifySharp.Filters
{
    public class ShopifyAbandonedCheckoutListFilter : ShopifyListFilter
    {
        [JsonProperty("status")]
        public string Status { get; set; } = "open";

        // Valid Values:
        // open - All open abandoned checkouts (default)
        // closed - Only closed abandoned checkouts
    }
}
