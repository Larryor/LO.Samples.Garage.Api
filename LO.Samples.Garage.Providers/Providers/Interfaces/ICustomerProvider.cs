using Microsoft.AspNetCore.JsonPatch;
using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.Tables;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Providers.Providers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<CustomerTable?> Get(Guid id);
        Task<ResultList<CustomerTable>> Get(CustomerTableQuery dtoQuery);

        Task<CustomerTable> Add(CustomerTable dto);

        Task<CustomerTable> Update(CustomerTable customer, JsonPatchDocument patchDocument);

        Task Delete(Guid id);
    }
}
