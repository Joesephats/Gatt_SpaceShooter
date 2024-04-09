using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] GameObject explosionParticles;
    [SerializeField] AudioClip explosionSFX;

    Ship asteroidShip;

    private void Start()
    {
        asteroidShip = new Ship(1);
    }

    private void Update()
    {
        if (!asteroidShip.Alive)
        {
            GameObject explosion = GameObject.Instantiate(explosionParticles, transform.position, Quaternion.identity);
            explosion.transform.eulerAngles = new Vector3(90, 0,0);
            AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
            ChanceDropPickup();

            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().DamagePlayer(1);
            Debug.Log("Asteroid collide player");
        }
        asteroidShip.TakeDamage(1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerLaser" || other.gameObject.tag == "EnemyLaser")
        {
            asteroidShip.TakeDamage(1);
        }
    }


    [SerializeField] GameObject extraLifePickup;
    [SerializeField] GameObject doubleLaserPickup;
    void ChanceDropPickup()
    {
        int dice1 = Random.Range(1, 7);
        int dice2 = Random.Range(1, 7);
        int result = dice1 + dice2;

        Debug.Log(result);
        if ((result) > 10)
        {
            if (result == 11 || result == 10)
            {
                Debug.Log("Spawn life");
                GameObject newPickup = GameObject.Instantiate(extraLifePickup, transform.position, Quaternion.identity);
            }
            else if (result == 12)
            {
                GameObject newPickup = GameObject.Instantiate(doubleLaserPickup, transform.position, Quaternion.identity);
            }
        }
        //yield return null;
    }
}
