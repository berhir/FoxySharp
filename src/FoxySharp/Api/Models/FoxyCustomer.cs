using Newtonsoft.Json;
using System;

namespace FoxySharp.Api
{
    public class FoxyCustomer
    {
        /// <summary>
        /// The FoxyCart customer id, useful for Single Sign On integrations.
        /// Read only.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The date of the last time this customer authenticated with the FoxyCart checkout.
        /// Read only.
        /// </summary>
        [JsonProperty("last_login_date")]
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// The customer's given name.
        /// Optional. 50 characters or less.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// The customer's surname.
        /// Optional. 50 characters or less.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// The customer's email address. This is used as the login to the FoxyCart checkout for this customer.
        /// Required. 100 characters or less.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// A tax identification number for this customer.
        /// Optional. 50 characters or less.
        /// </summary>
        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        /// <summary>
        /// Your customer's clear text password.
        /// This value is never stored, not displayed for this resource, and is not available in our system.
        /// You can, however, pass it via clear text when creating or modifying a customer.
        /// 50 characters or less.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// The salt for this customer's login password. If your integration syncs passwords, you will need to keep this value in sync as well.
        /// 100 characters or less.
        /// </summary>
        [JsonProperty("password_salt")]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// The hash of this customer's login password. If your integration syncs passwords, you will need to keep this value in sync as well.
        /// 200 characters or less.
        /// </summary>
        [JsonProperty("password_hash")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// This will be a copy of your store's current password_hash_type at the time of creation or modificaiton.
        /// This way, if you change your store's settings, your customer will still be able to login.
        /// It will be updated automatically to match that of the store the next time the customer logs in.
        /// </summary>
        [JsonProperty("password_hash_type")]
        public string PasswordHashType { get; set; }

        /// <summary>
        /// This will be a copy of your store's current password_hash_config at the time of creation or modification.
        /// This way, if you change your store's settings, your customer will still be able to login.
        /// It will be updated automatically to match that of the store the next time the customer logs in.
        /// </summary>
        [JsonProperty("password_hash_config")]
        public string PasswordHashConfig { get; set; }

        /// <summary>
        /// If your customer forgot their password and requested a forgotten password, it will be set here.
        /// 50 characters or less.
        /// </summary>
        [JsonProperty("forgot_password")]
        public string ForgotPassword { get; set; }

        /// <summary>
        /// The exact time the forgot password was set.
        /// Read only.
        /// </summary>
        [JsonProperty("forgot_password_timestamp")]
        public DateTime? ForgotPasswordTimestamp { get; set; }

        /// <summary>
        /// If this customer checks out as a guest, this will be set to true. Once it is set, it can not be changed.
        /// Read only after creation
        /// </summary>
        [JsonProperty("is_anonymous")]
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// The date this resource was created.
        /// Read only.
        /// </summary>
        [JsonProperty("date_created")]
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// The date this resource was last modified.
        /// Read only.
        /// </summary>
        [JsonProperty("date_modified")]
        public DateTime? DateModified { get; set; }

        [JsonIgnore]
        public FoxyCustomerAddress DefaultBillingAddress { get; set; }

        [JsonIgnore]
        public FoxyCustomerAddress DefaultShippingAddress { get; set; }
    }
}
