using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    Vector3 relativePos;

    public float speed;
    public float baseSpeed;
    public float maxSpeed;

    bool accelerating = false;
    public float accelRate;
    public float deccelRate;

    public List<int> angles;
    int angleIndex = 0;
    Vector3 endPoint;

    private void Start()
    {
        baseSpeed = speed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float rad = angles[angleIndex] * Mathf.Deg2Rad;

            float xPos = Mathf.Cos(rad); //x
            float yPos = Mathf.Sin(rad); //y

            endPoint = new Vector3(xPos, yPos, 0);

            Debug.Log("drawing angle: " + angles[angleIndex] + " with endpoint at: " + endPoint);

            if (angleIndex == 10) angleIndex = 0;
            angleIndex++;
        }
        Debug.DrawLine(Vector3.zero, endPoint, Color.red);

        


    }

    void FixedUpdate()
    {
        relativePos = Camera.main.WorldToViewportPoint(transform.position); // used to detect screen edges. with help from https://discussions.unity.com/t/how-to-detect-screen-edge-in-unity/459224/3

        if (Input.GetKey(KeyCode.LeftArrow) && relativePos.x > 0)
        {
            PlayerMovement(Vector3.left);
            accelerating = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && relativePos.x < 1)
        {
            PlayerMovement(Vector3.right);
            accelerating = true;
        }
        if (Input.GetKey(KeyCode.UpArrow) && relativePos.y < 1)
        {
            PlayerMovement(Vector3.up);
            accelerating = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && relativePos.y > 0)
        {
            PlayerMovement(Vector3.down);
            accelerating = true;
        }

        if (accelerating == false)
        {
            accelCalc();
        }
    }

    void PlayerMovement(Vector3 offset)
    {
        accelerating = true;
        accelCalc();

        if (speed >= maxSpeed)
        {
            Debug.Log("slow down");
            accelerating = false;
        }

        transform.position += offset * speed * Time.deltaTime;
    }

    void accelCalc()
    {
        if (accelerating == false && speed >= baseSpeed)
        {
            speed -= deccelRate * Time.deltaTime;
        } else if (speed < maxSpeed)
        {
            accelerating = true;
            speed += accelRate * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, baseSpeed, maxSpeed);
    }
}
