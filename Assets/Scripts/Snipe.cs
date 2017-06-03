using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snipe : MonoBehaviour {
    bool SniperMode;
    public GameObject snipe;
    public GameObject Camera;
	// Use this for initialization
	void Start () {
        SniperMode = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            if (SniperMode == false)
            {
                SniperMode = true;
                snipe.SetActive(true);
                Camera.GetComponent<Camera>().fieldOfView = 20;
            }
            else
            {
                SniperMode = false;
                snipe.SetActive(false);
                Camera.GetComponent<Camera>().fieldOfView = 60;
            }
        }
	}
}
