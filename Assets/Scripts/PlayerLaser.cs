using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerLaser : MonoBehaviour
{

    PlayerLaserManager parentManager;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        parentManager = GameObject.FindGameObjectWithTag("PlayerLaserManager").GetComponent<PlayerLaserManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3)
        {
            parentManager.laserPool.Release(gameObject);
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<HUDManager>().UpdateScore(10);
        }

        if (other.gameObject.tag != "Player")
        {
            parentManager.laserPool.Release(gameObject);
        }
    }
}
