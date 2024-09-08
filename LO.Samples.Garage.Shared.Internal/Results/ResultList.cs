using LO.Samples.Framework.Queries;

namespace LO.Samples.Garage.Shared.Internal.Results
{
    public class ResultList<T>
    {
        public ResultList()
        {
            
        }

        public ResultList(BaseQuery query, int totalCount, List<T> items)
        {
            Metadata = new ResultListMetadata
            {
                Limit = query.Limit,
                Offset = query.Offset,
                TotalCount = totalCount
            };
            Items = items;
        }

        public ResultListMetadata Metadata { get; set; }

        public IList<T> Items { get; set; }
    }
}
