using UnityEngine;
using UnityEngine.InputSystem;

public class SquareSpawner : MonoBehaviour
{
    //the prefab that we will be spawning on click
    public GameObject whiteSquarePrefab;

    //how fast scrolling the mouse increases or decreases size of square
    public float scale;

    void Start()
    {
        scale = 0.5f;
    }

    void Update()
    {
        //The Semi-Transparent Square follows the Mouse
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mousePos;

        //Change the localScale of the semi-Transparent square when the scrollwheel is used
        Vector3 scaleChange = Input.mouseScrollDelta * scale;
        transform.localScale += scaleChange;

        //when mouse is clicked spawn 1 square
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SpawnSquare();
        }


    }

    public void SpawnSquare()
    {
        //spawn the square at the position of the semi-Transparent Square
        Vector2 spawnPoint = transform.position;
        Instantiate(whiteSquarePrefab, spawnPoint, Quaternion.identity);

        //draw a line from 0,0 to the spawnPoint
        Debug.DrawLine(Vector2.zero, spawnPoint, Color.blue, 100f);
    }
}
