using UnityEngine;

public class AsteroidBehaviors : MonoBehaviour
{
    public Vector3 currentVelocity;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + currentVelocity * Time.deltaTime;
    }

    public void OnSpawn(Vector3 playerPos, float currentAcceleration)
    {
        Vector3 direction = playerPos - transform.position;
        currentVelocity = direction.normalized * currentAcceleration;
    }

}
