using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
    public GameObject muzzle;
    public GameObject target;
    public GameObject headMarker;
    Target targetScript;
    float bulletInterval;
    int Bullet;
    int Bulletbox;
    Vector3 HeadMarkerPosition;
    float score;
    bool Isalive;
    AudioClip reloadSound;
    AudioClip fireSound;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        targetScript = target.GetComponent<Target>();
        bulletInterval = 0;
        Bullet = 30;
        Bulletbox = 150;
        score = 0;
        Isalive = true;
        HeadMarkerPosition = headMarker.transform.position;
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
        GameObject effectObj = Resources.Load<GameObject>("Effects/FireEffect");
        GameObject effectObj_1 = Resources.Load<GameObject>("Effects/FireEffect(1)");
        Instantiate(effectObj_1, muzzle.transform.position, effectObj_1.transform.rotation);
        Ray rayOrigin = new Ray(muzzle.transform.position, -transform.forward);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(rayOrigin,out hit))
        {
            Instantiate(effectObj, hit.point-new Vector3(0f,0f,0.5f), effectObj.transform.rotation);
            Vector3 HitPosition = hit.point;
            float dis = Vector3.Distance(HeadMarkerPosition, HitPosition);
            if (hit.collider.gameObject.tag == "enemy")
            {
                if (targetScript.life > 0)
                {
                    targetScript.life -= 1;
                    score = 1000 / (dis+1);
                    print(score);
                }
                if (targetScript.life == 0 && Isalive==true)
                {
                    targetScript.anim.SetBool("broken", true);
                    Isalive = false;
                    StartCoroutine("Revive");
                }
            }
        }
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
    IEnumerator Revive()
    {
        yield return new WaitForSeconds(10f);
        targetScript.anim.SetBool("broken", false);
        targetScript.life = 5;
        Isalive = true;
    }
}
