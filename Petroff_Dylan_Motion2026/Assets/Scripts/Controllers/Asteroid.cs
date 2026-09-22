using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    public Vector3 endPos;

    public bool randomLocationSelected = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        if (randomLocationSelected == false)
        {
            endPos = (Random.insideUnitCircle * maxFloatDistance) + (Vector2)transform.position;
            Debug.DrawLine(transform.position, endPos, Color.white, 100f);
            randomLocationSelected = true;
        }

        float dist = Vector2.Distance(transform.position, endPos);

        if (arrivalDistance < dist)
        {
            Vector3 direction = endPos - transform.position;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            randomLocationSelected = false;
        }
    }
}
