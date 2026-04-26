using books_store_BLL.Dtos.Genre;
using books_store_BLL.Dtos.Services;
using books_store_DAL.Repositories;
using Quartz;

namespace books_store_api.infrastructure.jobs
{
    public class TokensCleaningJob : IJob
    {
        private readonly JwtService _jwtService;

        public TokensCleaningJob(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _jwtService.CleanTokensAsync();

            return Task.CompletedTask;
        }
    }
}


