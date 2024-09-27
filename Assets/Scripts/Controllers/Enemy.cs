using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float enemySpeed;

    public float fixedYMax;
    public float fixedYMin;
    int yIndex;
    bool reverse = false;

    Vector3 playerPos;

    private void Start()
    {
        yIndex = 0;
    }

    private void Update()
    {
        // references and stores the player's location in playerPos variable
        playerPos = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);
    }

    private void FixedUpdate()
    {
        enemyMovement();
    }

    public void enemyMovement()
    {
        if (!reverse)
        {
            yIndex++;
            if (yIndex >= fixedYMax - fixedYMin) reverse = true;
        }

        if (reverse)
        {
            yIndex--;
            if (yIndex <= 0) reverse = false;
        }
        Debug.Log($"yIndex: {yIndex}");



        float fixedY = Mathf.Lerp(fixedYMin, fixedYMax, yIndex / (fixedYMax - fixedYMin)*Time.deltaTime); // Lerps between min and max, using the fraction of the index/length between min and max so it returns between 0-1
        


        Vector3 lockedEnemyPos = new Vector3(transform.position.x, fixedY, 0);
        
        transform.position = Vector3.Lerp(lockedEnemyPos, playerPos, enemySpeed * Time.deltaTime);


    }
}
