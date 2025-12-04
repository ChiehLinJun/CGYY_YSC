using CGYY_YSC.Services.Finance;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace CGYY_YSC.JOB
{
    class DoFinanceJob
    {
        [DisallowConcurrentExecution]
        public class DoFinance : IJob
        {
            private readonly ILogger<DoFinance> _logger;
            public DoFinance(ILogger<DoFinance> logger)
            {
                _logger = logger;
            }

            public Task Execute(IJobExecutionContext context)
            {
                FinanceBase main = new FinanceBase();
                try
                {
                    _logger.LogInformation(DateTime.Now + " | DoFinanceJob Patient is runing");
                    Log.DebugLog("in job");
                    main.InsertFinance();
                }
                catch (Exception e)
                {
                    Log.ErrorLog("[DoAigelJob Patient] " + e.ToString());
                    _logger.LogWarning("[DoAigelJob Patient] " + e.ToString());
                }

                return Task.CompletedTask;
            }
        }
    }
}