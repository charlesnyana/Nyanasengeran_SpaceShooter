using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    Vector3 relativePos;

    public float speed;

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        relativePos = Camera.main.WorldToScreenPoint(transform.position);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerMove(Vector3.left);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerMove(Vector3.right);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayerMove(Vector3.up);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            PlayerMove(Vector3.down);
        }
    }

    void PlayerMove(Vector3 offset)
    {
        if (relativePos.x < 0) speed = 0;
        transform.position = transform.position + (offset * speed * Time.deltaTime);
    }
}
