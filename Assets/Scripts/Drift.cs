using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    [SerializeField] int driftForce = 3;
    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 randomDir = new Vector3(Random.Range(-10, 11), 0, Random.Range(-10, 11));

        rb.AddForce((randomDir.normalized) * driftForce, ForceMode.Impulse);
    }


}
