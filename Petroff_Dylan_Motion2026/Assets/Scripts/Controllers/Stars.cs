using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    bool startLoop = true;

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (startLoop == true)
        {
            DrawConstellation();
            startLoop = false;
        }
    }

    public void DrawConstellation()
    {
        StartCoroutine(StartDrawingCoroutine());
    }

    public IEnumerator StartDrawingCoroutine()
    {
        //go through the whole list
       for (int i = 0; i < starTransforms.Count; i++)
       {
            //when you get to 10 (because the list ends at 10 and 10+1 will give error)
            //break out of the coroutine
            if (i == starTransforms.Count - 1)
            {
                break;
            }

            //get the start position (currentPos) the endPos, the direction we are going
            Vector3 currentPos = starTransforms[i].transform.position;
            Vector3 endPos = starTransforms[i + 1].transform.position;
            Vector3 direction = endPos - currentPos;

            //calculate how fast we will have to be going to reach endPos in drawtime
            float timeToDraw = direction.magnitude / drawingTime;

            while (Vector2.Distance(currentPos, starTransforms[i + 1].transform.position) > 0.1f)
            {
                currentPos += direction.normalized * timeToDraw * Time.deltaTime;
                Debug.DrawLine(starTransforms[i].position, currentPos);
                yield return null;
            }

        yield return null;

       }

        startLoop = true;
    }
}
