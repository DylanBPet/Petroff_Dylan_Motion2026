using UnityEngine;
using UnityEngine.InputSystem;

public class VectorMath : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        DrawSquare(mousPos, 5, Color.red, 0.5f);
    }

    public static float GetMag(Vector2 v)
    {
        return Mathf.Sqrt(v.x * v.x + v.y * v.y);
    }

    public static Vector2 GetNormalizedVector(Vector2 v)
    {
        float sizeOfVector = GetMag(v);

        //gives us a vector that has a size of 1 that has the same direction as before
        Vector2 normalizedVector = new Vector2(v.x / sizeOfVector, v.y / sizeOfVector);

        return normalizedVector;
    }

    public static void DrawSquare(Vector2 centerPos, float size, Color col, float duration)
    {

        //top line
        Vector2 startPos = centerPos + new Vector2(-size, size);
        Vector2 endPos = centerPos + new Vector2(size, size);

        Debug.DrawLine(startPos, endPos, col, duration);

        //left line
        startPos = centerPos + new Vector2(-size, size);
        endPos = centerPos + new Vector2(-size, -size);

        Debug.DrawLine(startPos, endPos, col, duration);

        //Bottom line
        startPos = centerPos + new Vector2(-size, -size);
        endPos = centerPos + new Vector2(size, -size);

        Debug.DrawLine(startPos, endPos, col, duration);

        //Right line
        startPos = centerPos + new Vector2(size, -size);
        endPos = centerPos + new Vector2(size, size);

        Debug.DrawLine(startPos, endPos, col, duration);
    }

}
