using UnityEngine;
using System.Collections;

public sealed class SearchforPlayer : StateInterface<Zombie> {

    static readonly SearchforPlayer instance = new SearchforPlayer();
	// Use this for initialization
    public static SearchforPlayer Instance
    {
        get
        {
            return instance;
        }
    }
    static SearchforPlayer() { }
    private SearchforPlayer() { }
    public override void Enter(Zombie Z)
    {
        if (Z.PlayerDistance() >= Z.Search)
        {
            //Debug.Log("No Player ");
            Z.ZombieAnime.SetFloat("run", 0);
            Z.ZombieAnime.SetBool("Search", true);
        }
    }
    public override void Execute(Zombie Z)
    {
        if(Z.PlayerDistance() >= Z.Search)
        {
            //Debug.Log("SearchForPlayer");
        }
        else
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
        Z.ZombieAnime.SetBool("Search", false);
        //Debug.Log("Exiting SearchState");
    }
}
