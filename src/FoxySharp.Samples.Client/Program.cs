using FoxySharp.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System;

namespace FoxySharp.Samples.Client
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

            var foxyService = new FoxyCartClient(
                config.GetSection("FoxyCart")["ApiEndpoint"],
                config.GetSection("FoxyCart")["ClientId"],
                config.GetSection("FoxyCart")["ClientSecret"],
                config.GetSection("FoxyCart")["RefreshToken"]);

            var customers = foxyService.GetCustomers();

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName}");
            }

            //var customer = foxyService.GetCustomer(customers.First().Id);

            //customer.DefaultBillingAddress.FirstName = customer.DefaultShippingAddress.FirstName = customer.FirstName;
            //customer.DefaultBillingAddress.LastName = customer.DefaultShippingAddress.LastName = customer.LastName;

            //foxyService.UpdateCustomer(customer);

            //var newCustomer = new FoxyCustomer()
            //{
            //    FirstName = "John",
            //    LastName = "Doe",
            //    Email = "john@doe.com",
            //};

            //var created = foxyService.CreateCustomer(newCustomer);
        }
    }
}
