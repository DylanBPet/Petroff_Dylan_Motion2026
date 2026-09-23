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

    public float cornerDistance;

    public float ratio;

    public float asteroidDetectionRange;

    public Vector3 movement;
    public float speed;

    public Vector3 currentVelocity;
    public float accelerationTime;
    public float currentAcceleration;

    public float decelerationTime;
    public float currentDeceleration;

    Vector3 enemyVelocity;
    Vector3 enemyDrag;
    public float maxDistanceFalloff;

    void Start()
    {
        currentAcceleration = speed / accelerationTime;

        currentDeceleration = speed / decelerationTime;
    }

    void Update()
    {
        playerPos = transform.position;

        PlayerMovement();

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombAtOffset(bombOffset);
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SpawnBombAhead(inOffset);
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            SpawnBombOnRandomCorner(cornerDistance);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            WarpPlayer(enemyTransform, ratio);
        }

        DetectAsteroids(asteroidDetectionRange, asteroidTransforms);
        EnemyMovement();
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

    public void SpawnBombOnRandomCorner(float inDistance)
    {
        Vector2 spawnPos = playerPos;
        //calculate the 4 corners and normailze

        //top
        Vector2 topL = playerPos + new Vector2 (-1, 1);
        Vector2 topR = playerPos + new Vector2(1, 1);
        //bottom 
        Vector2 botL = playerPos + new Vector2(-1, -1);
        Vector2 botR = playerPos + new Vector2(1, -1);


        ////testing
        Debug.DrawLine(playerPos, topL, Color.blue, 100f);
        Debug.DrawLine(playerPos, topR, Color.red, 100f);
        Debug.DrawLine(playerPos, botL, Color.purple, 100f);
        Debug.DrawLine(playerPos, botR, Color.orange, 100f);

        //choose one at random
        int randomNumber = Random.Range(1, 5);
        if (randomNumber == 1)
        {
            //Top Left

            //get the direction from the player to Top Left
            Vector2 playerToTopL = topL - playerPos;

            //calculate the spawn position which will be the normalized vector multiplied by the distance
            spawnPos = playerPos + playerToTopL.normalized * inDistance;
        }
        else if (randomNumber == 2)
        {
            //Top Right


            //get the direction from the player to Top Right
            Vector2 playerToTopR = topR - playerPos;

            //calculate the spawn position which will be the normalized vector multiplied by the distance (add back in player position)
            spawnPos = playerPos + playerToTopR.normalized * inDistance;
        }
        else if (randomNumber == 3)
        {
            //Bottom Left


            //get the direction from the player to Bottom Left
            Vector2 playerToBotL = botL - playerPos;

            //calculate the spawn position which will be the normalized vector multiplied by the distance
            spawnPos = playerPos + playerToBotL.normalized * inDistance;
        }
        else if (randomNumber == 4)
        {
            //Bottom Right

            //get the direction from the player to Bottom Right
            Vector2 playerToBotR = botR - playerPos;

            //calculate the spawn position which will be the normalized vector multiplied by the distance
            spawnPos = playerPos + playerToBotR.normalized * inDistance;
        }
        Instantiate(bombPrefab, spawnPos, Quaternion.identity);
    }

    public void WarpPlayer(Transform target, float ratio)
    {
        Vector3 direction = (Vector2)target.transform.position - playerPos;

        float distance = direction.magnitude;

        //////////////////////////
        //Where the player should end up
        //75%
        Debug.DrawLine(transform.position + direction.normalized * 0.75f * distance + new Vector3(0, 1, 0), transform.position + direction.normalized * 0.75f * distance + new Vector3(0, -1, 0), Color.yellow, 100f);

        //50%
        Debug.DrawLine(transform.position + direction.normalized * 0.5f * distance + new Vector3(0, 1, 0), transform.position + direction.normalized * 0.5f * distance + new Vector3(0, -1, 0), Color.green, 100f);

        //25%
        Debug.DrawLine(transform.position + direction.normalized * 0.25f * distance + new Vector3(0, 1, 0), transform.position + direction.normalized * 0.25f * distance + new Vector3(0, -1, 0), Color.orange, 100f);
        /////////////////////////////////

        direction = direction.normalized * ratio;


        Vector3 moveDistance = direction * distance;
        transform.position += moveDistance;

        //find the target
        Debug.DrawLine(transform.position, target.position, Color.red, 100f);
    }

    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {
        for (int i = 0; i < inAsteroids.Count; i++)
        {
            //check how far ALL asteroids are from the player
            float distanceFromPlayer = Vector2.Distance(playerPos, inAsteroids[i].position);

            //if the asteroid is within the max range, do calculation
            if (distanceFromPlayer < inMaxRange)
            {
                Vector2 direction = (Vector2)inAsteroids[i].position - playerPos;
                direction = direction.normalized * 2.5f;

                //display the line
                Debug.DrawLine(playerPos, direction + playerPos, Color.green, 0.05f);
            }
        }
    }

    void PlayerMovement()
    {
        Vector3 accelerationDirection = Vector3.zero;
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            accelerationDirection += Vector3.left;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            accelerationDirection += Vector3.right;
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            accelerationDirection += Vector3.up;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            accelerationDirection += Vector3.down;
        }
        if(!Keyboard.current.downArrowKey.isPressed && !Keyboard.current.upArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
        {
            //decelerization 
            currentVelocity -= currentVelocity.normalized * currentDeceleration * Time.deltaTime;
        }

        //what we are adding to the transform.position = direction we are going (normalized) * how fast we are going * time since last frame
        currentVelocity += accelerationDirection.normalized * currentAcceleration * Time.deltaTime;

        //if we are going faster then speed
        if (currentVelocity.magnitude > speed)
        {
            //redo the calculation for velocity
            currentVelocity = currentVelocity.normalized * speed;
        }

        if (currentVelocity.magnitude < 0.0001f)
        {
            currentVelocity *= 0;
        }

        transform.position = transform.position + currentVelocity * Time.deltaTime;

        
    }

    public void EnemyMovement()
    {
        
        //calculate how fast it will go. It will speed up half as fast as the player
        float enemySpeed = speed / (accelerationTime / 2);

        //get the direction it is going
        Vector3 direction = (Vector3)playerPos - enemyTransform.position;
        
        //point at player
        enemyTransform.transform.up = direction;
        
        

        //calculating how far the enemy is from the player and turning it into a decemal percentage
        float distancePercentage = Vector3.Distance(playerPos, enemyTransform.position) / maxDistanceFalloff;

        //enemy acceleration
        enemyVelocity += direction.normalized * enemySpeed * distancePercentage * Time.deltaTime;

        //enemy drag
        enemyDrag -= direction.normalized * enemySpeed * distancePercentage * Time.deltaTime;
        
        if (enemyVelocity.magnitude > speed)
        {
            //redo the calculation for velocity
            enemyVelocity = enemyVelocity.normalized * speed;
        }
        

        //add it to player
        enemyTransform.position += (enemyVelocity - enemyDrag) * Time.deltaTime;

        Debug.DrawLine(enemyTransform.position, direction);
        
    }
}
