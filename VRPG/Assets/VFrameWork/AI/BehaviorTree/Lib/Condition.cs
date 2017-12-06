using System;
using System.Collections.Generic;

namespace V.AI
{
    public class Condition : Behavior
    {
        public Func<bool> CanPass { protected get; set; }

        public Condition() {}

        public override Status Update()
        {
            if (CanPass != null && CanPass())
            {
                return Status.BhSuccess;
            }
            return Status.BhFailure;
        }
    }
}
