using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float currentAcceleration;
    float currentDeceleration;
    Vector3 currentVelocity;

    [Header("Must be set Before Running")]
    public float playerSpeed;
    public float timeToAccelerate;

    public float timeToDecelerate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAcceleration = playerSpeed / timeToAccelerate;

        currentDeceleration = playerSpeed / timeToDecelerate;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementAssignment();
    }
    void PlayerMovementAssignment()
    {
        Vector3 accelerationDirection = Vector3.zero;
        if (Keyboard.current.aKey.isPressed)
        {
            accelerationDirection += Vector3.left;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            accelerationDirection += Vector3.right;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            accelerationDirection += Vector3.up;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            accelerationDirection += Vector3.down;
        }
        if (!Keyboard.current.downArrowKey.isPressed && !Keyboard.current.upArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
        {
            //decelerization 
            currentVelocity -= currentVelocity.normalized * currentDeceleration * Time.deltaTime;
        }

        //what we are adding to the transform.position = direction we are going (normalized) * how fast we are going * time since last frame
        currentVelocity += accelerationDirection.normalized * currentAcceleration * Time.deltaTime;

        //if we are going faster then speed
        if (currentVelocity.magnitude > playerSpeed)
        {
            //redo the calculation for velocity
            currentVelocity = currentVelocity.normalized * playerSpeed;
        }

        if (currentVelocity.magnitude < 0.0001f)
        {
            currentVelocity *= 0;
        }

        transform.position = transform.position + currentVelocity * Time.deltaTime;


    }
}
