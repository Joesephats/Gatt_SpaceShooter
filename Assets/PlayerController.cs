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

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

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
        Debug.Log(turnInput);

        transform.localEulerAngles += new Vector3(0, turnInput, 0);
    }
}
