using AutoMapper;
using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.Tables;
using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Services.Configuration
{
    public class ServicesMapperProfile : Profile
    {
        public ServicesMapperProfile()
        {
            #region Entities
            
            CreateMap<Branch, BranchTable>().ReverseMap();
            CreateMap<Vehicle, VehicleTable>().ReverseMap();
            CreateMap<Customer, CustomerTable>().ReverseMap();

            #endregion

            #region Queries
            
            CreateMap<BranchQuery, BranchTableQuery>();
            CreateMap<CustomerQuery, CustomerTableQuery>();
            CreateMap<VehicleQuery, VehicleTableQuery>();

            #endregion
        }
    }
}
