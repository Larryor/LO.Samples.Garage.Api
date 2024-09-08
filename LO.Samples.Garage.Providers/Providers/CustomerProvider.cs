using Microsoft.AspNetCore.JsonPatch;
using LO.Samples.Garage.Providers.Context;
using LO.Samples.Garage.Providers.Providers.Interfaces;
using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.Tables;
using LO.Samples.Garage.Shared.Internal.Results;
using Microsoft.EntityFrameworkCore;
using LO.Samples.Garage.Shared.Internal.Entities;

namespace LO.Samples.Garage.Providers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly GarageDbContext _context;

        public CustomerProvider(GarageDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerTable?> Get(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<ResultList<CustomerTable>> Get(CustomerTableQuery dtoQuery)
        {
            var customers = await dtoQuery.GetQuery(_context.Customers.AsQueryable()).ToListAsync();
            var totalCount = await dtoQuery.GetCountQuery(_context.Customers.AsQueryable()).CountAsync();
            return new ResultList<CustomerTable>(dtoQuery, totalCount, customers); ;
        }

        public async Task<CustomerTable> Add(CustomerTable dto)
        {
            dto.CreatedAtUTC = DateTime.UtcNow;
            dto.LastModifiedAtUTC = DateTime.UtcNow;
            var newCustomer = await _context.Customers.AddAsync(dto);
            await _context.SaveChangesAsync();
            return newCustomer.Entity;
        }

        /// <summary>
        /// Dto must be a valid and tracked entity
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        public async Task<CustomerTable> Update(CustomerTable customer, JsonPatchDocument patchDocument)
        {
            patchDocument.ApplyTo(customer);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        /// <summary>
        /// we will intentionally not throw an exception if the customer does not exist for the delete
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task Delete(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
