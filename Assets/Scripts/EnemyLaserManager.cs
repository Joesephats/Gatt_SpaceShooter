using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyLaserManager : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;

    public ObjectPool<GameObject> laserPool;

    private static EnemyLaserManager instance;
    public static EnemyLaserManager Instance => instance;


    private void Awake()
    {
        instance = this;
    }

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
        laser.SetActive(true);
    }

    public void ReturnLaser(GameObject laser)
    {
        laser.SetActive(false);
    }

    void DestroyLaser(GameObject laser)
    {
        Destroy(laser);
    }

    public GameObject FireLaser()
    {
        GameObject newLaser = laserPool.Get();
        return newLaser;
    }
}
