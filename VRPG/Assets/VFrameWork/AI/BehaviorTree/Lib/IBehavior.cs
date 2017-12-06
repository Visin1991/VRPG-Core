using System;
using System.Collections.Generic;

namespace V.AI
{
    public interface IBehavior
    {
        
        Status Status { get; set; }
        Action Initialize { set; }
        //Func<Status> Update { set; } 
        Action<Status> Terminate { set; }

        Status Tick();
        void Reset();
    }
}