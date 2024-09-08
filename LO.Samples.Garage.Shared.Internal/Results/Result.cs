using LO.Samples.Garage.Shared.Internal.BusinessErrors;

namespace LO.Samples.Garage.Shared.Internal.Results
{
    public class Result<T>
    {
        public Result(T value, BusinessError error)
        {
            Value = value;
            BusinessError = error;
        }

        public Result(T value) : this(value, null) { }

        public Result(BusinessError error) : this(default, error) { }

        public bool IsSuccess => Value != null && !Value.Equals(default(T));

        public bool IsFailure => !IsSuccess;

        public BusinessError BusinessError { get; set; }

        public T Value { get; set; }
    }
}
