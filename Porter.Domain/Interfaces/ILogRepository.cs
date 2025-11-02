namespace Porter.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task Register(Log room);
    }
}
