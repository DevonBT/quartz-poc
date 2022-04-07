using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Proof_Of_Concept
{
    internal class ScanConnectorJob : IDomainJob
    {
        public static Dictionary<string, Type> ParametersTypes => new Dictionary<string, Type>() { 
            { "Connector Id", typeof(int) } 
        };

        public string Name => "ScanConnector";
        public string Group => "ScanGroup";


        public Task Execute(IJobExecutionContext context)
        {
            var connectorId = context.JobDetail.JobDataMap.GetString("Connector Id");
            Console.WriteLine($"Scan connector {connectorId}: job running at {context.FireTimeUtc}");
            return Task.CompletedTask;
        }

        public IJobDetail ToQuartz()
        {
            return JobBuilder.Create<ScanConnectorJob>()
                .WithIdentity(Guid.NewGuid().ToString(), Name) // new guid every time?
                .Build();
        }
    }
}
