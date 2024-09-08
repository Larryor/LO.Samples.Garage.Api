using LO.Samples.Garage.Providers.UnitOfWork.Interfaces;
using LO.Samples.Garage.Shared.Internal.BusinessErrors;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Services.Services
{
    public abstract class ServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            Console.WriteLine("protected ServiceBase(IUnitOfWork unitOfWork) CTOR");
            _unitOfWork = unitOfWork;
            _unitOfWork.Start();
        }

        protected async Task<Result<T>> ResultWithCommit<T>(Result<T> result)
        {
            if (result.IsFailure)
            {
                return await BusinessErrorWithRollBack<T>(result.BusinessError);
            }
            else
            {
                await _unitOfWork.Commit();
                return result;
            }
        }

        protected async Task<Result<T>> ResultWithCommit<T>(T model)
        {
            await _unitOfWork.Commit();
            return new Result<T>(model);
        }

        protected Result<T> ResultWithoutCommit<T>(T model)
        {
            if (model is null)
            {
                return new Result<T>(new NotFoundBusinessError<T>());
            }

            return new Result<T>(model);
        }

        protected async Task<Result<T>> BusinessErrorWithRollBack<T>(BusinessError businessError)
        {
            await _unitOfWork.Rollback();
            return new Result<T>(businessError);
        }
    }
}
