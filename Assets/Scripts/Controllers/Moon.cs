using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;

    Vector3 target;
    public float orbitalSpeed;
    public float radius;

    float currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        currentAngle = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OrbitalMotion(radius, orbitalSpeed, planetTransform);
    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        currentAngle += speed * Time.deltaTime;

        currentAngle = Mathf.Clamp(currentAngle, 0, 360);
        if (currentAngle >= 360) currentAngle = 0;

        float rad = currentAngle * Mathf.Deg2Rad; 

        float xPos = Mathf.Cos(rad) * radius;
        float yPos = Mathf.Sin(rad) * radius;
        transform.position = new Vector3(xPos, yPos, 0) + planetTransform.position;
    }

}
