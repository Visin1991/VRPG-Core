using UnityEngine;
using System.Collections;

public class AttackthePlayer : StateInterface<Zombie> {
    static readonly AttackthePlayer instance = new AttackthePlayer();
    // Use this for initialization
    public static AttackthePlayer Instance
    {
        get
        {
            return instance;
        }
    }
    static AttackthePlayer() { }
    private AttackthePlayer() { }
    public override void Enter(Zombie Z)
    {
        if (Z.PlayerDistance() <= Z.Attack)
        {
            Z.ZombieAnime.SetBool("attack", true);
            //Debug.Log("check in range");
        }
        else if (Z.PlayerDistance() <= Z.Search)
        {
            Z.ChangeState(ChasethePlayer.Instance);
        }
    }
    public override void Execute(Zombie Z)
    {
        if (Z.PlayerDistance() <= Z.Attack)
        {
            Z.ZombieAnime.SetBool("attack", true);
            if (Time.time >= Z.nextAttack)
            {
                Z.nextAttack = Time.time + Z.AttackSpeed;
                //Z.PlayerHP.PlayerHealthDamage(10);
            }
            Z.ZombieLocation(new Vector3(Z.Player.transform.position.x, Z.transform.position.y, Z.Player.transform.position.z));
        }
        else if (Z.PlayerDistance() >= Z.Attack)
        {
            Z.ChangeState(ChasethePlayer.Instance);
        }
        if (Z.damage == true)
        {
            Z.ChangeState(DeathorHurt.Instance);
        }
    }
    public override void Exit(Zombie Z)
    {
        Z.ZombieAnime.SetBool("attack", false);
        //Debug.Log("Exiting ChaseState");
    }
}
