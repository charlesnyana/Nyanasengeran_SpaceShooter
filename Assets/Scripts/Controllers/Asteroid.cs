using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance; //radius of random point

    Vector3 targetPos;

    Vector3 grabRandomPoint(float radius)
    {
        float randomRadius = Random.Range(0, radius);

        float randomAngle = Random.Range(0, Mathf.PI * 2); // full coverage of a circle in radians

        float x = Mathf.Cos(randomAngle) * randomRadius; // kinda cheating since I already attended Week 3 and learned this...
        float y = Mathf.Sin(randomAngle) * randomRadius; // I hope that's okay ;_;
        return new Vector3(transform.position.x + x, transform.position.y + y, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = grabRandomPoint(maxFloatDistance);
        //Debug.Log(targetPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) <= arrivalDistance) // if distance of asteroid to target is less than arrival length,
        {
            targetPos = grabRandomPoint(maxFloatDistance);
            Debug.Log("Asteroid shifting course!");
        }
    }

    private void FixedUpdate()
    {
        if (targetPos != null)
        {
            asteroidMovement();
        }
    }

    void asteroidMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

}
