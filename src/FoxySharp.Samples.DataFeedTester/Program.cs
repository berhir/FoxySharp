using FoxySharp.DataFeed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FoxySharp.Samples.DataFeedTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load config
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(PlatformServices.Default.Application.ApplicationBasePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var config = configBuilder.Build();
            var dataFeedKey = config.GetSection("FoxyCart")["ApiKey"];
            var dataFeedUrl = config.GetSection("FoxyCart")["DataFeedUrl"];


            //Load plain XML data
            string transactionData = File.ReadAllText("FoxyData.xml");

            var encryptedTransactionData = RC4.Encrypt(Encoding.UTF8.GetBytes(dataFeedKey), Encoding.UTF8.GetBytes(transactionData));
            var encodedBytes = WebUtility.UrlEncodeToBytes(encryptedTransactionData, 0, encryptedTransactionData.Length);

            using (var client = new HttpClient())
            {
                var requestContent = new MultipartFormDataContent();
                var dataContent = new ByteArrayContent(encodedBytes);
                dataContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                requestContent.Add(dataContent, "FoxyData");

                try
                {
                    var responseTask = client.PostAsync(dataFeedUrl, requestContent);
                    responseTask.Wait();
                    var response = responseTask.Result;

                    var messageTask = response.Content.ReadAsStringAsync();
                        messageTask.Wait();
                        Console.WriteLine($"Result from server: {messageTask.Result}");
                }
                catch (WebException ex)
                {
                    string err = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    Console.WriteLine(err);
                }
            }

            Console.ReadKey();
        }
    }
}
