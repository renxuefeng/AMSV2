using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMSV2.Jobs
{
    public class HandleLogJob : IJob, IDisposable
    {
        private readonly ILogger<HandleLogJob> _logger;
        //private readonly IJobService _jobService;
        public HandleLogJob(ILogger<HandleLogJob> logger/*, IJobService jobService*/)
        {
            this._logger = logger;
            //_jobService = jobService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //_jobService.DeleteLog();
            _logger.LogInformation(context.JobDetail.Key + " job executing, triggered by " + context.Trigger.Key);
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        public void Dispose()
        {
            _logger.LogInformation("Example job disposing");
        }
    }
}
