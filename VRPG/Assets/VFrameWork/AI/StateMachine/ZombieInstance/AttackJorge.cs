using UnityEngine;
using System.Collections;

public class AttackJorge : StateInterface<Zombie> {
    static readonly AttackJorge instance = new AttackJorge();
    // Use this for initialization
    public static AttackJorge Instance
    {
        get
        {
            return instance;
        }
    }

    static AttackJorge() { }
    private AttackJorge() { }
    public override void Enter(Zombie Z)
    {
            
    }
    public override void Execute(Zombie Z)
    {       
    }
    public override void Exit(Zombie Z)
    {
    }
}
