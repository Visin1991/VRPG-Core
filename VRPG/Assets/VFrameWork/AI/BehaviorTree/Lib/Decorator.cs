using System;
using System.Collections.Generic;

namespace V.AI
{
    public class Decorator : Composite
    {
        public Func<bool> canPass { protected get; set; }

        public Decorator()
        { }

        public override Status Update()
        {
            if (canPass != null && canPass() && Children != null && Children.Count > 0)
            {
                return Children[0].Tick();
            }
            return Status.BhFailure;
        }
    }
}
