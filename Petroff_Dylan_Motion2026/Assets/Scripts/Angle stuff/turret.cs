using UnityEngine;

public class turret : MonoBehaviour
{
    public Transform targetTransform;
    public float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.red);

        Vector3 directionToTarget = targetTransform.position - transform.position;

        //if the dot product is positive, turn right, if negative, turn left
        float dotProduct = LengthToAngle.VectorDot(transform.right, directionToTarget);
        
        //should we turn left or right
        bool shouldWeTurnRight = false;

        if (dotProduct < 0) //On the Left
        {
            shouldWeTurnRight = false;
        }
        else if (dotProduct > 0) //On the Right
        {
            shouldWeTurnRight = true;
        }
        
        if (shouldWeTurnRight)
        {
            //Rotate Right
            transform.eulerAngles -= Vector3.forward * rotationSpeed * Time.deltaTime;
        }
        else
        {
            //Rotate Left
            transform.eulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

        Debug.Log(shouldWeTurnRight);
    }
}
