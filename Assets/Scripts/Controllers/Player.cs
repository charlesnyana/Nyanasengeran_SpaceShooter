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

    public float currentSpeed = 0; //public just to read
    public float maxSpeed;

    bool moving = false;
    float accelRate;
    float deccelRate;

    float elapsedTime = 0;
    public float secondsToMaxAccel;
    public float secondsToMaxDeccel;

    //public List<int> angles;
    //int angleIndex = 0;
    //Vector3 endPoint;

    public int circlePoints;
    Color circleColor = Color.green;
    public float detectionRadius;

    public int numberOfPowerups;
    public float powerupRadius;
    public GameObject powerUp;

    private void Start()
    {
        currentSpeed = 0;
        accelRate = maxSpeed / secondsToMaxAccel; //a = final speed / seconds^2
        deccelRate = maxSpeed / secondsToMaxDeccel;


    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    float rad = angles[angleIndex] * Mathf.Deg2Rad;

        //    float xPos = Mathf.Cos(rad); //x
        //    float yPos = Mathf.Sin(rad); //y

        //    endPoint = new Vector3(xPos, yPos, 0);

        //    Debug.Log("drawing angle: " + angles[angleIndex] + " with endpoint at: " + endPoint);

        //    if (angleIndex == 10) angleIndex = 0;
        //    angleIndex++;
        //}
        //Debug.DrawLine(Vector3.zero, endPoint, Color.red);

        if (Input.GetKeyDown(KeyCode.Space)) // spawn powerups
        {
            SpawnPowerups(powerupRadius, numberOfPowerups);
        }


    }

    void FixedUpdate()
    {
        relativePos = Camera.main.WorldToViewportPoint(transform.position); // used to detect screen edges. with help from https://discussions.unity.com/t/how-to-detect-screen-edge-in-unity/459224/3

        // movement logic
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
        } else
        {
            moving = false;
            PlayerMovement(Vector3.zero);
        }

        EnemyRadar(detectionRadius, circlePoints);  
    }

    public void PlayerMovement(Vector3 offset)
    {
        accelRate = maxSpeed / secondsToMaxAccel; //recalculate in case vars changed
        deccelRate = maxSpeed / secondsToMaxDeccel;

        if (moving && currentSpeed < maxSpeed)
        {
            elapsedTime += Time.deltaTime;

            currentSpeed = accelRate * elapsedTime;
        }
        else if (currentSpeed > 0)
        {
            elapsedTime -= Time.deltaTime;

            currentSpeed -= deccelRate * Time.deltaTime;
        }
        else if (!moving)
            elapsedTime = 0;

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        transform.position += offset * currentSpeed * Time.deltaTime;
    }

    public void EnemyRadar(float radius, int circlePoints)
    {
        List<float> circleAngles = new List<float>();
        Vector3[] circleVertex = new Vector3[circlePoints];

        // calculates for each angle a number of times equal to circlePoints and stores it in circlesAngles list
        float angleInterval = 360 / circlePoints;
        for (int i = 0; i < circlePoints; i++)
        {
            float angle = i * angleInterval;
            circleAngles.Add(angle); 
        }

        for (int i = 0; i < circlePoints; i++)
        {
            float rad = circleAngles[i] * Mathf.Deg2Rad;

            float xPos = Mathf.Cos(rad) * radius; //x
            float yPos = Mathf.Sin(rad) * radius; //y

            // store the vertex position using the player as its origin
            circleVertex[i] = new Vector3(xPos, yPos, 0) + transform.position;
        }

        for (int i = 0; i < circlePoints; i++)
        {
            Vector3 startPoint = circleVertex[i];
            Vector3 endPoint = circleVertex[(i + 1) % circlePoints]; // wrap around to the first point if over the count

            Debug.DrawLine(startPoint, endPoint, circleColor); 
        }

        if ((Vector3.Distance(enemyTransform.position, transform.position)) <= radius) 
        {
            circleColor = Color.red;
        } else circleColor = Color.green;
    }

    public void SpawnPowerups(float radius, int numberOfPowerups)
    {
        List<float> powerupAngles = new List<float>(); // copied from previous function in task 1
        Vector3[] powerupVertex = new Vector3[numberOfPowerups];


        float angleInterval = 360 / numberOfPowerups;
        for (int i = 0; i < numberOfPowerups; i++)
        {
            float angle = i * angleInterval;
            powerupAngles.Add(angle);
        }

        for (int i = 0; i < numberOfPowerups; i++)
        {
            float rad = powerupAngles[i] * Mathf.Deg2Rad;

            float xPos = Mathf.Cos(rad) * radius; //x
            float yPos = Mathf.Sin(rad) * radius; //y

            powerupVertex[i] = new Vector3(xPos, yPos, 0) + transform.position;
        }

        foreach (Vector3 point in powerupVertex) 
        {
            Instantiate(powerUp, point, Quaternion.identity);
        }

    }
}
