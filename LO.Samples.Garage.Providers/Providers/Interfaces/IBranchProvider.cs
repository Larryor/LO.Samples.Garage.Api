using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.Tables;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Providers.Providers.Interfaces
{
    public interface IBranchProvider
    {
        Task<BranchTable?> Get(Guid id);
        Task<ResultList<BranchTable>> Get(BranchTableQuery query);
    }
}
