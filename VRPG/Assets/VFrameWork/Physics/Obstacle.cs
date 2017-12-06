using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public Collider c;

    private void Start()
    {
        c = GetComponent<Collider>();
    }

}
