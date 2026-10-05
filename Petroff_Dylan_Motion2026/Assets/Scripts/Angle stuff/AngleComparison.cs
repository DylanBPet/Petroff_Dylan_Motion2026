using UnityEngine;

public class AngleComparison : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 facingDirectoin = transform.up;
        float facingAngle = LengthToAngle.VectorToAngle(facingDirectoin);

        Debug.Log(facingAngle);
        Debug.Log(transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
