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
        calcAngleFromPlayer();
        BuddyMovePath(distance, angularSpeed, playerPos);
    }

    public void BuddyMovePath(float radius, float speed, Transform target)
    {
        currentAngle += speed * Time.deltaTime;

        currentAngle = Mathf.Clamp(currentAngle, 0, 360);
        if (currentAngle >= 360) currentAngle = 0;

        float rad = currentAngle * Mathf.Deg2Rad;

        float xPos = Mathf.Cos(rad) * radius;
        float yPos = Mathf.Sin(rad) * radius;

        Vector3 budPos = new Vector3(xPos, yPos, 0) + playerPos.position;
        transform.position = budPos;
        budLookAt.position = budPos + (transform.up * lookAtOffset);
        //attempt to make buddy face where he shoots using LookAt.
        Vector3 budToLookAtPos = transform.position - budLookAt.position;
        angleFromPlayer = Mathf.Atan2(budToLookAtPos.y, budToLookAtPos.x) * Mathf.Rad2Deg;
    }

    void calcAngleFromPlayer()
    {
        Vector3 playerToBudPos = playerPos.position - transform.position;
        angleFromPlayer = Mathf.Atan2(playerToBudPos.y, playerToBudPos.x) * Mathf.Rad2Deg;
    }

    void DropBomb(Vector3 bombSpawn, float angle)
    {
        //https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        Quaternion rotation = Quaternion.Euler(0, 0, 90 + angle);
        Instantiate(bomb, bombSpawn + (transform.up * bombOffset), rotation);
    }
}
