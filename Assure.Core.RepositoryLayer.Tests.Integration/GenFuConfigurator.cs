using System;
using System.Collections.Generic;
using System.Linq;
using Assure.Core.RepositoryLayer.Interfaces.Models;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using GenFu;

using Invoice = Assure.Core.DataAccessLayer.Interfaces.Models.Invoice;

namespace Assure.Core.RepositoryLayer.Tests.Integration
{
    public static class GenFuConfigurator
    {
        public static IList<string> ProductsList = new List<string>()
        {
            "Business Class International",
            "Business Class Domestic",
            "Leisure Single Trip International",
            "Leisure Single Trip Domestic",
            "Leisure Annual International",
            "Leisure Annual Domestic",
            "Corporate Annual",
            "Corporate Single Trip"
        };


        static GenFuConfigurator()
        {
            A.Configure<Account>().Fill(x => x.Type, "Test");
                //.Fill(x => x.ObjectDocument, "{}");

            A.Configure<Agent>().Fill(x => x.Type, "Test");

            A.Configure<TestCompany>().Fill(x => x.CompanyId, 0);

            //A.Configure<Card>().Fill(x => x.ObjectDocument, "{}");

            //A.Configure<Customer>().Fill(x => x.ObjectDocument, "{}");

            A.Configure<TestInvoice>().Fill(x => x.CurrencyAlpha3, "EUR")
                                       .Fill(x => x.Product, "Leisure Annual International");

            A.Configure<TestPolicy>().Fill(x => x.Product, ProductsList[A.Random.Next(0, ProductsList.Count - 1)])
                                  .Fill(x => x.StartDateTime, DateTimeOffset.Now.AddMonths(1))
                                  .Fill(x => x.EndDateTime, DateTimeOffset.Now.AddMonths(2));

            //A.Configure<Product>().Fill(x => x.ObjectDocument, "{}");

            A.Configure<Quote>().Fill(x => x.Product, ProductsList[A.Random.Next(0, ProductsList.Count - 1)])
                                 .Fill(x => x.StartDateTime, DateTimeOffset.Now.AddMonths(1))
                                 .Fill(x => x.EndDateTime, DateTimeOffset.Now.AddMonths(2));

            //A.Configure<Benefit>().Fill(x => x.ObjectDocument, "{}");
        }

        internal static void Initialise() { }

        public static GenFuConfigurator<TType> AsRandom<TType, TData>(
            this GenFuComplexPropertyConfigurator<TType, IList<TData>> configurator,
            IList<TData> data, int min, int max)
            where TType : new()
        {
            configurator.Maggie.RegisterFiller(
                new CustomFiller<IList<TData>>(
                    configurator.PropertyInfo.Name, typeof(TType),
                    () => data.GetRandom(min, max)));

            return configurator;
        }

        private static IList<T> GetRandom<T>(this IList<T> source, int min, int max)
        {
            var length = source.Count();
            var index = A.Random.Next(0, length - 1);
            var count = A.Random.Next(min, max);

            return source.Skip(index).Take(count).ToList();
        }
    }
}
