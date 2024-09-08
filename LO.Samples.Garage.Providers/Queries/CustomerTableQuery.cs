using LO.Samples.Framework.Queries;
using LO.Samples.Garage.Providers.Tables;

namespace LO.Samples.Garage.Providers.Queries
{
    public class CustomerTableQuery : BaseQuery
    {
        public string? Name { get; set; }

        public IQueryable<CustomerTable> GetQuery(IQueryable<CustomerTable> query)
        {
            query = GetCountQuery(query);
            query = query.Skip(Offset).Take(Limit);
            return query;
        }

        public IQueryable<CustomerTable> GetCountQuery(IQueryable<CustomerTable> query)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(c => c.Name.Equals(Name));
            }

            return query;
        }
    }
}
