using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroidScript : MonoBehaviour
{
    public float timeToMaxDifficulty = 60;
    public float minSpawnDelay = 1;
    public float initialSpawnDelay = 6;
    private float spawnTimer = 0;

    public GameObject asteroidPrefab;

    public List<GameObject> spawnLocations;

    public GameObject playerPos;

    public float maxSpeed;
    public float minSpeed;

    public Assignment1Script enemyListScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float difficultyPercent = Mathf.Clamp01(Time.time / timeToMaxDifficulty);

        float currentSpawnDelay = Mathf.Lerp(initialSpawnDelay, minSpawnDelay, difficultyPercent);

        spawnTimer += Time.deltaTime;

        if (spawnTimer > currentSpawnDelay)
        {
            SpawnAsteroid();
            SpawnAsteroid();
            spawnTimer = 0;
        }


    }

    public void SpawnAsteroid()
    {
        //pick random spawn
        int index = Random.Range(0, 10);
        Debug.Log(index);

        //spawn the asteroid
        GameObject spawnedAsteroid = Instantiate(asteroidPrefab, spawnLocations[index].transform);

        //add asteroid to the enemies script in Assignment1Script
        enemyListScript.enemies.Add(spawnedAsteroid);

        //get asteroid script
        AsteroidBehaviors asteroidScript = spawnedAsteroid.GetComponent<AsteroidBehaviors>();

        //calculate speed it will go
        float asteroidSpeed = maxSpeed / timeToMaxDifficulty;
        
        if (asteroidSpeed < minSpeed)
        {
            asteroidSpeed = minSpeed;
        }

        //trigger method aim asteroid and pass into the asteroid script the playerPosition and its velocity
        asteroidScript.OnSpawn(playerPos.transform.position, asteroidSpeed);
    }

}
