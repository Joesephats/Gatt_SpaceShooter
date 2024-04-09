using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] GameObject explosionParticles;
    [SerializeField] AudioClip explosionSFX;

    GameObject player;

    Ship zombieShip;
    Rigidbody rb;
    [SerializeField] float speed = 9.5f;
    // Start is called before the first frame update
    void Start()
    {
        zombieShip = new Ship(1);

        player = GameObject.FindGameObjectWithTag("Player");

        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (player.transform.position - transform.position);

        transform.forward = dir;
        rb.AddForce((dir.normalized * speed - rb.velocity) * Time.fixedDeltaTime, ForceMode.VelocityChange);


        if (!zombieShip.Alive)
        {
            GameObject explosion = GameObject.Instantiate(explosionParticles, transform.position, Quaternion.identity);
            explosion.transform.eulerAngles = new Vector3(90, 0, 0);
            AudioSource.PlayClipAtPoint(explosionSFX, transform.position);

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
        zombieShip.TakeDamage(1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerLaser" || other.gameObject.tag == "EnemyLaser")
        {
            zombieShip.TakeDamage(1);
        }
    }
}
