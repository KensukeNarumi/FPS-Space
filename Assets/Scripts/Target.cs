using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public int life;
    public Animator anim;
	// Use this for initialization
	void Start () {
        life = 5;
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
