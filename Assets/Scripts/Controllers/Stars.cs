using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    int starIndex = 0;
    float elapsedTime = 0;
    float timeLeft;

    bool drawing;

    Transform starA;
    Transform starB;

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    private void FixedUpdate()
    {
        if (drawing)
        {
            Vector3 currentPoint = Vector3.Lerp(starA.position, starB.position, timeLeft);

            Debug.DrawLine(starA.position, currentPoint, Color.white);
        }
       
    }

    public void DrawConstellation()
    {
        if (starTransforms.Count > 2) //if there are less than two stars in a List left,
        {
            drawing = true;
            starA = starTransforms[starIndex];
            starB = starTransforms[(starIndex + 1) % starTransforms.Count];

            elapsedTime += Time.deltaTime;
            timeLeft = Mathf.Clamp(elapsedTime / drawingTime, 0, 1);

            if (timeLeft >= 1) // if drawTime is finished,
            {
                Debug.Log("star connected");
                starIndex = (starIndex +1) % starTransforms.Count; // increment star index, not going over the number of stars.
                //reset time
                elapsedTime = 0;
            }
        } else
        {
            starIndex = 0;
        }

    }
}
