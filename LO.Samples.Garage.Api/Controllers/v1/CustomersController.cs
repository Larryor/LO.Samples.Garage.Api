using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using LO.Samples.Garage.Services.Services.Interfaces;
using LO.Samples.Garage.Shared.External.Queries;
using LO.Samples.Garage.Shared.External.Requests.POST;
using LO.Samples.Garage.Shared.External.Responses;
using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Shared.Internal.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LO.Samples.Garage.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : LOControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(IMapper mapper,
            ICustomerService customerService) : base(mapper)
        {
            _customerService = customerService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCustomers([FromRoute] CustomerRequestQuery request)
        {
            var query = Mapper.Map<CustomerRequestQuery, CustomerQuery>(request);
            var result = await _customerService.Get(query);
            return ResultOkOrFailure<ResultList<Customer>, ResultList<CustomerResponse>>(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerPostRequest request)
        {
            var customer = Mapper.Map<CustomerPostRequest, Customer>(request);
            var result = await _customerService.Add(customer);
            return ResultCreatedOrFailure<Customer, CustomerResponse>(result);
        }

        [HttpPatch("{customerId:guid}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid customerId, [FromBody] JsonPatchDocument patchDocument)
        {
            var customer =  await _customerService.Update(customerId, patchDocument);
            return ResultOkOrFailure<Customer, CustomerResponse>(customer);
        }

        [HttpDelete("{customerId:guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid customerId)
        {
            var result = await _customerService.Delete(customerId);
            return ResultNoContentOrFailure(result);
        }
    }
}
