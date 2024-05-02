namespace ASPNetCoreFundamental_Day1
{
    public class WriteLog : IWriteLog
    {
        private readonly ILogger<WriteLog> _logger;
        public WriteLog(ILogger<WriteLog> logger)
        {
            _logger = logger;
        }
        void IWriteLog.WriteLog(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
