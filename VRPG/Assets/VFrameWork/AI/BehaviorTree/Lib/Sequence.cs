using System;
using System.Collections.Generic;

namespace V.AI
{
    public class Sequence : Composite
    {
        protected int childIndex;

        public Sequence()
        {
            Initialize = () => { childIndex = 0;};
        }

        public override Status Update()
        {
            for (;;)
            {
                Status s = GetChild(childIndex).Tick();
                if (s != Status.BhSuccess)
                {
                    if (s == Status.BhFailure)
                    {
                        childIndex = 0;
                    }
                    return s;
                }

                if (++childIndex == ChildCount)
                {
                    childIndex = 0;
                    return Status.BhSuccess;
                }
            }
        }

        //We dont need to Initialize in Reset, because in Behavior class. When we Tick, We will Re-Initialize 
        public override void Reset()
        {
            Status = Status.BhInvalid;
            //dont call the bass Reset because, we ......
        }
    }
}
