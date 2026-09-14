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
    public Vector2 inOffset;

    void Start()
    {
        inOffset = (new Vector2(0, 1));
    }
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SpawnBombAtOffset(inOffset);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            MoveSpaceShip(inOffset);
        }
    }

    void SpawnBombAtOffset(Vector2 inOffset)
    {
        Instantiate(bombPrefab, inOffset, Quaternion.identity);
    }

    void MoveSpaceShip(Vector2 inOffset)
    {
        transform.position += (Vector3)inOffset;
    }
}
