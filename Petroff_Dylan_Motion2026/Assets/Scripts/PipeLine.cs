using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PipeLine : MonoBehaviour
{
    public float totalMag; 

    public Vector2 mouseOldPos;
    public Vector2 mouseNewPos;

    private Coroutine drawLineCoroutine;

    private bool coroutineFinished = true;

    // Update is called once per frame
    void Update()
    {
        mouseNewPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (coroutineFinished == true && Mouse.current.leftButton.isPressed)
        {
            StartCoroutine(StartLineDrawCoroutine());
        }
        
    }

    public IEnumerator StartLineDrawCoroutine()
    {
        if (drawLineCoroutine == null)
        {
            coroutineFinished = false;
            drawLineCoroutine = StartCoroutine(DrawLineAtMouse());
        }
        else
        {
            yield return (drawLineCoroutine);
        }
    }

    public IEnumerator DrawLineAtMouse()
    {
        float t = Time.deltaTime;

        while (Mouse.current.leftButton.isPressed)
        {
            //store the old mouse pos
            mouseOldPos = mouseNewPos;

            //wait 0.1 seconds
            yield return new WaitForSeconds(0.1f);
            t = 0;

            //draw line from old position to new position
            Debug.DrawLine(mouseOldPos, mouseNewPos, Color.red, 100f);

            //add the two numbers together and find the megnitude of the line
            Vector2 rightAngleOfNewLine = (mouseNewPos - mouseOldPos);
            float mag = Mathf.Sqrt(rightAngleOfNewLine.x * rightAngleOfNewLine.x + rightAngleOfNewLine.y * rightAngleOfNewLine.y);

            //add the total to totalMag
            totalMag += mag;
        }
       
        //display the total magnitude of the pipeline
        Debug.Log(totalMag);

        //coroutine is done
        coroutineFinished = true;

    }
}
