using System;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using GenFu;


namespace Assure.Core.DataAccessLayer.Tests.Integration
{
    public static class GenFuConfigurator
    {
        static GenFuConfigurator()
        {
            A.Configure<ObjectDocumentContainer>().Fill(x => x.ObjectDocument, "{}");

            A.Configure<Company>().Fill(x => x.CompanyId, 0);
            A.Configure<Lead>().Fill(x => x.LeadId, 0);


            A.Configure<Account>().Fill(x => x.Type, "Test")
                                   .Fill(x => x.ObjectDocument, "{}");

            A.Configure<Agent>().Fill(x => x.Type, "Test")
                                 .Fill(x => x.ObjectDocument, "{}");

            A.Configure<Card>().Fill(x => x.ObjectDocument, "{}");

            A.Configure<Customer>().Fill(x => x.ObjectDocument, "{}");
            
            A.Configure<Invoice>().Fill(x => x.CurrencyAlpha3, "EUR")
                                   .Fill(x => x.Product, "Leisure Annual International")
                                   .Fill(x => x.ObjectDocument, "{}");

            A.Configure<Policy>().Fill(x => x.Product, "Leisure Annual International")
                                  .Fill(x => x.StartDateTime, DateTimeOffset.Now.AddMonths(1))
                                  .Fill(x => x.EndDateTime, DateTimeOffset.Now.AddMonths(2))
                                  .Fill(x => x.ObjectDocument, "{}");

            A.Configure<Product>().Fill(x => x.ObjectDocument, "{}");

            A.Configure<Quote>().Fill(x => x.Product, "Leisure Annual International")
                                 .Fill(x => x.StartDateTime, DateTimeOffset.Now.AddMonths(1))
                                 .Fill(x => x.EndDateTime, DateTimeOffset.Now.AddMonths(2))
                                 .Fill(x => x.ObjectDocument, "{}");

            A.Configure<Benefit>().Fill(x => x.ObjectDocument, "{}");
        }

        internal static void Initialise() { }
    }
}
