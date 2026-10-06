using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Assignment1Script : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject player;
    public GameObject laserShooterLeftGO;
    public GameObject laserShooterRightGO;
    [Space]
    [Space]

    [Header("The Angles of the laser Shooters")]
    public List<float> laserShooterLeft;
    public List<float> laserShooterRight;

    [Header("Space between player and laser shooters")]
    public float distanceFromPlayer;

    [Header("List of Enemies")]
    public List<GameObject> enemys;

    [Header("Laser Shooters rotation Speed")]
    public float rotationSpeed;

    [Header("Speed of the LASERS")]
    public float laserSpeed;

    //tracking where we are in list
    int currentLaserPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLaserPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            MoveLaserShootersDown();

        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            MoveLaserShootersUp();
        }

        //calculate the position
        Vector2 moveToPositionOfLeft = new Vector2(Mathf.Cos(laserShooterLeft[currentLaserPosition] * Mathf.Deg2Rad), Mathf.Sin(laserShooterLeft[currentLaserPosition] * Mathf.Deg2Rad));
        Vector2 moveToPositionOfRight = new Vector2(Mathf.Cos(laserShooterRight[currentLaserPosition] * Mathf.Deg2Rad), Mathf.Sin(laserShooterRight[currentLaserPosition] * Mathf.Deg2Rad));
        moveToPositionOfLeft *= distanceFromPlayer;
        moveToPositionOfRight *= distanceFromPlayer;

        //Find closest Enemy
        //Will work on this later, for now
        int enemyTarget = 0;

        //get Dot product
        float leftDotProduct = GetDotProduct(laserShooterLeftGO.transform, enemyTarget);
        float rightDotProduct = GetDotProduct(laserShooterRightGO.transform, enemyTarget);

        if (leftDotProduct > 0)
        {
            //rotate Right
            laserShooterLeftGO.transform.eulerAngles -= Vector3.forward * rotationSpeed * Time.deltaTime;
        }
        else if (leftDotProduct < 0)
        {
            //rotate Left
            laserShooterLeftGO.transform.eulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

        if (rightDotProduct > 0)
        {
            //rotate Right
            laserShooterRightGO.transform.eulerAngles -= Vector3.forward * rotationSpeed * Time.deltaTime;
        }
        else if (rightDotProduct < 0)
        {
            //rotate Left
            laserShooterRightGO.transform.eulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }


        //then add the player position
        moveToPositionOfLeft += (Vector2)player.transform.position;
        moveToPositionOfRight += (Vector2)player.transform.position;

        laserShooterLeftGO.transform.position = moveToPositionOfLeft;
        laserShooterRightGO.transform.position = moveToPositionOfRight;

        //shooting the laser
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShootLaser();
        }
        
    }

    public void MoveLaserShootersUp()
    {
        //increase in the list
        currentLaserPosition++;

        if (currentLaserPosition >= laserShooterLeft.Count)
        {
            currentLaserPosition = laserShooterLeft.Count-1;
        }
    }

    public void MoveLaserShootersDown()
    {
        //decrease in the list
        currentLaserPosition--;

        if (currentLaserPosition < 0)
        {
            currentLaserPosition = 0;
        }
    }

    public float GetDotProduct(Transform laserShooterSide, int i)
    {
        Vector3 direction = enemys[i].transform.position - laserShooterSide.transform.position;

        float dotProduct = laserShooterSide.transform.right.x * direction.x + laserShooterSide.transform.right.y * direction.y;

        return dotProduct;
    }

    public void ShootLaser()
    {
        //get position of the laser shooter and the further point
        Vector2 startOfLaserLeft = laserShooterLeftGO.transform.position;
        Vector2 startOfLaserRight = laserShooterRightGO.transform.position;

        //Vector2 endOfLaserLeft = new Vector2(Mathf.Cos(laserShooterLeft[currentLaserPosition] * Mathf.Deg2Rad), Mathf.Sin(laserShooterLeft[currentLaserPosition] * Mathf.Deg2Rad));
        //endOfLaserLeft *= distanceFromPlayer + 2;
        //endOfLaserLeft += (Vector2)player.transform.position;

        //Vector2 endOfLaserRight = new Vector2(Mathf.Cos(laserShooterRight[currentLaserPosition] * Mathf.Deg2Rad), Mathf.Sin(laserShooterRight[currentLaserPosition] * Mathf.Deg2Rad));
        //endOfLaserRight *= distanceFromPlayer + 2;
        //endOfLaserRight += (Vector2)player.transform.position;

        Vector2 endOfLaserLeft = laserShooterLeftGO.transform.up + laserShooterLeftGO.transform.position;
        Vector2 endOfLaserRight = laserShooterRightGO.transform.up + laserShooterRightGO.transform.position;

        Debug.DrawLine(startOfLaserLeft, endOfLaserLeft, Color.red);
        Debug.DrawLine(startOfLaserRight, endOfLaserRight, Color.red);

        StartCoroutine(MoveTheLasers(startOfLaserLeft, endOfLaserLeft));
        StartCoroutine(MoveTheLasers(startOfLaserRight, endOfLaserRight));
    }

    public IEnumerator MoveTheLasers(Vector2 startOfLaser, Vector2 endOfLaser)
    {
        float t = 0;
        while (t < 3)
        {
            Debug.DrawLine(startOfLaser, endOfLaser, Color.red);
            t += Time.deltaTime;

            //find the direction of travel
            Vector2 direction = endOfLaser - startOfLaser;

            //make the travel speed of the laser
            Vector2 laserTravelSpeed = Vector2.zero + direction.normalized * laserSpeed;

            //apply the travel speed to the start and end of the line
            endOfLaser += laserTravelSpeed * laserSpeed * Time.deltaTime;
            startOfLaser += laserTravelSpeed * laserSpeed * Time.deltaTime;

            yield return null;
        }
        
    }
}
