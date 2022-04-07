using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Proof_Of_Concept
{
    public enum RunState
    {
        Pending,
        InProgress,
        Complete
    }

    internal class Run
    {
        public RunState State;
        public Guid Id;
        public string Parameters;
        public DateTime RunAt;
    }
}
