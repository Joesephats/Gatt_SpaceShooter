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
    [SerializeField] GameObject moveTarget;

    Ship playerShip;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerShip = new Ship(3, speed);
    }

    // Update is called once per frame
    void Update()
    {
        KeepInBounds();
        HandleShoot();
    }

    private void FixedUpdate()
    {
        Move();
        Steer();
    }

    void Move()
    {
        Vector3 dir = moveTarget.transform.position - transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce((dir.normalized * speed - rb.velocity) / Time.fixedDeltaTime);
        }
    }

    void Steer()
    {
        turnInput = Input.GetAxisRaw("Horizontal") * turnSpeed * Time.fixedDeltaTime;

        //produces slight warbling on x and z rotations
        rb.AddTorque(transform.up * turnInput);

        //transform.localEulerAngles += new Vector3(0, turnInput, 0);
    }

    void HandleShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<PlayerSoundManager>().PlayLaserSFX();
            Debug.Log("FIRE!");
            //object pooling bullet stuff.
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

    bool CheckDead()
    {
        if (!playerShip.Alive)
        {
            return true;
        }
        return false;
    }
}
