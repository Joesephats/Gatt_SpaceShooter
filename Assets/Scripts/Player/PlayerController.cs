using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] int speed = 10;
    [SerializeField] int turnSpeed = 10;
    float turnInput = 0;

    [SerializeField] GameObject laserSpawnCenter;
    [SerializeField] GameObject laserSpawnRight;
    [SerializeField] GameObject laserSpawnLeft;

    [SerializeField] GameObject moveTargetCenter;
    [SerializeField] GameObject targetRight;
    [SerializeField] GameObject targetLeft;

    [SerializeField] GameObject laserPrefab;

    HUDManager hud;

    PlayerLaserManager laserManager;
    Ship playerShip;

    bool doubleLasers = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerShip = new Ship(3);

        laserManager = GameObject.FindGameObjectWithTag("PlayerLaserManager").GetComponent<PlayerLaserManager>();
        hud = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HUDManager>();
    }

    // Update is called once per frame
    void Update()
    {
        KeepInBounds();
        HandleShoot();

        if (CheckDead())
        {
            hud.DisplayGameOver(transform.position);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Move();
        Steer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DoubleLasers")
        {
            GetComponent<PlayerSoundManager>().PlayDoubleLasersSFX();

            doubleLasers = true;
            StartCoroutine(DoubleLasersTimer());
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "ExtraLife")
        {
            GetComponent<PlayerSoundManager>().PlayExtraLifeSFX();

            playerShip.TakeDamage(-1);
            hud.UpdateLives(playerShip.HP);
            Destroy(other.gameObject);
        }
    }

    void Move()
    {
        Vector3 dir = moveTargetCenter.transform.position - transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce((dir.normalized * speed - rb.velocity) * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    void Steer()
    {
        turnInput = Input.GetAxisRaw("Horizontal") * turnSpeed * Time.fixedDeltaTime;

        rb.AddTorque(transform.up * turnInput);
    }

    void HandleShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(HandleShooting());
        }
    }
    IEnumerator HandleShooting()
    {
        if (!doubleLasers)
        {
            GetComponent<PlayerSoundManager>().PlayLaserSFX();

            Vector3 dir = (moveTargetCenter.transform.position - laserSpawnCenter.transform.position);

            //GameObject newLaser = GameObject.Instantiate(laserPrefab);
            GameObject newLaser = laserManager.FireLaser();

            newLaser.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            newLaser.gameObject.transform.localRotation = Quaternion.identity;

            yield return null;

            newLaser.transform.position = laserSpawnCenter.transform.position;
            newLaser.transform.up = (dir);
            newLaser.gameObject.SetActive(true);
            newLaser.GetComponent<Rigidbody>().AddForce(dir * 1000);
        }
        else if (doubleLasers)
        {
            GetComponent<PlayerSoundManager>().PlayLaserSFX();

            Vector3 rightLaserDir = (targetRight.transform.position - laserSpawnRight.transform.position);
            Vector3 leftLaserDir = (targetLeft.transform.position - laserSpawnLeft.transform.position);

            GameObject rightLaser = laserManager.FireLaser();
            GameObject leftLaser = laserManager.FireLaser();

            rightLaser.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            rightLaser.gameObject.transform.localRotation = Quaternion.identity;
                leftLaser.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                leftLaser.gameObject.transform.localRotation = Quaternion.identity;

            yield return null;

            rightLaser.transform.position = laserSpawnRight.transform.position;
            rightLaser.transform.up = (rightLaserDir);
                leftLaser.transform.position = laserSpawnLeft.transform.position;
                leftLaser.transform.up = (leftLaserDir);

            rightLaser.gameObject.SetActive(true);
                leftLaser.gameObject.SetActive(true);

            rightLaser.GetComponent<Rigidbody>().AddForce(rightLaserDir * 1000);
                leftLaser.GetComponent<Rigidbody>().AddForce(leftLaserDir * 1000);
        }
    }

    IEnumerator DoubleLasersTimer()
    {
        //Debug.Log("double timer started");
        int counter = 15;
        for (int i = 15; i > 0; i--, counter--)
        {
            //Debug.Log($"double timer: {counter}");
            yield return new WaitForSeconds(1);
        }

        if (counter < 1)
        {
            doubleLasers = false;
        }
    }

    void KeepInBounds()
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.localEulerAngles;

        if (pos.x < -37.5f || pos.x > 37.5)
        {
            if (pos.x < -37.5)
            {
                pos.x = 36.5f;
                transform.position = new Vector3(pos.x, pos.y, pos.z);
                transform.localEulerAngles = rot;
            }
            else if (pos.x > 37.5)
            {
                pos.x = -36.5f;
                transform.position = new Vector3(pos.x, pos.y, pos.z);
                transform.localEulerAngles = rot;

            }
            Debug.Log("TOO FAR X");
        }
        if (pos.z < -21.5f || pos.z > 21.5f)
        {
            if (pos.z < -22)
            {
                pos.z = 21;
                transform.position = new Vector3(pos.x, pos.y, pos.z);
                transform.localEulerAngles = rot;

            }
            else if (pos.z > 22)
            {
                pos.z = -21;
                transform.position = new Vector3(pos.x, pos.y, pos.z);
                transform.localEulerAngles = rot;

            }

        }
    }
    public void DamagePlayer(int damage)
    {
        doubleLasers = false;
        playerShip.TakeDamage(damage);
        UpdateLivesUI();
    }

    void UpdateLivesUI()
    {
        hud.UpdateLives(playerShip.HP);
    }

    bool CheckDead()
    {
        if (!playerShip.Alive)
        {
            return true;
        }
        return false;
    }

}
