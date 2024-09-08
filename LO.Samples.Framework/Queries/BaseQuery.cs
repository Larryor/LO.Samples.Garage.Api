namespace LO.Samples.Framework.Queries
{
    public class BaseQuery
    {
        public BaseQuery()
        {
            Limit = 100;
            Offset = 0;
        }

        public BaseQuery(int limit, int offset)
        {
            if (limit > 100) throw new ArgumentException("limit is limited to 100", nameof(limit));

            Limit = limit;
            Offset = offset;
        }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public IQueryable<T> GetQuery<T>(IQueryable<T> query)
        {
            return query.Skip(Offset).Take(Limit);
        }

        public IQueryable<T> GetCountQuery<T>(IQueryable<T> query)
        {
            //because we do not have in the base any filtering parameters there is nothing here, if we had filter by name this would be done here, see CustomerTableQuery.cs
            return query;
        }
    }
}
