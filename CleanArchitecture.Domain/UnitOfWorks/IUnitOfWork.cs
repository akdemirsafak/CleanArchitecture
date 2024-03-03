namespace CleanArchitecture.Domain.UnitOfWorks;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}
