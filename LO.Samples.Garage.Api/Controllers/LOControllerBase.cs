using System.Net;
using AutoMapper;
using Azure;
using LO.Samples.Garage.Shared.Internal.BusinessErrors;
using LO.Samples.Garage.Shared.Internal.Results;
using Microsoft.AspNetCore.Mvc;

namespace LO.Samples.Garage.Api.Controllers
{
    public class LOControllerBase : ControllerBase
    {
        protected IMapper Mapper;

        public LOControllerBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        public IActionResult ResultOkOrFailure<TEntity, TResponse>(Result<TEntity> result)
        {
            return result.IsFailure ? ResultFailure<TEntity>(result) : Ok(Mapper.Map<TResponse>(result.Value));
        }

        private IActionResult ResultFailure<TEntity>(Result<TEntity> result)
        {
            if (result.IsSuccess) throw new Exception("result is success but we called ResultFailure");

            var statusCode = GetStatusCodeFromBusinessError(result.BusinessError);
            return StatusCode((int) statusCode, result.BusinessError.ErrorMessage);
        }

        public IActionResult ResultCreatedOrFailure<TEntity, TResponse>(Result<TEntity> result)
        {
            if (result.IsFailure) return ResultFailure<TEntity>(result);
            var response = Mapper.Map<TResponse>(result.Value);
            return Created("TODO", response);
        }

        public IActionResult ResultNoContentOrFailure<T>(Result<T> result)
        {
            return result.IsFailure ? ResultFailure<T>(result) : NoContent();
        }

        private HttpStatusCode GetStatusCodeFromBusinessError(BusinessError error)
        {
            switch (error.ErrorType)
            {
                case "NotFoundBusinessError":
                    return HttpStatusCode.NotFound;
                case "ValidationBusinessError":
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
