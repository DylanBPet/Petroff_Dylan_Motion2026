using UnityEngine;
using UnityEngine.UI;

public class RowGeneration : MonoBehaviour
{
    public InputField numberOfSquares;

    public GameObject squarePrefab;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateSquareLine()
    {
        
        int totalSquares = int.Parse(numberOfSquares.text);

        Vector2 spawnPos = new Vector2(-7, 2);

        //check to see if its a valid number
        int number;
        int.TryParse(numberOfSquares.text, out number);
        if (number > 0 && number < 10)
        {
            //draw all the squares
            for (int i = 0; i < totalSquares; i++)
            {
                //draw line to each square
                Debug.DrawLine(Vector2.zero, spawnPos, Color.blue, 100f);

                //spawn square
                Instantiate(squarePrefab, spawnPos, Quaternion.identity);

                //change spawn pos
                spawnPos.x += 1.5f;
            }
        }
        else
        {
            Debug.Log(numberOfSquares.text + " Is Not a valid number");
        }
    }
}
