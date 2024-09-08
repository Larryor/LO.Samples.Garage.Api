namespace LO.Samples.Garage.Shared.Internal.Results
{
    public class ResultListMetadata
    {
        public int TotalCount { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; } = 100;
    }
}
