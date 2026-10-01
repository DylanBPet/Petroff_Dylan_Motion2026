using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Assignment1Script : MonoBehaviour
{
    public GameObject player;
    public GameObject laserShooter;

    public List<float> laserShooterAnglePositions;

    int currentLaserPosition;
    public float distanceFromPlayer;

    bool needsToGetDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLaserPosition = 0;
        needsToGetDirection = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            MoveLaserShooterLeft();

            //needs to get direction to point at
            needsToGetDirection = true;

        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            MoveLaserShooterRight();

            //needs to get direction to point at
            needsToGetDirection = true;
        }

        //calculate the position
        Vector2 moveToPosition = new Vector2(Mathf.Cos(laserShooterAnglePositions[currentLaserPosition] * Mathf.Deg2Rad), Mathf.Sin(laserShooterAnglePositions[currentLaserPosition] * Mathf.Deg2Rad));
        moveToPosition *= distanceFromPlayer;
        
        //get the direction to point
        if (needsToGetDirection == true)
        {
            PointLaserShooter(moveToPosition);
            needsToGetDirection = false;
        }

        //then add the player position
        moveToPosition += (Vector2)player.transform.position;

        laserShooter.transform.position = moveToPosition;

        //shooting the laser
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShootLaser();
        }
        
    }

    public void MoveLaserShooterLeft()
    {
        //increase in the list
        currentLaserPosition++;
        if (currentLaserPosition >= laserShooterAnglePositions.Count)
        {
            currentLaserPosition = 0;
        }
    }

    public void MoveLaserShooterRight()
    {
        //decrease in the list
        currentLaserPosition--;

        if (currentLaserPosition < 0)
        {
            currentLaserPosition = laserShooterAnglePositions.Count-1;
        }
    }

    public void PointLaserShooter(Vector2 currentPos)
    {
        Vector2 furtherPoint = currentPos * 2;

        Vector2 pointAt = furtherPoint - currentPos;

        laserShooter.transform.up = pointAt;
    }

    public void ShootLaser()
    {
        //get position of the laser shooter and the further point
        Vector2 startOfLaserm = laserShooter.transform.position;

        Vector2 endOfLaser = new Vector2(Mathf.Cos(laserShooterAnglePositions[currentLaserPosition] * Mathf.Deg2Rad), Mathf.Sin(laserShooterAnglePositions[currentLaserPosition] * Mathf.Deg2Rad));
        endOfLaser *= distanceFromPlayer + 2;
        endOfLaser += (Vector2)player.transform.position;

        Debug.DrawLine(startOfLaserm, endOfLaser);

        StartCoroutine(MoveTheLasers(startOfLaserm, endOfLaser, 3));
    }

    public IEnumerator MoveTheLasers(Vector2 startOfLaser, Vector2 endOfLaser, float speed)
    {
        float t = 0;
        while (t < 3)
        {
            Debug.DrawLine(startOfLaser, endOfLaser);
            t += Time.deltaTime;

            //find the direction of travel
            Vector2 direction = endOfLaser - startOfLaser;

            //make the travel speed of the laser
            Vector2 laserTravelSpeed = Vector2.zero + direction.normalized * speed * Time.deltaTime;

            //apply the travel speed to the start and end of the line
            endOfLaser += laserTravelSpeed * speed;
            startOfLaser += laserTravelSpeed * speed;

            yield return null;
        }
        
    }
}
