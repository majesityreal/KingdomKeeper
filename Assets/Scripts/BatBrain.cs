using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBrain : MonoBehaviour
{

    public Transform playerPos;

    public float movementSpeed;
    public float sweepSpeed; // speed in y direction that bat goes down
    public float antiGravity; // gravity that sweep attack will follow

    // the distance away to initiate sweet attack
    public float sweepDistance;

    public Vector2 movementDirection;

    private SpriteRenderer batSprite;

    public bool isAttacking;

    public float airPos; // the position to return to in the air

    // Start is called before the first frame update
    void Start()
    {
        PlayerController temp = FindObjectOfType<PlayerController>();
        batSprite = GetComponentInChildren<SpriteRenderer>();
        if (temp == null)
        {
            playerPos = null;
        }
        else
        {
            playerPos = temp.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if player is dead
        if (playerPos == null)
        {
            // testing editing this repo
            return;
        }
        //
        Vector3 direction = (playerPos.position - gameObject.transform.position);

        // behavior when not in sweep state
        if (!isAttacking)
        {
            // initiate sweep
            if (Mathf.Abs(direction.x) < sweepDistance)
            {
                isAttacking = true;
                movementDirection.y = sweepSpeed;
            }
            else if (gameObject.transform.position.y > airPos)
            {
                movementDirection.y = sweepSpeed * 0.75f;
            }
            else
            {
                movementDirection.y = 0f;
            }

            if (direction.x > 0)
            {
                movementDirection.x = movementSpeed;
            }
            else if (direction.x < 0)
            {
                movementDirection.x = -movementSpeed;
            }
            else
            {
                movementDirection.x = 0f;
            }
        }
        
        // sweep state behavior
        if (isAttacking)
        {
            movementDirection.y += antiGravity * Time.deltaTime;
        }
        else
        {
            // flips sprite based on player direction, when not doing sweep attack
            if (direction.x > 0)
            {
                batSprite.flipX = true;
            }
            else
            {
                batSprite.flipX = false;
            }
        }

        gameObject.transform.Translate(movementDirection * Time.deltaTime);

        // condition to end the attack
        if (gameObject.transform.position.y > airPos)
        {
            movementDirection.y = 0;
            // snaps player to the groundPos
            isAttacking = false;
        }

    }



}
