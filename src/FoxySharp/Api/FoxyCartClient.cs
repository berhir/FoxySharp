using HoneyBear.HalClient;
using HoneyBear.HalClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace FoxySharp.Api
{
    /// <summary>
    /// Client to access the FoxyCart API.
    /// The FoxyCart API uses HAL (Hypertext Application Language): http://stateless.co/hal_specification.html
    /// </summary>
    public class FoxyCartClient
    {
        private const string FOXY_CART_CURIE = "fx";

        private readonly string _baseUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _refreshToken;
        private string _accessToken;
        private DateTime _accessTokenExpires;

        public FoxyCartClient(string baseUrl, string clientId, string clientSecret, string refreshToken)
        {
            _baseUrl = baseUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _refreshToken = refreshToken;
            _accessToken = null;
            _accessTokenExpires = DateTime.MinValue;
        }

        private void RefreshAccessTokenIfNeeded()
        {
            if (!string.IsNullOrEmpty(_accessToken) && _accessTokenExpires > DateTime.Now)
            {
                return;
            }

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri( _baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/hal+json"));
            httpClient.DefaultRequestHeaders.Add("FOXY-API-VERSION", "1");

            var byteArray = System.Text.Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var parameters = new Dictionary<string, string>()
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", _refreshToken },
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
            };

            var content = new FormUrlEncodedContent(parameters);

            var responseTask = httpClient.PostAsync("/token", content);
            responseTask.Wait();
            var tokenTask = responseTask.Result.Content.ReadAsAsync<FoxyAccessToken>(new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() });
            tokenTask.Wait();
            var token = tokenTask.Result;

            _accessToken = token.AccessToken;
            // set date time when token expires. remove 5 minutes to make sure the token gets refresed soon enough.
            _accessTokenExpires = DateTime.Now.AddSeconds(token.ExpiresIn).AddMinutes(-5);
        }

        private IHalClient GetClient()
        {
            RefreshAccessTokenIfNeeded();

            var halClient = new HalClient();
            halClient.HttpClient.BaseAddress = new Uri(_baseUrl);
            halClient.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
            halClient.HttpClient.DefaultRequestHeaders.Add("FOXY-API-VERSION", "1");
            return halClient;
        }

        public ICollection<FoxyCustomer> GetCustomers()
        {
            var halClient = GetClient();

            return halClient
                .Root()
                .Get("store", FOXY_CART_CURIE)
                .Get("customers", FOXY_CART_CURIE)
                .Get("customers", FOXY_CART_CURIE)
                .Items<FoxyCustomer>()
                .Data()
                .ToList();
        }

        public FoxyCustomer GetCustomer(int id)
        {
            var halClient = GetClient();

            var customerResource = halClient
                .Root($"{_baseUrl}/customers/{id}?zoom=default_billing_address,default_shipping_address")
                .Item<FoxyCustomer>();

            var customer = customerResource.Data;

            customer.DefaultBillingAddress = halClient
                .Get(customerResource, "default_billing_address", FOXY_CART_CURIE)
                .Item<FoxyCustomerAddress>()
                .Data;

            customer.DefaultShippingAddress = halClient
                .Get(customerResource, "default_shipping_address", FOXY_CART_CURIE)
                .Item<FoxyCustomerAddress>()
                .Data;

            return customer;
        }

        public FoxyCustomer CreateCustomer(FoxyCustomer customer)
        {
            var halClient = GetClient();

            return halClient
                .Root()
                .Get("store", FOXY_CART_CURIE)
                .Post("customers", customer, null, FOXY_CART_CURIE)
                .Get("self")
                .Item<FoxyCustomer>()
                .Data;
        }

        public FoxyCustomer UpdateCustomer(FoxyCustomer customer)
        {
            var halClient = GetClient();

            var customerResource = halClient
             .Root($"{_baseUrl}/customers/{customer.Id}")
             .Put("self", customer)
             .Item<FoxyCustomer>();

            var updatedCustomer = customerResource.Data;

            if (customer.DefaultBillingAddress != null)
            {
                updatedCustomer.DefaultBillingAddress = halClient
                    .Get(customerResource, "default_billing_address", FOXY_CART_CURIE)
                    .Put("self", customer.DefaultBillingAddress)
                    .Item<FoxyCustomerAddress>()
                    .Data;
            }

            if (customer.DefaultShippingAddress != null)
            {
                updatedCustomer.DefaultShippingAddress = halClient
                    .Get(customerResource, "default_shipping_address", FOXY_CART_CURIE)
                    .Put("self", customer.DefaultShippingAddress)
                    .Item<FoxyCustomerAddress>()
                    .Data;
            }

            return updatedCustomer;
        }

    }
}
