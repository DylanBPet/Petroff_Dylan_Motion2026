using UnityEngine;

public class DotProductTest : MonoBehaviour
{
    public float redAngle;
    public float blueAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 redVector = new Vector3(Mathf.Cos(redAngle * Mathf.Deg2Rad), Mathf.Sin(redAngle * Mathf.Deg2Rad));


        Vector3 blueVector = new Vector3(Mathf.Cos(blueAngle * Mathf.Deg2Rad), Mathf.Sin(blueAngle * Mathf.Deg2Rad));

        Debug.DrawLine(Vector3.zero, redVector, Color.red);
        Debug.DrawLine(Vector3.zero, blueVector, Color.blue);

        Debug.Log(LengthToAngle.VectorDot(redVector, blueVector));
    }
}
