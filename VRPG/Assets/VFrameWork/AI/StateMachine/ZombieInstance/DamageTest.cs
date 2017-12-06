using UnityEngine;
using System.Collections;

public class DamageTest : MonoBehaviour {
    Zombie Zscript;
    public int damage = 10;
	// Use this for initialization
	void Start ()
    {
        Zscript = FindObjectOfType<Zombie>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Zscript.DamageDone(true);
            Zscript.health -= damage;
        }
	
	}
}
