using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {

	}
    private void OnCollisionEnter(Collision collision)
    {
        GameObject effectObj = Resources.Load<GameObject>("Effects/FireImage2");
        Instantiate(effectObj, this.gameObject.transform.position, effectObj.transform.rotation);
        Destroy(this.gameObject);
    }
}
