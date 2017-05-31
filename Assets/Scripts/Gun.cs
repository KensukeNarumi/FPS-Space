using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
    public GameObject bullet;
    public GameObject muzzle;
    float bulletInterval;
    public float bulletspeed;
    int Bullet;
    int Bulletbox;
    AudioClip reloadSound;
    AudioClip fireSound;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        bulletInterval = 0;
        Bullet = 30;
        Bulletbox = 150;
        fireSound = Resources.Load<AudioClip>("Audio/fire");
        reloadSound = Resources.Load<AudioClip>("Audio/reload");
        audioSource = transform.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        bulletInterval += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Bullet>0)
        {
            if (bulletInterval >= 1.0f)
            {
                Fire();
                PlayFireAudio();     
            }
        }
        if (Input.GetKey(KeyCode.R) && Bullet<30)
        {
            Reload();
            PlayReloadAudio();
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
        Bullet -= 1;
    }
    void PlayFireAudio()
    {
        audioSource.PlayOneShot(fireSound);
    }
    void Reload()
    {
        int usedBullet = 30-Bullet;
        Bullet = 30;
        Bulletbox = Bulletbox - usedBullet;
    }
    void PlayReloadAudio()
    {
        audioSource.PlayOneShot(reloadSound);
    }
}
