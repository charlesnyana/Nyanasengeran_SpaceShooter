using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float driftSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BombDrift(driftSpeed);
    }

    void BombDrift(float speed)
    {
        transform.position += transform.up * speed;
    }
}
