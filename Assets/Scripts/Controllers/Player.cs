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
        relativePos = Camera.main.WorldToViewportPoint(transform.position); // used to detect screen edges. with help from https://discussions.unity.com/t/how-to-detect-screen-edge-in-unity/459224/3

        if (Input.GetKey(KeyCode.LeftArrow) && relativePos.x > 0)
        {
            PlayerMove(Vector3.left);
        }
        if (Input.GetKey(KeyCode.RightArrow) && relativePos.x < 1)
        {
            PlayerMove(Vector3.right);
        }
        if (Input.GetKey(KeyCode.UpArrow) && relativePos.y < 1)
        {
            PlayerMove(Vector3.up);
        }
        if (Input.GetKey(KeyCode.DownArrow) && relativePos.y > 0)
        {
            PlayerMove(Vector3.down);
        }
    }

    void PlayerMove(Vector3 offset)
    {
        transform.position += offset * speed * Time.deltaTime;
    }
}
