using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public float sightDistance;
    public float visionAngle;

    public Transform targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 visionConeLeft = calcVector(visionAngle, sightDistance);
        Vector3 visionConeRight = calcVector(-visionAngle, sightDistance);

        Debug.DrawLine(transform.position, transform.position + visionConeLeft, Color.cyan);
        Debug.DrawLine(transform.position, transform.position + visionConeRight, Color.cyan);

        Vector3 vectorToTarget = targetPos.position - transform.position;
        Debug.DrawLine(transform.position, targetPos.position, Color.yellow);
        float targetDotProduct = Vector3.Dot(transform.right, vectorToTarget.normalized);
        float visionDotProduct = Vector3.Dot(transform.right, visionConeLeft.normalized);

        if (targetDotProduct >= visionDotProduct && vectorToTarget.magnitude <= sightDistance)
        {
            print("target in sight.");
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
