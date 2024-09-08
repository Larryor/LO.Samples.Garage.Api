using System.Runtime.CompilerServices;

namespace LO.Samples.Garage.Shared.Internal.BusinessErrors
{
    public class NotFoundBusinessError<T> : BusinessError
    {
        public NotFoundBusinessError() : base(typeof(T).Name, "NotFoundBusinessError", "")
        {

        }
    }
}
