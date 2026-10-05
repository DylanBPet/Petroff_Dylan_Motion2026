using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looker : MonoBehaviour
{
    public List<Transform> target;
    public int listNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            listNumber++;
            if (listNumber >= target.Count)
            {
                listNumber = 0;
            }
        }
        Vector3 direction = target[listNumber].position - transform.position;
        float newRotation = LengthToAngle.VectorToAngle(direction);
        
        transform.eulerAngles = new Vector3(0, 0, newRotation);
    }
}
