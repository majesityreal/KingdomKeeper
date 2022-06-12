using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerBrain : MonoBehaviour
{

    public Transform playerPos;

    public float movementSpeed;
    public float sweepSpeed; // speed of the charge
    public float sweepDuration;
    private float timer;

    // the distance away to initiate sweet attack
    public float sweepDistance;

    public Vector2 movementDirection;

    private SpriteRenderer chargerSprite;

    public bool isAttacking;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController temp = FindObjectOfType<PlayerController>();
        chargerSprite = GetComponentInChildren<SpriteRenderer>();
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
        //
        Vector3 direction = (playerPos.position - gameObject.transform.position);

        // behavior when not in sweep state
        if (!isAttacking)
        {
            // initiate sweep, with two seconds delay between sweep attacks
            if (Mathf.Abs(direction.x) < sweepDistance && (timer += Time.deltaTime) > 2.0f)
            {
                timer = 0f;
                StartCoroutine(Sweep(direction));
            }
            // behavior for when moving regularly, not initiating sweep
            else
            {
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
                    movementDirection.x = 0.0f;
                }
                animator.SetInteger("MovementSpeed", 1);
            }

        }

        // sweep state behavior
        if (isAttacking)
        {
            // condition for ending the sweep attack, after given duration
            if ((timer += Time.deltaTime) > sweepDuration)
            {
                timer = 0;
                StartCoroutine(StopCharge());
            }
        }
        // flips sprite based on player direction, when not doing sweep attack
        else
        {
            if (direction.x > 0)
            {
                chargerSprite.flipX = false;
            }
            else
            {
                chargerSprite.flipX = true;
            }
        }

        gameObject.transform.Translate(movementDirection * Time.deltaTime);

    }

    private IEnumerator Sweep(Vector3 direction)
    {
        isAttacking = true;
        movementDirection.x = 0f;
        animator.SetInteger("MovementSpeed", 0);
        // TODO play prepare animation
        yield return new WaitForSeconds(0.75f);
        if (direction.x > 0)
        {
            movementDirection.x = sweepSpeed;
        }
        else if (direction.x < 0)
        {
            movementDirection.x = sweepSpeed * -1;
        }
        animator.SetInteger("MovementSpeed", 2);
    }

    private IEnumerator StopCharge()
    {
        movementDirection.x = 0.0f;
        animator.SetInteger("MovementSpeed", 0);
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

}
