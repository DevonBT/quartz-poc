using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Proof_Of_Concept
{
    internal class Schedule
    {
        public int JobId;
        public Guid Id;
        public string Cron;
        public Dictionary<string, string> Parameters;
        private IDomainJob _Job;

        public IDomainJob Job 
        {
            get  
            {
                if (_Job != null) return _Job;

                IDomainJob job = JobId switch
                {
                    1 => new LogJob(),
                    2 => new ScanConnectorJob()
                };
                _Job = job;
                return job;
            }
        }

        public ITrigger Trigger
        {
            get
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(Id.ToString(), Job.Name)
                    .StartNow()
                    .WithCronSchedule(Cron,
                        // Immediately invoke runs that were missed due to shutdown
                        // Derive this from Job type?
                        x => x.WithMisfireHandlingInstructionFireAndProceed());

                Parameters.ToList().ForEach(kvp =>
                {
                    trigger = trigger.UsingJobData(kvp.Key, kvp.Value);
                });
                return trigger.Build();
            }
        }
    }
}
