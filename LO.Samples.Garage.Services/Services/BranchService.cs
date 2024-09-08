using AutoMapper;
using LO.Samples.Garage.Providers.Providers.Interfaces;
using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.UnitOfWork.Interfaces;
using LO.Samples.Garage.Services.Services.Interfaces;
using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Services.Services
{
    public class BranchService : ServiceBase, IBranchService
    {
        private readonly IMapper _mapper;
        private readonly IBranchProvider _branchProvider;
        
        public BranchService(IMapper mapper,
            IBranchProvider branchProvider, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mapper = mapper;
            _branchProvider = branchProvider;
        }

        public async Task<Result<ResultList<Branch>>> Get(BranchQuery query)
        {
            var dtoQuery = _mapper.Map<BranchQuery, BranchTableQuery>(query);
            var branchDtos = await _branchProvider.Get(dtoQuery);

            var branches = _mapper.Map<ResultList<Branch>>(branchDtos);
            return ResultWithoutCommit(branches);
        }
    }
}
