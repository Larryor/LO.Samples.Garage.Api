using LO.Samples.Garage.Providers.Context;
using LO.Samples.Garage.Providers.Providers.Interfaces;
using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.Tables;
using LO.Samples.Garage.Shared.Internal.Results;
using Microsoft.EntityFrameworkCore;

namespace LO.Samples.Garage.Providers.Providers
{
    public class BranchProvider : IBranchProvider
    {
        private readonly GarageDbContext _context;

        public BranchProvider(GarageDbContext context)
        {
            _context = context;
        }

        public async Task<BranchTable?> Get(Guid id)
        {
            return await _context.Branches.FindAsync(id);
        }

        public async Task<ResultList<BranchTable>> Get(BranchTableQuery dtoQuery)
        {
            var branches = await dtoQuery.GetQuery(_context.Branches.AsQueryable()).ToListAsync();
            var totalCount = await dtoQuery.GetCountQuery(_context.Branches.AsQueryable()).CountAsync();
            return new ResultList<BranchTable>(dtoQuery, totalCount, branches);
        }
    }
}
