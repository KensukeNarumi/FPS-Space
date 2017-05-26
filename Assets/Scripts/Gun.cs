using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
    public GameObject bullet;
    float bulletInterval;
    public float bulletspeed;
    AudioClip fireSound;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        bulletInterval = 0;
        fireSound = Resources.Load<AudioClip>("Audio/fire");
        audioSource = transform.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        bulletInterval += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletInterval >= 1.0f)
            {
                GameObject bullet2 = (GameObject)Instantiate(bullet, transform.position, bullet.transform.rotation);
                Ray rayOrigin = new Ray(transform.position,-transform.forward);
                Vector3 direction = rayOrigin.direction;
                bullet2.GetComponent<Rigidbody>().velocity = direction * bulletspeed;
                audioSource.PlayOneShot(fireSound);
                GameObject effectObj = Resources.Load<GameObject>("Effects/FireImage");
                Instantiate(effectObj, effectObj.transform.position, effectObj.transform.rotation);
            }
        }
	}
}
