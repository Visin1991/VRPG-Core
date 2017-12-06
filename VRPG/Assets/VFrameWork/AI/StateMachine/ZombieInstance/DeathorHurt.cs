using UnityEngine;
using System.Collections;

public class DeathorHurt : StateInterface<Zombie> {
    static readonly DeathorHurt instance = new DeathorHurt();
    // Use this for initialization
    public static DeathorHurt Instance
    {
        get
        {
            return instance;
        }
    }
    static DeathorHurt() { }
    private DeathorHurt() { }
    public override void Enter(Zombie Z)
    {
        if (Z.health <= 0)
        {
            //death animation;
            Z.DeathInstan();
            Z.ZombieAnime.enabled = false;
            //Debug.Log("dead");
            Z.gameObject.SetActive(false);
        }
    }
    public override void Execute(Zombie Z)
    {
        if(Z.damage == true && Z.health >= 0)
        {
            Debug.Log("im hurt");
            //do hurt animation
            Z.damage = false;
        }
        else
        {
            Z.LastState();
        }
    }
    public override void Exit(Zombie Z)
    {
        Debug.Log("going backastate");
        Z.damage = false;
        //Debug.Log("Exiting ChaseState");
    }
}
