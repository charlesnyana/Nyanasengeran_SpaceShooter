using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buddy : MonoBehaviour
{
    public Transform playerPos;
    public GameObject bomb;
    public Transform budLookAt;

    public float lookAtOffset;

    Vector3 bombSpawn;
    public float bombOffset;
    float angleFromPlayer;

    public float angularSpeed;
    public float distance;

    float currentAngle;

    //made method to calculate vector.
    Vector3 calcVectorFromAngle(float angle, float radius)
    {
        float rad = angle * Mathf.Deg2Rad;

        float xPos = Mathf.Cos(rad) * radius;
        float yPos = Mathf.Sin(rad) * radius;

        return new Vector3(xPos, yPos);
    }

    //made method to calculate angle from two points.
    float calcAngleFromVectors(Vector3 startPos, Vector3 endPos)
    {
        Vector3 newVector = endPos - startPos;
        float angle = Mathf.Atan2(newVector.y, newVector.x) * Mathf.Rad2Deg;
        return angle;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAngle = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bombSpawn = transform.position + (transform.up * bombOffset);
            DropBomb(bombSpawn, angleFromPlayer);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angleFromPlayer = calcAngleFromVectors(transform.position, playerPos.position);
        BuddyMovePath(distance, angularSpeed, playerPos);
    }

    public void BuddyMovePath(float radius, float speed, Transform target)
    {
        currentAngle += speed * Time.deltaTime;

        currentAngle = Mathf.Clamp(currentAngle, 0, 360);
        if (currentAngle >= 360) currentAngle = 0;

        Vector3 budPos = calcVectorFromAngle(currentAngle, radius) + playerPos.position;

        //calc forward angle to face the ship there.
        float budAngle = calcAngleFromVectors(budPos, transform.position);

        // sets the new position and rotation!!
        transform.position = budPos;
        transform.rotation = Quaternion.Euler(0, 0, budAngle);
    }

    void DropBomb(Vector3 bombSpawn, float angle)
    {
        //https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        Quaternion rotation = Quaternion.Euler(0, 0, 90 + angle);
        Instantiate(bomb, bombSpawn + (transform.up * bombOffset), rotation);
    }
}
