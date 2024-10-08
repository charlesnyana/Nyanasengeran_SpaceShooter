using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DotProductExercise : MonoBehaviour
{
    public float redAngle;
    public float blueAngle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 redVector = calcVector(redAngle, 1);
        Vector3 blueVector = calcVector(blueAngle, 1);

        Debug.DrawLine(Vector3.zero, redVector, Color.red);
        Debug.DrawLine(Vector3.zero, blueVector, Color.blue);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float redDotBlue = Vector3.Dot(redVector, blueVector);
            Debug.Log($"Red dot Blue is = {redDotBlue}");
        }
    }

    Vector3 calcVector(float angle, float radius)
    {
        float rad = angle * Mathf.Deg2Rad;

        float xPos = Mathf.Cos(rad) * radius;
        float yPos = Mathf.Sin(rad) * radius;

        return new Vector3(xPos, yPos);
    }
}
