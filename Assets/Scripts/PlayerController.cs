using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed;
    private Vector2 movementDirection;

    public bool attacking;

    public SpriteRenderer playerSprite;

    public float jumpVel;
    private bool grounded;
    public float gravity;
    public float groundPos = -2f;

    public GameManager gameManager;

    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        movementDirection = new Vector2();
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if (playerSprite == null)
        {
            playerSprite = GetComponentInChildren<SpriteRenderer>();
        }
        if (playerAnimator == null)
        {
            playerAnimator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // set animation to idle if no buttons pressed

        // MOVEMENT
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection.x = -movementSpeed;
            if (!attacking)
            {
                playerSprite.flipX = true;
            }
            playerAnimator.SetFloat("Speed", 1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x = movementSpeed;
            if (!attacking)
            {
                playerSprite.flipX = false;
            }
            playerAnimator.SetFloat("Speed", 1f);
        }
        else
        {
            movementDirection.x = 0;
            playerAnimator.SetFloat("Speed", 0f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                movementDirection.y = jumpVel;
                grounded = false;
            }
        }

        // calculate Gravity!
        if (!grounded)
        {
            movementDirection.y += gravity * Time.deltaTime;
            // when hits ground

        }

        // move
        gameObject.transform.Translate(movementDirection * Time.deltaTime);

        // ground check
        if (gameObject.transform.position.y <= groundPos)
        {
            movementDirection.y = 0;
            // snaps player to the groundPos
            Vector2 yMove = new Vector2(0, groundPos - gameObject.transform.position.y);
            gameObject.transform.Translate(yMove);
            grounded = true;
        }
    }
}
