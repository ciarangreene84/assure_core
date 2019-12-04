using Assure.Core.Shared.Interfaces.Models;
using AutoMapper;
using DalModels = Assure.Core.DataAccessLayer.Interfaces.Models;
using RepoModels = Assure.Core.RepositoryLayer.Interfaces.Models;

namespace Assure.Core.RepositoryLayer.Implementation.Profiles
{
    public class AssureCoreRepositoryLayerAutoMapperProfile : Profile
    {
        public AssureCoreRepositoryLayerAutoMapperProfile()
        {
            CreateMap<DalModels.Account, RepoModels.Account>().ReverseMap();
            CreateMap<DalModels.Agent, RepoModels.Agent>().ReverseMap();

            CreateMap<DalModels.Card, RepoModels.Card>().ReverseMap();
            CreateMap<DalModels.Claim, RepoModels.Claim>().ReverseMap();
            CreateMap<DalModels.Company, RepoModels.Company>().ReverseMap();
            CreateMap<DalModels.Country, RepoModels.Country>().ReverseMap();
            CreateMap<DalModels.Currency, RepoModels.Currency>().ReverseMap();
            CreateMap<DalModels.Customer, RepoModels.Customer>().ReverseMap();

            CreateMap<DalModels.Document, RepoModels.Document>().ReverseMap();
            CreateMap<PageResponse<DalModels.Document>, PageResponse<RepoModels.Document>>().ReverseMap();

            CreateMap<DalModels.Invoice, RepoModels.Invoice>().ReverseMap();

            CreateMap<DalModels.Lead, RepoModels.Lead>().ReverseMap();

            CreateMap<DalModels.Payment, RepoModels.Payment>().ReverseMap();
            CreateMap<DalModels.Policy, RepoModels.Policy>().ReverseMap();
            CreateMap<DalModels.Product, RepoModels.Product>().ReverseMap();

            CreateMap<DalModels.Quote, RepoModels.Quote>().ReverseMap();

            CreateMap<DalModels.Request, RepoModels.Request>().ReverseMap();
            CreateMap<DalModels.Benefit, RepoModels.Benefit>().ReverseMap();
        }
    }
}
