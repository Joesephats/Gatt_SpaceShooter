using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerLaserManager : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;

    public ObjectPool<GameObject> laserPool;


    // Start is called before the first frame update
    void Start()
    {
        laserPool = new ObjectPool<GameObject>(CreateNewLaser, GetLaser, ReturnLaser, DestroyLaser);
    }

    GameObject CreateNewLaser()
    {
        GameObject newLaser = GameObject.Instantiate(laserPrefab);
        newLaser.SetActive(false);
        return newLaser;
    }

    void GetLaser(GameObject laser)
    {
        //laser.gameObject.SetActive(true);
    }

    public void ReturnLaser(GameObject laser)
    {
        laser.gameObject.SetActive(false);
    }

    void DestroyLaser(GameObject laser)
    {
        Destroy(laser);
    }

    public GameObject FireLaser()
    {
        GameObject newLaser = laserPool.Get();
        newLaser.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        newLaser.gameObject.transform.localRotation = Quaternion.identity;
        return newLaser;
    }
}
