
using Quartz;

namespace books_store_api.infrastructure.jobs
{
    public class LogCleaningJob : IJob
    {
        private readonly IWebHostEnvironment _environment;

        public LogCleaningJob(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var logsPath = Path.Combine(_environment.ContentRootPath, "logs");
            if (Directory.Exists(logsPath))
            {
     
                foreach (var filePath in Directory.GetFiles(logsPath))
                {
                    var file = new FileInfo(filePath);
                    if(DateTime.Now - file.CreationTime > TimeSpan.FromDays(7))
                    {
                        File.Delete(filePath);
                    }
                }

            }
            return Task.CompletedTask;
        }
    }
}
