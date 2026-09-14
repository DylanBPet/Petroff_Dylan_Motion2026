using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public Vector3 inOffset;

    public Vector3 bombOffset;
    private Vector2 playerPos;

    public int numberOfBombs;

    void Start()
    {
    
    }
    void Update()
    {
        playerPos = transform.position;

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombAtOffset(bombOffset);
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SpawnBombAhead(inOffset);
        }

    }

    void SpawnBombAhead(Vector3 inOffset)
    {
        Vector3 spawnPos = transform.position + inOffset;
        Instantiate(bombPrefab, spawnPos, Quaternion.identity);

        Debug.Log("player Pos: " + transform.position);
        Debug.Log("Bomb Spawned at: " + spawnPos);
        Debug.Log("offset is: " + inOffset);
    }

    void SpawnBombAtOffset(Vector2 distanceBetweenBombs)
    {

        for (int i = 1; i < numberOfBombs + 1; i++)
        {
            Vector2 spaceBetweenBombs = distanceBetweenBombs * i;

            Vector2 spawnPos = playerPos + spaceBetweenBombs;

            Instantiate(bombPrefab, spawnPos, Quaternion.identity);

            Debug.Log("player Pos: " + transform.position);
            Debug.Log("offset is: " + distanceBetweenBombs);
            Debug.Log("bomb " + i + " Spawned at " + spawnPos);
            
        }
       
    }

}
