using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Shared.Internal.Results;
using Microsoft.AspNetCore.JsonPatch;

namespace LO.Samples.Garage.Services.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Result<ResultList<Customer>>> Get(CustomerQuery query);

        Task<Result<Customer>> Add(Customer vehicle);

        Task<Result<Customer>> Update(Guid customerId, JsonPatchDocument patchDocument);

        Task<Result<bool>> Delete(Guid customerId);
    }
}
