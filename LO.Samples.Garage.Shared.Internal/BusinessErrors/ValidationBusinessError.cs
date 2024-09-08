namespace LO.Samples.Garage.Shared.Internal.BusinessErrors
{
    public class ValidationBusinessError<T> : BusinessError
    {
        public ValidationBusinessError(string errorMessage) : base(typeof(T).Name, "ValidationBusinessError", errorMessage)
        {

        }
    }
}
