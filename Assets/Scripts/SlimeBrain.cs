using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBrain : MonoBehaviour
{

    public Transform playerPos;

    public Animator slimeAnimator;

    public float jumpCooldown; // the thing that resets cooldown
    private float jumpCooldownCounter; // keeps track of last jump

    public Vector2 movementDirection;

    private SpriteRenderer slimeSprite;

    public float movementSpeed;
    public float jumpSpeed;

    public float gravity;
    public float groundPos;
    public bool grounded;


    // Start is called before the first frame update
    void Start()
    {
        PlayerController temp = FindObjectOfType<PlayerController>();
        slimeSprite = GetComponentInChildren<SpriteRenderer>();
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
            return;
        }
        if (jumpCooldownCounter <= 0f && grounded)
        {
            // calculate jump vector
            Vector3 direction = (playerPos.position - gameObject.transform.position);

            // flips sprite based on direction
            if (direction.x > 0)
            {
                slimeSprite.flipX = true;
            }
            else
            {
                slimeSprite.flipX = false;
            }

            float movementRandSpeed = Random.Range(-0.05f, 0.05f);
            if (direction.x > 0)
            {
                movementDirection.x = movementSpeed + movementRandSpeed;
            }
            else if (direction.x < 0)
            {
                movementDirection.x = -movementSpeed + movementRandSpeed;
            }
            else
            {
                movementDirection.x = 0f;
            }

            float jumpRandCooldown = Random.Range(-0.5f, 0.5f);
            jumpCooldownCounter = jumpCooldown + jumpRandCooldown;
            float jumpRandSpeed = Random.Range(-0.3f, 0.3f);
            movementDirection.y = jumpSpeed + jumpRandSpeed;
            slimeAnimator.SetTrigger("Jump");
            // final condition that makes us jump! in HandleMovement() we start the jump
            grounded = false;
        }
        else if (grounded) // this condition ensures timer only counts down when on ground
        {
            movementDirection.x = 0f;
            jumpCooldownCounter -= Time.deltaTime;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        // calculate Gravity!
        if (!grounded)
        {
            movementDirection.y += gravity * Time.deltaTime;
            // move sideways too!
        }


        gameObject.transform.Translate(movementDirection * Time.deltaTime);

        // ground check
        if (gameObject.transform.position.y <= groundPos && !grounded)
        {
            movementDirection.y = 0;
            // snaps player to the groundPos
            Vector2 yMove = new Vector2(0, groundPos - gameObject.transform.position.y);
            yMove.y = yMove.y + 0.05f;
            gameObject.transform.Translate(yMove);
            grounded = true;
        }
    }

}
