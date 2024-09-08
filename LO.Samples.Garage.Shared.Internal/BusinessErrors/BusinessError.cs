namespace LO.Samples.Garage.Shared.Internal.BusinessErrors
{
    public class BusinessError
    {
        public BusinessError(string entityType, string errorType, string errorMessage)
        {
            EntityType = entityType;
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }

        public string EntityType { get; set; }

        public string ErrorType { get; set; }

        public string ErrorMessage { get; set; }
    }
}
