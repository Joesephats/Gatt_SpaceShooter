using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject asteroid;
    [SerializeField] GameObject zombie;
    [SerializeField] GameObject bomber;

    GameObject playerObject;

    float asteroidSpawnAmplifier = 0;
    float zombieSpawnAmplifier = 0;
    float bomberSpawnAmplifier = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnAsteroids());
        StartCoroutine(SpawnZombies());
        StartCoroutine(SpawnBombers());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnAsteroids()
    {
        for (int i = 0; i < 8; i++)
        {
            float randX = Random.Range(-60, 61);
            float randZ = Random.Range(-40, 41);
            while (randX < 37 && randX > -37)
            {
                randX = Random.Range(-60, 61);
            }
            while (randZ < 20 && randZ > -20)
            {
                randZ = Random.Range(-40, 41);
            }

            Vector3 outOfFramePosition = new Vector3(randX, -10, randZ);
            GameObject newAsteroid = Instantiate(asteroid, outOfFramePosition, Quaternion.identity);

            newAsteroid.GetComponent<Rigidbody>().AddForce((playerObject.transform.position - newAsteroid.transform.position).normalized * 400, ForceMode.Impulse);

            yield return new WaitForSeconds(3 - asteroidSpawnAmplifier);
            if (asteroidSpawnAmplifier < 2.5f)
            {
                asteroidSpawnAmplifier += .1f;
            }
        }
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnZombies()
    {
        zombieSpawnAmplifier = 0;
        yield return new WaitForSeconds(5);

        for (int i = 0; i < 8; i++)
        {
            float randX = Random.Range(-60, 61);
            float randZ = Random.Range(-40, 41);
            while (randX < 37 && randX > -37)
            {
                randX = Random.Range(-60, 61);
            }
            while (randZ < 20 && randZ > -20)
            {
                randZ = Random.Range(-40, 41);
            }

            Vector3 outOfFramePosition = new Vector3(randX, -10, randZ);
            GameObject newZombie = Instantiate(zombie, outOfFramePosition, Quaternion.identity);

            yield return new WaitForSeconds(4 - zombieSpawnAmplifier);
            if (zombieSpawnAmplifier < 3.5)
            {
                zombieSpawnAmplifier += .1f;
            }
        }

        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnBombers()
    {
        Vector3 spawnPosition;
        bomberSpawnAmplifier = 0;
        yield return new WaitForSeconds(10);

        for (int i = 0; i < 8; i++)
        {
            int positionPicker = Random.Range(1, 5);
            GameObject newBomber = Instantiate(bomber, new Vector3(100, -10, 0), Quaternion.identity);

            switch (positionPicker)
            {
                case 1:
                    spawnPosition = new Vector3(-40, -10, 16.9f);
                    newBomber.transform.position = spawnPosition;
                    newBomber.transform.forward = transform.right;
                    break;

                case 2:
                    spawnPosition = new Vector3(40, -10, 16.9f);
                    newBomber.transform.position = spawnPosition;
                    newBomber.transform.forward = transform.right * -1;
                    break;

                case 3:
                    spawnPosition = new Vector3(-40, -10, -16.9f);
                    newBomber.transform.position = spawnPosition;
                    newBomber.transform.forward = transform.right;
                    break;

                case 4:
                    spawnPosition = new Vector3(40, -10, -16.9f);
                    newBomber.transform.position = spawnPosition;
                    newBomber.transform.forward = transform.right * -1;
                    break;
            }

            yield return new WaitForSeconds(5 - bomberSpawnAmplifier);
            if (bomberSpawnAmplifier < 2.5)
            {
                bomberSpawnAmplifier += .1f;
            }
        }
        StartCoroutine(SpawnBombers());
    }
}
