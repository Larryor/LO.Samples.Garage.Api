using AutoMapper;
using LO.Samples.Garage.Shared.External.Queries;
using LO.Samples.Garage.Shared.External.Requests.POST;
using LO.Samples.Garage.Shared.External.Responses;
using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Enums;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Api.Configurations
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            #region Requests

            CreateMap<CustomerPostRequest, Customer>();


            CreateMap<VehiclePostRequest, Vehicle>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => CarState.Available))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAtUTC, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedAtUTC, opt => opt.Ignore());

            CreateMap<BranchRequestQuery, BranchQuery>();
            CreateMap<CustomerRequestQuery, CustomerQuery>();
            CreateMap<VehicleRequestQuery, VehicleQuery>();

            #endregion

            #region Responses

            CreateMap(typeof(ResultList<>), typeof(ResultList<>));

            CreateMap<Branch, BranchResponse>();
            CreateMap<Customer, CustomerResponse>();
            CreateMap<Vehicle, VehicleResponse>();

            #endregion
        }
    }
}
