namespace Porter.Common.Services
{
    public interface ILogService
    {
        Task LogList<T>(T entity, string methodName);
        Task LogView<T>(T entity, string methodName);

        Task LogUpdate<T>(T entity, string methodName);
        Task LogInsert<T>(T entity, string methodName);
        Task LogDelete<T>(T entity, string methodName);

    }
}
