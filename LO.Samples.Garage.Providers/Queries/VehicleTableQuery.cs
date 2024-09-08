using LO.Samples.Framework.Queries;
using LO.Samples.Garage.Providers.Tables;

namespace LO.Samples.Garage.Providers.Queries
{
    public class VehicleTableQuery : BaseQuery
    {
        public string? Name { get; set; }

        public IQueryable<VehicleTable> GetQuery(IQueryable<VehicleTable> query)
        {
            query = GetCountQuery(query);
            query = query.Skip(Offset).Take(Limit);
            return query;
        }

        public IQueryable<VehicleTable> GetCountQuery(IQueryable<VehicleTable> query)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(c => c.Name.Equals(Name));
            }

            return query;
        }
    }
}
