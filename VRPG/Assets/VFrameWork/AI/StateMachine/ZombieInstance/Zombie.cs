using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

    private StateMachine<Zombie> myMachine;
    //zombie animations
    [HideInInspector]
    public Animator ZombieAnime;
    //player
    public GameObject Player;
    //////stateDistances///////
    [SerializeField]
    private int searchdist = 15;
    [SerializeField]
    private int attackdist = 2;

////////////Zombie Health and death Variables//////////////////////
    public int health = 100;
    public bool damage;

    public GameObject deathGameObj;

    public float PlayerHP;
    public AnimationClip Attackanimation;
    public float AttackSpeed;
    public float nextAttack;
///////////////////////////////////////////////////////////////////
    [HideInInspector]
    public Transform[] WayPoints;
    public bool DamageDone(bool Bool)
    {
        damage = Bool;
        return damage;
    }
    public int Search
    {
        get
        {
            return searchdist;
        }
    }
    public int Attack
    {
        get
        {
            return attackdist;
        }
    }
    void Awake ()
    {
        AttackSpeed = Attackanimation.length;
        ZombieAnime = gameObject.GetComponent<Animator>();
        myMachine = new StateMachine<Zombie>();
        myMachine.Configure(this,SearchforPlayer.Instance);
	}
	public void ChangeState(StateInterface<Zombie> e)
    {
        myMachine.ChangeState(e);
    }
    public void LastState()
    {
        myMachine.RevertToPreviousState();
    }
    void Update()
    {
        myMachine.Update();
	}
    public int getHealth(int dmg)
    {
        health -= dmg;
        return health;
    }
    public void DeathInstan()
    {
        //GameObject Death = (GameObject)Instantiate(deathGameObj, transform.position, transform.rotation);
    }
    public float PlayerDistance()
    {
        float dist = Vector3.Distance(Player.transform.position,transform.position);
        return dist;
    }
    public void ZombieLocation(Vector3 location)
    {
        transform.LookAt(location);
    }
}
