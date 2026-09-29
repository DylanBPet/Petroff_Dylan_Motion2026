using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AngleTest : MonoBehaviour
{
    public List<float> angles;
    public int angleListNumber = 0;

    public float circleRadius;
    public Vector2 circleOffset;

    public GameObject playerPos;
    public GameObject enemyPos;

    Color lineColor;

    [Space]
    [Space]
    [Space]

    public List<float> powerUpAngles;
    public GameObject powerUpOrbs;
    public int powerUpRadius;
    public int numberOfPowerups;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float fortyFiveDegree = 45f;

        //converstion to go from Degrees to Radians
        float ffDinRadians = fortyFiveDegree * Mathf.Deg2Rad;



        float twoPiRadians = 2 * Mathf.PI;
        //converstion to go from Radians to Degrees
        float tprInDegrees = twoPiRadians * Mathf.Rad2Deg;

        //cos sin and tan must have a Radians and NOT a degree
        //Mathf.Cos();
        //Mathf.Sin();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Get the length of the ajacent line (x) using the ANGLE LIST and Mathf.Cos() and change it to a radian (This gives you your x for a vector)
        //Get the length of the opposite line (y) using the ANGLE LIST and Mathf.Sin() and change it to a radian (This gives you your y for a vector)
        Vector2 vectorPosition = new Vector2(Mathf.Cos(angles[angleListNumber] * Mathf.Deg2Rad), Mathf.Sin(angles[angleListNumber] * Mathf.Deg2Rad));

        //make it longer!
        vectorPosition *= circleRadius;

        //in order to change where the line is draw but KEEP the angle, add our new variable circleOffset to BOTH
        Debug.DrawLine(Vector2.zero + circleOffset, vectorPosition + circleOffset);

        //Get keyboard press and reset when it reaches end of list
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            angleListNumber++;
            if (angleListNumber >= angles.Count)
            {
                angleListNumber = 0;
            }
        }
        */

        EnemyRadar(circleRadius, angles.Count);
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnPowerUps(powerUpRadius, numberOfPowerups);
        }
        
    }

    public void EnemyRadar(float radius, int circlePoints)
    {
        for (int i = 0; i < angles.Count; i++)
        {
            float d = (float)i;
            angles[i] = d * (360 / circlePoints); 
        }

        for (int i = 0; i < angles.Count; ++i)
        {
            if (i < circlePoints - 1)
            {
                Vector2 endPos = new Vector2(Mathf.Cos(angles[i] * Mathf.Deg2Rad), Mathf.Sin(angles[i] * Mathf.Deg2Rad));
                endPos *= radius;

                Vector2 startPos = new Vector2(Mathf.Cos(angles[i + 1] * Mathf.Deg2Rad), Mathf.Sin(angles[i + 1] * Mathf.Deg2Rad));
                startPos *= radius;

                Debug.DrawLine(startPos + (Vector2)playerPos.transform.position, endPos + (Vector2)playerPos.transform.position, lineColor, 0.01f);
            }
            else
            {
                Vector2 endPos = new Vector2(Mathf.Cos(angles[i] * Mathf.Deg2Rad), Mathf.Sin(angles[i] * Mathf.Deg2Rad));
                endPos *= radius;

                Vector2 startPos = new Vector2(Mathf.Cos(angles[0] * Mathf.Deg2Rad), Mathf.Sin(angles[0] * Mathf.Deg2Rad));
                startPos *= radius;

                Debug.DrawLine(startPos + (Vector2)playerPos.transform.position, endPos + (Vector2)playerPos.transform.position, lineColor, 0.01f);
            }
        }

        float dist = Vector2.Distance(playerPos.transform.position, enemyPos.transform.position);
        if (dist <= radius)
        {
            lineColor = Color.red;
        }
        else
        {
            lineColor = Color.green;
        }
    }

    public void SpawnPowerUps(float radius, int numberOfPowerups)
    {
        while (powerUpAngles.Count < numberOfPowerups)
        {
            powerUpAngles.Add(0);
        }

        for (int i = 0; i < powerUpAngles.Count; i++)
        {
            powerUpAngles[i] = i * (360 / powerUpAngles.Count);
            
        }

        for (int i = 0; i < powerUpAngles.Count; i++)
        {
            Vector2 spawnPos = new Vector2(Mathf.Cos(powerUpAngles[i] * Mathf.Deg2Rad), Mathf.Sin(powerUpAngles[i] * Mathf.Deg2Rad));
            spawnPos *= radius;
            spawnPos += (Vector2)playerPos.transform.position;

            Instantiate(powerUpOrbs, spawnPos, Quaternion.identity);
        }
    }
}
