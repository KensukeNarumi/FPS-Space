using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
    public GameObject bullet;
    public GameObject muzzle;
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
                Fire();
                PlayFireAudio();     
            }
         }
	}
    void Fire()
    {
        GameObject bullet2 = (GameObject)Instantiate(bullet, muzzle.transform.position, bullet.transform.rotation);
        Ray rayOrigin = new Ray(muzzle.transform.position, -transform.forward);
        Vector3 direction = rayOrigin.direction;
        bullet2.GetComponent<Rigidbody>().velocity = direction * bulletspeed;
        GameObject effectObj = Resources.Load<GameObject>("Effects/FireEffect(1)");
        Instantiate(effectObj, muzzle.transform.position, effectObj.transform.rotation);
    }
    void PlayFireAudio()
    {
        audioSource.PlayOneShot(fireSound);
        
    }
}
