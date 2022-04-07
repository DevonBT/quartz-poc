using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Proof_Of_Concept
{
    internal interface IDomainJob : IJob
    {
        public IJobDetail ToQuartz();  
        public string Name { get; }
        public string Group { get; }
    }
}
