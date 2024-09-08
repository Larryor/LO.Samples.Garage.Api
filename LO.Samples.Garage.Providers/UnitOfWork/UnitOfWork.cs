using LO.Samples.Garage.Providers.Context;
using LO.Samples.Garage.Providers.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace LO.Samples.Garage.Providers.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GarageDbContext _dbContext;
        private IDbContextTransaction? _transaction;
        private bool _isCompleted;
        private bool _isStarted;

        public UnitOfWork(GarageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Start()
        {
            _isStarted = true;
            _isCompleted = false;

            if (_isStarted && _transaction == null) _transaction = _dbContext.Database.BeginTransaction();
        }

        public async Task Commit()
        {
            if (_isCompleted) throw new InvalidOperationException("Unit of work is completed already");
            
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction.Dispose();
                _transaction = null;
                _isCompleted = true;
            }
        }

        public async Task Rollback()
        {
            if (_isCompleted) throw new InvalidOperationException("Unit of work is completed already");

            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction.Dispose();
                _transaction = null;
                _isCompleted = true;
            }
        }
    }
}