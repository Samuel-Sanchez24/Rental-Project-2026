using System;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task RollbackAsync();
}
