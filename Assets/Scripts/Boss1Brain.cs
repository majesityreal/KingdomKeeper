using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Brain : MonoBehaviour
{

    public float chargeCooldown; // resets to max value
    public float chargeCooldownCounter; // keeps track of the time

    private int direction;
    public Vector2 movementDirection;
    public float movementSpeed;

    public float edgeDistance;

    public bool isCharging;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        // ensures it is either -1 or 1
        direction = ((int)Random.Range(0f, 2f) * 2 ) - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (chargeCooldownCounter <= 0f)
        {
            chargeCooldownCounter = chargeCooldown;
            movementDirection.x = movementSpeed * direction;
            isCharging = true;
        }
        else if (!isCharging)
        {
            chargeCooldownCounter -= Time.deltaTime;
        }
        HandleMovement();
    }

    private void HandleMovement()
    {
        // if it reaches the edge, stop it
        if (Mathf.Abs(gameObject.transform.position.x) > edgeDistance)
        {
            gameObject.transform.Translate(new Vector2((edgeDistance * direction) - gameObject.transform.position.x, 0));
            movementDirection.x = 0f;
            direction *= -1;
            if (direction == -1)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().flipX = false;
            }
            isCharging = false;
        }
        gameObject.transform.Translate(movementDirection * Time.deltaTime);
    }

    // next level!
    private void OnDestroy()
    {
        gameManager = GameManager.Instance;
        // handle edge case where player pauses in between the thingy
        if (gameManager.GetPaused())
        {
            gameManager.UnpauseGame();
        }
        // ensures that boss was killed
        if (GetComponent<Damageable>().GetHealth() <= 0.0f)
        {
            gameManager.NextLevel();
        }
    }
}
