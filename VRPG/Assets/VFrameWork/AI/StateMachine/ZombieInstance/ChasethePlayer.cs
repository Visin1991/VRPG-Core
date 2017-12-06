using UnityEngine;
using System.Collections;

public sealed class ChasethePlayer : StateInterface<Zombie>
{
    static readonly ChasethePlayer instance = new ChasethePlayer();
     ///Use this for initialization
    public static ChasethePlayer Instance
    {
        get
        {
            return instance;
        }
    }
    static ChasethePlayer() { }
    private ChasethePlayer() { }
    public override void Enter(Zombie Z)
    {
        if (Z.PlayerDistance() <= Z.Search && Z.PlayerDistance() >= Z.Attack)
        {
            Z.ZombieAnime.SetFloat("run", 2);
            if(Z.PlayerDistance() <= Z.Attack)
            {
                Z.ChangeState(AttackthePlayer.Instance);
            }
            //Debug.Log("FoundPlayer");
        }
        else if (Z.PlayerDistance() >= Z.Search)
        {
            Z.ChangeState(SearchforPlayer.Instance);
        }
    }
    public override void Execute(Zombie Z)
    {
        if (Z.PlayerDistance() <= Z.Search && Z.PlayerDistance() >= Z.Attack)
        {
            //Debug.Log("RunToPlayer");
            Z.ZombieLocation(new Vector3(Z.Player.transform.position.x,Z.transform.position.y,Z.Player.transform.position.z));
        }
        else if (Z.PlayerDistance() <= Z.Attack)
        {
            Z.ChangeState(AttackthePlayer.Instance);
        }
        else if(Z.PlayerDistance() >= Z.Search)
        {
            Z.ChangeState(SearchforPlayer.Instance);
        }
        if(Z.damage == true)
        {
            Z.ChangeState(DeathorHurt.Instance);
        }
    }
    public override void Exit(Zombie Z)
    {
        //Debug.Log("Exiting ChaseState");
    }
}
