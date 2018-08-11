using Newtonsoft.Json.Linq;
using System.Net.Http;
using ShopifySharp.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp.Infrastructure;

namespace ShopifySharp
{
    /// <summary>
    /// A service for manipulating Shopify refunds.
    /// </summary>
    public class RefundService : ShopifyService
    {
        /// <summary>
        /// Creates a new instance of <see cref="RefundService" />.
        /// </summary>
        /// <param name="myShopifyUrl">The shop's *.myshopify.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public RefundService(string myShopifyUrl, string shopAccessToken) : base(myShopifyUrl, shopAccessToken) { }

        /// <summary>
        /// Gets a list of up to 250 of the shop's refunds.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Refund>> ListAsync(long orderId)
        {
            var req = PrepareRequest($"orders/{orderId}/refunds.json");

            return await ExecuteRequestAsync<List<Refund>>(req, HttpMethod.Get, rootElement: "refunds");
        }

        /// <summary>
        /// Retrieves the <see cref="Refund"/> with the given id.
        /// </summary>
        /// <param name="orderId">The id of the order for which the refund is to be retrieved.</param>
        /// <param name="refundId">The id of the refund to retrieve.</param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Refund"/>.</returns>
        public virtual async Task<Refund> GetAsync(long orderId, long refundId, string fields = null)
        {
            var req = PrepareRequest($"orders/{orderId}/refunds/{refundId}.json");

            if (string.IsNullOrEmpty(fields) == false)
            {
                req.QueryParams.Add("fields", fields);
            }

            return await ExecuteRequestAsync<Refund>(req, HttpMethod.Get, rootElement: "refund");
        }

        /// <summary>
        /// Creates a new <see cref="Refund"/> on the store.
        /// </summary>
        /// <param name="orderId">The id of the order for which the refund is being created.</param>
        /// <param name="refund">A new <see cref="Refund"/>. Id should be set to null.</param>
        /// <returns>The new <see cref="Refund"/>.</returns>
        public virtual async Task<Refund> CreateAsync(int orderId, Refund refund)
        {
            var req = PrepareRequest($"orders/{orderId}/refunds.json");
            var body = refund.ToDictionary();

            var content = new JsonContent(new
            {
                refund = body
            });

            return await ExecuteRequestAsync<Refund>(req, HttpMethod.Post, content, "refund");
        }
    }
}
