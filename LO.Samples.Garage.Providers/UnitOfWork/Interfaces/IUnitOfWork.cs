namespace LO.Samples.Garage.Providers.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        void Start();

        Task Commit();

        Task Rollback();
    }
}
