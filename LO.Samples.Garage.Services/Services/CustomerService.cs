using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using LO.Samples.Garage.Providers.Providers.Interfaces;
using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.Tables;
using LO.Samples.Garage.Services.Services.Interfaces;
using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Providers.UnitOfWork.Interfaces;
using LO.Samples.Garage.Shared.Internal.BusinessErrors;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Services.Services
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerProvider _customerProvider;

        public CustomerService(IMapper mapper,
            ICustomerProvider customerProvider, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mapper = mapper;
            _customerProvider = customerProvider;
        }

        public async Task<Result<ResultList<Customer>>> Get(CustomerQuery query)
        {
            var dtoQuery = _mapper.Map<CustomerQuery, CustomerTableQuery>(query);

            var customerDtos = await _customerProvider.Get(dtoQuery);
            var customers = _mapper.Map<ResultList<Customer>>(customerDtos);
            return ResultWithoutCommit(customers);
        }

        public async Task<Result<Customer>> Add(Customer customer)
        {
            var dto = _mapper.Map<Customer, CustomerTable>(customer);

            var customersWithDuplicateName = await _customerProvider.Get(new CustomerTableQuery { Name = customer.Name });
            if (customersWithDuplicateName.Items.Any()) return await BusinessErrorWithRollBack<Customer>(new ValidationBusinessError<Customer>($"customer with the name: {customer.Name} already exists"));

            dto = await _customerProvider.Add(dto);
            customer = _mapper.Map<CustomerTable, Customer>(dto);
            return await ResultWithCommit(customer);
        }

        public async Task<Result<Customer>> Update(Guid customerId, JsonPatchDocument patchDocument)
        {
            if (!IsCustomerPatchDocumentValid(patchDocument)) return await BusinessErrorWithRollBack<Customer>(new ValidationBusinessError<Customer>("Patch request is invalid, only replacing the name is supported"));

            var dto = await _customerProvider.Get(customerId);
            if (dto == null) return await BusinessErrorWithRollBack<Customer>(new NotFoundBusinessError<Customer>());

            var replaceNameOperation = patchDocument.Operations.First(o =>
                o.op.Equals("replace", StringComparison.InvariantCultureIgnoreCase) &&
                o.path.Equals("/name", StringComparison.InvariantCultureIgnoreCase));
            
            var customersWithDuplicateName = await _customerProvider.Get(new CustomerTableQuery { Name = (string)replaceNameOperation.value });
            if (customersWithDuplicateName.Items.Any()) return await BusinessErrorWithRollBack<Customer>(new ValidationBusinessError<Customer>($"customer with the name: {(string)replaceNameOperation.value} already exists"));

            dto = await _customerProvider.Update(dto, patchDocument);
            var customer = _mapper.Map<CustomerTable, Customer>(dto);
            return await ResultWithCommit(customer);
        }

        /// <summary>
        /// Current we only allow to change the name of the customer, anything else is not valid
        /// </summary>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        private bool IsCustomerPatchDocumentValid(JsonPatchDocument patchDocument)
        {
            foreach (var operation in patchDocument.Operations)
            {
                if (!operation.op.Equals("replace")) return false;
                if (!operation.path.Equals("/name")) return false;
                if (!string.IsNullOrEmpty(operation.from)) return false;

                var value = operation.value as string;
                if (string.IsNullOrEmpty(value)) return false; // if we are trying to set name to empty value or any other type, we return false
            }

            return true;
        }

        public async Task<Result<bool>> Delete(Guid customerId)
        {
            await _customerProvider.Delete(customerId);
            return await ResultWithCommit(true);
        }
    }
}
