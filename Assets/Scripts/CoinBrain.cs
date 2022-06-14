using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBrain : MonoBehaviour
{

    private bool hasBeenPickedUp = false;

    public float timeAlive;
    private float timer;

    private GameManager gameManager;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeAlive)
        {
            Destroy(gameObject);
        }
        else if (timer > (timeAlive * 2) / 3)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable enemyHealth = collision.gameObject.GetComponent<Damageable>();
        if (enemyHealth != null)
        {
            if (enemyHealth.gameObject.tag == "Player" && !hasBeenPickedUp)
            {
                // add one coin to the total!
                gameManager.AddMoney(1);
                // TODO - add animation here, wait, then destroy
                StartCoroutine(CoinPickup());
            }
        }
    }

    private IEnumerator CoinPickup()
    {
        // play animation, then wait
        animator.SetTrigger("Pickup");
        AudioManager.Instance.Play("CoinPickup");
        hasBeenPickedUp = true;
        yield return new WaitForSeconds(0.67f);
        Destroy(gameObject);
    }

}
