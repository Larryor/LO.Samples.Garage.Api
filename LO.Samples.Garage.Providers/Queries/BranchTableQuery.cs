using LO.Samples.Framework.Queries;
using LO.Samples.Garage.Providers.Tables;

namespace LO.Samples.Garage.Providers.Queries
{
    public class BranchTableQuery : BaseQuery
    {
        public string? Name { get; set; }

        public IQueryable<BranchTable> GetQuery(IQueryable<BranchTable> query)
        {
            query = GetCountQuery(query);
            query = query.Skip(Offset).Take(Limit);
            return query;
        }

        public IQueryable<BranchTable> GetCountQuery(IQueryable<BranchTable> query)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(c => c.Name.Equals(Name));
            }

            return query;
        }
    }
}
