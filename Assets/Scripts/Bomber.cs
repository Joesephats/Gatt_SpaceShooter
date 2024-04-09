using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField] GameObject explosionParticles;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] GameObject laser;

    GameObject player;

    Ship bomberShip;
    Rigidbody rb;
    [SerializeField] float speed = 5;

    float deathTimer = 20;

    // Start is called before the first frame update
    void Start()
    {
        bomberShip = new Ship(3);

        player = GameObject.FindGameObjectWithTag("Player");

        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce((transform.forward * speed - rb.velocity) * Time.deltaTime, ForceMode.VelocityChange);

        if (!bomberShip.Alive)
        {
            GameObject explosion = GameObject.Instantiate(explosionParticles, transform.position, Quaternion.identity);
            explosion.transform.eulerAngles = new Vector3(90, 0, 0);
            AudioSource.PlayClipAtPoint(explosionSFX, transform.position);

            Destroy(gameObject);
        }

        if (deathTimer < 1)
        {
            Destroy(gameObject);
        }
        deathTimer -= Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().DamagePlayer(1);
            Debug.Log("Asteroid collide player");
        }
        bomberShip.TakeDamage(1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerLaser" || other.gameObject.tag == "EnemyLaser")
        {
            bomberShip.TakeDamage(1);
        }
    }

    IEnumerator FireLasers()
    {
        yield return new WaitForSeconds(1);
    }
}
