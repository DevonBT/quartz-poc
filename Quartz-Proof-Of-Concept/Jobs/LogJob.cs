using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Proof_Of_Concept
{
    internal class LogJob : IDomainJob
    {
        //public Guid Id = Guid.Parse("85556676-fa31-499c-9a3e-de3978455550");
        public string Name => "Log";
        public string Group => "TestGroup";
        public Dictionary<string, string> Parameters => new Dictionary<string, string>();

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("LogJob running at " + context.FireTimeUtc);
            return Task.CompletedTask;
        }

        public void Parameterize(Dictionary<string, string> Params)
        {
            // no-op
        }

        public IJobDetail ToQuartz()
        {
            return JobBuilder.Create<LogJob>()
                .WithIdentity(Name, Group)
                .Build();
        }
    }
}
