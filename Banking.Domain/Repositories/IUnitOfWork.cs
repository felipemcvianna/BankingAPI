namespace Banking.Domain.Repositories;

public interface IUnitOfWork
{
    public Task Commit();
}