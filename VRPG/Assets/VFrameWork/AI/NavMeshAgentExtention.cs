using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NavMeshAgentExtention{

    public static bool ArriveDestination_NotPathPending(this UnityEngine.AI.NavMeshAgent agent)
    {
        return  (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending);
    }
}
