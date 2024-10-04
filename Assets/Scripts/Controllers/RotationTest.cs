using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public float angularSpeed;
    public float targetAngle;

    public Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lineEndPoint = transform.position + transform.up;
        Debug.DrawLine(transform.position, lineEndPoint, Color.white);

        Vector3 targetPos = targetTransform.position;
        Vector3 pointToTarget = targetPos - transform.position; //blue line 
        Debug.DrawLine(transform.position, targetPos, Color.blue);

        

        float currentAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;

        targetAngle = Mathf.Atan2(pointToTarget.y, pointToTarget.x) * Mathf.Rad2Deg;

        if (currentAngle < targetAngle)
        {
            transform.Rotate(0, 0, angularSpeed * Time.deltaTime);
        }

        // Look-At Exercise


    }
}
