using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
    public GameObject muzzle;
    public GameObject target;
    public GameObject headMarker;
    public Text[] text = new Text[4];
    Vector3 center;
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
        center = new Vector3(Screen.width / 2, Screen.height / 2);
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
        text[0].text = "Time:" + Time.time;
        text[1].text = "Pt:" + score;
        text[2].text = "BulletBox:" + Bulletbox;
        text[3].text = "Bullet:" + Bullet+"/30";
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
        Ray rayOrigin = Camera.main.ScreenPointToRay(center);
        Vector3 direction = rayOrigin.direction;
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(rayOrigin,out hit))
        {
            Instantiate(effectObj, hit.point-direction, effectObj.transform.rotation);
            Vector3 HitPosition = hit.point;
            float dis = Vector3.Distance(HeadMarkerPosition, HitPosition);
            if (hit.collider.gameObject.tag == "enemy")
            {
                if (targetScript.life > 0)
                {
                    targetScript.life -= 1;
                    score = Mathf.Floor(1000 / (dis+1));
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
