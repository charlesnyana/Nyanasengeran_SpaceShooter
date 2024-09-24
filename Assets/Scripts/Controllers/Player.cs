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

    bool moving = false;
    float accelRate;
    float deccelRate;

    float elapsedTime = 0;
    float moveTime = 0;
    float currentTime;
    public float secondsToMaxAccel;
    public float secondsToMaxDeccel;
    float secondsToMax;

    public List<int> angles;
    int angleIndex = 0;
    Vector3 endPoint;

    private void Start()
    {
        speed = baseSpeed;
    }

    void Update()
    {
        // accel deccel logic
        elapsedTime += Time.deltaTime; //global time

        if (speed > baseSpeed && !moving)
        {
            deccelCalc();
        }




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
            moving = true;
            PlayerMovement(Vector3.left);
        } else
        if (Input.GetKey(KeyCode.RightArrow) && relativePos.x < 1)
        {
            moving = true;
            PlayerMovement(Vector3.right);
        } else
        if (Input.GetKey(KeyCode.UpArrow) && relativePos.y < 1)
        {
            moving = true;
            PlayerMovement(Vector3.up);
        } else
        if (Input.GetKey(KeyCode.DownArrow) && relativePos.y > 0)
        {
            moving = true;
            PlayerMovement(Vector3.down);
        } else moving = false;
    }

    void PlayerMovement(Vector3 offset)
    {
        if (moveTime < secondsToMax) // if time while moving is less than accel threshold,
        {
            moveTime += Time.deltaTime; //increment move time
        }
        moveTime = Mathf.Clamp(moveTime, 0, secondsToMax);
        secondsToMax *= Time.deltaTime;
        
        if (moving && speed < maxSpeed) // if speed is not yet max speed,
        {
            accelRate = ((maxSpeed - speed) / Time.deltaTime);
            speed += accelRate * Time.deltaTime; //increment speed
        }

        transform.position += offset * speed * Time.deltaTime;
    }

    void deccelCalc()
    {
        // formula for calculating accel is a = (final v - initial v) / time elapsed

        float currentTime = Mathf.Lerp(secondsToMax, 0, Time.deltaTime); //timer
        Debug.Log($"currentTime reads {currentTime}");

        if (!moving && speed >= baseSpeed) // if speed is greater than base speed and deccelerating
        {
            
            deccelRate = ((baseSpeed - speed) / secondsToMax);
            speed += deccelRate * Time.deltaTime; //decrement speed
        }

        
        //Debug.Log($"acceleration is {moving}");




        speed = Mathf.Clamp(speed, baseSpeed, maxSpeed); //clamp the values
    }
}
