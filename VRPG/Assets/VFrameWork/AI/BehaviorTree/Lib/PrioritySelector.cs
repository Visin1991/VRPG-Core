using System;
using System.Collections.Generic;

namespace V.AI
{
    public class PrioritySelector : Selector
    {
        private int lastChildIndex;

        public PrioritySelector()
        {}

        public override Status Update()
        {
            childIndex = 0;

            for (;;)
            {
                Status s = GetChild(childIndex).Tick();
                if (s != Status.BhFailure)
                {
                    for (int i = childIndex + 1; i <= lastChildIndex; i++)
                    {
                        GetChild(i).Reset();
                    }
                    lastChildIndex = childIndex;
                    return s;
                }

                if (++childIndex == ChildCount)
                {
                    return Status.BhFailure;
                }
            }
        }
    }
}
