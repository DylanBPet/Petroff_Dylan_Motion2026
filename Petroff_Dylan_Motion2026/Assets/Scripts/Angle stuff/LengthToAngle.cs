using UnityEngine;

public class LengthToAngle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Converting a vector to an angle based around the x axis
    public static float VectorToAngle(Vector3 inVector)
    {
        float angle = Mathf.Atan2(inVector.y, inVector.x) * Mathf.Rad2Deg;

        //return angle;
        return angle - 90f;
    }

    public static float VectorDot(Vector3 a, Vector3 b)
    {
        float dotProduct = a.x * b.x + a.y * b.y;

        return dotProduct;
    }
}
