using FoxySharp.DataFeed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FoxySharp.Samples.Web.Controllers
{
    public class FoxyCartController : Controller
    {
        private FoxyCartSettings _foxyCartSettings;
        private ILogger _logger;

        public FoxyCartController(IOptions<FoxyCartSettings> foxyCartSettings, ILogger<FoxyCartController> logger)
        {
            _foxyCartSettings = foxyCartSettings.Value;
            _logger = logger;
        }

        public IActionResult Sso(string fcsid, long timestamp, string checkout_type)
        {
            //
            // insert your logic to map to an existing FoxyCart customer here.
            // or use the FoxySharp client to create a new customer.
            //
            int customerId = 1234; // replace with real customer id
            long tokenExpiresTimestamp = DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds();
            string authToken;

            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes($"{customerId}|{tokenExpiresTimestamp}|{_foxyCartSettings.ApiKey}"));
                authToken = BitConverter.ToString(hash);
                authToken = authToken.Replace("-", "");
            }

            var redir = $"{_foxyCartSettings.StoreUrl}/checkout?fc_auth_token={authToken}&fcsid={fcsid}&fc_customer_id={customerId}&timestamp={tokenExpiresTimestamp}";

            return Redirect(redir);
        }

        [HttpPost]
        public IActionResult DataFeed([FromForm(Name = "FoxyData")] string foxyData)
        {
            var foxyDataBytes = Encoding.UTF8.GetBytes(foxyData);
            var foxyDataDecoded = System.Net.WebUtility.UrlDecodeToBytes(foxyDataBytes, 0, foxyDataBytes.Length);
            var foxyDataDecrypted = Encoding.UTF8.GetString(RC4.Decrypt(Encoding.UTF8.GetBytes(_foxyCartSettings.ApiKey), foxyDataDecoded));

            _logger.LogInformation("Received FoxyData");

            var serializer = new XmlSerializer(typeof(foxydata));
            foxydata data = null;
            using (TextReader reader = new StringReader(foxyDataDecrypted))
            {
                data = (foxydata)serializer.Deserialize(reader);

                foreach (var transaction in data.transactions)
                {
                    _logger.LogInformation($"Processing transaction with ID '{transaction.id}'");
                }
            }

            return Content("foxy");
        }
    }
}
