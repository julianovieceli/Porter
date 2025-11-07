using Porter.Common.Domain.ExtensionMethods;

namespace Porter.Common.Domain
{
    public class Log : BaseDomain
    {
        public enum ACTION
        {
            INSERT,
            UPDATE,
            DELETE,
            LIST,
            VIEW
        }

        public ACTION Action { get;set; }

        public string EntityType { get; set; }

        public string MethodName { get; set; }

        public string Data { get; set; }

        public Log()
        {
            
        }

        public Log(ACTION action, Type entityType, string methodName,  string data) : this(action,  entityType, methodName)
        {
            Data = data;
        }

        public Log(ACTION action, Type entityType, string methodName )
        {
            Action = action;
            EntityType = entityType.Name;
            MethodName = methodName;
            CreateTime = DateTime.SpecifyKind(DateTime.Now.ToBrazilDatetime(), DateTimeKind.Utc);
        }



    }
}
