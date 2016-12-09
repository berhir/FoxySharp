using Newtonsoft.Json;
using System;

namespace FoxySharp.Api
{
    public class FoxyCustomerAddress
    {
        /// <summary>
        /// The name of this address. This is also the value used as the shipto entry for a multiship item.
        /// Required. Must be unique per customer. 100 characters or less.
        /// </summary>
        [JsonProperty("address_name")]
        public string AddressName { get; set; }

        /// <summary>
        /// The given name associated with this address.
        /// 50 characters or less.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// The surname associated with this address.
        /// 50 characters or less.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// The company associated with this address.
        /// 50 characters or less.
        /// </summary>
        [JsonProperty("company")]
        public string Company { get; set; }

        /// <summary>
        /// The first line of the street address.
        /// Required. 100 characters or less.
        /// </summary>
        [JsonProperty("address1")]
        public string Address1 { get; set; }

        /// <summary>
        /// The second line of the street address.
        /// 100 characters or less.
        /// </summary>
        [JsonProperty("address2")]
        public string Address2 { get; set; }

        /// <summary>
        /// The city of this address.
        /// 50 characters or less.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// The two character code for states in the United States.
        /// Other countries may call this a province. When a two character code isn't available, use the full region name.
        /// 2 characters if code exists, otherwise 50 characters or less. 
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }

        /// <summary>
        /// The postal code of this address.
        /// 50 characters or less.
        /// </summary>
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The country code of this address.
        /// Two character ISO 3166-1-alpha-2 code.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// The phone of this address.
        /// 50 characters or less.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Specifies if this address is the default billing address for the customer.
        /// Read only.
        /// </summary>
        [JsonProperty("is_default_billing")]
        public bool IsDefaultBilling { get; set; }

        /// <summary>
        /// Specifies if this address is the default shipping address for the customer.
        /// Read only.
        /// </summary>
        [JsonProperty("is_default_shipping")]
        public bool IsDefaultShipping { get; set; }

        /// <summary>
        /// The date this resource was created.
        /// Read only.
        /// </summary>
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date this resource was last modified.
        /// Read only.
        /// </summary>
        [JsonProperty("date_modified")]
        public DateTime DateModified { get; set; }
    }
}
