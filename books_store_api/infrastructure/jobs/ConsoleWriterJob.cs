using Quartz;

namespace books_store_api.infrastructure.jobs
{
    public class ConsoleWriterJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync($"{DateTime.Now.ToLongTimeString()} - quarz is running");
            Console.ResetColor();
        }
    }
}
