using System;
using System.Collections.Generic;

namespace V.AI
{
    public class Selector : Composite
    {
        protected int childIndex;

        public Selector() {
            Initialize = () =>
            {
                childIndex = 0;
            };
        }

        public override Status Update()
        {
            for (;;)
            {
                Status s = GetChild(childIndex).Tick();

                if (s != Status.BhFailure)
                {
                    if (s == Status.BhSuccess)
                    {
                        childIndex = 0;
                    }
                    return s;
                }
                if (++childIndex == ChildCount)
                {
                    childIndex = 0;
                    return Status.BhFailure;
                }
            }
        }

        //We dont need to Initialize in Reset, because in Behavior class. When we Tick, We will Re-Initialize 
        public override void Reset()
        {
            Status = Status.BhInvalid;
            base.Reset(); //Do we need To Reset all children??????
        }
    }
}
